namespace EquipmentRentalSystem.Exceptions;

public class RentalNotFoundException : Exception
{
    public RentalNotFoundException(string message) : base(message)
    {
    }
}