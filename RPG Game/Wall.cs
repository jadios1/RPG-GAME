namespace RPG_Game;

public class Wall : Field
{
    public Wall(){}
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

    public override Item GetItem()
    {
        return null;
    }

    public override void PutItem(Item item)
    {
        Console.WriteLine("Cannot place item on a wall");
    }
}