namespace RPG_Game.Items.Weapon;

public class SingleHandedWeapon : LightWeapon
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
    public override bool IsEquipable()
    {
        return true;
    }

}