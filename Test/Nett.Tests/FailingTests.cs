using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Nett.Tests
{
    [ExcludeFromCodeCoverage]
    public class FailingTests
    {
        private class Foo
        {
            public DateTime Timestamp { get; set; }
        }

        [Fact]
        public void WriteAndReadDateTime()
        {
            // Arrange
            var subject = new Foo { Timestamp = DateTime.Now };
            var expected = Toml.WriteString(subject);

            // Act
            var result = Toml.WriteString(Toml.ReadString<Foo>(expected));

            // Assert
            result.Should().Be(expected);
        }

        private class Parent
        {
            public Child Child { get; set; }
        }

        private class Child
        {
            public Parent Parent { get; set; }
        }

        [Fact(Skip = "Stack overflow exception occurred in test. Test run is aborted.")]
        public void WriteCircularReference()
        {
            // Arrange
            var subject = new Parent();
            subject.Child = new Child { Parent = subject };

            // Act
            Toml.WriteString(subject);
        }
    }
}