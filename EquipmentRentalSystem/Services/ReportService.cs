using System.Linq;
using EquipmentRentalSystem.Models;

namespace EquipmentRentalSystem.Services;

public class ReportService
{
    public void GenerateReport(List<Equipment> equipmentList, List<Rental> rentals)
    {
        var totalEquipment = equipmentList.Count;
        var availableEquipment = equipmentList.Count(e => e.Status == Enums.EquipmentStatus.Available);
        var unavailableEquipment = totalEquipment - availableEquipment;

        var activeRentals = rentals.Count(r => !r.IsReturned);
        var overdueRentals = rentals.Count(r => !r.IsReturned && DateTime.Now > r.DueDate);

        var totalPenalties = rentals.Sum(r => r.Penalty);

        Console.WriteLine("===== REPORT =====");
        Console.WriteLine($"Total equipment: {totalEquipment}");
        Console.WriteLine($"Available: {availableEquipment}");
        Console.WriteLine($"Unavailable: {unavailableEquipment}");
        Console.WriteLine($"Active rentals: {activeRentals}");
        Console.WriteLine($"Overdue rentals: {overdueRentals}");
        Console.WriteLine($"Total penalties: {totalPenalties} PLN");
    }
}