using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Staggs
{
    internal class Bank
    {
        private double BankBal = 10000;
        private string[] userBal = { "jlennon", "1250", "pmccartney", "2500", "gharrison", "3000", "rstarr", "1000" };
        public Bank() { }
        private int balIndex(string username)
        {
            for (int i = 0; i < userBal.Length; i++)
            {
                if (userBal[i] == username)
                {
                    return i + 1;
                }
            }
            return -1;
        }
        public bool withDraw(double amount, string username)
        {
            if (amount > withdrawLimit)
            {
                amount = withdrawLimit;
            }
            BankBal -= amount;
            double parseBal;
            double.TryParse(userBal[balIndex(username)], out parseBal);
            userBal[balIndex(username)] = (parseBal - amount).ToString();
            if (BankBal < 0)
            {
                BankBal = 0;
            }
            if (BankBal <= 0)
            {
                return false;
            }
            return true;
        }
        public bool deposit(double amount, string username)
        {
            BankBal += amount;
            double parseBal;
            double.TryParse(userBal[balIndex(username)], out parseBal);
            if (parseBal < 0)
            {
                parseBal = 0;
            }
            userBal[balIndex(username)] = (parseBal + amount).ToString();
            if (BankBal < 0)
            {
                BankBal = 0;
            }
            return true;
        }
        public double getBalance(string username)
        {
            double parseBalance;
            double.TryParse(userBal[balIndex(username)], out parseBalance);
            if (parseBalance < 0)
            {
                parseBalance = 0;
            }
            return parseBalance;
        }
        public double BankBalance {
            get { return BankBal; }
        }
        public double withdrawLimit {
            get { return 500; }
        }
    }
}