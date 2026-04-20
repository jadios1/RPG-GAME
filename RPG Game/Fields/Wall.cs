namespace RPG_Game;

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

    public override Item RemoveItem()
    {
        throw new NotImplementedException();
    }

    public override Item GetItem()
    {
        throw new NotImplementedException();
    }

    public override void PutItem(Item item)
    {
        throw new NotImplementedException();
    }
}