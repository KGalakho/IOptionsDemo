using Xunit;
using Microsoft.Extensions.Options;
using IOptionsDemo.Configuration;
using IOptionsDemo.Services;

namespace IOptionsDemo.Tests
{
    public class SnapshotOptionsServiceTests
    {
        private class TestOptionsSnapshot<T> : IOptionsSnapshot<T> where T : class
        {
            private readonly T _value;

            public TestOptionsSnapshot(T value)
            {
                _value = value;
            }

            public T Value => _value;

            public T Get(string? name) => _value;
        }

        [Fact]
        public void GetMessage_ReturnsConfiguredMessage()
        {
            // Arrange
            var options = new FeatureOptions
            {
                Enabled = true,
                Message = "Snapshot message"
            };

            var snapshot = new TestOptionsSnapshot<FeatureOptions>(options);
            var service = new SnapshotOptionsService(snapshot);

            // Act
            var result = service.GetMessage();

            // Assert
            Assert.Equal("Snapshot message", result);
        }

        [Fact]
        public void GetMessage_ReturnsUpdatedMessage_WhenSnapshotChanges()
        {
            // Arrange
            var first = new FeatureOptions { Enabled = true, Message = "First" };
            var second = new FeatureOptions { Enabled = true, Message = "Second" };

            var snapshot = new TestOptionsSnapshot<FeatureOptions>(first);
            var service = new SnapshotOptionsService(snapshot);

            // Simuler un changement de configuration (dans un vrai test, cela nécessiterait un scope différent)
            var updatedSnapshot = new TestOptionsSnapshot<FeatureOptions>(second);
            var updatedService = new SnapshotOptionsService(updatedSnapshot);

            // Assert
            Assert.Equal("First", service.GetMessage());
            Assert.Equal("Second", updatedService.GetMessage());
        }

        [Fact]
        public void GetMessage_ReturnsMessage_WhenFeatureIsDisabled()
        {
            // Arrange
            var options = new FeatureOptions
            {
                Enabled = false,
                Message = "Should not be shown"
            };

            var snapshot = new TestOptionsSnapshot<FeatureOptions>(options);
            var service = new SnapshotOptionsService(snapshot);

            // Act
            var result = service.GetMessage();

            // Assert
            Assert.Equal("Feature disabled", result);
        }
    }
}
