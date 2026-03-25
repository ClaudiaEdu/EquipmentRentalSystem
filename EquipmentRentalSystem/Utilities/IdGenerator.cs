namespace EquipmentRentalSystem.Utilities;

public static class IdGenerator
{
    private static int _currentId = 1;

    public static int GenerateId()
    {
        return _currentId++;
    }
}