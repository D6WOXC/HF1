using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HF1A
{
    class Shop
    {
        //termékek árát tartalmazó lista
        static public Dictionary<string, int> products;

        //típusonként szeparált kedvezmények
        static public List<Discount> Discounts;

        //a memberID-kat tartalmazó lista
        static public Dictionary<string,Customer> Customers;

        //a kuponokat tartalmazó lista kulcs=kód érték=kedvezmény százaléka
        static public Dictionary<int, int> CuponCodes;

        public void RegisterCupon(int Code, int percent)
        {
            CuponCodes.Add(Code, percent);
        }

        public void RegisterProduct(string ID, int price)
        {
            products.Add(ID, price);
        }

        public void RegisterCountDiscount(string ID, int price)
        {
            CountDiscount Dsc = new CountDiscount(ID, price);
            Discounts.Add(Dsc);
        }

        public void RegisterAmountDiscount(string ID, int percent)
        {
            AmountDiscount Dsc = new AmountDiscount(ID, percent);
            Discounts.Add(Dsc);

        }

        public void RegisterComboDiscount(string ID, int price)
        {
            ComboDiscount Dsc = new ComboDiscount(ID, price);
            Discounts.Add(Dsc);
        }
        public int GetPrice(string productsInCart)
        {
            //teljes fizetendő összeg:
            int sum = 0;

            //kosár tartalmának rendezése
            productsInCart = String.Concat(productsInCart.OrderBy(c => c));

            Tuple<string,int> temp;

            foreach (Discount discount in Discounts)
            {
                temp = discount.UseDiscount(productsInCart);
                //a productsInCart-ból kikerültek az aktuális discounttal megvásárolt termékek
                productsInCart = temp.Item1;
                //az aktuális discounttal megvásárolt termékek ára hozzáadódik az összárhoz
                sum += temp.Item2;
            }

            //a maradékot egyenként fizetem
            for (int i=1; i <= productsInCart.Length; i++) {
                try
                {
                    sum += products[Char.ToString(productsInCart[i])];
                }
                //ha számot kapna véletlenül
                catch
                {
                    continue;
                }
            }

            string customerID = Regex.Match(productsInCart, @"v\d*").Value;

            //ha klubtag
            if (IsCustomerClubMember(customerID))
            {
                sum= sum * 90 / 100;
            }

            //ha van kuponja
            if(productsInCart.Contains("k"))
            {
                //ez a kupon kódját kinézi a cart-ból és az annak megfelelő leárazás értékét berakja egy változóba
                int cuponDiscountPercent = CuponCodes[Int32.Parse(Regex.Match(productsInCart, @"k\d*").Value)];

                sum = sum * (100-cuponDiscountPercent) / 100;
            }

            //ha supershoppal akarna fizetni
            if (productsInCart.Contains("p"))
            {
                sum -=Customers[customerID].points;
                Customers[customerID].points = 0;
            }

            return sum;
        }

        //ez a függvény megállapítja a vásárlói azonosító alapján, hogy a vásárló jogosult-e az adott Discount-ra
        public static bool IsDiscountAvailable(string ConsumerID,Discount discount)
        {
            //ezt az üzlet adatbázisa szerint kell majd implementálni
            return true;
        }

        //ez a függvény állapítja meg a vásárlói azonosító alapján, hogy klubtag-e
        public static bool IsCustomerClubMember(string ConsumerID)
        {
            //ezt az üzlet adatbázisa szerint kell majd implementálni
            return true;
        }
    }
}