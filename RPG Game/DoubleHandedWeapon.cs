namespace RPG_Game;

public class DoubleHandedWeapon : Weapon
{
    
    public DoubleHandedWeapon(int dmg = 100)
    {
        Damage = dmg;
    }
    public override char GetSymbol()
    {
        return 'P';
    }

    public override string GetName()
    {
        return "Double Handed Weapon";
    }

    public override void TryEquip(Player player,Hand hand)
    {
        player.EquipDoubleHanded(this);
    }
    
    public override void TryRemove(Player player, Hand hand)
    {
        player.RemoveDoubleHanded(hand);
    }



}