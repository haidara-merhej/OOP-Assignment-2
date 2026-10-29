namespace SrpLab;

public class GradeCsvExporter
{
    public string Export(IEnumerable<string> rows)
    {
        var output = new List<string> { "studentId,average,letter,honor" };

        output.AddRange(rows);
        return string.Join('\n', output);
    }
}