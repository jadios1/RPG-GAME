namespace RPG_Game.Items;

public class Skull : Item
{
    public override char GetSymbol()
    {
        return '~';
    }

    public override string GetName()
    {
        return "Skull";
    }
}