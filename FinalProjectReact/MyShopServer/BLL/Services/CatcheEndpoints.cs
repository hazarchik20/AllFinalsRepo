using Azure;
using BLL.Models;
using BLL.Translaters;
using DLL;
using DLL.Repository;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CatcheEndpoints
    {
        private DataContext _context;
        private ShopReposytory<DLL.Models.Product> _productRepository;
        private ShopReposytory<DLL.Models.Category> _categoryRepository;
        private ShopReposytory<DLL.Models.Cart> _cartRepository;
        private OrderRepo _orderRepository;
        private UserRepo _userRepository;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
           
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true 
        };
        public CatcheEndpoints(DataContext context) 
        {
            _context = context;
            _productRepository = new ShopReposytory<DLL.Models.Product>(_context);
            _categoryRepository = new ShopReposytory<DLL.Models.Category>(_context);
            _cartRepository = new ShopReposytory<DLL.Models.Cart>(_context);
            _userRepository = new UserRepo(_context);
            _orderRepository = new OrderRepo(_context);
        }

        public async Task<(string, int)> CatcheGET(HttpListenerRequest request)
        {
            string response;
            int statusCode;
            var url = request.Url?.AbsolutePath;

            var productIdMatch = Regex.Match(url, @"^/product/(\d+)$");
            var categoryIdMatch = Regex.Match(url, @"^/category/(\d+)$");
            var userIdMatch = Regex.Match(url, @"^/user/(\d+)$");
            var orderIdMatch = Regex.Match(url, @"^/orders/(\d+)$");

            if (url == "/product")
            {
                var query = ParseProductQuery(request);

                IEnumerable<Product> products = TranslateDLLtoBLL.TranslateProduct(await _productRepository.GetAllAsync());

                if (!string.IsNullOrWhiteSpace(query.Search))
                {
                    products = products.Where(p =>
                        p.Name.Contains(query.Search, StringComparison.OrdinalIgnoreCase) ||
                        p.Description.Contains(query.Search, StringComparison.OrdinalIgnoreCase)
                    );
                }

                if (query.CategoryId.HasValue)
                {
                    products = products.Where(p => p.CategoryId == query.CategoryId.Value);
                }
                if (query.PriceFrom.HasValue)
                    products = products.Where(p => p.Price >= query.PriceFrom.Value);

                if (query.PriceTo.HasValue)
                    products = products.Where(p => p.Price <= query.PriceTo.Value);

                products = (query.SortBy, query.SortDir) switch
                {
                    ("price", "asc") => products.OrderBy(p => p.Price),
                    ("price", "desc") => products.OrderByDescending(p => p.Price),
                    ("name", "desc") => products.OrderByDescending(p => p.Name),
                    _ => products.OrderBy(p => p.Name)
                };

                var total = products.Count();

                products = products
                    .Skip((query.Page - 1) * query.PageSize)
                    .Take(query.PageSize);

                var result = products.ToArray();

                foreach (var product in result)
                {
                    product.Category = TranslateDLLtoBLL.TranslateCategory(await _categoryRepository.GetByIdAsync(product.CategoryId));
                }

                response = JsonSerializer.Serialize(new
                {
                    items = result,
                    total,
                    page = query.Page,
                    pageSize = query.PageSize
                }, _jsonOptions);

                statusCode = 200;
            }
            else if (orderIdMatch.Success)
            {
                var id = int.Parse(orderIdMatch.Groups[1].Value);
                var orders = await _orderRepository.GetByUserIdAsync(id);
                if (orders == null)
                {
                    response = "Product not found";
                    statusCode = 404;
                }
                else
                {
                    response = JsonSerializer.Serialize(orders, _jsonOptions);
                    statusCode = 200;
                }

            }
            else if(productIdMatch.Success)
            {
                var id = int.Parse(productIdMatch.Groups[1].Value);
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    response = "Product not found";
                    statusCode = 404;
                }
                else
                {
                    var productBLL = TranslateDLLtoBLL.TranslateProduct(product);
                    productBLL.Category = TranslateDLLtoBLL.TranslateCategory(await _categoryRepository.GetByIdAsync(product.CategoryId));
                    response = JsonSerializer.Serialize(productBLL, _jsonOptions);
                    statusCode = 200;
                }
            }
            else if (url == "/category")
            {
                var categories = await _categoryRepository.GetAllAsync();
                response = JsonSerializer.Serialize(categories, _jsonOptions);
                statusCode = 200;
            }
            else if (categoryIdMatch.Success)
            {
                var id = int.Parse(categoryIdMatch.Groups[1].Value);
                var category = await _categoryRepository.GetByIdAsync(id);

                if (category == null)
                {
                    response = "Category not found";
                    statusCode = 404;
                }
                else
                {
                    response = JsonSerializer.Serialize(category);
                    statusCode = 200;
                }
            }
            else if (url == "/user")
            {
                var users = await _userRepository.GetAllAsync();
                response = JsonSerializer.Serialize(users);
                statusCode = 200;
            }
            else if (userIdMatch.Success)
            {
                var id = int.Parse(userIdMatch.Groups[1].Value);
                var user = await _userRepository.GetByIdAsync(id);

                if (user == null)
                {
                    response = "User not found";
                    statusCode = 404;
                }
                else
                {
                    response = JsonSerializer.Serialize(user, _jsonOptions); 
                    statusCode = 200;
                }
            }
            else
            {
                response = "INVALID GET ENDPOINT";
                statusCode = 400;
            }

            return (response, statusCode);
        }
        public async Task<(string, int)> CatchePOST(HttpListenerRequest request)
        {
            string response;
            int statusCode;
            var url = request.Url?.AbsolutePath;

            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            var body = await reader.ReadToEndAsync();

            if (url == "/product")
            {
                var product = TranslateBLLtoDLL.TranslateProduct(JsonSerializer.Deserialize<Product>(body));
                await _productRepository.AddAsync(product);

                response = JsonSerializer.Serialize(product, _jsonOptions);
                statusCode = 201;
            }
            else if (url == "/category")
            {
                var category = TranslateBLLtoDLL.TranslateCategory(JsonSerializer.Deserialize<Category>(body));
                await _categoryRepository.AddAsync(category);

                response = JsonSerializer.Serialize(category, _jsonOptions);
                statusCode = 201;
            }
            else if (url == "/user/login")
            {
                var loginDto = JsonSerializer.Deserialize<SmaleUser>(body);

                if (loginDto == null)
                    return ("Invalid JSON", 400);

                var User = await _userRepository.GetByEmailAsync(loginDto.Email);

                if (User == null || User.Password != loginDto.Password)
                {
                    return (
                        JsonSerializer.Serialize(new { message = "Невірний email або пароль" }),
                        401
                    );
                }
                return (
                    JsonSerializer.Serialize(User, _jsonOptions),
                    200
                );
            }
            else if (url == "/user")
            {

                var user = TranslateBLLtoDLL.TranslateUser(JsonSerializer.Deserialize<User>(body));
                var TempU = await _userRepository.GetByEmailAsync(user.Email);

                if (TempU == null || TempU.Password != user.Password)
                {
                    var cart = await _cartRepository.AddAsync(new DLL.Models.Cart());
                    user.CartId = cart.Id;
                    user.Cart = cart;
                    await _userRepository.AddAsync(user);
                    response = JsonSerializer.Serialize(user, _jsonOptions);
                    statusCode = 201;
                }
                else 
                {

                    response = "THIS USER IS ALREDADY CREATED";
                    statusCode = 423;
                }

            }
            else
            {
                response = "INVALID POST ENDPOINT";
                statusCode = 400;
            }

            return (response, statusCode);
        }
        public async Task<(string, int)> CatchePUT(HttpListenerRequest request)
        {
            string response;
            int statusCode;
            var url = request.Url?.AbsolutePath;

            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            var body = await reader.ReadToEndAsync();

            var productMatch = Regex.Match(url, "/product");
            var categoryMatch = Regex.Match(url, "/category");
            var userMatch = Regex.Match(url, "/user");

            if (productMatch.Success)
            {
                var product = TranslateBLLtoDLL.TranslateProduct(JsonSerializer.Deserialize<Product>(body));

                await _productRepository.UpdateAsync(product);
                response = JsonSerializer.Serialize(product , _jsonOptions);
                statusCode = 200;
            }
            else if (categoryMatch.Success)
            {
                var category = TranslateBLLtoDLL.TranslateCategory(JsonSerializer.Deserialize<Category>(body));

                await _categoryRepository.UpdateAsync(category);
                response = JsonSerializer.Serialize(category, _jsonOptions);
                statusCode = 200;
            }
            else if (userMatch.Success)
            {
                var user = TranslateBLLtoDLL.TranslateUser(JsonSerializer.Deserialize<User>(body));
                user.Cart = new DLL.Models.Cart();

                await _userRepository.UpdateAsync(user);
                response = JsonSerializer.Serialize(user, _jsonOptions);
                statusCode = 200;
            }
            else
            {
                response = "INVALID PUT ENDPOINT";
                statusCode = 400;
            }

            return (response, statusCode);
        }
        public async Task<(string, int)> CatcheDELETE(HttpListenerRequest request)
        {
            string response = string.Empty;
            int statusCode = 0;
            var url = request.Url?.AbsolutePath;

            var ProductIDMatch = Regex.Match(url, @"^/product/(\d+)$");
            var CategoryIDMatch = Regex.Match(url, @"^/category/(\d+)$");
            var UserIDMatch = Regex.Match(url, @"^/user/(\d+)$");


            if (ProductIDMatch.Success)
            {
                var id = int.Parse(ProductIDMatch.Groups[1].Value);
                await _productRepository.DeleteAsync(id);
                response = JsonSerializer.Serialize("Deleted Successful", _jsonOptions);
                statusCode = 200;
            }
            else if (CategoryIDMatch.Success)
            {
                var id = int.Parse(CategoryIDMatch.Groups[1].Value);
                await _categoryRepository.DeleteAsync(id);
                response = JsonSerializer.Serialize("Deleted Successful", _jsonOptions);
                statusCode = 200;
            }
            else if (UserIDMatch.Success)
            {
                var id = int.Parse(UserIDMatch.Groups[1].Value);
                await _userRepository.DeleteAsync(id);
                response = JsonSerializer.Serialize("Deleted Successful", _jsonOptions);
                statusCode = 200;
            }
            else
            {
                response = "DEVELOPER INVALID";
                statusCode = 400;
            }
            return (response, statusCode);
        }
        private QueryProduct ParseProductQuery(HttpListenerRequest request)
        {
            var q = request.QueryString;

            return new QueryProduct
            {
                Page = int.TryParse(q["page"], out var p) ? p : 1,
                PageSize = int.TryParse(q["pageSize"], out var ps) ? ps : 8,
                Search = q["search"],
                CategoryId = int.TryParse(q["categoryId"], out var c) ? c : null,
                PriceFrom = decimal.TryParse(q["priceFrom"], out var pf) ? pf : null,
                PriceTo = decimal.TryParse(q["priceTo"], out var pt) ? pt : null,
                SortBy = q["sortBy"] ?? "name",
                SortDir = q["sortDir"] ?? "asc"
            };
        }

    }

}
