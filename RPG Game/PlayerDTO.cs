namespace RPG_Game;

public class PlayerDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int X { get; set; }
    public int Y { get; set; }
    public int Health { get; set; }
    public int Gold { get; set; }
    public int Coins { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Wisdom { get; set; }
    public int Aggression { get; set; }
    public int Luck { get; set; }
    public char Symbol { get; set; }
    
    public List<string> InventoryNames { get; set; } = new();
    public string LeftHandName { get; set; } = string.Empty;
    public string RightHandName { get; set; } = string.Empty;
    public int SelectedSlot { get; set; }
}