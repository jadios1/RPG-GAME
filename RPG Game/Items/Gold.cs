namespace RPG_Game;

public class Gold : Currency
{
    public override char GetSymbol()
    {
        return '$';
    }

    public override string GetName()
    {
        return "Gold";
    }

    public override void OnPickup(Player player,Field field)
    {
        player.Gold++;
        field.RemoveItem();

    }
}