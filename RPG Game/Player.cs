namespace RPG_Game;

public class Player : IDisplayable
{
    public Player()
    {
        X = 2;
        Y = 2;
        Strength = 0;
        Dexterity = 0;
        Luck = 0;
        Aggression = 0;
        Wisdom = 0;
        Gold = 0;
        Coins = 0;
        Health = 100;
        LeftHand = new Hand();
        RightHand = new Hand();
        Inventory = new List<Item>(3);
        SelectedSlot = 0;

    }

    public int SelectedSlot;
    public List<Item> Inventory { get; }
    public int X{ get;private set; }
    public int Y{ get;private set; }
    public int Strength { get; set; }
    public int Dexterity{ get; set; }
    public int Health{ get; set; }
    public int Luck{ get; set; }
    public int Aggression{ get; set; }
    public int Wisdom{ get; set; }
    public int Gold{ get; set; }
    public int Coins{ get; set; }
    public Hand LeftHand;
    public Hand RightHand;


    public void Move(int dx, int dy)
    {
        X += dx;
        Y += dy;
    }

    public void PutIntoInventory(Item item,Field field)
    {

        item.OnPickup(this,field);
    }

    public void RemoveFromInventory(int index)
    {
        if (index >= 0 && index < Inventory.Count)
        {
            Inventory.RemoveAt(index);
        }
    }
    
 
    
    public void EquipSingleHanded(Item item,Hand hand)
    {
        Console.WriteLine($"EquipSingleHanded: item={item.GetName()}, hand.IsEmpty={hand.IsEmpty()}, inventory count={Inventory.Count}, contains item={Inventory.Contains(item)}");
        if (hand.IsEmpty())
        {
            hand.Hold(item);
            Inventory.Remove(item);
        }
    }
    
    public void EquipDoubleHanded(Item item)
    {
        if (LeftHand.IsEmpty() && RightHand.IsEmpty())
        {
            LeftHand.Hold(item);
            RightHand.Hold(item);
            Inventory.Remove(item);
        }
    }
    
    public void RemoveDoubleHanded(Hand hand)
    {
        if (Inventory.Count < 3)
        {
            if (hand.HeldItem != null) Inventory.Add(hand.HeldItem);

            LeftHand.Clear();
            RightHand.Clear();

        }
    }
    
    public void RemoveSingleHanded(Hand hand)
    {
        if (Inventory.Count < 3)
        {
            if (hand.HeldItem != null) Inventory.Add(hand.HeldItem);
            hand.Clear();
        }
        
    }

    public Item? SelectedItem()
    {
        if (Inventory.Count > SelectedSlot )
        {
            return Inventory[SelectedSlot];
        }

        return null;
    }
    
    
    public bool DropItem(Map _map)
    {
        var itemToDrop = this.SelectedItem();
        if (itemToDrop != null)
        {
            itemToDrop.OnDrop(this);
            _map.GetField(X, Y).PutItem(itemToDrop);
            this.RemoveFromInventory(this.SelectedSlot);
            return true;
        }
        return false;
    }

    public bool LeftHandPickup()
    {
        Console.WriteLine($"TryRemove on: {LeftHand.HeldItem?.GetName()}");
        if (LeftHand.IsEmpty())
        {
            var item = this.SelectedItem();
            if (item != null)
            {
                item.TryEquip(this,LeftHand);
                return true;
            }
        }
        else
        {
            LeftHand.HeldItem?.TryRemove(this,LeftHand);
            return true;
        }

        return false;

    }

    public bool RightHandPickup()
    {
        if (RightHand.IsEmpty())
        {
            var item = SelectedItem();
            if (item != null)
            {
                item.TryEquip(this,RightHand);
                return true;
            }
        }
        else
        {
            RightHand.HeldItem?.TryRemove(this,RightHand);
            return true;
        }

        return false;

    }

    public bool PickUpItem(Map map)
    {
        if (!map.GetField(X, Y).IsEmpty())
        {
            PutIntoInventory(map.GetField(X, Y).GetItem(), map.GetField(X, Y));
            return true;
        }

        return false;


    }

    public int CalculateAttackDamage(IAttackVisitor visitor)
    {
        Console.SetCursorPosition(0, 24);
        Console.WriteLine($"Calc: L={LeftHand.HeldItem?.GetType().Name} R={RightHand.HeldItem?.GetType().Name}".PadRight(50));

        int left = LeftHand.HeldItem?.AcceptAttack(visitor, this) ?? 0;
        int right = RightHand.HeldItem?.AcceptAttack(visitor, this) ?? 0;
        return left + right;
    }

    public int CalculateDefense(IDefenseVisitor visitor)
    {
        int left = LeftHand.HeldItem?.AcceptDefense(visitor, this) ?? 0;
        int right = RightHand.HeldItem?.AcceptDefense(visitor, this) ?? 0;
        return left + right;
    }
    
    public char GetSymbol()
    {
        return '¶';
    }
    
    
    
    
}
