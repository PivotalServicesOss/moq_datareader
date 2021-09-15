using System;

namespace Moq
{
    public class MockDataColumn
    {
        public MockDataColumn(string name, Type type, bool isNullable = false)
        {
            Name = name;
            Type = type;
            IsNullable = isNullable;
        }

        public string Name { get; }
        public Type Type { get; }
        public bool IsNullable { get; }
    }
}