namespace RPG_Game.Items;

public class Clock : Item
{
    public override char GetSymbol()
    {
        return '&';
    }

    public override string GetName()
    {
        return "Clock";
    }
}