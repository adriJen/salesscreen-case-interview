using System;
using System.Collections.Generic;

namespace SalesScreen.CaseInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            var bankAccount = new BankAccount(1);
            bool finished = false;

            while (!finished)
            { 
                Console.WriteLine("Welcome to your bank account" +
                    "\nWhat would you like to do today?" +
                    "\nYour options are:" +
                    "\n1: Display your available credit" + 
                    "\n2: Show your total, and average expenditure for the past 30 days" +
                    "\n3: Show budget differentiations over the past three months" +
                    "\n4: Exit");
                string input = Console.ReadLine();

                switch (input)
                {
                    //Task 1 - Print the available funds (balance + credit) for the bank account
                    case "1":
                        var availableFunds = bankAccount.GetAvailableFunds();
                        Console.WriteLine($"\nAvailable funds: {availableFunds}\n");
                        break;

                    //Task 2: Fetch the transactions for the last 30 days. Print the total sum of the transactions, and the average value of the transactions.
                    case "2":
                        var totalAmount = bankAccount.GetTotalTransactions();

                        if (totalAmount == null)
                        {
                            Console.WriteLine("\nNo transactions have been made in the last thirty days\n");
                        }

                        else
                        {
                            Console.WriteLine("\n" + totalAmount + "\n");
                        }
                        break;

                    //Task 3: Compare the monthly spend for the last 3 months against the monthly budget configured for the account.
                    //        Print a list for each month, with the difference between the budget and actual expenses for each category.
                    case "3":
                        bankAccount.GetMonthlyExpenses();
                        break;

                    case "4":
                        finished = true;
                        break;
                    default:
                        Console.WriteLine("\nPlease select a valid option, use the keys 1, 2 or 3 on your keyboard\n");
                        break;
                }
            }
        }
    }
}
