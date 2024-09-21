namespace Disposable;

using System;
using System.Threading;
#if PROFILING_ENABLED || DEBUG
using System.Diagnostics;
#endif
    
public abstract class DisposableBase : IDisposable
{
    private int _disposed;

    protected bool IsDisposed => _disposed == 1;
            
    ~DisposableBase()
    {
        
        
#if PROFILING_ENABLED || DEBUG
        if (Debugger.IsAttached)
        {
            Debug.WriteLine($"Executing Dispose from finalizer call for object '{GetType().FullName}'");
            Debugger.Break();
        }
#endif
        Dispose(false);
    }


    #region IDisposable implementation

    public void Dispose()
    {
        var disposed = Interlocked.CompareExchange(ref _disposed, 1, 0);

        if (disposed == 0)
        {
            GC.SuppressFinalize(this);
            Dispose(true);

            _disposed |= 2;
        }
        else
        {
            
          
#if PROFILING_ENABLED || DEBUG
            if (Debugger.IsAttached)
            {
                Debug.WriteLine($"Double disposed object '{GetType().FullName}'");
                Debugger.Break();
            }    
#endif
        }
    }

    #endregion

    protected abstract void Dispose(bool disposing);
}
