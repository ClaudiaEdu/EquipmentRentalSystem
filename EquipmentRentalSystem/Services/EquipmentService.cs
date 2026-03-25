using System.Linq;
using EquipmentRentalSystem.Models;
using EquipmentRentalSystem.Enums;

namespace EquipmentRentalSystem.Services;

public class EquipmentService
{
    private readonly List<Equipment> _equipmentList = new();

    public void AddEquipment(Equipment equipment)
    {
        _equipmentList.Add(equipment);
    }

    public List<Equipment> GetAllEquipment()
    {
        return _equipmentList;
    }

    public List<Equipment> GetAvailableEquipment()
    {
        return _equipmentList
            .Where(e => e.Status == EquipmentStatus.Available)
            .ToList();
    }

    public Equipment? GetById(int id)
    {
        return _equipmentList.FirstOrDefault(e => e.Id == id);
    }

    public void MarkAsUnavailable(int id)
    {
        var equipment = GetById(id);
        if (equipment != null)
        {
            equipment.Status = EquipmentStatus.Unavailable;
        }
    }
}