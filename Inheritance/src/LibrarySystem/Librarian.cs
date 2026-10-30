namespace LibrarySystem;

public class Librarian : Staff
{
    public Librarian(string personId, string fullName, string phone, DateTime hireDate, decimal monthlySalary) : 
        base(personId, fullName, phone, hireDate, monthlySalary, responsibilityAllowance: 0)
    {
    }

    public void Return(Loan loan, DateTime returnDate)
    {
        if(loan is null)
            throw new ArgumentNullException(nameof(loan));

        loan.ReturnItem(returnDate);
    }

    public void MarkAsLost(Loan loan)
    {
        if(loan is null)
            throw new ArgumentNullException(nameof(loan));
            
        loan.MarkAsLost();
    }
}