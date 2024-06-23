using System.Linq;
using System.Text;
using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private BoothRepository booths;

        public Controller()
        {
            booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            var booth = new Booth(booths.Models.Count + 1, capacity);
            booths.AddModel(booth);

            //return $"Added booth number {booth.BoothId} with capacity {capacity} in the pastry shop!";
            return string.Format(OutputMessages.NewBoothAdded, booth.BoothId, capacity);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (delicacyTypeName != "Stolen" && delicacyTypeName != "Gingerbread")
            {
                //return $"Delicacy type {delicacyTypeName} is not supported in our application!";
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            var booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            var delicacy = booth.DelicacyMenu.Models.FirstOrDefault(d => d.Name == delicacyName);

            if (delicacy != null)
            {
                //return $"{delicacyName} is already added in the pastry shop!";
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }

            if (delicacyTypeName == "Stolen")
            {
                delicacy = new Stolen(delicacyName);
            }
            else if (delicacyTypeName == "Gingerbread")
            {
                delicacy = new Gingerbread(delicacyName);
            }

            booth.DelicacyMenu.AddModel(delicacy);

            // return "{delicacyTypeName} {delicacyName} added to the pastry shop!";
            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (cocktailTypeName != "Hibernation" && cocktailTypeName != "MulledWine")
            {
                //return $"Cocktail type {cocktailTypeName} is not supported in our application!";
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }

            if (size != "Small" && size != "Middle" && size != "Large")
            {
                //return $"{size} is not recognized as valid cocktail size!";
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            }

            var booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            var cocktail = booth.CocktailMenu.Models.FirstOrDefault(c => c.Size == size && c.Name == cocktailName);

            if (cocktail != null)
            {
                //return $"{size} {cocktailName} is already added in the pastry shop!";
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }

            if (cocktailTypeName == "Hibernation")
            {
                cocktail = new Hibernation(cocktailName, size);
            }
            else if (cocktailTypeName == "MulledWine")
            {
                cocktail = new MulledWine(cocktailName, size);
            }

            booth.CocktailMenu.AddModel(cocktail);

            //return $"{size} {cocktailName} {cocktailTypeName} added to the pastry shop!";
            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string ReserveBooth(int countOfPeople)
        {
            var booth = booths.Models
                .Where(b => !b.IsReserved && b.Capacity >= countOfPeople)
                .OrderBy(b => b.Capacity)
                .ThenByDescending(b => b.BoothId)
                .FirstOrDefault();

            if (booth == null)
            {
                //return $"No available booth for {countOfPeople} people!";
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }

            booth.ChangeStatus();

            //return $"Booth {booth.BoothId} has been reserved for {countOfPeople} people!";
            return string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            string[] input = order
                .Split("/")
                .ToArray();

            string itemTypeName = input[0];
            string itemName = input[1];
            int count = int.Parse(input[2]);
            string size = string.Empty;

            if (itemTypeName == "Hibernation" || itemTypeName == "MulledWine")
            {
                size = input[3];
            }

            var booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            if (itemTypeName != "Hibernation" && itemTypeName != "MulledWine" &&
                itemTypeName != "Gingerbread" && itemTypeName != "Stolen")
            {
                //return $"{itemTypeName} is not recognized type!";
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }

            if (itemTypeName == "Hibernation" || itemTypeName == "MulledWine")
            {
                if (!booth.CocktailMenu.Models.Any(c => c.Name == itemName))
                {
                    //return $"There is no {itemTypeName} {itemName} available!";
                    return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
                }

                var cocktail = booth.CocktailMenu.Models.FirstOrDefault(c => c.Name == itemName && c.Size == size);

                if (cocktail == null)
                {
                    //return $"There is no {size} {itemName} available!";
                    return string.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                }

                booth.UpdateCurrentBill(count * cocktail.Price);


                //return $"Booth {boothId} ordered {count} {itemName}!";
                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, count, itemName);
            }
            else
            {
                if (!booth.DelicacyMenu.Models.Any(d => d.Name == itemName))
                {
                    //return $"There is no {itemTypeName} {itemName} available!";
                    return string.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                }

                var delicacy = booth.DelicacyMenu.Models.FirstOrDefault(d => d.Name == itemName);

                if (delicacy == null)
                {
                    //return $"There is no {itemTypeName} {itemName} available!";
                    return string.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                }

                booth.UpdateCurrentBill(count * delicacy.Price);


                //return $"Booth {boothId} ordered {count} {itemName}!";
                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, count, itemName);

            }
        }

        public string LeaveBooth(int boothId)
        {
            var booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            StringBuilder sb = new StringBuilder();

            //sb.AppendLine($"Bill {booth.CurrentBill:f2} lv");
            sb.AppendLine(string.Format(OutputMessages.GetBill, booth.CurrentBill.ToString("f2")));

            //sb.AppendLine($"Booth {boothId} is now available!");
            sb.AppendLine(string.Format(OutputMessages.BoothIsAvailable, boothId));

            booth.Charge();
            booth.ChangeStatus();

            return sb.ToString().TrimEnd();
        }

        public string BoothReport(int boothId)
        {
            var booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            return booth.ToString();
        }
    }
}