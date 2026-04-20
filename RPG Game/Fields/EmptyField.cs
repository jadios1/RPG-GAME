namespace RPG_Game;

public class EmptyField : Field
{
    public List<Item> Items;
    public Enemy? enemy;
    public EmptyField()
    {
        Items = new List<Item>();
    }
    

    
    public override bool IsPassable() => enemy == null;
    public override Enemy? GetEnemy() => enemy;

    public override void SetEnemy(Enemy? e) { enemy = e; }
    
    public override bool HasEnemy() => enemy != null;
    
    public override char GetSymbol()
    {
        if (enemy != null) return enemy.GetSymbol();
        if (Items.Count > 0) return Items[0].GetSymbol();
        return ' ';
    }
    
    public override Item RemoveItem()
    {
        Item temp = Items[0];
        Items.RemoveAt(0);
        return temp;
        
    }

    public override Item GetItem()
    {
        return Items[0];
    }


    public override bool IsEmpty()
    {
        if (Items.Count == 0)
        {
            return true;
        }

        return false;
    }
    
    

    public override void PutItem(Item item)
    {
        Items.Add(item);
    }
}