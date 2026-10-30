namespace LibrarySystem;

public class DVD : LibraryItem
{
    public DVD(string catalogNumber, string title, decimal baseLateFee) : 
        base(catalogNumber, title, baseLateFee, loanPeriodDays: 7, lateFeeMultiplier: 2.0m)
    {
    }
}