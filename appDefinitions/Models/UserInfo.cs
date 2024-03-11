namespace appDefinitions.Models
{
    // User data after they have been created.
    public record UserInfo(string id,
    string FirstName,
    string LastName,
    string Mobile,
    string Email,
    string AddressLine1,
    string AddressLine2,
    string City,
    string Postcode,
    string State,
    string Country,
    DateTime DateOfBirth,
    bool emailVerified,
    bool mobileNumberVerified);

}
