namespace Cebv.core.util.data_virtualization;

public interface IItemsProvider<T>
{
    int FetchCount();
    IList<T> FetchRange(int startIndex, int count);
}