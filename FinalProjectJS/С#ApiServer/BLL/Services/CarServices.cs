using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CarServices
    {
        public static bool isCarValid(Car inputCar)
        {
            if (inputCar == null)
                return false;

           
            if (string.IsNullOrWhiteSpace(inputCar.make))
                return false;

            if (string.IsNullOrWhiteSpace(inputCar.model))
                return false;

            if (string.IsNullOrWhiteSpace(inputCar.type))
                return false;

            if (inputCar.description != null && inputCar.description.Trim().Length == 0)
                return false;

            if (inputCar.year < 1900 || inputCar.year > 2100)
                return false;

            if (inputCar.mileage < 0)
                return false;

            if (inputCar.price <= 100)
                return false;

            if (inputCar.discount < 0 || inputCar.discount > 100)
                return false;

            if (!string.IsNullOrWhiteSpace(inputCar.imageUrl))
            {
                if (!Uri.IsWellFormedUriString(inputCar.imageUrl, UriKind.Absolute))
                    return false;
            }

            return true;
        }
    
    }
}
