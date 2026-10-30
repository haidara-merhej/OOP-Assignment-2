namespace SrpLab;

/// <summary>
/// Course enrollment: capacity, waitlist math, welcome-packet markdown, and invoice lines.
/// </summary>
public sealed class CourseEnrollmentDesk
{
    private readonly CourseEnrollmentService _enrollmentService = new();
    private readonly WelcomePacketGenerator _packetGenerator = new();
    private readonly TuitionInvoiceFormatter _invoiceFormatter = new();

    public int Capacity { get; }
    public decimal Tuition { get; }
    public string CourseCode { get; }

    public CourseEnrollmentDesk(string courseCode, int capacity, decimal tuition)
    {
        CourseCode = courseCode;
        Capacity = capacity;
        Tuition = tuition;
    }

    public string Register(string studentEmail)
    {
        return _enrollmentService.Register(studentEmail);
    }

    public int WaitlistPosition(string studentEmail)
    {
        return _enrollmentService.WaitlistPosition(studentEmail);
    }

    public string WelcomePacketMarkdown(string studentEmail, string studentName)
    {
        // Content design changes with academy marketing — not with seat algorithms.
        var status = _enrollmentService.HasSeat(studentEmail) ?
             "confirmed seat" : $"waitlist #{WaitlistPosition(studentEmail)}";
        
        return _packetGenerator.Gnerate(CourseCode, studentName, status);
    }

    public string TuitionInvoiceLine(string studentEmail)
    {
        // Finance formatting / tax later — separate from enrollment capacity.
        return _invoiceFormatter.Format(CourseCode, Tuition, _enrollmentService.HasSeat(studentEmail));
    }   

    public void PromoteFromWaitlist(int seats)
    {
        // Operational promotion policy mixed with messaging responsibilities above.
        _enrollmentService.PromoteFromWaitlist(seats);
    }
}
