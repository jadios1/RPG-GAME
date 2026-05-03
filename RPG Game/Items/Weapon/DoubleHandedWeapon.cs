using RPG_Game.Logger;

namespace RPG_Game.Items.Weapon;

public class DoubleHandedWeapon : HeavyWeapon
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
    
    public override void TryEquipAsDecorated(Player player, Hand hand, Item decorated)
    {
        player.EquipDoubleHanded(decorated);
    }
    
    public override void TryRemoveAsDecorated(Player player, Hand hand, Item decorated)
    {
        player.RemoveDoubleHanded(hand);
    }
    public override void TryRemove(Player player, Hand hand)
    {
        player.RemoveDoubleHanded(hand);
    }

    public override bool IsEquipable()
    {
        return true;
    }

}