using System;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Models.Delicacies
{
    public abstract class Delicacy : IDelicacy
    {
        protected Delicacy(string name, double price)
        {
            Name = name;
            Price = price;
        }

        private string name;

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
        public double Price { get; private set; }

        public override string ToString()
        {
            return $"{Name} - {Price:F2} lv";
        }
    }
}