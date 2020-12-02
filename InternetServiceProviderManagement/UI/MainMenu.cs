using System;
using System.Collections.Generic;
using System.Linq;
using InternetServiceProviderManagement.Constants;
using InternetServiceProviderManagement.Entities;
using InternetServiceProviderManagement.Services;

namespace InternetServiceProviderManagement.UI
{
    public class MainMenu
    {
        public static void Start()
        {
            var options = new List<string>
            {
                MainMenuUiOptions.ShowAll,
                MainMenuUiOptions.Add,
                MainMenuUiOptions.Update,
                MainMenuUiOptions.Delete,
                MainMenuUiOptions.Quit
            };

            var selector = new ConsoleSelector(options, 0, 2);

            var service = new RecordsService();
            service.Initialize();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("ISP Service Records Management");

                var selectedIndex = selector.GetChosenOptionIndex();

                switch (options[selectedIndex])
                {
                    case MainMenuUiOptions.ShowAll:
                        {
                            var records = service.GetRecords();

                            PrintRecords(records);

                            Console.ReadKey();
                        }
                        break;
                    case MainMenuUiOptions.Add:
                        {
                            var records = service.GetRecords();

                            service.AddRecord(FillRecordData(records));

                            Console.WriteLine("The record was added sucessfully!");
                            Console.ReadKey();
                        }
                        break;
                    case MainMenuUiOptions.Update:
                        {
                            var records = service.GetRecords();

                            service.UpdateRecord(UpdateRecordData(records));

                            Console.Clear();
                            Console.WriteLine("The record was updated sucessfully!");
                            Console.ReadKey();
                        }
                        break;
                    case MainMenuUiOptions.Delete:
                        {
                            var records = service.GetRecords();

                            service.RemoveRecord(records[SelectRecordIndex(records)]);

                            Console.Clear();
                            Console.WriteLine("The record was deleted sucessfully!");
                            Console.ReadKey();
                        }
                        break;
                    case MainMenuUiOptions.Quit:
                        {
                            return;
                        }
                    default:
                        break;
                }
            }
        }

        private static void PrintRecords(List<Record> records)
        {
            Console.Clear();

            foreach (var record in records)
            {
                Console.WriteLine(record);
            }
        }

        private static Record FillRecordData(List<Record> records)
        {
            Console.Clear();

            var id = records.Max(r => r.Id) + 1;

            var finalRecord = new Record() { Id = id };

            finalRecord.Subscriber = GetSubscriber(records);
            Console.Clear();


            Console.WriteLine("Enter tariff: ");
            finalRecord.Tariff = Console.ReadLine();

            finalRecord.Date = DateTime.Now;

            Console.Clear();
            return finalRecord;
        }

        private static Subscriber GetSubscriber(List<Record> records)
        {
            Console.WriteLine("Subscriber: ");

            var subscribers = records
                .Select(r => r.Subscriber)
                .ToList();

            var uniqueSubIds = subscribers.Select(s => s.Id).Distinct();

            var uniqueSubs = new List<Subscriber>();
            foreach (var subId in uniqueSubIds)
            {
                uniqueSubs.Add(subscribers.FirstOrDefault(s => s.Id == subId));
            }

            var menuOptions = new List<string>
            {
                ChooseSubscriberUiOptions.SelectExisting,
                ChooseSubscriberUiOptions.CreateNew
            };

            var selector = new ConsoleSelector(menuOptions, 0, 2);

            switch (menuOptions[selector.GetChosenOptionIndex()])
            {
                case ChooseSubscriberUiOptions.SelectExisting:
                    {
                        return uniqueSubs[SelectSubscriberIndex(uniqueSubs)];
                    }
                case ChooseSubscriberUiOptions.CreateNew:
                    {
                        var sub = FillSubscriberData();
                        sub.Id = uniqueSubs.Max(s => s.Id) + 1;

                        return sub;
                    }
                default:
                    break;
            }


            Console.Clear();

            return new Subscriber();
        }

        private static int SelectSubscriberIndex(List<Subscriber> subs)
        {
            Console.Clear();

            var subOptions = subs.Select(s => s.ToString()).ToList();

            var subSelector = new ConsoleSelector(subOptions, 0, 0);

            return subSelector.GetChosenOptionIndex();
        }

        private static Subscriber FillSubscriberData()
        {
            Console.Clear();

            var resultSub = new Subscriber();


            Console.WriteLine("Enter subscriber's First name: ");
            resultSub.FisrtName = Console.ReadLine();

            Console.WriteLine("Enter subscriber's Last name: ");
            resultSub.LastName = Console.ReadLine();

            Console.WriteLine("Enter subscriber's Address: ");
            resultSub.Address = Console.ReadLine();

            Console.Clear();


            return resultSub;
        }

        private static Record UpdateRecordData(List<Record> records)
        {
            var rec = records[SelectRecordIndex(records)];
            Console.Clear();

            rec.Subscriber = GetSubscriber(records);
            Console.Clear();


            Console.WriteLine("Enter tariff: ");
            rec.Tariff = Console.ReadLine();

            rec.Date = DateTime.Now;

            return rec;

        }
        private static int SelectRecordIndex(List<Record> recs)
        {
            Console.Clear();

            var recOptions = recs.Select(r => r.ToString()).ToList();

            var subSelector = new ConsoleSelector(recOptions, 0, 0);

            return subSelector.GetChosenOptionIndex();
        }

    }
}
