namespace SrpLab;

public class KitchenTicketItem
{
    public string Item { get;  }
    public List<string> Ingredients { get;  }
    public int PrepMinutes { get; }
    
    public KitchenTicketItem(string item, IEnumerable<string> ingredients, int prepMinutes)
    {
        Item = item;
        Ingredients = ingredients.ToList();
        PrepMinutes = prepMinutes;
    }

}