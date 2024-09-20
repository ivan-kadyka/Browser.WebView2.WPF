using Browser.Abstractions.TypeId;

namespace Browser.Abstractions.Page;

public class PageId : TypedId
{
    public static PageId New() => new PageId(Guid.NewGuid().ToString());
    
    public PageId(string value) : base(value)
    {
    }
}