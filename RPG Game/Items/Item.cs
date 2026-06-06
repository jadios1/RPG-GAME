using RPG_Game.Fields;
using RPG_Game.Visitors;

namespace RPG_Game.Items;

public abstract class Item : IDisplayable
{
    public abstract char GetSymbol();

    public abstract string GetName();

    public virtual void TryEquip(Player player,Hand hand) { }

    public virtual void TryRemove(Player player, Hand hand) { }

    public virtual void OnPickup(Player player,Field field)
    {
        if (player.Inventory.Count < 3)
        {
            field.RemoveItem();
            player.Inventory.Add(this);
        }
    }

    public virtual void OnDrop(Player player) { }

    public virtual bool IsEquipable()
    {
        return false;
    }
    
    public virtual void TryRemoveAsDecorated(Player player, Hand hand, Item decorated) { }

    public virtual void TryEquipAsDecorated(Player player, Hand hand, Item decorated) { }

    public virtual int AcceptAttack(IAttackVisitor v, Player p)
    {
        return v.VisitOther(this,p);
    }

    public virtual int AcceptDefense(IDefenseVisitor v, Player p)
    {
        return v.VisitOtherDefense(this,p);
    }
    
    public virtual int GetSoundRange() => 0;


    public virtual string Description()
    {
        return "";
    }
}