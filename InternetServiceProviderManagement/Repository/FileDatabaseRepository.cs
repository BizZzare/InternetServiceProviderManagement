using System.Collections.Generic;
using System.IO;
using InternetServiceProviderManagement.Entities;
using Newtonsoft.Json;

namespace InternetServiceProviderManagement.Repository
{
    public class FileDatabaseRepository
    {
        private const string FILE_NAME = "ISP.dat";

        public bool IsFileExists()
        {
            return File.Exists(FILE_NAME);
        }

        public void Create(List<Record> records)
        {
            string recordsJson = JsonConvert.SerializeObject(records);

            using FileStream fs = new FileStream(FILE_NAME, FileMode.OpenOrCreate);
            using BinaryWriter w = new BinaryWriter(fs);

            w.Write(recordsJson);
        }

        public void Update(List<Record> records)
        {
            string recordsJson = JsonConvert.SerializeObject(records);

            using FileStream fs = new FileStream(FILE_NAME, FileMode.Truncate);
            using BinaryWriter w = new BinaryWriter(fs);

            w.Write(recordsJson);
        }

        public List<Record> GetAllServiceRecords()
        {
            if (File.Exists(FILE_NAME))
            {
                using FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read);
                using BinaryReader r = new BinaryReader(fs);

                string recordsJson = r.ReadString();

                return JsonConvert.DeserializeObject<List<Record>>(recordsJson);
            }

            return new List<Record>();
        }

    }
}
