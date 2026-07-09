from django.urls import path
from . import views

urlpatterns = [
    path('add/', views.add_transaction, name='add_transaction'),
    path('vaults/', views.vaults, name='vaults'),
    path('vault/add/', views.add_vault, name='add_vault'),
    path('vault/<int:vault_id>/', views.vault_detail, name='vault_detail'),
    path('vault/<int:vault_id>/member/add/', views.add_member, name='add_member'),
    path('vault/<int:vault_id>/transaction/add/', views.add_vault_transaction, name='add_vault_transaction'),
    path('vault/<int:vault_id>/limit/', views.set_limit, name='set_limit'),
    path('transaction/<int:transaction_id>/edit/', views.edit_transaction, name='edit_transaction'),
    path('transaction/<int:transaction_id>/delete/', views.delete_transaction, name='delete_transaction'),
    path('statistics/',views.statistics,name='statistics'),
]