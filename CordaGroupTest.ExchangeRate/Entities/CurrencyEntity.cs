using System;
using System.ComponentModel.DataAnnotations;

namespace CordaGroupTest.ExchangeRate.Entities
{
    public class CurrencyEntity
    {
        [Key]
        public int Id { get; set; }
        public string CurrencyId { get; set; }
        public int NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime TimeStampUtc { get; set; }
    }
}
