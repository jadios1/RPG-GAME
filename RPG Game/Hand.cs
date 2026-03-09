namespace RPG_Game;

public class Hand
{
    public Item? HeldItem;

    public bool IsEmpty()
    {
        if (HeldItem == null)
        {
            return true;
        }

        return false;
    }

    public void Hold(Item item)
    {
        HeldItem = item;
    }

    public void Clear()
    {
        HeldItem = null;
    }

}