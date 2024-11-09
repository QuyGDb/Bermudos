public class ItemJson
{
    public int quantity;
    public int inventorySlot;
    public int hotbarSlot;

    public ItemJson(int quantity, int inventorySlot, int hotbarSlot)
    {
        this.quantity = quantity;
        this.inventorySlot = inventorySlot;
        this.hotbarSlot = hotbarSlot;
    }
}
