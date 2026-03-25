using EquipmentRentalSystem.Enums;

namespace EquipmentRentalSystem.Models;

public class Employee : User
{
    public Employee(string firstName, string lastName)
        : base(firstName, lastName)
    {
        UserType = UserType.Employee;
    }
}