namespace RPG_Game.Items;

public class Junk : Item
{
    public override char GetSymbol()
    {
        return '#';
    }

    public override string GetName()
    {
        return "Junk";
    }

}