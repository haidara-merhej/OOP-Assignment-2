namespace SrpLab;

/// <summary>
/// Grade book: aggregation, letter-band policy, and transcript export.
/// </summary>
public sealed class GradeBook
{
    private readonly StudentGradRecords _records = new();
    private readonly LetterGradePolicy _letterPolicy = new();
    private readonly HonorRollPolicy _honorPolicy = new();
    private readonly TranscriptFormatter _transcriptFormatter = new();
    private readonly GradeCsvExporter _csvExporter = new();
    

    public void Record(string studentId, decimal score)
    {
        _records.Record(studentId, score);
    }

    public decimal Average(string studentId)
    {
        return _records.Average(studentId);
    }

    public string Letter(string studentId)
    {
        // Academic policy bands change with faculty senate — not with CSV layout.

        return _letterPolicy.Letter(studentId, Average(studentId));
    }

    public bool MeetsHonorRoll(string studentId)
    {
        // Extra academic rule set embedded beside averaging.
        return _honorPolicy.MeetsRequirements(Average(studentId), Letter(studentId));
    }

    public string TranscriptPlain(string studentId, string fullName)
    {
        // Registrar document format ≠ grading policy.
        return _transcriptFormatter.Format(fullName, studentId, 
                Average(studentId), Letter(studentId), MeetsHonorRoll(studentId));
    }

    public string ExportCsv()
    {
        var rows = _records.Scores.Keys.OrderBy(id => id)
                                        .Select(id => 
                                        $"{id},{Average(id)},{Letter(id)},{(MeetsHonorRoll(id) ? 1 : 0)}");
        return string.Join('\n', rows);
    }
}
