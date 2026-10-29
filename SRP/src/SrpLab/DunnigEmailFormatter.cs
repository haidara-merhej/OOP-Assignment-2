namespace SrpLab;

public class DunnigEmailFormatter
{
    public string DunningEmail(string customerName, DateOnly asOf, decimal amount, int failedPayments, 
        string invoice)
    {
        // Collections tone & legal boilerplate ≠ proration formula.
        var severity = failedPayments switch
        {
            <= 1 => "friendly reminder",
            2 => "second notice",
            _ => "final notice before suspension"
        };
        return $"Subject: {severity} {invoice}\nHi {customerName},\nBalance {amount:C} as of {asOf:o}" +
             $"({failedPayments} failures).\n";
    }
}