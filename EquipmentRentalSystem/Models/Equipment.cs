using EquipmentRentalSystem.Enums;
using EquipmentRentalSystem.Utilities;

namespace EquipmentRentalSystem.Models;

public abstract class Equipment
{
    public int Id { get; }
    public string Name { get; set; }
    public EquipmentStatus Status { get; set; }

    protected Equipment(string name)
    {
        Id = IdGenerator.GenerateId();
        Name = name;
        Status = EquipmentStatus.Available;
    }
    
    public bool IsAvailable => Status == EquipmentStatus.Available;
}