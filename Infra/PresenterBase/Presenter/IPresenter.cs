using System;
using System.Threading;
using System.Threading.Tasks;

namespace PresenterBase.Presenter;

public interface IPresenter :  IDisposable
{
    object Content { get; }
    
    Task Start(CancellationToken token = default);
    
    
    Task Stop(CancellationToken token = default);
}





