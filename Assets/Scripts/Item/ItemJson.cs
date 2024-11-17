public class ItemJson
{
    public int quantity;
    public int inventorySlot;
    public int hotbarSlot;
    public bool isOnHotBar;
    public ItemJson(int quantity, int inventorySlot, int hotbarSlot, bool isOnHotBar)
    {
        this.quantity = quantity;
        this.inventorySlot = inventorySlot;
        this.hotbarSlot = hotbarSlot;
        this.isOnHotBar = isOnHotBar;
    }
}
