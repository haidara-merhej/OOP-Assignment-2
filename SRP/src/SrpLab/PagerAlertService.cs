namespace SrpLab;

public class PagerAlertService
{
    private readonly List<string> _pagerLog = new();

    public void AddAlert(int bed)
    {
        _pagerLog.Add($"CODE-YELLOW bed={bed} at {DateTime.UtcNow:HH:mm}");
    }

    public IReadOnlyList<string> Drain()
    {
        var copy = _pagerLog.ToList();
        _pagerLog.Clear();
        return copy;
    }
}