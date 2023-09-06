using System;

namespace Cevent
{
    class Program
    {
        static void Main(string[] args)
        {
            var stock = new Stock("Amazon");
            stock.Price = 129;
            stock.OnPriceChanged += Stock_OnPriceChanged;
            stock.Change(0.05m);
            stock.Change(0.5m);
            stock.OnPriceChanged -= Stock_OnPriceChanged;
            stock.Change(-0.05m);
            stock.OnPriceChanged += Stock_OnPriceChanged;

            stock.Change(0.00m);



        }

        private static void Stock_OnPriceChanged(Stock stock, decimal OldPrice)
        {
            string res = " ";
            if (OldPrice < stock.Price)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                 res = "UP";
            }

            else if (OldPrice > stock.Price)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                 res = "DOWN";

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                 res = "The same";
            }

                Console.WriteLine($"{stock.Name}---: {stock.Price}--{res}");
        }
    }
}

public delegate void  OnpriceChange(Stock stock, decimal OldPrice);
public class Stock
{
    private decimal _price;
    private string _name;

    public string Name => this._name;
    public decimal Price { get => this._price; set => this._price = value; }

    public Stock(string StockName)
    {
        this._name = StockName;
    }

    public event OnpriceChange OnPriceChanged;

    public void Change(decimal percent)
    {
        decimal OldPrice = this._price;
        this._price += Math.Round(this._price * percent, 2);
        if (OnPriceChanged!=null)
        {
            OnPriceChanged(this, OldPrice);
        }
    }
}