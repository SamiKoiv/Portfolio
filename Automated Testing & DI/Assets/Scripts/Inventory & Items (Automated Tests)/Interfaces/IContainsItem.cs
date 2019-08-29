public interface IContainsItem
{
    bool ContainsItem(Item item);
    bool ContainsItem(Item item, out int quantity);
}
