using System;
using System.Collections.Generic;
using System.Text;

namespace HF1A
{
    abstract class Discount
    {
        public string ID;

        public Boolean onlyForClubMembers;
        public abstract int getProfit(string ID, int custromInt);

        public abstract int getProfit();

        public abstract Tuple<string,int> UseDiscount(string ID);
    }
}