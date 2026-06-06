namespace RPG_Game.Items;

public abstract class Currency : Item
{
    public abstract override char GetSymbol();
    public abstract override string GetName();

    
}