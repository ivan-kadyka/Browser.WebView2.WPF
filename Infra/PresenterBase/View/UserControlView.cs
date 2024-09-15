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
        await Task.CompletedTask;
    }

    public async Task Hide(CancellationToken token = default)
    {
        Visibility = Visibility.Hidden;
        await Task.CompletedTask;
    }
}
