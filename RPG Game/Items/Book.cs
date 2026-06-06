namespace RPG_Game.Items;

public class Book : Item
{
    public override char GetSymbol()
    {
        return 'b';
    }

    public override string GetName()
    {
        return "Book";
    }
}