namespace LibrarySystem;

public class HeadLibrarian : Staff
{
    public HeadLibrarian(string personId, string fullName, string phone, DateTime hireDate, decimal monthlySalary) 
        : base(personId, fullName, phone, hireDate, monthlySalary, responsibilityAllowance: 400)
    {
    }

    public void ChangeLateFee(LibraryItem item, decimal newBaseFee)
    {
        if(item is null)
            throw new ArgumentNullException(nameof(item));

        item.ChangeLateFee(newBaseFee);
    }

    public void WithdrawItem(LibraryItem item)
    {
        if(item is null)
            throw new ArgumentNullException(nameof(item));

        item.Withdraw();
    }

    public void RestoreItem(LibraryItem item)
    {
        if(item is null)
            throw new ArgumentNullException(nameof(item));

        item.Restore();
    }
}