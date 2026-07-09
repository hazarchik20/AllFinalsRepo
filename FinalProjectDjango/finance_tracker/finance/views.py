from django.shortcuts import render, redirect,get_object_or_404
from .forms import ExpenseForm,VaultForm,MembershipForm
from django.contrib.auth.decorators import login_required
from .models import Vault, Membership,Expense,GroupLimit,ExpenseSplit
from django.db.models import Sum, Count

@login_required
def add_transaction(request):

    if request.method == 'POST':
        form = ExpenseForm(request.POST, user=request.user)

        if form.is_valid():
            transaction = form.save(commit=False)
            transaction.owner = request.user
            transaction.vault = None

            if transaction.transaction_type == 'expense':
                income = Expense.objects.filter(
                    owner=request.user,
                    vault__isnull=True,
                    transaction_type='income'
                ).aggregate(
                    Sum('amount')
                )['amount__sum'] or 0
                expense = Expense.objects.filter(
                    owner=request.user,
                    vault__isnull=True,
                    transaction_type='expense'
                ).aggregate(
                    Sum('amount')
                )['amount__sum'] or 0
                balance = income - expense
                if transaction.amount > balance:
                    form.add_error(
                        'amount',
                        'Not enough money.'
                    )
                else:
                    transaction.save()

            else:
                transaction.save()

            if transaction.pk:
                return redirect('home')

    else:
        form = ExpenseForm(user=request.user)

    return render(request, 'finance/add_transaction.html', {
        'form': form
    })

@login_required
def vaults(request):

    vaults = Vault.objects.filter(
        membership__user=request.user
    ).distinct()
    

    return render(
        request,
        'finance/vaults.html',
        {
            'vaults': vaults
        }
    )
@login_required
def add_vault(request):

    if request.method == 'POST':
        form = VaultForm(request.POST)

        if form.is_valid():
            vault = form.save(commit=False)
            vault.owner = request.user
            vault.save()

            Membership.objects.create(
                user=request.user,
                vault=vault,
                role='manager'
            )

            return redirect('vaults')

    else:
        form = VaultForm()

    return render(request, 'finance/add_vault.html', {
        'form': form
    })
@login_required
def vault_detail(request, vault_id):

    vault = get_object_or_404(Vault, id=vault_id)

    if not Membership.objects.filter(
        user=request.user,
        vault=vault
    ).exists():
        return redirect('vaults')
    
    members = Membership.objects.filter(vault = vault)

    expenses = Expense.objects.filter(vault = vault).order_by('-created_at')
    
    total = expenses.filter(transaction_type='expense').aggregate(Sum('amount'))['amount__sum'] or 0
    income = expenses.filter(
    transaction_type='income' ).aggregate( Sum('amount'))['amount__sum'] or 0
    balance = income - total
    limit = GroupLimit.objects.filter(vault = vault).first()

    manager = is_manager(request.user, vault)

    return render(
        request,
        'finance/vault_detail.html',
        {
            'vault': vault,
            'members': members,
            'expenses': expenses,
            'manager': manager,
            'total': total,
            'limit': limit,
            'income': income,
            'balance': balance,
        }
    )

@login_required
def add_member(request, vault_id):

    vault = get_object_or_404(Vault, id=vault_id)

    if not Membership.objects.filter(
        user=request.user,
        vault=vault
    ).exists():
        return redirect('vaults')
    
    if not is_manager(request.user, vault):
        return redirect('vault_detail', vault.id)
    
    if request.method == 'POST':
        form = MembershipForm(request.POST)

        if form.is_valid():
            member = form.save(commit=False)
            member.vault = vault
            member.save()

            return redirect('vault_detail', vault_id=vault.id)

    else:
        form = MembershipForm()

    return render(request, 'finance/add_member.html', {
        'form': form,
        'vault': vault,
    })
def is_manager(user, vault):
    return Membership.objects.filter(
        user=user,
        vault=vault,
        role='manager'
    ).exists()


@login_required
def add_vault_transaction(request, vault_id):

    vault = get_object_or_404(Vault, id=vault_id)

    if not Membership.objects.filter(
        user=request.user,
        vault=vault
    ).exists():
        return redirect('home')

    if request.method == 'POST':
        form = ExpenseForm(request.POST, user=request.user)
        if form.is_valid():
            transaction = form.save(commit=False)
            transaction.owner = request.user
            transaction.vault = vault
            
            if transaction.transaction_type == 'expense':

                income = Expense.objects.filter(
                    vault=vault,
                    transaction_type='income'
                ).aggregate(
                    Sum('amount')
                )['amount__sum'] or 0
                expense = Expense.objects.filter(
                    vault=vault,
                    transaction_type='expense'
                ).aggregate(
                    Sum('amount')
                )['amount__sum'] or 0

                balance = income - expense
                current_expenses = expense

                limit = GroupLimit.objects.filter(
                    vault=vault
                ).first()
                if transaction.amount > balance:
                    form.add_error(
                        'amount',
                        'Not enough money in the vault.'
                    )
                elif limit and current_expenses + transaction.amount > limit.monthly_limit:
                    form.add_error(
                        'amount',
                        f'Group limit ({limit.monthly_limit} ₴) exceeded.'
                    )
                else:
                    transaction.save()
            else:
                transaction.save()
            if transaction.pk:
                members = Membership.objects.filter(vault=vault)
                count = members.count()
                if count > 0:
                    share = transaction.amount / count
                    for member in members:
                        ExpenseSplit.objects.create(
                            expense=transaction,
                            user=member.user,
                            amount=share
                        )
                return redirect('vault_detail', vault.id)
    else:
        form = ExpenseForm(user=request.user)

    return render(
        request,
        'finance/add_vault_transaction.html',
        {
            'form': form,
            'vault': vault,
        }
    )
@login_required
def edit_transaction(request, transaction_id):

    transaction = get_object_or_404( Expense, id=transaction_id, owner=request.user)

    form = ExpenseForm(request.POST or None, instance=transaction, user=request.user)

    if form.is_valid():
        form.save()
        return redirect('home')

    return render(
        request,
        'finance/edit_transaction.html',
        {
            'form': form,
        }
    )
@login_required
def delete_transaction(request, transaction_id):


    transaction = get_object_or_404(Expense,id=transaction_id,owner=request.user)

    if request.method == 'POST':
        transaction.delete()
        return redirect('home')

    return render(
        request,
        'finance/delete_transaction.html',
        {
            'transaction': transaction,
        }
    )

@login_required
def statistics(request):

    category_stats = (
        Expense.objects.filter(
            owner=request.user,
            vault__isnull=True,
            transaction_type='expense'
        )
        .values('category__name')
        .annotate(
            total=Sum('amount'),
            transactions=Count('id')
        )
        .order_by('-total')
    )
    
    total_expenses = Expense.objects.filter(
        owner=request.user,
        vault__isnull=True,
        transaction_type='expense'
    ).aggregate(
        Sum('amount')
    )['amount__sum'] or 0

    total_transactions = Expense.objects.filter(
        owner=request.user,
        vault__isnull=True
    ).count()

    return render(
        request,
        'dashboard/statistics.html',
        {
            'category_stats': category_stats,
            'total_expenses': total_expenses,
            'total_transactions': total_transactions,
        }
    )
@login_required
def set_limit(request, vault_id):

    vault = get_object_or_404(Vault, id=vault_id)

    if not Membership.objects.filter(
        user=request.user,
        vault=vault
    ).exists():
        return redirect('vaults')

    if not is_manager(request.user, vault):
        return redirect('vault_detail', vault.id)

    limit, created = GroupLimit.objects.get_or_create( vault=vault, defaults={ 'monthly_limit': 0 })

    if request.method == 'POST':
        limit.monthly_limit = request.POST.get('limit')
        limit.save()

        return redirect('vault_detail', vault.id)

    return render(
        request,
        'finance/set_limit.html',
        {
            'vault': vault,
            'limit': limit,
            'created': created
        }
    )

