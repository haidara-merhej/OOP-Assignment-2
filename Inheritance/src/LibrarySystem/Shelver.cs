namespace LibrarySystem;

public class Shelver : Staff
{
    public string Section { get; private set; }

    public Shelver(string personId, string fullName, string phone, DateTime hireDate, decimal monthlySalary, string section) 
        : base(personId, fullName, phone, hireDate, monthlySalary, responsibilityAllowance : 0)
    {
        if(string.IsNullOrEmpty(section))
         throw new ArgumentException("Section connot be empty.");

        Section = section;
    }

    public void ReassignSection(string newSection)
    {
        if(string.IsNullOrEmpty(newSection))
            throw new ArgumentException("Section connot be empty.");

        Section = newSection;
    }


}