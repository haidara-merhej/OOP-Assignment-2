namespace SrpLab;

public class HonorRollPolicy
{
    public bool MeetsRequirements(decimal average, string letter)
    {
        // Extra academic rule set embedded beside averaging.
        return average >= 85 && letter is "A" or "B";
    }
}