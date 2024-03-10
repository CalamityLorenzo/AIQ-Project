namespace DbLayer
{
    internal class DbUserInfo
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string? Postcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool emailVerified { get; set; }
        public bool mobileNumberVerified { get; set; }
    }
}