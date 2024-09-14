using System;
using System.Threading;
using System.Threading.Tasks;
using PresenterBase.View;

namespace PresenterBase.Presenter;

public interface IPresenter : IDisposable
{
    IView View { get; }
    
    
    Task Show(CancellationToken token = default);
    
    
    Task Hide(CancellationToken token = default);
}