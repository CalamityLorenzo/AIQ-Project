﻿namespace appDefinitions.Models
{
    public record BasicNewUserInfo(string FirstName, string LastName, string Mobile, string Email, string DateOfBirth, string AddressLine1, string AddressLine2, string City, string Postcode, string State, string Country, string UserIp);
    

    public record NewUser(string id, string FirstName, string LastName, string Mobile, string Email, string AddressLine1, string AddressLine2, string City, string Postcode, string State, string Country, string DateOfBirth);

    public record UserInfo(string id, string FirstName, string LastName, string Mobile, string Email, string AddressLine1, string AddressLine2, string City, string Postcode, string State, string Country, DateTime DateOfBirth, bool emailVerified, bool mobileNumberVerified);
}
