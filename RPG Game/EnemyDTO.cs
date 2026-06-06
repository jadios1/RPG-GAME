namespace RPG_Game;

public class EnemyDTO
{
    public string Name { get; set; } = string.Empty;
    public int X { get; set; }
    public int Y { get; set; }
    public int Health { get; set; }
    public int Armor { get; set; }
    public int Attack { get; set; }
    public char Symbol { get; set; }
}