namespace RPG_Game.Items.Weapon;

public class Magic_Double_Handed_Stick : MagicalWeapon
{
    public Magic_Double_Handed_Stick()
    {
        Damage = 100;
    }
    public override char GetSymbol()
    {
        return '{';
    }

    public override string GetName()
    {
        return "Magical Double Handed Stick";
    }

    public override void TryEquip(Player player, Hand hand)
    {
        player.EquipDoubleHanded(this);
    }

    public override void TryRemove(Player player, Hand hand)
    {
        player.RemoveDoubleHanded(hand);
    }
}