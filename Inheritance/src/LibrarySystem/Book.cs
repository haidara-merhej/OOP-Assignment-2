namespace LibrarySystem;

public class Book : LibraryItem
{
    public Book(string catalogNumber, string title, decimal baseLateFee) 
        : base(catalogNumber, title, baseLateFee, loanPeriodDays: 21, lateFeeMultiplier: 1.0m)
    {
    }
}