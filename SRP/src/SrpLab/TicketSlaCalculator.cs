namespace SrpLab;

public class TicketSlaCalculator
{
    public DateTimeOffset CalculateDeadline(string priority, DateTimeOffset openedAt)
    {
        var hours = priority switch
        {
            "P1" => 4,
            "P2" => 24,
            _ => 72
        };
        
        return openedAt.AddHours(hours);;
    }
}