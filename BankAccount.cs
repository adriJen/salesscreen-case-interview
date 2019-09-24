using System;
using System.Collections.Generic;
using SalesScreen.CaseInterview.Models;
using SalesScreen.CaseInterview.Services;
using System.Linq;

namespace SalesScreen.CaseInterview
{
    public class BankAccount
    {
        private readonly int AccountId;
        private readonly AccountInfo _accountInfo;
        private readonly List<Transaction> _transaction;
        private readonly List<Category> _category;
        private readonly List<CategoryMonthlyBudget> _categoryMonthlyBudget;
                    
        public BankAccount(int accountId)
        {
            AccountId = accountId;
            _accountInfo = BankService.GetAccountInfo(AccountId);
            _transaction = BankService.GetTransactions(AccountId);
            _category = BankService.GetCategories();
            _categoryMonthlyBudget = BankService.GetCategoryMonthlyBudgets(AccountId);
        }

        public double GetAvailableFunds()
        {
            return _accountInfo.Balance + _accountInfo.Credit;
        }
        
        //The variable "lastTransaction" is set to the date of the final transaction made. In a real scenario this would be set to "DateTime.Now".
        public string GetTotalTransactions()
        {

            DateTime lastTransaction = new DateTime(2019, 05, 30);
            int result = DateTime.Compare(lastTransaction, lastTransaction.AddDays(-30));

            IEnumerable<Transaction> query = _transaction.Where(i => i.Date >= lastTransaction.AddDays(-30));

            foreach (var transaction in query)
            {
                double total = query.Sum(item => item.Amount);
                double average = Math.Round(query.Average(item => item.Amount), 2);
                string output = $"The sum of transactions in the last 30 days is in total: {total}" + $"\nThe average amount is {average}";
                return output;
            }

            return null;

        }
        
        //This method uses three Enums to seperate transactions by month. These Enums are then used by the private method "Calculate Expenses" to divide sums by category. A list is returned which is then seperated in an output string. 
        public void GetMonthlyExpenses()
        {

            DateTime lastTransaction = new DateTime(2019, 05, 30);
            int lastMonth = lastTransaction.AddMonths(-1).Month;
            int twoMonths = lastTransaction.AddMonths(-2).Month;
            int threeMonths = lastTransaction.AddMonths(-3).Month;

            List<string> categories = new List<string>();

            IEnumerable<Transaction> month1Transactions = _transaction.Where(i => i.Date.Month == lastMonth);
            IEnumerable<Transaction> month2Transactions = _transaction.Where(i => i.Date.Month == twoMonths);
            IEnumerable<Transaction> month3Transactions = _transaction.Where(i => i.Date.Month == threeMonths);

            foreach (var category in _category)
            {
                categories.Add(category.Name);
            }

            var month1List = CalculateExpense(month1Transactions);
            var month2List = CalculateExpense(month2Transactions);
            var month3List = CalculateExpense(month3Transactions);

            System.Globalization.DateTimeFormatInfo monthConversion = new System.Globalization.DateTimeFormatInfo();

            string strMonth1Name = monthConversion.GetMonthName(lastMonth).ToString();
            string strMonth2Name = monthConversion.GetMonthName(twoMonths).ToString();
            string strMonth3Name = monthConversion.GetMonthName(threeMonths).ToString();

            string output = 
                $"\n                 |{strMonth1Name,10}|" + $"{strMonth2Name,10}|" + $" {strMonth3Name,10}|" +   
                $"\n---------------------------------------------------" +
                $"\n||{categories[0],-15}|{month1List[0],10}|" + $"{ month2List[0],10}| " + $"{ month3List[0],10}| " +
                $"\n||{categories[1],-15}|{month1List[1],10}|" + $"{ month2List[1],10}| " + $"{ month3List[1],10}| " +
                $"\n||{categories[2],-15}|{month1List[2],10}|" + $"{ month2List[2],10}| " + $"{ month3List[2],10}| " +
                $"\n||{categories[3],-15}|{month1List[3],10}|" + $"{ month2List[3],10}| " + $"{ month3List[3],10}| " +
                $"\n||{categories[4],-15}|{month1List[4],10}|" + $"{ month2List[4],10}| " + $"{ month3List[4],10}| " +
                $"\n||{categories[5],-15}|{month1List[5],10}|" + $"{ month2List[5],10}| " + $"{ month3List[5],10}| " +
                $"\n||{categories[6],-15}|{month1List[6],10}|" + $"{ month2List[6],10}| " + $"{ month3List[6],10}| " +
                $"\n||{categories[7],-15}|{month1List[7],10}|" + $"{ month2List[7],10}| " + $"{ month3List[7],10}| " +
                "\n----------------------------------------------------\n";

            Console.WriteLine(output);
        }
        
        //Private method which takes a parameter of IEnumerable of transactions and returns a list with the appropriate calculations and divisions by category. 
        private List<Double> CalculateExpense(IEnumerable<Transaction> expenses)
        {

            List<double> budgets = new List<double>();

            foreach (var category in _categoryMonthlyBudget)
            {
                budgets.Add(category.Amount);
            }

            List<double> cat1 = new List<double>();
            List<double> cat2 = new List<double>();
            List<double> cat3 = new List<double>();
            List<double> cat4 = new List<double>();
            List<double> cat5 = new List<double>();
            List<double> cat6 = new List<double>();
            List<double> cat7 = new List<double>();
            List<double> cat8 = new List<double>();

            foreach (var transaction in expenses)
            {
                if (transaction.CategoryID == 1)
                {
                    cat1.Add(transaction.Amount);
                }

                if (transaction.CategoryID == 2)
                {
                    cat2.Add(transaction.Amount);
                }

                if (transaction.CategoryID == 3)
                {
                    cat3.Add(transaction.Amount);
                }

                if (transaction.CategoryID == 4)
                {
                    cat4.Add(transaction.Amount);
                }

                if (transaction.CategoryID == 5)
                {
                    cat5.Add(transaction.Amount);
                }

                if (transaction.CategoryID == 6)
                {
                    cat6.Add(transaction.Amount);
                }

                if (transaction.CategoryID == 7)
                {
                    cat7.Add(transaction.Amount);
                }

                if (transaction.CategoryID == 8)
                {
                    cat8.Add(transaction.Amount);
                }

                
            }

            List<double> returnList = new List<double>
            {
                budgets[0] - cat1.Sum(item => item),
                budgets[1] - cat2.Sum(item => item),
                budgets[2] - cat3.Sum(item => item),
                budgets[3] - cat4.Sum(item => item),
                budgets[4] - cat5.Sum(item => item),
                budgets[5] - cat6.Sum(item => item),
                budgets[6] - cat7.Sum(item => item),
                budgets[7] - cat8.Sum(item => item),
            };

            return returnList;
        }
            
                   
            #region API

            //public void FetchAccountInfo() {
            //     _accountInfo = BankService.GetAccountInfo(AccountId);
            // }

            // public void FetchTransactionInfo() {
            //     _transaction = BankService.GetTransactions(AccountId);
            // }

            #endregion
        
    }
}