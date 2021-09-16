using Xunit;
using System.Collections.Generic;
using FluentAssertions;

namespace MoqExtensions.DataReader.Tests
{
    public class MockDataTableTests
    {
        [Fact]
        public void Empty_Constructor_Should_Return_Empty_Coliumns_AndRows()
        {
            var table = new MockDataTable();

            table.Columns.Should().NotBeNull();
            table.Columns.Should().BeEmpty();
            
            table.Rows.Should().NotBeNull();
            table.Rows.Should().BeEmpty();
        }

        [Fact]
        public void Should_Return_Same_Instance_Of_Column_And_Rows()
        {
            var columns = new List<MockDataColumn> { new MockDataColumn(string.Empty, typeof(string)) };
            var rows = new List<MockDataRow> { new MockDataRow(string.Empty) };
            var table = new MockDataTable(columns, rows);

            table.Columns.Should().BeSameAs(columns);
            table.Rows.Should().BeSameAs(rows);
        }
    }
}