using EquipmentRentalSystem.Interfaces;
using EquipmentRentalSystem.Models;

namespace EquipmentRentalSystem.Services;

public class UserLimitPolicy : IUserLimitPolicy
{
    public int GetMaxActiveRentals(User user)
    {
        return user switch
        {
            Student => 2,
            Employee => 5,
            _ => 0
        };
    }
}