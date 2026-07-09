from django.shortcuts import render
from django.db.models import Sum
from finance.models import Expense, Category
from django.contrib.auth.decorators import login_required


@login_required
def home(request):

    income_data = Expense.objects.filter(
        owner=request.user,
        vault__isnull=True,
        transaction_type='income'
    ).aggregate(total=Sum('amount'))

    expense_data = Expense.objects.filter(
        owner=request.user,
        vault__isnull=True,
        transaction_type='expense'
    ).aggregate(total=Sum('amount'))

    income_total = income_data['total'] or 0
    expense_total = expense_data['total'] or 0

    balance = income_total - expense_total

    expenses = Expense.objects.filter(
        owner=request.user,
        vault__isnull=True
    ).order_by('-created_at')[:5]
    
    categories = Category.objects.all()

    context = {
        'income': income_total,
        'expense': expense_total,
        'balance': balance,
        'expenses': expenses,
        'categories': categories,
    }

    return render(request, 'dashboard/home.html', context)