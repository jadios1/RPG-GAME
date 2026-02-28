namespace RPG_Game;

public abstract class Field : IDisplayable
{
    public abstract bool IsEmpty();
    public abstract bool IsPassable();
    public abstract char GetSymbol();

    public abstract Item GetItem();
    public abstract void PutItem(Item item);

}