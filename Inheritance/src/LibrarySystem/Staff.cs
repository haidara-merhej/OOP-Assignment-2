namespace LibrarySystem;

public class Staff : Person
{
    public DateTime HireDate { get; }
    public decimal MonthlySalary { get; private set; }
    public decimal ResponsibilityAllowance { get; }

    public Staff(string personId, string fullName, string phone, DateTime hireDate, decimal monthlySalary,
         decimal responsibilityAllowance) : base(personId, fullName, phone)
    {
        if(hireDate > DateTime.Now)
            throw new ArgumentException("Hire date cannot be in the future.");

        if(monthlySalary <= 0)
            throw new ArgumentException("Salary cannot be zero or negative.");

        if(responsibilityAllowance < 0)
            throw new ArgumentException("Allownce cannot be negative.");

        HireDate = hireDate;
        MonthlySalary = monthlySalary;
        ResponsibilityAllowance = responsibilityAllowance;
    }

    public void GiveRaise(decimal percentage)
    {
        if(percentage <= 0)
            throw new ArgumentException("Percentage cannot be zero or negative.");

        MonthlySalary += MonthlySalary * percentage / 100;
        
    }

    public decimal GetMonthlyPay()
    {
        return MonthlySalary + ResponsibilityAllowance;
    }
}