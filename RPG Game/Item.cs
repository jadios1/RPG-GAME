namespace RPG_Game;

public abstract class Item : IDisplayable
{
    public abstract char GetSymbol();

    public abstract string GetName();

    public virtual void TryEquip(Player player,Hand hand) { }
    
    public virtual void TryRemove(Player player,Hand hand) { }

    public virtual void OnPickup(Player player,Field field)
    {
        if (player.Inventory.Count < 3)
        {
            field.RemoveItem();
            player.Inventory.Add(this);
        }
    }

    public virtual string Description()
    {
        return GetName();
    }
}