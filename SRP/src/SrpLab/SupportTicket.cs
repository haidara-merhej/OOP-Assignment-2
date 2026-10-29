namespace SrpLab;

/// <summary>
/// Support ticket: SLA clocks, NLP-ish priority from free text, and public reply templates.
/// </summary>
public sealed class SupportTicket
{
    private readonly TicketPriorityDetector _priorityDetector = new();
    private readonly TicketSlaCalculator _slaCalculator = new();
    private readonly TicketMessageGenerator _messageGenerator = new();

    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; } = "P3";

    public SupportTicket(string id, string subject, string body, DateTimeOffset openedAt)
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;
        _priorityDetector.CulatePriority(Subject, Body);
    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
        Priority = _priorityDetector.CulatePriority(Subject, Body);
    }

    public void RecalculatePriorityFromText()
    {
        Priority = _priorityDetector.CulatePriority(Subject, Body);
    }

    public DateTimeOffset SlaDeadline()
    {
        // Operational SLA policy — different stakeholders than reply wording.
        return _slaCalculator.CalculateDeadline(Priority, OpenedAt);
        
    }

    public bool IsBreached(DateTimeOffset now) => now > SlaDeadline();

    public string DraftPublicReply(string agentName)
    {
        // Tone/templates owned by CX — not by SLA engineering.

        return _messageGenerator.DraftPublicReply(agentName, Priority, Id, SlaDeadline());
    }

    public string InternalEscalationBlurb()
    {
        return _messageGenerator.InternalEscalationBlurb(Priority, Id, SlaDeadline());
    }
}
