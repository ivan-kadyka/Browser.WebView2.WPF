using System.Reactive.Disposables;
using Reactive.Extensions.Observable;

namespace Browser.Core.Tests.Infra
{
    public class ObservableValueTests : DisposableBase
    {
        private readonly CompositeDisposable _disposables = new();
        
        [Fact]
        public void ObservableValue_Constructor_ShouldSetDefaultValue()
        {
            // Arrange
            var defaultValue = 42;

            // Act
            var observableValue = new ObservableValue<int>(defaultValue);

            // Assert
            Assert.Equal(defaultValue, observableValue.Value);
        }

        [Fact]
        public void ObservableValue_OnNext_ShouldUpdateValue()
        {
            // Arrange
            var defaultValue = 0;
            var observableValue = new ObservableValue<int>(defaultValue);

            // Act
            observableValue.OnNext(10);

            // Assert
            Assert.Equal(10, observableValue.Value);
        }

        [Fact]
        public void ObservableValue_Subscribe_ShouldReceiveInitialAndUpdatedValues()
        {
            // Arrange
            var defaultValue = "Initial";
            var observableValue = new ObservableValue<string>(defaultValue);
            var observer = Substitute.For<IObserver<string>>();

            // Act
            observableValue.Subscribe(observer);
            observableValue.OnNext("Updated");

            // Assert
            observer.Received(1).OnNext("Initial"); // Initial value
            observer.Received(1).OnNext("Updated"); // Updated value
        }

        [Fact]
        public void ObservableValue_OnCompleted_ShouldNotifyObservers()
        {
            // Arrange
            var observableValue = new ObservableValue<int>(0);
            var observer = Substitute.For<IObserver<int>>();

            // Act
            observableValue.Subscribe(observer);
            observableValue.OnCompleted();

            // Assert
            observer.Received(1).OnCompleted();
        }

        [Fact]
        public void ObservableValue_OnError_ShouldNotifyObservers()
        {
            // Arrange
            var observableValue = new ObservableValue<int>(0);
            var observer = Substitute.For<IObserver<int>>();
            var exception = new InvalidOperationException("Test exception");

            // Act
            observableValue.Subscribe(observer);
            observableValue.OnError(exception);

            // Assert
            observer.Received(1).OnError(exception);
        }

        [Fact]
        public void ObservableValue_Dispose_ShouldNotAllowFurtherNotifications()
        {
            // Arrange
            var observableValue = new ObservableValue<int>(0);
            var observer = Substitute.For<IObserver<int>>();

            observableValue.Subscribe(observer);
            
            // Act
            observableValue.Dispose();
            try
            {
                observableValue.OnNext(10);
            }
            catch
            {
                // ignored
            }


            // Assert
            observer.DidNotReceive().OnNext(10); // No more notifications after Dispose
        }

        [Fact]
        public void ObservableValue_Dispose_ShouldCompleteCurrentSubscription()
        {
            // Arrange
            var observableValue = new ObservableValue<int>(0);
            var observer = Substitute.For<IObserver<int>>();

            var subscription = observableValue.Subscribe(observer);
            _disposables.Add(subscription);

            // Act
            observableValue.Dispose();

            // Assert
            Assert.Throws<ObjectDisposedException>(() => observableValue.OnNext(20));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _disposables.Dispose();
        }
    }
}
