namespace RPG_Game;

public class EmptyField : Field
{
    public List<Item>? Items;

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

    public override Item GetItem()
    {
        Item temp = Items[0];
        Items.RemoveAt(0);
        return temp;   
        
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