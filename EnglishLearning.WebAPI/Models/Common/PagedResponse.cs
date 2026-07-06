namespace EnglishLearning.WebAPI.Models.Common;

public class PagedResponse<T> : ApiResponse<IReadOnlyList<T>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }

    public static PagedResponse<T> Ok(
        IReadOnlyList<T> items,
        int pageNumber,
        int pageSize,
        int totalRecords,
        string message = "Success")
    {
        return new PagedResponse<T>
        {
            Success = true,
            Message = message,
            Data = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
        };
    }
}
