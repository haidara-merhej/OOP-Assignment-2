namespace SrpLab;

public class TranscriptFormatter
{
    public string Format(string studentId, string fullName, decimal average, string letter, bool honor)
    {
        // Registrar document format ≠ grading policy.
        return $"TRANSCRIPT\nStudent: {fullName} ({studentId})\nAverage: {average}\nLetter: {letter}\n"+
            $"Honor: {honor}\n";
    }
}