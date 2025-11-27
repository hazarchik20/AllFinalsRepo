using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    public interface IRepositoryInterfaces
    {
        Task<Car> GetCarByIdAsync(int id);
        Task<Car> AddCarAsync(Car car);
        Task<Car> UpdateCarAsync(Car car);
        Task<List<Car>> GetAllCarAsync();
        Task DeleteCarAsync(int id); 
    }
}
