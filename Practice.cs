using System;
using System.Collections.Generic;


public class InsufficientFundsException : Exception
{
    public decimal CurrentBalance { get; }
    public decimal RequiredAmount { get; }

    public InsufficientFundsException(decimal currentBalance, decimal requiredAmount)
        : base($"Недостаточно средств! Текущий баланс: {currentBalance}, нужно: {requiredAmount}")
    {
        CurrentBalance = currentBalance;
        RequiredAmount = requiredAmount;
    }
}


public class IncorrectPinException : Exception
{
    public int AttemptsLeft { get; }

    public IncorrectPinException(int attemptsLeft)
        : base($"Неверный PIN! Осталось попыток: {attemptsLeft}")
    {
        AttemptsLeft = attemptsLeft;
    }
}


public class CardBlockedException : Exception
{
    public CardBlockedException()
        : base("Карта заблокирована!")
    {
    }
}


public class ATM
{
    private decimal balance;
    private int correctPin;
    private int attemptsLeft;
    private bool isBlocked;

    public ATM(decimal initialBalance, int pin)
    {
        balance = initialBalance;
        correctPin = pin;
        attemptsLeft = 3;
        isBlocked = false;
    }

    public void Withdraw(decimal amount)
    {
        if (amount > balance)
        {
            throw new InsufficientFundsException(balance, amount);
        }

        balance -= amount;
        Console.WriteLine($"Снято: {amount}. Остаток: {balance}");
    }

    public bool VerifyPin(int pin)
    {
        if (isBlocked)
        {
            throw new CardBlockedException();
        }

        if (pin == correctPin)
        {
            attemptsLeft = 3;
            Console.WriteLine("PIN верный!");
            return true;
        }
        else
        {
            attemptsLeft--;

            if (attemptsLeft <= 0)
            {
                isBlocked = true;
                throw new CardBlockedException();
            }
            else
            {
                throw new IncorrectPinException(attemptsLeft);
            }
        }
    }

    public void PrintBalance()
    {
        Console.WriteLine($"Баланс: {balance}");
    }
}


public class ProductOutOfStockException : Exception
{
    public string ProductName { get; }
    public int StockQuantity { get; }

    public ProductOutOfStockException(string productName, int stockQuantity)
        : base($"Товар '{productName}' закончился! В наличии: {stockQuantity}")
    {
        ProductName = productName;
        StockQuantity = stockQuantity;
    }
}


public class OnlineShop
{
    private Dictionary<string, int> stock;

    public OnlineShop()
    {
        stock = new Dictionary<string, int>();
    }

    public void AddProduct(string productName, int quantity)
    {
        if (stock.ContainsKey(productName))
        {
            stock[productName] += quantity;
        }
        else
        {
            stock[productName] = quantity;
        }
    }

    public void Buy(string productName, int quantity)
    {
        if (!stock.ContainsKey(productName))
        {
            throw new ProductOutOfStockException(productName, 0);
        }

        if (stock[productName] < quantity)
        {
            throw new ProductOutOfStockException(productName, stock[productName]);
        }

        stock[productName] -= quantity;
        Console.WriteLine($"Куплено: {productName} x{quantity}. Осталось: {stock[productName]}");
    }
}


public class LoginAlreadyExistsException : Exception
{
    public string Login { get; }

    public LoginAlreadyExistsException(string login)
        : base($"Логин '{login}' уже занят!")
    {
        Login = login;
    }
}


public class WeakPasswordException : Exception
{
    public WeakPasswordException()
        : base("Слабый пароль! Минимум 6 символов")
    {
    }
}


public class UserService
{
    private HashSet<string> registeredLogins = new HashSet<string>();

    public void Register(string login, string password)
    {
        if (registeredLogins.Contains(login))
        {
            throw new LoginAlreadyExistsException(login);
        }

        if (password.Length < 6)
        {
            throw new WeakPasswordException();
        }

        registeredLogins.Add(login);
        Console.WriteLine($"Успешная регистрация: {login}");
    }
}


public class HeroIsDeadException : Exception
{
    public int RemainingHealth { get; }

    public HeroIsDeadException(int remainingHealth)
        : base($"Герой погиб! Здоровье стало: {remainingHealth}")
    {
        RemainingHealth = remainingHealth;
    }
}


public class Game
{
    public int Health { get; private set; }

    public Game(int initialHealth)
    {
        Health = initialHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            throw new HeroIsDeadException(Health);
        }

        Console.WriteLine($"Получен урон: {damage}. Здоровье: {Health}");
    }

    public void PrintHealth()
    {
        Console.WriteLine($"Здоровье: {Health}");
    }
}


public class BookAlreadyTakenException : Exception
{
    public string BookName { get; }
    public string CurrentReader { get; }

    public BookAlreadyTakenException(string bookName, string currentReader)
        : base($"Книга '{bookName}' уже выдана читателю '{currentReader}'!")
    {
        BookName = bookName;
        CurrentReader = currentReader;
    }
}


public class Library
{
    private Dictionary<string, string> takenBooks = new Dictionary<string, string>();

    public void TakeBook(string bookName, string reader)
    {
        if (takenBooks.ContainsKey(bookName))
        {
            throw new BookAlreadyTakenException(bookName, takenBooks[bookName]);
        }

        takenBooks[bookName] = reader;
        Console.WriteLine($"{reader} берёт книгу: '{bookName}'");
    }

    public void ReturnBook(string bookName)
    {
        if (takenBooks.ContainsKey(bookName))
        {
            string reader = takenBooks[bookName];
            takenBooks.Remove(bookName);
            Console.WriteLine($"{reader} возвращает книгу: '{bookName}'");
        }
        else
        {
            Console.WriteLine($"Книга '{bookName}' не была выдана");
        }
    }
}
