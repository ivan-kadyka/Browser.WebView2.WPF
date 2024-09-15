using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PresenterBase.View;

public class UserControlView : UserControl, IView
{
    public async Task Show(CancellationToken token = default)
    {
        Visibility = Visibility.Visible;
        await OnShow(token);
    }

    public async Task Hide(CancellationToken token = default)
    {
        Visibility = Visibility.Hidden;
        await OnHide(token);
    }
    
    protected virtual Task OnShow(CancellationToken token = default)
    {
        return Task.CompletedTask;
    }
    
    protected virtual Task OnHide(CancellationToken token = default)
    {
        return Task.CompletedTask;
    }
}
