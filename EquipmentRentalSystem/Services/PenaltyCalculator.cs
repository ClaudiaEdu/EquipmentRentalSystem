using EquipmentRentalSystem.Interfaces;
using EquipmentRentalSystem.Models;

namespace EquipmentRentalSystem.Services;

public class PenaltyCalculator : IPenaltyCalculator
{
    private const decimal PenaltyPerDay = 10m;

    public decimal CalculatePenalty(Rental rental, DateTime returnDate)
    {
        if (returnDate <= rental.DueDate)
        {
            return 0m;
        }

        var lateDays = (returnDate.Date - rental.DueDate.Date).Days;
        return lateDays * PenaltyPerDay;
    }
}