using System;
using System.Collections.Generic;
using System.Linq;
using InternetServiceProviderManagement.Entities;
using InternetServiceProviderManagement.Repository;

namespace InternetServiceProviderManagement.Services
{
    public class RecordsService
    {
        private readonly FileDatabaseRepository _fileDatabaseRepository;

        private List<Record> _records;


        public RecordsService()
        {
            _fileDatabaseRepository = new FileDatabaseRepository();

            _records = _fileDatabaseRepository.GetAllServiceRecords();
        }

        public void Initialize()
        {
            if (!_fileDatabaseRepository.IsFileExists())
            {
                var sub1 = new Subscriber
                {
                    Id = 1,
                    FisrtName = "John",
                    LastName = "Doe",
                    Address = "Los Angeles"
                };

                var sub2 = new Subscriber
                {
                    Id = 2,
                    FisrtName = "Jane",
                    LastName = "Doe",
                    Address = "New York"
                };

                _records.Add(new Record { Id = 1, Subscriber = sub1, Date = DateTime.Now.AddDays(-7), Tariff = "Tariff One" });
                _records.Add(new Record { Id = 2, Subscriber = sub1, Date = DateTime.Now.AddDays(-1), Tariff = "Tariff One" });
                _records.Add(new Record { Id = 3, Subscriber = sub2, Date = DateTime.Now, Tariff = "Tariff Two" });

                _fileDatabaseRepository.Create(_records);
            }
        }

        public List<Record> GetRecords()
        {
            _records = _fileDatabaseRepository.GetAllServiceRecords();

            return _records;
        }

        public void AddRecord(Record record)
        {
            _records.Add(record);

            _fileDatabaseRepository.Update(_records);
        }

        public void UpdateRecord(Record record)
        {
            var initialRecord = _records.FirstOrDefault(r => r.Id == record.Id);

            initialRecord.Subscriber = record.Subscriber;
            initialRecord.Date = record.Date;
            initialRecord.Tariff = record.Tariff;

            _fileDatabaseRepository.Update(_records);
        }

        public void RemoveRecord(Record record)
        {
            _records.Remove(record);

            _fileDatabaseRepository.Update(_records);
        }


    }
}
