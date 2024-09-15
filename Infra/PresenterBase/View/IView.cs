using System.Threading;
using System.Threading.Tasks;

namespace PresenterBase.View;

public interface IView
{
    object DataContext { get; set; }
    
    Task Show(CancellationToken token = default);
    
    Task Hide(CancellationToken token = default);

}