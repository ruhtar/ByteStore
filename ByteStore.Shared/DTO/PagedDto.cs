namespace ByteStore.Shared.DTO;

public class PagedDto<T>
{
    public List<T> Items { get; }

    
    public PagedDto(List<T> items)
    {
        Items = items;
    }
    
    public static PagedDto<T> Create(ICollection<T> source, int pageSize, int pageIndex)
    // pageSize = how many items will return on the dto
    // page index = where it starts. 1 is the default value
    {
        // var count = source.Count;
        if (pageIndex == 0 && pageSize == 0)
        {
            return new PagedDto<T>(source.ToList()); 
        }

        var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PagedDto<T>(items);
    }
}