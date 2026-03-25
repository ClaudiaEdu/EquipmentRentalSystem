using EquipmentRentalSystem.Enums;

namespace EquipmentRentalSystem.Models;

public class Student : User
{
    public Student(string firstName, string lastName)
        : base(firstName, lastName)
    {
        UserType = UserType.Student;
    }
}