using System;
using System.Threading.Tasks;
using CordaGroupTest.ExchangeRate.Data;
using CordaGroupTest.ExchangeRate.Entities;
using CordaGroupTest.ExchangeRate.Models;
using Flurl.Http;
using Quartz;

namespace CordaGroupTest.ExchangeRate.Jobs
{
    public class CurrencyJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Идёт обновление базы.");

            var requestResult = await "https://www.cbr-xml-daily.ru/daily_json.js"
                .GetJsonAsync<CurrencyRequest>();

            var connectionString = context.JobDetail.JobDataMap.GetString("connectionString");

            using (var dataContext = new DataContext(connectionString))
            {
                foreach (var currency in requestResult.Valute)
                {
                    await dataContext.CurrencyEntities.AddAsync(new CurrencyEntity
                    {
                        CurrencyId = currency.Value.CurrencyId,
                        CharCode = currency.Value.CharCode,
                        NumCode = currency.Value.NumCode,
                        Name = currency.Value.Name,
                        Value = currency.Value.Value,
                        Nominal = currency.Value.Nominal,
                        TimeStampUtc = requestResult.Timestamp
                    });
                }

                await dataContext.SaveChangesAsync();
            }

            Console.WriteLine("Обновление базы закончено.");
            Console.WriteLine($"Сейчас {DateTime.Now}. Следующее обновление будет {DateTime.Now.AddDays(24)}.");
        }
    }
}
