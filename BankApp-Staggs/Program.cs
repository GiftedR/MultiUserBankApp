using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace BankApp_Staggs
{
    //Name: Colton Staggs
    //IT112
    //Notes: Put usernames and passwords into single arrays for ease of personal readability and adding new credentials in the future.
    internal class Program
    {
        static void Main(string[] args)
        {
            bool active = true;
            bool validLogin = false;
            string[] userInput = {"[USERNAME]","[PASSWORD]"};
            string[] savedLogins = {"jlennon","johnny","pmccartney","pauly","gharrison","georgy","rstarr","ringoy"};
            Bank mainBank = new Bank();
            getLogin();
            if(active) { consClear(); }
            while (active == true)
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
                        mainBank.deposit(depositAmount, userInput[0]);
                        dispBal(" New");
                        break;
                    case "e" or "4":
                        active = false;
                        break;
                    case "h" or "5":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("========================");
                        Console.WriteLine("B or 1 - returns your balance");
                        Console.WriteLine("C or 2 - clears the output");
                        Console.WriteLine("D or 3 - deposits an amount");
                        Console.WriteLine("E or 4 - closes the program");
                        Console.WriteLine("H or 5 - lists available commands");
                        Console.WriteLine("L or 6 - Logs out the current user");
                        if (mainBank.BankBalance > 0 && mainBank.getBalance(userInput[0]) > 0)
                        {
                            Console.WriteLine("W or 7 - withdraws an amount");
                        }
                        Console.WriteLine("========================");
                        break;
                    case "l" or "6":
                        getLogin();
                        consClear();
                        break;
                    case "w" or "7":
                        if (mainBank.BankBalance <= 0)
                        {
                            consError("The bank does not have the balance to support this withdraw.");
                            break;
                        }
                        if (mainBank.getBalance(userInput[0]) <= 0)
                        {
                            consError("You have no balance to withdraw.");
                            break;
                        }
                        double withdrawAmount;
                        Console.WriteLine("Please write the amound you would like to withdraw. Currently the system is limited to: " + mainBank.withdrawLimit.ToString("c"));
                        if (double.TryParse(Console.ReadLine(), out withdrawAmount))
                        {
                            mainBank.withDraw(withdrawAmount, userInput[0]);
                            if(withdrawAmount > mainBank.withdrawLimit)
                            {
                                Console.WriteLine("The system limited your withdraw to: " + mainBank.withdrawLimit.ToString("c"));
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
            };
            bool getLogin()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The banks current balance is: " + mainBank.BankBalance.ToString("c"));
                Console.ForegroundColor = ConsoleColor.Gray;
                do
                {
                    Console.WriteLine("Hello User, \nPlease enter 1 or L to login. \nPlease enter 2 or E to exit.");
                    switch (Console.ReadLine().ToLower())
                    {
                        case "1" or "l":
                            Console.WriteLine("Enter your Username");
                            userInput[0] = Console.ReadLine();
                            Console.WriteLine("Enter your Password");
                            userInput[1] = Console.ReadLine();
                            for (int i = 0; i < savedLogins.Length; i++)
                            {
                                if (userInput[0] == savedLogins[i] && userInput[1] == savedLogins[i + 1])
                                {
                                    validLogin = true;
                                }
                            }
                            if(validLogin == false)
                            {
                                consError("Invalid Username or Password!");
                            }
                            break;
                            case "2" or "e":
                                active = false;
                                validLogin = true;
                                break;
                    }
                } while (!validLogin);
                return true;
            }
            void dispBal(string type)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Your" + type + " Balance is: " + mainBank.getBalance(userInput[0]).ToString("c"));
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
                Console.WriteLine("Welcome, " + userInput[0]);
                dispBal(" Current");
            }
        }
    }
}