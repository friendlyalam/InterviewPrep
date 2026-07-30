using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace InterviewPrep.LLD.OOPS.Polymorphism
{
    #region Product Scenario

    //    Imagine a shopping cart.

    //    Instead of writing:

    //    cart.Add(item);

    //    You want:

    //cart = cart + item;

    //Much more intuitive.

    #endregion
    public class Cart
    {
        public int TotalItems { get; }

        public Cart(int totalItems)
        {
            TotalItems = totalItems;
        }

        public static Cart operator +(Cart cart, int items)
        {
            return new Cart(cart.TotalItems + items);
        }
    }
}
