namespace RPG_Game.Items.Weapon;

public class Magic_Stick : MagicalWeapon
{
    public Magic_Stick()
    {
        Damage = 30;
    }
    public override char GetSymbol()
    {
        return '/';
    }

    public override string GetName()
    {
        return "Magic Stick";
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