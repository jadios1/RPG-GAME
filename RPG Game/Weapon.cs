namespace RPG_Game;

public abstract class Weapon : Item
{
 

    public abstract override void TryEquip(Player player, Hand hand);

    public abstract override void TryRemove(Player player, Hand hand);

    public int Damage;

    public override string Description()
    {
        return GetName() + " Damage: " + Damage;
    }
}