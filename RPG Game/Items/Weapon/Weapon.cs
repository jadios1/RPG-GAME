using RPG_Game.Visitors;

namespace RPG_Game.Items.Weapon;

public abstract class Weapon : Item
{
 
    public abstract override int AcceptAttack(IAttackVisitor visitor, Player player);
    public abstract override int AcceptDefense(IDefenseVisitor visitor, Player player);    
    public abstract override void TryEquip(Player player, Hand hand);

    public override void TryEquipAsDecorated(Player player, Hand hand, Item decorated)
    {
        player.EquipSingleHanded(decorated, hand);
    }
    
    public abstract override void TryRemove(Player player, Hand hand);

    public int Damage;

    public override string Description()
    {
        return " Damage: " + Damage;
    }
    
    public override void TryRemoveAsDecorated(Player player, Hand hand, Item decorated)
    {
        player.RemoveSingleHanded(hand);
    }

}