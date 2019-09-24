using System;

namespace SalesScreen.CaseInterview.Models
{
    public class Transaction {
        //TODO: Define the class properties
        public int Id { get; set; }
        public double Amount { get; set; }
        public int CategoryID { get; set; }
        public DateTime Date { get; set; }
    }
}