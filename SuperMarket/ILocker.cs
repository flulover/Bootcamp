namespace SuperMarket
{
    public interface ILocker
    {
        Ticket Store(Bag bag);
        Bag Pick(Ticket ticket);
    }
}