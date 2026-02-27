namespace RPG_Game;

public abstract class Field : IDisplayable
{
    public abstract bool IsPassable();
    public abstract char GetSymbol();

}