using EquipmentRentalSystem.Models;

namespace EquipmentRentalSystem.Interfaces;

public interface IUserLimitPolicy
{
    int GetMaxActiveRentals(User user);
}