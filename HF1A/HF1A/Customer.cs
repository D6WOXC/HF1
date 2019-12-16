using System;
using System.Collections.Generic;
using System.Text;

namespace HF1A
{
    class Customer
    {
        public string name;
        public int points;
        public Boolean IsClubMember;

        public Customer(string name, int points=0, Boolean IsClubMember=false)
        {
            this.name = name;
            this.points = points;
            this.IsClubMember = IsClubMember;
        }

        public void addPoints(int points)
        {
            this.points += points;
        }
    }
}
