namespace LibrarySystem;

public class PremiumMember : Member
{
    public int ReadingPoints => GetTotalReadingPoints();

    public PremiumMember(string personId, string fullName, string phone, decimal lateFeeDiscount)
        : base(personId, fullName, phone,  maxLoans: 10, lateFeeDiscount)
    {
    }

    private int GetTotalReadingPoints()
    {
        int points = 0;

        foreach(var loan in Loans)
        {
            if(loan.Status == LoanStatus.Borrowed)
                points += 5;
        }

        return points;
    }
}