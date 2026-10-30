namespace LibrarySystem;

public class Magazine : LibraryItem
{
    public Magazine(string catalogNumber, string title, decimal baseLateFee) 
        : base(catalogNumber, title, baseLateFee, loanPeriodDays: 3, lateFeeMultiplier: 0.5m)
    {
    }
}