using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {

       
        Console.WriteLine("=== Задание 1: Банкомат ===");
        Console.WriteLine(new string('-', 50));
        try
        {
            ATM atm = new ATM(500, 1234);
            atm.PrintBalance();

            
            Console.WriteLine("\nПопытка снять 1000:");
            try
            {
                atm.Withdraw(1000);
            }
            catch (InsufficientFundsException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nПроверка PIN:");
            int[] pins = { 0000, 1211, 3999, 1234 };
            foreach (int pin in pins)
            {
                try
                {
                    Console.WriteLine($"Ввод PIN: {pin}");
                    atm.VerifyPin(pin);
                    break;
                }
                catch (IncorrectPinException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (CardBlockedException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Общая ошибка: {ex.Message}");
        }

        
        Console.WriteLine("\n\n=== Задание 2: Интернет-магазин ===");
        Console.WriteLine(new string('-', 50));
        try
        {
            OnlineShop shop = new OnlineShop();
            shop.AddProduct("Ноутбук", 5);
            shop.AddProduct("Мышь", 10);

            Console.WriteLine("Покупаем 3 ноутбука:");
            shop.Buy("Ноутбук", 3);

            Console.WriteLine("\nПокупаем еще 3 ноутбука (должно остаться 2):");
            shop.Buy("Ноутбук", 3);

            Console.WriteLine("\nПытаемся купить 5 ноутбуков (должна быть ошибка):");
            try
            {
                shop.Buy("Ноутбук", 5);
            }
            catch (ProductOutOfStockException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Общая ошибка: {ex.Message}");
        }

        
        Console.WriteLine("\n\n=== Задание 3: Регистрация ===");
        Console.WriteLine(new string('-', 50));
        try
        {
            UserService userService = new UserService();

            Console.WriteLine("Регистрация: admin, 123");
            try
            {
                userService.Register("admin", "123");
            }
            catch (WeakPasswordException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nРегистрация: user1, secret123");
            userService.Register("user1", "secret123");

            Console.WriteLine("\nПовторная регистрация: user1, password123");
            try
            {
                userService.Register("user1", "password123");
            }
            catch (LoginAlreadyExistsException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Общая ошибка: {ex.Message}");
        }

       
        Console.WriteLine("\n\n=== Задание 4: Игра ===");
        Console.WriteLine(new string('-', 50));
        try
        {
            Game game = new Game(100);
            game.PrintHealth();

            Console.WriteLine("\nПолучаем урон: 50");
            game.TakeDamage(50);

            Console.WriteLine("\nПолучаем урон: 150");
            try
            {
                game.TakeDamage(150);
            }
            catch (HeroIsDeadException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Общая ошибка: {ex.Message}");
        }

        
        Console.WriteLine("\n\n=== Задание 5: Библиотека ===");
        Console.WriteLine(new string('-', 50));
        try
        {
            Library library = new Library();

            Console.WriteLine("Анна берёт 'Война и мир'");
            library.TakeBook("Война и мир", "Анна");

            Console.WriteLine("\nПетр пытается взять 'Война и мир'");
            try
            {
                library.TakeBook("Война и мир", "Петр");
            }
            catch (BookAlreadyTakenException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nПетр возвращает книгу 'Война и мир':");
            library.ReturnBook("Война и мир");

            Console.WriteLine("\nАнна возвращает книгу 'Война и мир':");
            library.ReturnBook("Война и мир");

            Console.WriteLine("\nТеперь Петр может взять 'Война и мир':");
            library.TakeBook("Война и мир", "Петр");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Общая ошибка: {ex.Message}");
        }
    }
}
