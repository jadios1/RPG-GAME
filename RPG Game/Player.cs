namespace RPG_Game;

public class Player : IDisplayable
{
    public Player()
    {
        X = 1;
        Y = 1;
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
    
    public char GetSymbol()
    {
        return '¶';
    }
}