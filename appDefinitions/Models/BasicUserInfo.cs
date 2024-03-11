namespace appDefinitions.Models
{
    // Basic data when first registering a consumer
    public record BasicNewUserInfo(string FirstName,
        string LastName,
        string Mobile,
        string Email,
        string DateOfBirth,
        string AddressLine1,
        string AddressLine2,
        string City,
        string Postcode,
        string State,
        string Country,
        string UserIp);


}
