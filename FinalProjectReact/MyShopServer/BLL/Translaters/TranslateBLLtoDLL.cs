using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Translaters
{
    public static class TranslateBLLtoDLL
    {
        // -------------------- ADDRESS --------------------
        public static DLL.Models.Address TranslateAddress(BLL.Models.Address address)
        {
            if (address == null)
                return null;

            return new DLL.Models.Address
            {
                Id = address.Id,
                City = address.City,
                Street = address.Street,
                HouseNumber = address.HouseNumber,
                PostalCode = address.PostalCode
            };
        }

        // -------------------- CATEGORY --------------------
        public static DLL.Models.Category TranslateCategory(BLL.Models.Category category)
        {
            if (category == null)
                return null;

            return new DLL.Models.Category
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        // -------------------- PRODUCT --------------------
        public static DLL.Models.Product TranslateProduct(BLL.Models.Product product)
        {
            if (product == null)
                return null;

            return new DLL.Models.Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                Count = product.Count,
                Image = product.Image,
                CategoryId = product.CategoryId
            };
        }

        // -------------------- CART ITEM --------------------
        public static DLL.Models.CartItem TranslateCartItem(BLL.Models.CartItem cartItem)
        {
            if (cartItem == null)
                return null;

            return new DLL.Models.CartItem
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity
            };
        }

        // -------------------- CART --------------------
        public static DLL.Models.Cart TranslateCart(BLL.Models.Cart cart)
        {
            if (cart == null)
                return null;

            return new DLL.Models.Cart
            {
                Id = cart.Id,
                Items = cart.Items?
                    .Select(TranslateCartItem)
                    .ToList()
            };
        }

        // -------------------- USER --------------------
        public static DLL.Models.User TranslateUser(BLL.Models.User user)
        {
            if (user == null)
                return null;

            return new DLL.Models.User
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                IsBlocked = user.IsBlocked,
                Password = user.Password,

                CartId= user.CartId,
                Cart = TranslateCart(user.Cart)
            };
        }

        // -------------------- ORDER --------------------
        public static DLL.Models.Order TranslateOrder(BLL.Models.Order order)
        {
            if (order == null)
                return null;

            return new DLL.Models.Order
            {
                Id = order.Id,
                State = order.State,
                Comment = order.Comment,
                Phone = order.Phone,

                UserId = order.UserId,
                AddressId = order.AddressId
            };
        }
    }
}
