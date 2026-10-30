using System.Security.Cryptography.X509Certificates;

namespace LibrarySystem;

public class Member : Person
{
    private readonly List<Loan> _loans = new();

    public IReadOnlyList<Loan> Loans => _loans;
    public int MaxLoans { get; }
    public decimal LateFeeDiscount { get; }

    protected Member(string personId, string fullName, string phone, int maxLoans, decimal lateFeeDiscount)
        : base (personId, fullName, phone)
    {
        if(maxLoans <= 0)
            throw new ArgumentException("Maximum loans must be positive.");

        if(lateFeeDiscount < 0 || lateFeeDiscount > 100)
            throw new ArgumentOutOfRangeException("Discount must be between 0 and 100");

        MaxLoans = maxLoans;
    }

    public Loan BorrowItem(LibraryItem item, DateTime borrowDate)
    {
        if(item is null)
            throw new ArgumentNullException(nameof(item));

        int activeLoans = GetActiveLoanCount();

        if(activeLoans == MaxLoans)
            throw new InvalidOperationException("Maximum loan limit reached.");

        if(item.IsWithdrawn)
            throw new InvalidOperationException("item is withdrawn.");

        Loan loan = new Loan(Guid.NewGuid(), DateTime.Now, this, item);

        item.MarkAsOnLoan();
        _loans.Add(loan);

        return loan;
    }   

    public int GetActiveLoanCount()
    {
        int count = 0;

        foreach(var loan in _loans)
        {
            if(loan.Status == LoanStatus.Borrowed)
                count++;
        }
        
        return count;
    }

}