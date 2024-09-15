using System.Threading;
using System.Threading.Tasks;

namespace PresenterBase.View;

public interface IView
{
    Task Show(CancellationToken token = default);
    
    Task Hide(CancellationToken token = default);
    
    object DataContext { get; set; }
}