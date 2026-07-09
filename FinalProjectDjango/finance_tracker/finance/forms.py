from django import forms
from .models import Expense, Vault,Membership
from django.contrib.auth.models import User


class MembershipForm(forms.ModelForm):

    class Meta:
        model = Membership
        fields = [
            'user',
            'role',
        ]
        widgets = {
            'title': forms.TextInput(attrs={'class': 'form-control'}),
            'amount': forms.NumberInput(attrs={'class': 'form-control'}),
            'transaction_type': forms.Select(attrs={'class': 'form-select'}),
            'category': forms.Select(attrs={'class': 'form-select'}),
            'vault': forms.Select(attrs={'class': 'form-select'}),
        }

class ExpenseForm(forms.ModelForm):

    class Meta:
        model = Expense
        fields = [
            'title',
            'amount',
            'transaction_type',
            'category',
            'vault',
        ]
        widgets = {
            'title': forms.TextInput(attrs={'class': 'form-control'}),
            'amount': forms.NumberInput(attrs={'class': 'form-control'}),
            'transaction_type': forms.Select(attrs={'class': 'form-select'}),
            'category': forms.Select(attrs={'class': 'form-select'}),
            'vault': forms.Select(attrs={'class': 'form-select'}),
            'user': forms.Select(attrs={'class':'form-select'}),
            'role': forms.Select(attrs={'class':'form-select'}),
        }
    def __init__(self, *args, user=None, **kwargs):
        super().__init__(*args, **kwargs)

        if user:
            self.fields['vault'].queryset = Vault.objects.filter(
                membership__user=user
            ).distinct()




class VaultForm(forms.ModelForm):

    class Meta:
        model = Vault
        fields = [
            'name',
            'description',
        ]
        widgets = {
            'title': forms.TextInput(attrs={'class': 'form-control'}),
            'amount': forms.NumberInput(attrs={'class': 'form-control'}),
            'transaction_type': forms.Select(attrs={'class': 'form-select'}),
            'category': forms.Select(attrs={'class': 'form-select'}),
            'vault': forms.Select(attrs={'class': 'form-select'}),
            'name': forms.TextInput(attrs={'class': 'form-control'}),
            'description': forms.Textarea(attrs={'class':'form-control','rows':3}),
        }
