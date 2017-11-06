using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Item
    {
        private string slot;
        private string name;
        private int price;  //in pennies, converted from text file decimal during population of items
        private int quantity;

        public string Slot { get => slot;  }
        public string Name { get => name;  }
        public int Price { get => price;  }
        public int Quantity { get => quantity; set => quantity = value; }
 

        public Item(string slot, string name, int price)
        {
            this.slot = slot;
            this.name = name;
            this.price = price;    
            this.quantity = 5;
        }

        public void Decrement()  
        {
            if (quantity > 0)
            {
                quantity--;
            }
        }
    }

}
