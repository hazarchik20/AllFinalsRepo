using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DLL.Models;

namespace BLL.Translaters
{
    public static class TranslateDLLtoBLL
    {  
        // -------------------- ADDRESS --------------------
        public static BLL.Models.Address TranslateAddress(DLL.Models.Address address)
        {
            if (address == null)
                return null;

            return new BLL.Models.Address
            {
                Id = address.Id,
                City = address.City,
                Street = address.Street,
                HouseNumber = address.HouseNumber,
                PostalCode = address.PostalCode
            };
        }

        // -------------------- CATEGORY --------------------
        public static BLL.Models.Category TranslateCategory(DLL.Models.Category category)
        {
            if (category == null)
                return null;

            return new BLL.Models.Category
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        // -------------------- PRODUCT --------------------
        public static BLL.Models.Product TranslateProduct(DLL.Models.Product product)
        {
            if (product == null)
                return null;

            return new BLL.Models.Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                Count = product.Count,
                Image = product.Image,
                CategoryId = product.CategoryId,
                Category = TranslateCategory(product.Category)
            };
        }

        public static BLL.Models.Product[] TranslateProduct(DLL.Models.Product[] product)
        {
            if (product == null)
                return null;
            BLL.Models.Product[] result = new BLL.Models.Product[product.Length];
            for(int i = 0; i < product.Length; i++)
            {
                result[i] = TranslateProduct(product[i]);
            }
            return result;
        }

        // -------------------- CART ITEM --------------------
        public static BLL.Models.CartItem TranslateCartItem(DLL.Models.CartItem cartItem)
        {
            if (cartItem == null)
                return null;

            return new BLL.Models.CartItem
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,

                Product = TranslateProduct(cartItem.Product)
            };
        }

        // -------------------- CART --------------------
        public static BLL.Models.Cart TranslateCart(DLL.Models.Cart cart)
        {
            if (cart == null)
                return null;

            return new BLL.Models.Cart
            {
                Id = cart.Id,
                Items = cart.Items?
                    .Select(TranslateCartItem)
                    .ToList()
            };
        }

        // -------------------- USER --------------------
        public static BLL.Models.User TranslateUser(DLL.Models.User user)
        {
            if (user == null)
                return null;

            return new BLL.Models.User
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                IsBlocked = user.IsBlocked,
                Password = user.Password,

                Cart = TranslateCart(user.Cart)
            };
        }

        // -------------------- ORDER --------------------
        public static BLL.Models.Order TranslateOrder(DLL.Models.Order order)
        {
            if (order == null)
                return null;

            return new BLL.Models.Order
            {
                Id = order.Id,
                State = order.State,
                Comment = order.Comment,
                Phone = order.Phone,

                UserId = order.UserId,
                AddressId = order.AddressId,

                User = TranslateUser(order.User),
                Address = TranslateAddress(order.Address)
            };
        }
        
    }
}
