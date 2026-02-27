namespace RPG_Game;

public class EmptyField : Field
{
    public List<Item> Items;

    public EmptyField()
    {
        Items = new List<Item>();
    }
    
    public override bool IsPassable()
    {
        return true;
    }

    public override char GetSymbol()
    {
        return ' ';
    }
}