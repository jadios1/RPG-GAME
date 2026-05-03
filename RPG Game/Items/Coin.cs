using RPG_Game.Fields;

namespace RPG_Game.Items;

public class Coin : Currency
{
    public override char GetSymbol()
    {
        return 'o';
    }

    public override string GetName()
    {
        return "Coin";
    }

    public override void OnPickup(Player player,Field field)
    {
        player.Coins++;
        field.RemoveItem();
    }

}