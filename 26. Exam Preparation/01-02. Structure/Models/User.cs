using System.Xml.Linq;
using System;
using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Models;

public class User : IUser
{
    private string firstName;
    private string lastName;
    private string drivingLicenseNumber;

    public User(string firstName, string lastName, string drivingLicenseNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        DrivingLicenseNumber = drivingLicenseNumber;
        Rating = 0;
        IsBlocked = false;
    }

    public string FirstName
    {
        get => firstName;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                //throw new ArgumentException("FirstName cannot be null or whitespace!");
                throw new ArgumentException(ExceptionMessages.FirstNameNull);
            }

            firstName = value;
        }
    }
    public string LastName
    {
        get => lastName;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                //throw new ArgumentException("LastName cannot be null or whitespace!");
                throw new ArgumentException(ExceptionMessages.LastNameNull);
            }

            lastName = value;
        }
    }

    public double Rating { get; private set; }

    public string DrivingLicenseNumber
    {
        get => drivingLicenseNumber;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                //throw new ArgumentException("Driving license number is required!");
                throw new ArgumentException(ExceptionMessages.DrivingLicenseRequired);
            }

            drivingLicenseNumber = value;
        }
    }

    public bool IsBlocked { get; private set; }

    public void IncreaseRating()
    {
        double ratingIncrease = 0.5;

        Rating += ratingIncrease;

        if (Rating > 10.0)
        {
            Rating = 10.0;
        }
    }

    public void DecreaseRating()
    {
        double ratingDecrease = 2.0;

        Rating -= ratingDecrease;

        if (Rating < 0.0)
        {
            Rating = 0.0;
            IsBlocked = true;
        }
    }

    public override string ToString() => $"{FirstName} {LastName} Driving license: {DrivingLicenseNumber} Rating: {Rating}";
}