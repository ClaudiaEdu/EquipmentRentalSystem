using System.Linq;
using EquipmentRentalSystem.Exceptions;
using EquipmentRentalSystem.Models;
using EquipmentRentalSystem.Services;
using EquipmentRentalSystem.Data;

namespace EquipmentRentalSystem;

public class Program
{
    public static void Main(string[] args)
    {
        var equipmentService = new EquipmentService();
        var userService = new UserService();
        var userLimitPolicy = new UserLimitPolicy();
        var penaltyCalculator = new PenaltyCalculator();
        var rentalService = new RentalService(userLimitPolicy, penaltyCalculator);
        var reportService = new ReportService();

        Console.WriteLine("===== EQUIPMENT RENTAL SYSTEM =====");
        Console.WriteLine();

        DataSeeder.Seed(equipmentService, userService);

        var allEquipment = equipmentService.GetAllEquipment();
        var allUsers = userService.GetAllUsers();

        var student1 = (Student)allUsers[0];
        var student2 = (Student)allUsers[1];
        var employee1 = (Employee)allUsers[2];

        var laptop1 = allEquipment[0];
        var projector1 = allEquipment[2];
        var camera1 = allEquipment[4];
        var camera2 = allEquipment[5];

        Console.WriteLine("Added equipment:");
        DisplayEquipment(allEquipment);
        Console.WriteLine();

        Console.WriteLine("Added users:");
        DisplayUsers(allUsers);
        Console.WriteLine();

        Console.WriteLine("Available equipment:");
        DisplayEquipment(equipmentService.GetAvailableEquipment());
        Console.WriteLine();

        Console.WriteLine("Correct rental:");
        rentalService.RentEquipment(student1, laptop1, 7);
        Console.WriteLine($"{student1.FirstName} {student1.LastName} rented {laptop1.Name}");
        Console.WriteLine();

        Console.WriteLine("Attempt to rent unavailable equipment:");
        try
        {
            rentalService.RentEquipment(student2, laptop1, 5);
        }
        catch (EquipmentUnavailableException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.WriteLine();

        Console.WriteLine("Attempt to exceed student rental limit:");
        rentalService.RentEquipment(student1, projector1, 5);
        try
        {
            rentalService.RentEquipment(student1, camera1, 3);
        }
        catch (RentalLimitExceededException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.WriteLine();

        Console.WriteLine("Active rentals of first student:");
        DisplayRentals(rentalService.GetActiveRentalsByUser(student1.Id));
        Console.WriteLine();

        Console.WriteLine("Returning one rental on time:");
        var firstRental = rentalService.GetAllRentals().First(r => r.Equipment.Id == laptop1.Id);
        var penalty1 = rentalService.ReturnEquipment(firstRental.Id);
        Console.WriteLine($"Returned: {firstRental.Equipment.Name}, penalty: {penalty1} PLN");
        Console.WriteLine();

        Console.WriteLine("Creating overdue rental for demo:");
        var oldBorrowDate = DateTime.Now.AddDays(-5);
        rentalService.RentEquipment(employee1, camera2, 2, oldBorrowDate);
        var overdueRental = rentalService.GetAllRentals().First(r => r.Equipment.Id == camera2.Id && !r.IsReturned);

        Console.WriteLine("Overdue rentals:");
        DisplayRentals(rentalService.GetOverdueRentals());
        Console.WriteLine();

        Console.WriteLine("Returning overdue rental:");
        var penalty2 = rentalService.ReturnEquipment(overdueRental.Id);
        Console.WriteLine($"Returned: {overdueRental.Equipment.Name}, penalty: {penalty2} PLN");
        Console.WriteLine();

        Console.WriteLine("Final report:");
        reportService.GenerateReport(allEquipment, rentalService.GetAllRentals());
    }

    private static void DisplayEquipment(List<Equipment> equipmentList)
    {
        foreach (var equipment in equipmentList)
        {
            Console.WriteLine($"ID: {equipment.Id} | Name: {equipment.Name} | Status: {equipment.Status}");
        }
    }

    private static void DisplayUsers(List<User> users)
    {
        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id} | {user.FirstName} {user.LastName} | Type: {user.UserType}");
        }
    }

    private static void DisplayRentals(List<Rental> rentals)
    {
        foreach (var rental in rentals)
        {
            Console.WriteLine(
                $"Rental ID: {rental.Id} | User: {rental.User.FirstName} {rental.User.LastName} | Equipment: {rental.Equipment.Name} | Borrowed: {rental.BorrowDate:g} | Due: {rental.DueDate:g} | Returned: {(rental.IsReturned ? rental.ReturnDate?.ToString("g") : "No")}");
        }
    }
}