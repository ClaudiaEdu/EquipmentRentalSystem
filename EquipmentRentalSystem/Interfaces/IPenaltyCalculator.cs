using EquipmentRentalSystem.Models;

namespace EquipmentRentalSystem.Interfaces;

public interface IPenaltyCalculator
{
    decimal CalculatePenalty(Rental rental, DateTime returnDate);
}