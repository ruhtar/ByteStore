namespace AuthenticationService.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
