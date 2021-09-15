using System.Collections.Generic;

namespace Moq
{
    public class MockDataTable
    {
        public MockDataTable() : this(new List<MockDataColumn>(), new List<MockDataRow>()) {}

        public MockDataTable(List<MockDataColumn> columns, List<MockDataRow> rows)
        {
            Columns = columns;
            Rows = rows;
        }

        public List<MockDataColumn> Columns { get; private set; }
        public List<MockDataRow> Rows { get; private set; }
    }
}