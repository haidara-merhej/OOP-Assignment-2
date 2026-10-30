namespace LibrarySystem;

public class Loan
{
    public Guid LoanId { get; }
    public DateTime BorrowDate { get; }
    public DateTime DueDate => BorrowDate.AddDays(Item.LoanPeriodDays);

    public DateTime? ReturnDate { get; private set; }
    public LoanStatus Status { get; private set; }
    public LibraryItem Item { get; } = null!;
    public Member Member { get; } = null!;

    public decimal LateFee => GetLateFee();


    public Loan(Guid loanId, DateTime borrowDate, Member member, LibraryItem item)
    {
        if(loanId == Guid.Empty)
            throw new ArgumentException("LoanId is required.");

        if(member is null)
            throw new ArgumentNullException("Member cannot be null");

        if(item is null) 
            throw new ArgumentNullException("Library item cannot be null");

        LoanId = loanId;
        BorrowDate = borrowDate;
        Member = member;
        Item = item;
        Status = LoanStatus.Borrowed;
    }

    public void ReturnItem(DateTime returnDate)
    {
        if(Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Item is already returned");

        if(returnDate.Date < BorrowDate.Date)
            throw new ArgumentException("Return date cannot be before Borrow date.");

        ReturnDate = returnDate;
        Status = LoanStatus.Returned;
        Item.MarkAsAvailable();
    }

    public void MarkAsLost()
    {
        if(Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Only borrowed loans can be marked as lost.");

        Status = LoanStatus.Lost;
    }

    private decimal GetLateFee()
    {
        if(ReturnDate == null || ReturnDate <= DueDate)
            return 0;

        int lateDays =(ReturnDate.Value.Date - DueDate.Date).Days;

        decimal fee = lateDays * Item.GetDailyLateFee();

        return fee * (1 - Member.LateFeeDiscount / 100);
    }
}