using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HF1A
{
    class ComboDiscount:Discount
    {
        public int price;
        public ComboDiscount(string ID, int price, Boolean onlyForClubMembers = false)
        {
            this.ID = ID;
            this.price = price;
            this.onlyForClubMembers = onlyForClubMembers;
        }

        public override int getProfit(string ID, int price)
        {
            int totalcost = 0;
            for(int i = 0; i <= ID.Count(); i++)
            {
                Shop.products.TryGetValue(Char.ToString(ID[i]), out int cost);
                totalcost += cost;
            }
            return totalcost - price;
        }

        public override int getProfit()
        {
            return getProfit(this.ID, this.price);
        }


        public override Tuple<string, int> UseDiscount(string CartID)
        {
            if (Shop.IsDiscountAvailable(CartID.Substring(1, 7), this))
            {
                return new Tuple<string, int>(CartID, 0);
            }

            int Fullprice = 0;

            while (true)
            {
                //alkalmazható-e rá a discount?
                foreach (char product in this.ID)
                {
                    if (! CartID.Contains(product)) return new Tuple<string, int>(CartID, Fullprice);
                }
                //ha igen, akkor fizessünk a discount-ot és vegyük ki a fizetett termékeket.
                Fullprice += this.price;

                foreach (char product in this.ID)
                {
                    CartID=CartID.Remove(product.ToString().LastIndexOf(product));
                }
            }
        }
    }
}
