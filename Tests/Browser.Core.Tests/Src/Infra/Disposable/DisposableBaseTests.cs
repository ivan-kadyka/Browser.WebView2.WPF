namespace Browser.Core.Tests.Infra.Disposable;

public class DisposableBaseTests
{
    [Fact]
    public void Dispose_ShouldSetIsDisposedToTrue()
    {
        // Arrange
        var disposable = new TestDisposable();

        // Act
        disposable.Dispose();

        // Assert
        Assert.True(disposable.DisposedProperly);
    }

    [Fact]
    public void Dispose_ShouldCallDisposeTrueOnce()
    {
        // Arrange
        var disposable = new TestDisposable();

        // Act
        disposable.Dispose();

        // Assert
        Assert.True(disposable.DisposedProperly);
        Assert.True(disposable.DisposedCalled);
    }

    [Fact]
    public void Dispose_CalledMultipleTimes_ShouldNotCallDisposeAgain()
    {
        // Arrange
        var disposable = new TestDisposable();

        // Act
        disposable.Dispose();
        disposable.Dispose();

        // Assert
        Assert.True(disposable.DisposedCalled); 
        Assert.True(disposable.DisposedProperly);
    }

    [Fact]
    public void Finalizer_ShouldCallDisposeWithFalse()
    {
        // Arrange
        bool isFinalizedCalled = false;

        WeakReference CreateWeakRefFunc()
        {
            return new WeakReference(new TestDisposable(() => isFinalizedCalled = true));
        }

        var weakRefToUnit = CreateWeakRefFunc();
        
        // Act
        GC.Collect();
        GC.WaitForPendingFinalizers();

        // Assert
        Assert.True(isFinalizedCalled);
        Assert.False(weakRefToUnit.IsAlive);
    }
    

    [Fact]
    public void Dispose_ShouldSuppressTrueDispose()
    {
        // Arrange
        var disposable = new TestDisposable();

        // Act
        disposable.Dispose();

        // Assert
        Assert.True(disposable.DisposedCalled);
    }

    [Fact]
    public void IsDisposed_ShouldReturnFalseBeforeDispose()
    {
        // Arrange
        var disposable = new TestDisposable();

        // Act & Assert
        Assert.False(disposable.DisposedProperly);
    }
}

