using System;
using Xunit;

namespace ShopTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Cart cart = new Cart();
            cart.addToCart("ABEE");
            Assert.Equal(cart.content,"ABEE");

        }
    }
}
