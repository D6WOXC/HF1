using System;
using System.Collections.Generic;
using System.Text;

namespace HF1A
{
    class Cart
    {
        string content;
        public Cart(string ConsumerID)
        {
            this.content = ConsumerID;
        }

        public void addToCart(string ID)
        {
            this.content += ID;
        }
    }
}
