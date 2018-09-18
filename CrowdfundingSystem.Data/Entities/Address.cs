namespace CrowdfundingSystem.Data.Entities
{
    //[Owned]
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int? HouseNumber { get; set; }
        public int? FlatNumber { get; set; }
    }
}