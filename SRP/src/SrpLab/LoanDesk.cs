namespace SrpLab;

/// <summary>
/// Loan desk: eligibility arithmetic, required-document lists, and rejection letter prose.
/// </summary>
public sealed class LoanDesk
{
    private readonly LoanRiskAssessor _loanRiskAssessor = new();
    private readonly LoanDocumentChecklist _documentChecklist = new();
    private readonly LoanDecisionLetterGenerator _letterGenerator = new();
    private readonly LoanApplicationCsvFormatter _csvFormatter = new();

    public decimal RequestedAmount { get; }
    public int CreditScore { get; }
    public int EmploymentMonths { get; }
    public bool HasCollateral { get; }

    public LoanDesk(decimal requestedAmount, int creditScore, int employmentMonths, bool hasCollateral)
    {
        RequestedAmount = requestedAmount;
        CreditScore = creditScore;
        EmploymentMonths = employmentMonths;
        HasCollateral = hasCollateral;
    }

    public decimal RiskScore()
    {
        // Risk model will change with risk committee — not with letter templates.

        return _loanRiskAssessor.RiskScore(CreditScore, EmploymentMonths, RequestedAmount, HasCollateral);
    }

    public bool IsEligible() => RiskScore() >= 55m && CreditScore >= 580;

    public IReadOnlyList<string> RequiredDocuments()
    {
        return _documentChecklist.GetRequiredDocuments(RequestedAmount, HasCollateral, EmploymentMonths, IsEligible());
    }

    public string DecisionLetter(string applicantName)
    {
        // Legal/comms wording ≠ underwriting math.
        return _letterGenerator.GenerateDecisionLetter(applicantName, IsEligible(), 
            RequestedAmount, RiskScore(), RequiredDocuments());
    }

    public string UnderwriterCsvRow(string applicationId)
    {
        // Analytics export schema is yet another reason to change.
        return _csvFormatter.Format(applicationId, CreditScore ,EmploymentMonths ,
            HasCollateral, RiskScore() , IsEligible());
    }
}
