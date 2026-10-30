namespace SrpLab;

/// <summary>
/// Hospital ward board: tracks beds, computes acuity scores, drafts nurse handoff notes,
/// and decides which pager code to fire. Looks like "one ward concern" — it is not.
/// </summary>
public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();
    private readonly Dictionary<int, int> _vitalsScore = new();
    

    private readonly AcuityScorer _acuityScorer = new();
    private readonly HandofNoteFormatter _handofFormatter = new();
    private readonly PagerAlertService _pagerAlertService = new();
    private readonly WardCensusCsvExporter _csvExporter = new();

    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        if (bed <= 0) throw new ArgumentOutOfRangeException(nameof(bed));
        if (string.IsNullOrWhiteSpace(patientId)) throw new ArgumentException("patient required");

        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();
        _vitalsScore[bed] = ScoreAcuity(heartRate, spo2);

        if (_vitalsScore[bed] >= 8)
            _pagerAlertService.AddAlert(bed);
    }

    public int ScoreAcuity(int heartRate, int spo2)
    {
        // Clinical scoring mixed with arbitrary pager thresholds (policy will churn separately).

        return _acuityScorer.Score(heartRate, spo2);
    }

    public string BuildHandoffNote(int bed)
    {
        if (!_bedPatient.TryGetValue(bed, out var patient))
            return _handofFormatter.EmptyBed(bed);

        var acuity = _vitalsScore[bed];

        return _handofFormatter.Format(bed, patient, acuity);
    }

    public IReadOnlyList<string> DrainPagerLog()
    {
        return _pagerAlertService.Drain();
    }

    public string ExportCensusCsv()
    {
        // Persistence/export shape mixed into the same type as acuity + paging.
        return _csvExporter.Export(_bedPatient, _vitalsScore);
    }
}
