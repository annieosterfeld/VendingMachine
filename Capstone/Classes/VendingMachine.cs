using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        private List<Item> inventoryList = new List<Item>();    
        private int balance;                                    // balance in pennies
        
        public VendingMachine()
        {
            
            using (StreamReader sr = new StreamReader("vendingmachine.csv"))  // restocks vending machine on instatiation from csv file
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] words = line.Split('|');
                    Item myItem = new Item(words[0], words[1], (int)(100*Decimal.Parse(words[2]))   );  // converting a stringto a decimal, converting that to pennies and casting into an integer
                    inventoryList.Add(myItem);
                }
                
            }
            this.balance = 0;
        }

        public List<Item> InventoryList { get => inventoryList; }
        public int Balance { get => balance; }

        public void ReduceInventory(string itemSlot)
        {

            inventoryList[SlotIndex(itemSlot)].Decrement();

        }

        public List<int> MakeChange()
        {
            List<int> returnChange = new List<int>();       //creating a list for interface to output as change and decrementing balance to zero
            
            returnChange.Add(balance / 25);        //removing quarters
            balance %= 25;
            returnChange.Add(balance / 10);         //removing dimes
            balance %= 10;
            returnChange.Add(balance / 5);
            balance %= 5;
            returnChange.Add(balance);
            balance = 0;
            return returnChange;
        }
        public void DecreaseBalance(int costOfItem)
        {
            balance -= costOfItem;
        }
        public void IncreaseBalance(int moneyFed)
        {
            balance += moneyFed;
        }

        public int SlotIndex(string itemSlot)
        {
            itemSlot = itemSlot.ToUpper();  //handling case sensitivity
            if (itemSlot == "A1")
            { return 0; }
            if (itemSlot == "A2")
            { return 1; }
            if (itemSlot == "A3")
            { return 2; }
            if (itemSlot == "A4")
            { return 3; }
            if (itemSlot == "B1")
            { return 4; }
            if (itemSlot == "B2")
            { return 5; }
            if (itemSlot == "B3")
            { return 6; }
            if (itemSlot == "B4")
            { return 7; }
            if (itemSlot == "C1")
            { return 8; }
            if (itemSlot == "C2")
            { return 9; }
            if (itemSlot == "C3")
            { return 10; }
            if (itemSlot == "C4")
            { return 11; }
            if (itemSlot == "D1")
            { return 12; }
            if (itemSlot == "D2")
            { return 13; }
            if (itemSlot == "D3")
            { return 14; }
            if(itemSlot == "D4")
            { return 15; }
            else
            { return 16; }

        }
    }




}

