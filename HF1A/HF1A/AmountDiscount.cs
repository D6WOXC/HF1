using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HF1A
{
    class AmountDiscount : Discount
    {
        public int percent;

        public AmountDiscount(string ID, int percent, Boolean onlyForClubMembers = false)
        {
            this.ID = ID;
            this.percent = percent;
            this.onlyForClubMembers = onlyForClubMembers;
        }

        public override int getProfit(string ID, int percent)
        {
            int amount = ID.Count();
            Shop.products.TryGetValue(Char.ToString(ID[0]), out int cost);
            return cost * amount - cost * (100-percent) * amount;
        }

        public override int getProfit()
        {
            return getProfit(this.ID, this.percent);
        }

        public override Tuple<string, int> UseDiscount(string CartID)
        {
            if (Shop.IsDiscountAvailable(CartID.Substring(1, 7), this))
            {
                return new Tuple<string, int>(CartID, 0);
            }

            int price = 0;

            int amount = CartID.Count(f => f == ID[0]);
            if (amount>0)
            {
                price = amount * Shop.products[ID[0].ToString()]*(100-percent);

                for( int index=0 ; index <= CartID.Length ; index++)
                {
                    if(CartID[index] == ID[0])
                    {
                        CartID.Remove(index);
                    }
                }
            }
            
            return new Tuple<string, int>(CartID, price);
        }
    }
}
