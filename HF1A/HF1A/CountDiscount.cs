using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HF1A
{
    class CountDiscount:Discount
    {
        public int price;

        public CountDiscount(string ID, int price, Boolean onlyForClubMembers = false)
        {
            this.ID = ID;
            this.price = price;
            this.onlyForClubMembers = onlyForClubMembers;
        }

        public override int getProfit(string ID, int price)
        {
            int amount = ID.Count();
            Shop.products.TryGetValue(Char.ToString(ID[0]), out int cost);
            return cost * amount - cost * price;
        }

        public override int getProfit()
        {
            return getProfit(this.ID, this.price);
        }

        public override Tuple<string, int> UseDiscount(string CartID)
        {
            if (Shop.IsDiscountAvailable(CartID.Substring(1,7), this))
            {
                return new Tuple<string, int>(CartID, price);
            }

            int Fullprice = 0;

            while (true)
            {
                if (CartID.Contains(ID))
                {
                    Fullprice += price;
                    CartID = Regex.Replace(CartID, ID, "");
                }
                else return new Tuple<string, int>(CartID, Fullprice);
            }
        }
    }
}
