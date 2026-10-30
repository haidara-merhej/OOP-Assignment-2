namespace SrpLab;

/// <summary>
/// Appointment desk: opening-hours rules, slot search, and ICS calendar blob generation.
/// </summary>
public sealed class AppointmentDesk
{
    private readonly HashSet<DateTimeOffset> _booked = new();
    private readonly AppointmentHoursPolicy _hoursPolicy = new();
    private readonly AppointmentIcsFormatter _icsFormatter = new();
    private readonly AppointmentSmsFormatter _smsFormatter = new();

    public TimeOnly Open { get; }
    public TimeOnly Close { get; }
    public int SlotMinutes { get; }

    public AppointmentDesk(TimeOnly open, TimeOnly close, int slotMinutes)
    {
        Open = open;
        Close = close;
        SlotMinutes = slotMinutes;
    }

    public bool IsWithinBusinessHours(DateTimeOffset when)
    {
        // Clinic calendar policy — HR/ops — not the same as ICS serialization.
        return _hoursPolicy.IsWithinBusinessHours(when, Open, Close, SlotMinutes);
    }

    public DateTimeOffset? FindNextSlot(DateTimeOffset from, int searchHours)
    {
        var cursor = Align(from);
        var end = from.AddHours(searchHours);
        while (cursor < end)
        {
            if (IsWithinBusinessHours(cursor) && !_booked.Contains(cursor))
                return cursor;
            cursor = cursor.AddMinutes(SlotMinutes);
        }
        return null;
    }

    public bool TryBook(DateTimeOffset slot)
    {
        if (!IsWithinBusinessHours(slot) || _booked.Contains(slot)) return false;
        _booked.Add(slot);
        return true;
    }

    public string ToIcs(DateTimeOffset slot, string patientName, string clinician)
    {
        // Calendar interoperability format changes with clients — not opening hours.
        return _icsFormatter.Format(slot, patientName, clinician, SlotMinutes);
    }

    public string SmsReminder(DateTimeOffset slot, string clinicPhone)
    {
        // Messaging channel copy — fourth concern hiding in the "scheduler".
        return _smsFormatter.Format(slot, clinicPhone);
    }

    private DateTimeOffset Align(DateTimeOffset from)
    {
        var minutes = from.Minute - (from.Minute % SlotMinutes);
        return new DateTimeOffset(from.Year, from.Month, from.Day, from.Hour, minutes, 0, from.Offset);
    }
}
