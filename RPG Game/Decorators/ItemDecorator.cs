using RPG_Game.Fields;

namespace RPG_Game.Decorators;

public abstract class ItemDecorator : Item
{
    protected Item _inner;
    
    public ItemDecorator(Item inner)
    {
        _inner = inner;
    }
    
    public override char GetSymbol() => _inner.GetSymbol();
    public override string GetName() => _inner.GetName();

    public override void TryEquip(Player p, Hand h)
    {
        
        _inner.TryEquipAsDecorated(p, h, this);    
    } 
    public override void TryRemove(Player p, Hand h)
    {
        _inner.TryRemoveAsDecorated(p, h, this);
    }
    public override int AcceptAttack(IAttackVisitor v, Player p) => _inner.AcceptAttack(v, p);
    public override int AcceptDefense(IDefenseVisitor v, Player p) => _inner.AcceptDefense(v, p);
    
    public override bool IsEquipable() => _inner.IsEquipable();
    public override void OnPickup(Player player, Field field)
    {
        if (player.Inventory.Count < 3)
        {
            field.RemoveItem();
            player.Inventory.Add(this);
        }
    }    
    public override void TryRemoveAsDecorated(Player player, Hand hand, Item decorated)
    {
        _inner.TryRemoveAsDecorated(player, hand, decorated);
    }
    public override void TryEquipAsDecorated(Player player, Hand hand, Item decorated)
    {
        _inner.TryEquipAsDecorated(player, hand, decorated);
    }
    public override string Description() => _inner.Description();
}