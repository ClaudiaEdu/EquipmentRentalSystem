using EquipmentRentalSystem.Utilities;
using EquipmentRentalSystem.Enums;
namespace EquipmentRentalSystem.Models;

public abstract class User
{
    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public UserType UserType { get; protected set; }
    protected User(string firstName, string lastName)
    {
        Id = IdGenerator.GenerateId();
        FirstName = firstName;
        LastName = lastName;
    }
}