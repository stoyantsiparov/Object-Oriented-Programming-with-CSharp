using System;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Models.Cocktails
{
    public class Cocktail : ICocktail
    {
        public Cocktail(string name, string size, double price)
        {
            Name = name;
            Size = size;
            Price = price;
        }

        private string name;
        private double price;

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    //throw new ArgumentException("Name cannot be null or whitespace!");
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }

                name = value;
            }
        }

        public string Size { get; private set; }

        public double Price
        {
            get => price;
            private set
            {
                if (Size == "Large")
                {
                    price = value;
                }
                else if (Size == "Middle")
                {
                    price = (2.0 / 3) * value;
                }
                else if (Size == "Small")
                {
                    price = (1.0 / 3) * value;
                }
            }
        }

        public override string ToString()
        {
            return $"{Name} ({Size}) - {Price:F2} lv";
        }
    }
}