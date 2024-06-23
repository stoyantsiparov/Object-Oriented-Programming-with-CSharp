using System.Collections.Generic;
using System.Linq;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;

namespace EDriveRent.Repositories;

public class UserRepository : IRepository<IUser>
{
    private List<IUser> users;

    public UserRepository()
    {
        users = new List<IUser>();
    }

    public void AddModel(IUser model) => users.Add(model);

    public bool RemoveById(string identifier) => users.Remove(FindById(identifier));

    public IUser FindById(string identifier) => users.FirstOrDefault(u => u.DrivingLicenseNumber == identifier);

    public IReadOnlyCollection<IUser> GetAll() => users.AsReadOnly();
}