using Xunit;
using Microsoft.Extensions.Options;
using System;
using IOptionsDemo.Configuration;
using IOptionsDemo.Services;

namespace IOptionsDemo.Tests
{
    public class MonitorOptionsServiceTests
    {
        private class TestOptionsMonitor<T> : IOptionsMonitor<T>
        {
            private T _currentValue;
            private Action<T, string>? _onChange;

            public TestOptionsMonitor(T initialValue)
            {
                _currentValue = initialValue;
            }

            public T CurrentValue => _currentValue;

            public T Get(string? name) => _currentValue;

            public IDisposable OnChange(Action<T, string> listener)
            {
                _onChange = listener;
                return new DummyDisposable();
            }

            public void TriggerChange(T newValue)
            {
                _currentValue = newValue;
                _onChange?.Invoke(newValue, string.Empty);
            }

            private class DummyDisposable : IDisposable
            {
                public void Dispose() { }
            }
        }

        [Fact]
        public void GetMessage_ReturnsInitialValue()
        {
            // Arrange
            var initialOptions = new FeatureOptions
            {
                Enabled = true,
                Message = "Initial message"
            };

            var monitor = new TestOptionsMonitor<FeatureOptions>(initialOptions);
            var service = new MonitorOptionsService(monitor);

            // Act
            var result = service.GetMessage();

            // Assert
            Assert.Equal("Initial message", result);
        }

        [Fact]
        public void GetMessage_ReflectsUpdatedValue_AfterChange()
        {
            // Arrange
            var initialOptions = new FeatureOptions
            {
                Enabled = true,
                Message = "Initial message"
            };

            var updatedOptions = new FeatureOptions
            {
                Enabled = true,
                Message = "Updated message"
            };

            var monitor = new TestOptionsMonitor<FeatureOptions>(initialOptions);
            var service = new MonitorOptionsService(monitor);

            // Act
            monitor.TriggerChange(updatedOptions);
            var result = service.GetMessage();

            // Assert
            Assert.Equal("Updated message", result);
        }

        [Fact]
        public void GetMessage_ReturnsDisabledMessage_WhenFeatureIsOff()
        {
            // Arrange
            var options = new FeatureOptions
            {
                Enabled = false,
                Message = "Should not be shown"
            };

            var monitor = new TestOptionsMonitor<FeatureOptions>(options);
            var service = new MonitorOptionsService(monitor);

            // Act
            var result = service.GetMessage();

            // Assert
            Assert.Equal("Feature disabled", result);
        }
    }
}
