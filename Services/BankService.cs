using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using SalesScreen.CaseInterview.Models;

namespace SalesScreen.CaseInterview.Services
{
    public static class BankService
    {    
        private const string BaseUrl = "https://salesscreen-bank-api.azurewebsites.net/api/";

        //Deserializes a Json payload into a an AccountInfo object using the AccountInfo model.
        public static AccountInfo GetAccountInfo(int accountId) {
            var endpoint = $"account/{accountId}";
            var jsonString = GetJson(endpoint);

            return JsonConvert.DeserializeObject<AccountInfo>(jsonString);
        }

        //Deserializes a Json payload into a list of Transactions using the Transaction model.
        public static List<Transaction> GetTransactions(int accountId) {
            var endpoint = $"transaction/{accountId}";
            var jsonString = GetJson(endpoint);

            return JsonConvert.DeserializeObject<List<Transaction>>(jsonString);
        }

        //Deserializes a Json payload into a list of Category objects using the Category model.
        public static List<Category> GetCategories() {
            var jsonString = GetJson("category/");

            return JsonConvert.DeserializeObject<List<Category>>(jsonString);
        }

        //Deserializes a Json payload into a list of CategoryMonthlyBudget objects using the CategoryMonthlyBudget model.
        public static List<CategoryMonthlyBudget> GetCategoryMonthlyBudgets(int accountId) {
            var endpoint = $"budget/{accountId}";
            var jsonString = GetJson(endpoint);

            return JsonConvert.DeserializeObject<List<CategoryMonthlyBudget>>(jsonString);
        }

        //Uses CRUD language (GET) to fetch a Json payload from one of the API endpoints. 
        private static string GetJson(string endpoint) {
            using (var client = new HttpClient()) {
                var url = $"{BaseUrl}/{endpoint}";
                var response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}