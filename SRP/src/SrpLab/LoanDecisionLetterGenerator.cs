namespace SrpLab;

public class LoanDecisionLetterGenerator
{
    public string GenerateDecisionLetter(string applicantName, bool isEligible, decimal requestedAmount, 
        decimal riskScore, IReadOnlyList<string> requiredDocuments)
    {
        // Legal/comms wording ≠ underwriting math.
        if (isEligible)
        {
            return $"Dear {applicantName},\nYour request for {requestedAmount:C} is pre-approved (risk {riskScore:0}).\n" +
                   $"Please upload: {string.Join("; ", requiredDocuments)}.\n";
        }

        return $"Dear {applicantName},\nWe are unable to approve {requestedAmount:C} at this time.\n" +
               $"Reference risk={riskScore:0}. You may reapply after improving documentation.\n";
    }
}