namespace RPG_Game.Items.Weapon;

public class Pen : LightWeapon
{
    public Pen()
    {
        Damage = 5;
    }
    public override char GetSymbol()
    {
        return '|';
    }

    public override string GetName()
    {
        return "Pen";
    }

    public override void TryEquip(Player player, Hand hand)
    {
        player.EquipSingleHanded(this,hand);
    }

    public override void TryRemove(Player player, Hand hand)
    {
        player.RemoveSingleHanded(hand);
    }
}