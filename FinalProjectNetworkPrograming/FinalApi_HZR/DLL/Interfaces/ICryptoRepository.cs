using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    public interface ICryptoRepository
    {
        
        Task<CryptoMonet[]> Get200CryptosAsync();
        Task<CryptoMonet[]> SortCryptosAsync();
        Task<CryptoMonet> СhangeСurrencyCryptosAsync(string Symble, string Convert);
        Task<CryptoMonet> GetCryptosBySymbolAsync(string Symble);
        Task <CryptoMonet> GetCryptoByIdAsync(int id);
        Task <CryptoMonet> AddCryptoAsync(CryptoMonet crypto);
        Task <CryptoMonet> UpdateCryptoAsync(CryptoMonet crypto);
        Task DeleteCryptoAsync(int id);
    }
}
