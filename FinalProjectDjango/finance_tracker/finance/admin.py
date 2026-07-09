from django.contrib import admin
from .models import Category, Vault, Membership, Expense, GroupLimit
from .models import ExpenseSplit

admin.site.register(Category)
admin.site.register(Vault)
admin.site.register(Membership)
admin.site.register(Expense)
admin.site.register(GroupLimit)
admin.site.register(ExpenseSplit)