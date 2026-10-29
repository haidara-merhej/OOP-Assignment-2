namespace SrpLab;

public class LoanDocumentChecklist
{
    public IReadOnlyList<string> GetRequiredDocuments(decimal requestedAmount, bool hasCollateral, 
        int employmentMonths, bool isEligible)
    {
        // Compliance checklist changes with regulation, independently of risk formula.
        var docs = new List<string> { "National ID", "Proof of income (3 months)" };
        if (requestedAmount > 40_000m) docs.Add("Bank statements (6 months)");
        if (hasCollateral) docs.Add("Collateral ownership deed");
        if (employmentMonths < 12) docs.Add("Employer letter");
        if (!isEligible) docs.Add("Manual underwriter referral form");
        return docs;
    }
}