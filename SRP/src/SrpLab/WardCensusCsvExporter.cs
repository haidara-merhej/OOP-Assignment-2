namespace SrpLab;

public class WardCensusCsvExporter
{
    public string Export(IReadOnlyDictionary<int, string> bedPatients,
        IReadOnlyDictionary<int, int> vitalScores)
    {
        // Persistence/export shape mixed into the same type as acuity + paging.
        var lines = new List<string> { "bed,patient,acuity" };
        foreach (var bed in bedPatients.Keys.OrderBy(x => x))
            lines.Add($"{bed},{bedPatients[bed]},{vitalScores[bed]}");
        return string.Join('\n', lines);
    }
}