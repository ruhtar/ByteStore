namespace ByteStore.Shared.DTO;

public class GetProductsInputPagination
{
    public int PageIndex { get; set; } = 0; //where it starts taking the items. 0 -> will take the first one and beyond. 1 -> the second and beyond
    public int PageSize { get; set; } = 50;
}