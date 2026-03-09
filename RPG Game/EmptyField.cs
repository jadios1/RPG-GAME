namespace RPG_Game;

public class EmptyField : Field
{
    public List<Item> Items;

    public EmptyField()
    {
        Items = new List<Item>();
    }
    
    public override bool IsPassable()
    {
        return true;
    }
    
    public override char GetSymbol()
    {
        if (Items.Count > 0)
        {
            return Items[0].GetSymbol();
        }
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