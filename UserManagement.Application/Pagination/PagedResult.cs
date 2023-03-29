namespace UserManagement.Application.Pagination;

public class PagedResult<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public long TotalRecords { get; set; }
    public IEnumerable<T> Data { get; set; }
}