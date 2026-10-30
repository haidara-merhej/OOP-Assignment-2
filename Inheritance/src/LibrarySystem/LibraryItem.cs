namespace LibrarySystem;

public class LibraryItem
{
    public string CatalogNumber { get; } = null!;
    public string Title { get; } = null!;
    public int LoanPeriodDays { get; }
    public decimal BaseLateFee { get; private set; }
    protected decimal LateFeeMultiplier { get; }
    public bool IsWithdrawn { get; private set; }
    public bool IsOnLoan { get; private set; }

    protected LibraryItem(string catalogNumber, string title, 
        decimal baseLateFee, int loanPeriodDays, decimal lateFeeMultiplier)
    {
        if(string.IsNullOrEmpty(catalogNumber))
            throw new ArgumentException("Catalog number cannot be empty.");
        
        if(string.IsNullOrEmpty(title))
            throw new ArgumentException("Title cannot be empty.");
        
        if(loanPeriodDays <= 0)
            throw new ArgumentException("Loan period days cannot be zero or negative.");

        if(baseLateFee < 0)
            throw new ArgumentException("Base late fee cannot be negative.");

        if(lateFeeMultiplier <= 0)
            throw new ArgumentException("Late fee multiplier cannot be zero or negative.");
    } 

    internal decimal GetDailyLateFee()
    {
        return BaseLateFee * LateFeeMultiplier;
    }

    internal void ChangeLateFee(decimal newFee)
    {
        if(newFee < 0)
            throw new ArgumentException("Base late fee cannot be negative.");

        BaseLateFee = newFee;
    }

    internal void Withdraw()
    {
        if(IsWithdrawn)
            throw new InvalidOperationException("Item is already withdrawn.");

        IsWithdrawn = true;

    }

    internal void Restore()
    {
        if(!IsWithdrawn)
            throw new InvalidOperationException("Item is not withdrawn.");

        IsWithdrawn = false;
    }

    internal void MarkAsOnLoan()
    {
        if(IsOnLoan)
            throw new InvalidOperationException("Item is already on loan.");

        IsOnLoan = true;
    }

    internal void MarkAsAvailable()
    {
        if(!IsOnLoan)
            throw new InvalidOperationException("Item is not on loan.");

        IsOnLoan = false;
    }

}