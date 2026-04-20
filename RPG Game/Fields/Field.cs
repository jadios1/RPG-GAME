namespace RPG_Game.Fields;

public abstract class Field : IDisplayable
{
    public abstract bool IsEmpty();
    public abstract bool IsPassable();
    public abstract char GetSymbol();

    public abstract Item? RemoveItem();

    public abstract Item? GetItem();
    public abstract void PutItem(Item? item);
    
    public virtual Enemy? GetEnemy() => null;
    public virtual void SetEnemy(Enemy? e) { }
    public virtual bool HasEnemy() => false;

}