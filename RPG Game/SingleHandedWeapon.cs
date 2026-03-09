namespace RPG_Game;

public class SingleHandedWeapon : Weapon
{
    public SingleHandedWeapon(int dmg = 50)
    {
        Damage = dmg;
    }
    
    public override char GetSymbol()
    {
        return 'p';
    }

    public override string GetName()
    {
        return "Single Handed Weapon";
    }


    public override void TryEquip(Player player,Hand hand)
    {
        player.EquipSingleHanded(this,hand);
    }

    public override void TryRemove(Player player, Hand hand)
    {
        player.RemoveSingleHanded(hand);
    }

}