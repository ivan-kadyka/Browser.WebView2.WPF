namespace Browser.Core.Tests.Infra.Disposable;

public class TestDisposable : DisposableBase
{
    public bool DisposedProperly { get; private set; }
    public bool DisposedCalled { get; private set; }
    public event Action? FinalizerCalled;

    public TestDisposable(Action? onFinalized = null)
    {
        FinalizerCalled = onFinalized;
    }

    protected override void Dispose(bool disposing)
    {
        DisposedCalled = true;
        DisposedProperly = disposing;

        if (!disposing)
            FinalizerCalled?.Invoke();
    }
}