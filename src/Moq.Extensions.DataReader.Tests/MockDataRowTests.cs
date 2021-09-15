using Xunit;
using FluentAssertions;

namespace Moq.Extensions.DataReader.Tests
{
    public class MockDataRowTests
    {
        [Fact]
        public void Should_Return_Correct_Value_Based_On_Index()
        {
            var row = new MockDataRow("value1", "value2", 0, true);

            row[0].Should().Be("value1");
            row[1].Should().Be("value2");
            row[2].Should().Be(0);
            row[3].Should().Be(true);
        }
    }
}