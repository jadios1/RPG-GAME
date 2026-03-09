namespace RPG_Game;

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