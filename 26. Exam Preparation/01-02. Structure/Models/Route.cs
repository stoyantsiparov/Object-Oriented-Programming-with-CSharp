using System;
using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;

namespace EDriveRent.Models;

public class Route : IRoute
{
    private string startPoint;
    private string endPoint;
    private double lenght;

    public Route(string startPoint, string endPoint, double lenght, int routeId)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
        Length = lenght;
        RouteId = routeId;
        IsLocked = false;
    }

    public string StartPoint
    {
        get => startPoint;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                //throw new ArgumentException("StartPoint cannot be null or whitespace!");
                throw new ArgumentException(ExceptionMessages.StartPointNull);
            }

            startPoint = value;
        }
    }

    public string EndPoint
    {
        get => endPoint;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                //throw new ArgumentException("Endpoint cannot be null or whitespace!");
                throw new ArgumentException(ExceptionMessages.EndPointNull);
            }

            endPoint = value;
        }
    }

    public double Length
    {
        get => lenght;
        private set
        {
            if (value < 1)
            {
                //throw new ArgumentException("Length cannot be less than 1 kilometer.".);
                throw new ArgumentException(ExceptionMessages.RouteLengthLessThanOne);
            }

            lenght = value;
        }
    }
    public int RouteId { get; private set; }
    public bool IsLocked { get; private set; }
    public void LockRoute() => IsLocked = true;
}