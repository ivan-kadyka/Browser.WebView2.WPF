using System;
using System.Threading;
using System.Threading.Tasks;
using PresenterBase.View;

namespace PresenterBase.Presenter;

public interface IPresenter : IDisposable
{
    IView View { get; }


    Task Start(CancellationToken token = default);
    
    
    Task Stop(CancellationToken token = default);
}





