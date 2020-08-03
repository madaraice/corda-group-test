namespace CordaGroupTest.ExchangeRate.Models
{
    public class Currency
    {
        public string CurrencyId { get; set; }
        public int NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Previous { get; set; }
    }
}