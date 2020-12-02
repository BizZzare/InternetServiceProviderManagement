using System;

namespace InternetServiceProviderManagement.Entities
{
    public class Record
    {
        public int Id { get; set; }
        public Subscriber Subscriber { get; set; }
        public string Tariff { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; Subscriber: ({Subscriber}); Tariff: {Tariff}, Date: {Date}";
        }
    }
}
