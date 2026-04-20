namespace RPG_Game.Fields;

public class Wall : Field
{
    
    public override bool IsEmpty()
    {
        return false;
    }

    public override bool IsPassable()
    {
        return false;
    }

    public override char GetSymbol()
    {
        return '█';
    }

    public override Item? RemoveItem()
    {
        return null;
    }

    public override Item? GetItem()
    {
        return null;
    }

    public override void PutItem(Item? item) { }
}