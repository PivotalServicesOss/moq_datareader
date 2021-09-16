using Xunit;
using FluentAssertions;

namespace MoqExtensions.DataReader.Tests
{
    public class MockDataColumnTests
    {
        [Fact]
        public void Should_Default_IsNullable_To_False()
        {
            var column = new MockDataColumn("name", typeof(string));
            column.IsNullable.Should().Be(false);
        }

        [Fact]
        public void Should_Return_Same_Instances_And_Values()
        {
            var name = "Column1";
            var type = typeof(string);
            var isNullable = true;

            var column = new MockDataColumn(name, type, isNullable);

            column.Name.Should().BeSameAs(name);
            column.Type.Should().BeSameAs(type);
            column.IsNullable.Should().Be(isNullable);
        }
    }
}