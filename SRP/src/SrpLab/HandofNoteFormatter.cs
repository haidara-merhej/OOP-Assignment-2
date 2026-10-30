namespace SrpLab;

public class HandofNoteFormatter
{
    public string EmptyBed(int bed) => $"Bed {bed}: empty";

    public string Format(int bed, string patient, int acuity)
    {
        var tone = acuity >= 8 ? "ESCALATE" : acuity >= 4 ? "WATCH" : "STABLE";
        // Presentation / narrative format will change without clinical rules changing.
        return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] Bed {bed} · {patient} · acuity={acuity} · {tone}";
    }
}