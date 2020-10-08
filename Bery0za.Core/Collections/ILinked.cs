namespace Bery0za.Core.Collections
{
    public interface ILinked<T>
        where T : ILinked<T>
    {
        T Next { get; set; }
        T Previous { get; set; }
    }
}