namespace SrpLab;

public class AppointmentIcsFormatter
{
    public string Format(DateTimeOffset slot, string patientName, string clinician, int slotMinutes)
    {
        // Calendar interoperability format changes with clients — not opening hours.
        var uid = Guid.NewGuid();
        var end = slot.AddMinutes(slotMinutes);
        return "BEGIN:VCALENDAR\nVERSION:2.0\nBEGIN:VEVENT\n" +
               $"UID:{uid}\nDTSTART:{slot:yyyyMMdd'T'HHmmss'Z'}\nDTEND:{end:yyyyMMdd'T'HHmmss'Z'}\n" +
               $"SUMMARY:Visit {patientName} / {clinician}\nEND:VEVENT\nEND:VCALENDAR\n";
    }
}