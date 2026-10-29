namespace SrpLab;

public class TicketMessageGenerator
{
    public string DraftPublicReply(string agentName, string priority, string id, DateTimeOffset deadLine)
    {
        var apology = priority == "P1" ? "We are treating this as a critical incident." : "Thanks for reaching out.";
        return $"Hi,\n{apology}\nTicket {id} is with {agentName}. Next update before {deadLine:u}.\n";
    }

    public string InternalEscalationBlurb(string priority, string id, DateTimeOffset deadLine)
    {
        return $"ESCALATE {id} priority={priority} breachAt={deadLine:u} keywords-scanned=yes";
    }
}