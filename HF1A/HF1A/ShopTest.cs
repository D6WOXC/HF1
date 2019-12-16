using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;

namespace HF1A
{
    class ShopTest
    {
        [TestClass]
        public class TaskTests
        {
            private Shop shop;

            public TaskTests()
            {
                shop = new Shop();
            }

            [TestMethod]
            public void TestSimple()
            {
                shop.RegisterProduct("A", 10);
                Assert.AreEqual(shop.GetPrice("A"), 10);
            }

            public void TestCountDiscount()
            {
                shop.RegisterCountDiscount("AAA", 3);
            }
        }
    }
}
