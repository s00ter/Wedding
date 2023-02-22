using Wedding.AdminPanel.Constants;

namespace Wedding.AdminPanel.Models.Pagination;

public class BasePagedModel<T>
{
    public int Total { get; set; }

    public int Page { get; set; }

    public List<T> PageItems { get; set; }

    public int LastPage => (int)Math.Ceiling((double)Total / PaginationConstants.ElementsOnPage);
}