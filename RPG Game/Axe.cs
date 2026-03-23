namespace RPG_Game;

public class Axe : Weapon
{
    public Axe(int dmg = 20)
    {
        Damage = dmg;
    }
    public override char GetSymbol()
    {
        return 'A';
    }

    public override string GetName()
    {
        return "Axe";
    }

    public override void TryEquip(Player player,Hand hand)
    {
        player.EquipSingleHanded(this,hand);
    }

    public override void TryRemove(Player player, Hand hand)
    {
        player.RemoveSingleHanded(hand);
    }
    public override bool IsEquipable()
    {
        return true;
    }
}