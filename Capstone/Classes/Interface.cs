using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;



namespace Capstone.Classes
{
    public class Interface
    {

        VendingMachine myVendingMachine = new VendingMachine();
        public Interface(VendingMachine myVendingMachine)
        {
            ConstructLog();
            WriteToLog("Restocked All Items", 0, 0);                // log entry for restocking
        }
        public void Runner()
        {
            string input = "0";

            while (input != "3")              //Outer First Menu
            {

                DisplayFirstMenu();
                input = Console.ReadLine();   // waits for menu selection
                if (input == "1")
                {
                    DisplayVendingItems();
                    Console.WriteLine("Press Enter to Continue");
                    Console.ReadLine();
                }
                if (input == "2")
                {
                    string purchaseMenuInput = "";

                    while (purchaseMenuInput != "3")   //  INNER MENU
                    {
                        Console.Clear();
                        DisplaySecondMenu();
                        purchaseMenuInput = Console.ReadLine();  //waits for menu selection

                        if (purchaseMenuInput == "1")
                        {
                            AddMoney();                            // ADDING MONEY
                            
                        }
                        if (purchaseMenuInput == "2")
                        {
                            DisplayVendingItems();
                            SelectProduct();
                        }
                        if (purchaseMenuInput == "3")
                        {
                            CashOut();
                            
                        }
                    }
                }
                  // program ends when 3 is selected, no need to catch
            }
        }

        public void CashOut()
        {
            WriteToLog("GIVE CHANGE:", myVendingMachine.Balance, -myVendingMachine.Balance);
            Console.WriteLine($"Your ending balance is {(ToDollars(myVendingMachine.Balance)).ToString("C2")} ");
            List<int> change = myVendingMachine.MakeChange();  //returns array with change values and sets balance of myVendingMachine to 0
            Console.WriteLine($"Your change is {change[0]} quarters, {change[1]} dimes, {change[2]} nickles, and {change[3]} pennies.  Press Enter to Continue");
            Console.ReadLine();
        }
        public void SelectProduct()  //handles both input and output for product selection, also decrements balance and inventory and writes log
        {
            Console.Write("Please select which slot number you would like to purchase:      ");
            string input = Console.ReadLine();
            if (myVendingMachine.SlotIndex(input) == 16)  //handles improper inputs
            {
                Console.WriteLine("That is not a valid selection, Press Enter and try again:");
                Console.ReadLine();
            }

            if (ReturnQuantity(input) < 1)  //checks to make sure empty slots never get consumed
            {
                Console.WriteLine("That item is all sold out, sorry.  Press Enter to try again:");
                Console.ReadLine();
            }
            else
            {
                if (ReturnPrice(input) < myVendingMachine.Balance)
                {// dispense
                    if (input.ToLower().StartsWith("a"))
                    {
                        Console.WriteLine("Crunch Crunch, Yum!");
                        Console.ReadLine();
                    }
                    if (input.ToLower().StartsWith("b"))
                    {
                        Console.WriteLine("Munch Munch, Yum!");
                        Console.ReadLine();
                    }
                    if (input.ToLower().StartsWith("c"))
                    {
                        Console.WriteLine("Glug Glug, Yum!");
                        Console.ReadLine();
                    }
                    if (input.ToLower().StartsWith("d"))
                    {
                        Console.WriteLine("Chew Chew, Yum!");
                        Console.ReadLine();
                    }
                    //decrement inventory
                    myVendingMachine.ReduceInventory(input);
                                      
                    // write to log                                 
                    WriteToLog($"{ReturnName(input)} {input}", myVendingMachine.Balance, -ReturnPrice(input));
                    // and lower balance
                    myVendingMachine.DecreaseBalance(ReturnPrice(input));
                }
                else  //when price of item is > balance
                {
                    Console.WriteLine("Sorry, you need more money.  Press Enter and try again.");
                 Console.ReadLine();
                }
            }

        }

        //Cleans up constructor for Interface
        public void ConstructLog()
        {
            this.myVendingMachine = myVendingMachine;
            using (StreamWriter sw = new StreamWriter("log.txt", false))  //adding headers to the log.txt file
            {
                sw.WriteLine("Date".PadRight(25) + "Transaction Type".PadRight(25) + "Start Bal".PadRight(10) + "End Bal".PadRight(10));
            }
        }
        // following 4 methods used to make code more readable when making calls
        public string ReturnName(string input)
        {
            return myVendingMachine.InventoryList[myVendingMachine.SlotIndex(input)].Name;
        }
        public string ReturnSlot(string input)  // maintains upper case consistency for output
        {
            return myVendingMachine.InventoryList[myVendingMachine.SlotIndex(input)].Slot;
        }
        public int ReturnPrice(string input)
        {
            return myVendingMachine.InventoryList[myVendingMachine.SlotIndex(input)].Price;
        }
        public int ReturnQuantity(string input)
        {
            return myVendingMachine.InventoryList[myVendingMachine.SlotIndex(input)].Quantity;
        }


        //Adds money and handles making sure bills are valid
        public void AddMoney()
        {
            Console.WriteLine();
            Console.Write("Please insert a bill amount:  ");
            int addedBillInPennies = 100 * Int32.Parse(Console.ReadLine());
            if (addedBillInPennies == 100 || addedBillInPennies == 200 || addedBillInPennies == 500 || addedBillInPennies == 1000 || addedBillInPennies == 2000)
            {
                WriteToLog("FEED MONEY:", myVendingMachine.Balance, addedBillInPennies);
                myVendingMachine.IncreaseBalance(addedBillInPennies);
                
            }
            else
            {
                Console.WriteLine("That is not a valid bill amount.  Press Enter to try again.");
                Console.ReadLine();
            }

        }
        //Generic display of inventory
        public void DisplayVendingItems()
        {
            Console.Clear();
            Console.WriteLine("Slot Number".PadRight(20) + "Item Name".PadRight(20) + "Quantity Left".PadRight(20) + " Cost".PadRight(20));
            for (int i = 0; i < myVendingMachine.InventoryList.Count; i++)
            {
                Console.WriteLine(myVendingMachine.InventoryList[i].Slot.PadRight(20) + myVendingMachine.InventoryList[i].Name.PadRight(20) + myVendingMachine.InventoryList[i].Quantity.ToString().PadRight(20) + ToDollars(myVendingMachine.InventoryList[i].Price).ToString("C2").PadRight(20));
            }
            Console.WriteLine();

        }

        public void DisplayFirstMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose from:");
            Console.WriteLine("");
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) End Transaction");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        public void DisplaySecondMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose from:");
            Console.WriteLine("");
            Console.WriteLine("(1) Feed Money");
            Console.WriteLine("(2) Select Product");
            Console.WriteLine("(3) Finish Transaction");
            Console.WriteLine($"Current Money Provided:  {((decimal)(myVendingMachine.Balance)/100).ToString("C2")} ");
            Console.WriteLine("");
        }
        //Handles all writes to log
        public void WriteToLog(string transactionType, int balance, int balanceChange)
        {
            using (StreamWriter sw = new StreamWriter("log.txt", true))
            {
                sw.WriteLine(DateTime.UtcNow.ToString().PadRight(25) + transactionType.PadRight(25) + ToDollars(myVendingMachine.Balance).ToString("C2").PadRight(10) + ToDollars((myVendingMachine.Balance + balanceChange)).ToString("C2").PadRight(10));
            }

        }
        //Handles conversion to decimal format for output prices
        public decimal ToDollars(int pennies)
        {
            return ((decimal)pennies / 100);

        }
    }

}