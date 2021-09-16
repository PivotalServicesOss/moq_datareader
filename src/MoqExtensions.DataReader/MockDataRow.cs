using System.Collections.Generic;
using System.Linq;

namespace Moq
{
    public class MockDataRow
    {
        private readonly List<object> _values;

        public MockDataRow(params object[] values)
        {
            _values = values.ToList();
        }

        public object this[int index]
        {
            get
            {
                return _values[index];
            }
        }
    }
}