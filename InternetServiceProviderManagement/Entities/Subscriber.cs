namespace InternetServiceProviderManagement.Entities
{
    public class Subscriber
    {
        public int Id { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; {FisrtName} {LastName}, {Address}";
        }
    }
}
