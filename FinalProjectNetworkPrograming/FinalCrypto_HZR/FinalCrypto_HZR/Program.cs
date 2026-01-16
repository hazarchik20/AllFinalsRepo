using DLL.Model;
using DLL.Repository;

var repo = new CryptoAPIRepository(); // Передай свій контекст
bool exit = false;

while (!exit)
{
    Console.WriteLine("\n=== Crypto Console UI ===");
    Console.WriteLine("1. Get 200 Cryptos");
    Console.WriteLine("2. Sort Cryptos");
    Console.WriteLine("3. Change Currency of Crypto");
    Console.WriteLine("4. Get Crypto by Symbol");
    Console.WriteLine("5. Get Crypto by ID");
    Console.WriteLine("6. Add Crypto");
    Console.WriteLine("7. Update Crypto");
    Console.WriteLine("8. Delete Crypto");
    Console.WriteLine("0. Exit");
    Console.Write("Choose option: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            {
                var cryptos200 = await repo.Get200CryptosAsync();
                ShowCryptos(cryptos200);
            }
            break;

        case "2":
            {
                var sorted = await repo.SortCryptosAsync();
                ShowCryptos(sorted);
            }
            break;

        case "3":
            {
                Console.Write("Enter Symbol: ");
                string sym = Console.ReadLine();

                Console.Write("Enter Convert Currency (USD, EUR...): ");
                string convert = Console.ReadLine();

                var changed = await repo.СhangeСurrencyCryptosAsync(sym, convert);

                Console.WriteLine($"ID: {changed.id}, Name: {changed.name}, Symbol: {changed.symbol}, Price USD: {changed.quote?.USD?.price}");
            }
            break;

        case "4":
            {
                Console.Write("Enter Symbol: ");
                string symbol = Console.ReadLine();

                var bySymbol = await repo.GetCryptosBySymbolAsync(symbol);

                Console.WriteLine($"ID: {bySymbol.id}, Name: {bySymbol.name}, Symbol: {bySymbol.symbol}, Price USD: {bySymbol.quote?.USD?.price}");
            }
            break;

        case "5":
            { 
                Console.Write("Enter ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                var byId = await repo.GetCryptoByIdAsync(id);

                Console.WriteLine($"ID: {byId.id}, Name: {byId.name}, Symbol: {byId.symbol}, Price USD: {byId.quote?.USD?.price}");
            }
            break;

        case "6":
            {
                var newCrypto = new CryptoMonet();
                Console.Write("Name: "); 

                newCrypto.name = Console.ReadLine();
                Console.Write("Symbol: "); 

                newCrypto.symbol = Console.ReadLine();
                newCrypto.quote = new Quote { USD = new QuoteName() };

                Console.Write("Price USD: "); 
                newCrypto.quote.USD.price = float.Parse(Console.ReadLine());

                var added = await repo.AddCryptoAsync(newCrypto);

                Console.WriteLine($"ID: {added.id}, Name: {added.name}, Symbol: {added.symbol}, Price USD: {added.quote?.USD?.price}");
            }
            break;

        case "7":
            {
                Console.Write("Enter ID to update: ");
                int updateId = Convert.ToInt32(Console.ReadLine());

                var cryptoToUpdate = await repo.GetCryptoByIdAsync(updateId);

                Console.Write("New Name: ");
                string newName = Console.ReadLine();

                if (!string.IsNullOrEmpty(newName)) cryptoToUpdate.name = newName;

                Console.Write("New Symbol: ");
                string SymbolInput = Console.ReadLine();

                cryptoToUpdate.symbol = SymbolInput;

                Console.Write("New Price USD: ");
                float priceInput = float.Parse(Console.ReadLine());

                cryptoToUpdate.quote.USD.price = priceInput;

                var updated = await repo.UpdateCryptoAsync(cryptoToUpdate);

                Console.WriteLine($"ID: {updated.id}, Name: {updated.name}, Symbol: {updated.symbol}, Price USD: {updated.quote?.USD?.price}");

            }
            break;

        case "8":
            { 
                Console.Write("Enter ID to delete: ");
                int delId = Convert.ToInt32(Console.ReadLine());
                await repo.DeleteCryptoAsync(delId);
                Console.WriteLine("Deleted crypto with ID delId");
            }
            break;

        case "0":
            exit = true;
            break;

        default:
            Console.WriteLine("Invalid option");
            break;
    }
}
static void ShowCryptos(CryptoMonet[] cryptos)
{
    foreach (var c in cryptos)
    {
        Console.WriteLine($"ID: {c.id}, Name: {c.name}, Symbol: {c.symbol}, Price USD: {c.quote?.USD?.price}");
    }
}
