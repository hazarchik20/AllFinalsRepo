using BLL.Models;
using DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class CarApiRepository : IRepositoryInterfaces
    {
        private readonly CarApiContext _context;

        public CarApiRepository(CarApiContext context)
        {
            _context = context;
        }
        public async Task<Car> AddCarAsync(Car car)
        {
            _context.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _context.Set<Car>().FirstAsync(x => x.id == id);
            _context.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _context.Set<Car>().FirstAsync(x => x.id == id);
        }

        public async Task<Car> UpdateCarAsync(Car car)
        {
            var existing = await _context.Set<Car>().FirstOrDefaultAsync(x => x.id == car.id);

            if (existing == null)
                throw new Exception("Car not found");

            _context.Entry(existing).CurrentValues.SetValues(car);

            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<List<Car>> GetAllCarAsync()
        {
            return await _context.Set<Car>().ToListAsync();
        }
    }
}
