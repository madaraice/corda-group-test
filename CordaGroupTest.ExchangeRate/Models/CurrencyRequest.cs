using System;
using System.Collections.Generic;

namespace CordaGroupTest.ExchangeRate.Models
{
    public class CurrencyRequest
    {
        public DateTime Date { get; set; }
        public DateTime PreviousDate { get; set; }
        public string PreviousURL { get; set; }
        public DateTime Timestamp { get; set; }
        public Dictionary<string, Currency> Valute { get; set; }
    }
}
