using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PresenterBase.View;

public class WindowView : Window, IView
{
    public async Task Show(CancellationToken token = default)
    {
        base.Show();
        await Task.CompletedTask;
    }

    public async Task Hide(CancellationToken token = default)
    {
        Close();
        await Task.CompletedTask;
    }
}
