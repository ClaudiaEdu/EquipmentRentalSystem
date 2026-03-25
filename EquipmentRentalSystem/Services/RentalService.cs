using System.Linq;
using EquipmentRentalSystem.Exceptions;
using EquipmentRentalSystem.Enums;
using EquipmentRentalSystem.Interfaces;
using EquipmentRentalSystem.Models;

namespace EquipmentRentalSystem.Services;

public class RentalService
{
    private readonly List<Rental> _rentals = new();
    private readonly IUserLimitPolicy _userLimitPolicy;
    private readonly IPenaltyCalculator _penaltyCalculator;

    public RentalService(IUserLimitPolicy userLimitPolicy, IPenaltyCalculator penaltyCalculator)
    {
        _userLimitPolicy = userLimitPolicy;
        _penaltyCalculator = penaltyCalculator;
    }
    
    
    // Handles equipment rental and checks business rules
    public void RentEquipment(User user, Equipment equipment, int days, DateTime? borrowDate = null)
    {
        if (equipment.Status != EquipmentStatus.Available)
        {
            throw new EquipmentUnavailableException("This equipment is unavailable.");
        }

        var activeRentalsCount = _rentals.Count(r => r.User.Id == user.Id && !r.IsReturned);
        var maxAllowed = _userLimitPolicy.GetMaxActiveRentals(user);

        if (activeRentalsCount >= maxAllowed)
        {
            throw new RentalLimitExceededException("User has reached the rental limit.");
        }

        var rental = new Rental(user, equipment, days, borrowDate);
        _rentals.Add(rental);
        equipment.Status = EquipmentStatus.Unavailable;
    }
    // Handles equipment return and calculates penalty
    public decimal ReturnEquipment(int rentalId)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == rentalId);

        if (rental == null)
        {
            throw new RentalNotFoundException("Rental not found.");
        }

        if (rental.IsReturned)
        {
            throw new InvalidOperationException("Equipment has already been returned.");
        }

        var returnDate = DateTime.Now;
        var penalty = _penaltyCalculator.CalculatePenalty(rental, returnDate);

        rental.Return(penalty);
        rental.Equipment.Status = EquipmentStatus.Available;

        return penalty;
    }

    public List<Rental> GetAllRentals()
    {
        return _rentals;
    }

    public List<Rental> GetActiveRentalsByUser(int userId)
    {
        return _rentals
            .Where(r => r.User.Id == userId && !r.IsReturned)
            .ToList();
    }

    public List<Rental> GetOverdueRentals()
    {
        return _rentals
            .Where(r => !r.IsReturned && DateTime.Now > r.DueDate)
            .ToList();
    }
}