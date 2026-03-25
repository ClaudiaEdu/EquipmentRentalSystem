using EquipmentRentalSystem.Models;
using EquipmentRentalSystem.Services;

namespace EquipmentRentalSystem.Data;

public static class DataSeeder
{
    public static void Seed(EquipmentService equipmentService, UserService userService)
    {
        // EQUIPMENT
        equipmentService.AddEquipment(new Laptop("Dell Latitude", 16, "Intel i7"));
        equipmentService.AddEquipment(new Laptop("Lenovo ThinkPad", 8, "Intel i5"));
        equipmentService.AddEquipment(new Projector("Epson X1", 3200, "1920x1080"));
        equipmentService.AddEquipment(new Projector("BenQ Pro", 4000, "4K"));
        equipmentService.AddEquipment(new Camera("Canon EOS", 24, "Zoom Lens"));
        equipmentService.AddEquipment(new Camera("Nikon D3500", 20, "Prime Lens"));

        // USERS
        userService.AddUser(new Student("Anna", "Nowak"));
        userService.AddUser(new Student("Jan", "Kowalski"));
        userService.AddUser(new Employee("Maria", "Zielinska"));
    }
}