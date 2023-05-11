using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace BankApp_Staggs
{
    //Name: Colton Staggs
    //IT112
    //Notes: Originally had full words for the commands, reccomended to take it down to just letters
    internal class ATM
    {
        static void Main(string[] args)
        {
            bool active = true;
            bool validLogin = false;
            string[] credentials = {"[USERNAME]","[PASSWORD]"};
            string[] usernames = {"jlennon", "pmccartney", "gharrison", "rstarr"};
            string[] passwords = {"johnny","pauly","georgy","ringoy"};
            getLogin();
            Bank mainBank = new Bank(10000.00);
            consClear();
            do
            {
                Console.WriteLine("Enter letter for corresponding action, or type h or 5 for a list of actions.");
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "b" or "1":
                        dispBal("");
                        break;
                    case "c" or "2":
                        consClear();
                        break;
                    case "d" or "3":
                        double depositAmount;
                        Console.WriteLine("Please enter deposit amount:");
                        if (!double.TryParse(Console.ReadLine(), out depositAmount))
                        {
                            consError("Make sure number is formatted properly!");
                            break;
                        }
                        mainBank.deposit(depositAmount, credentials[0]);
                        dispBal(" New");
                        break;
                    case "e" or "4":
                        active = false;
                        break;
                    case "h" or "5":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("========================");
                        Console.WriteLine("b or 1 - returns your balance");
                        Console.WriteLine("c or 2 - clears the output");
                        Console.WriteLine("d or 3 - deposits an amount");
                        Console.WriteLine("e or 4 - closes the program");
                        Console.WriteLine("h or 5 - lists available commands");
                        Console.WriteLine("l or 6 - Logs out the current user");
                        if (mainBank.Balance > 0 && mainBank.getBalance(credentials[0]) > 0)
                        {
                            Console.WriteLine("w or 7 - withdraws an amount");
                        }
                        Console.WriteLine("========================");
                        break;
                    case "l" or "6":
                        getLogin();
                        consClear();
                        break;
                    case "w" or "7":
                        if (mainBank.Balance <= 0)
                        {
                            consError("The bank does not have the balance to support this withdraw.");
                            break;
                        }
                        if (mainBank.getBalance(credentials[0]) <= 0)
                        {
                            consError("You have no balance to withdraw.");
                            break;
                        }
                        double withdrawAmount;
                        Console.WriteLine("Please write the amound you would like to withdraw. Currently the system is limited to $500");
                        if (double.TryParse(Console.ReadLine(), out withdrawAmount))
                        {
                            mainBank.withDraw(withdrawAmount, credentials[0]);
                            if(withdrawAmount > 500)
                            {
                                Console.WriteLine("The system limited your withdraw to $500.");
                            }
                        }
                        else
                        {
                            consError("Make sure number is formatted properly!");
                            break;
                        }
                        dispBal(" New");
                        break;
                    default:
                        consError("Unknown action: " + input);
                        break;
                }
            } while (active == true);
            bool getLogin()
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("You are not Logged in.");
                    Console.WriteLine("Enter your Username");
                    credentials[0] = Console.ReadLine();
                    Console.WriteLine("Enter your Password");
                    credentials[1] = Console.ReadLine();
                    for (int i = 0; i < usernames.Length; i++)
                    {
                        if (credentials[0] == usernames[i] && credentials[1] == passwords[i])
                        {
                            validLogin = true;
                        }
                    }
                } while (!validLogin);
                return true;
            }
            void dispBal(string type)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Your" + type + " Balance is: " + mainBank.getBalance(credentials[0]).ToString("c"));
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            void consError(string message)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERR: " + message, Console.ForegroundColor);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            void consClear()
            {
                Console.Clear();
                Console.WriteLine("Welcome, " + credentials[0]);
                dispBal(" Current");
            }
        }
    }
}