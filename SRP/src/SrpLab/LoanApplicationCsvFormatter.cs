namespace SrpLab;

public class LoanApplicationCsvFormatter
{
    public string Format(string applicationId, int creditScore, int employmentMonths, bool hasCollateral, 
        decimal riskScore, bool isElligible)
    {
        // Analytics export schema is yet another reason to change.
        return $"{applicationId},{creditScore},{employmentMonths},{(hasCollateral ? 1 : 0)},{riskScore:0.00},{(isElligible ? "Y" : "N")}";
    }
}