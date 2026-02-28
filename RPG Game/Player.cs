namespace RPG_Game;

public class Player : IDisplayable
{
    public Player()
    {
        X = 0;
        Y = 1;
        Strength = 0;
        Dexterity = 0;
        Luck = 0;
        Aggression = 0;
        Wisdom = 0;
        Gold = 0;
        Coins = 0;
        Health = 100;
        LeftHand = null;
        RightHand = null;
        Inventory = new List<Item>(5);
    }

    public List<Item> Inventory { get; }
    public int X{ get;private set; }
    public int Y{ get;private set; }
    private int Strength { get; set; }
    private int Dexterity{ get; set; }
    private int Health{ get; set; }
    private int Luck{ get; set; }
    private int Aggression{ get; set; }
    private int Wisdom{ get; set; }
    private int Gold{ get; set; }
    private int Coins{ get; set; }
    private Weapon? LeftHand;
    private Weapon? RightHand;

    public void Move(int dx, int dy)
    {
        X += dx;
        Y += dy;
    }

    public bool PutIntoInventory(Item item)
    {
        if (Inventory.Count() < 5)
        {
            Inventory.Add(item);
            return true;
        }
        return false;
        
    }
    
    
    public char GetSymbol()
    {
        return '¶';
    }
}