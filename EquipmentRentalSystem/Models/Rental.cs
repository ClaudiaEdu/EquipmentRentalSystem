using EquipmentRentalSystem.Utilities;

namespace EquipmentRentalSystem.Models;

public class Rental
{
    public int Id { get; }
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime BorrowDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }
    public decimal Penalty { get; private set; }

    public bool IsReturned => ReturnDate.HasValue;

    public Rental(User user, Equipment equipment, int days, DateTime? borrowDate = null)
    {
        Id = IdGenerator.GenerateId();
        User = user;
        Equipment = equipment;
        BorrowDate = borrowDate ?? DateTime.Now;
        DueDate = BorrowDate.AddDays(days);
    }

    public void Return(decimal penalty)
    {
        ReturnDate = DateTime.Now;
        Penalty = penalty;
    }
}