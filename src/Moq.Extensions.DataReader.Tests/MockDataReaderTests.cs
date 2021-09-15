using Xunit;
using FluentAssertions;
using System.Data;
using System.Collections.Generic;
using System;

namespace Moq.Extensions.DataReader.Tests
{
    public class MockDataReaderTests
    {
        [Fact]
        public void Should_Return_Correct_Value_Based_On_Index()
        {
            var reader = new Mock<IDataReader>();
            
            var columns = new List<MockDataColumn> 
            { 
                new MockDataColumn("int16_Column", typeof(short)),
                new MockDataColumn("int32_Column", typeof(int)),
                new MockDataColumn("int64_Column", typeof(long)),
                new MockDataColumn("decimal_Column", typeof(decimal)),
                new MockDataColumn("double_Column", typeof(double)),
                new MockDataColumn("float_Column", typeof(float)),
                new MockDataColumn("bool_Column", typeof(bool)),
                new MockDataColumn("boolean_Column", typeof(Boolean)),
                new MockDataColumn("guid_Column", typeof(Guid)),
                new MockDataColumn("string_Column", typeof(string)),
                new MockDataColumn("datetime_Column", typeof(DateTime)),
                new MockDataColumn("char_Column", typeof(char)),
                new MockDataColumn("byte_Column", typeof(byte)),
            };

            var rows = new List<MockDataRow> 
            { 
                new MockDataRow(1, 111111, 1111111111111, 1111111111.11m, 11111111.11d, 1111.11, true, true, Guid.NewGuid(), "11111", DateTime.Now, '1', Convert.ToByte("11")),
                new MockDataRow(2, 222222, 2222222222222, 2222222222.22m, 22222222.22d, 2222.22, true, true, Guid.NewGuid(), "22222", DateTime.Now, '2', Convert.ToByte("22")),
                new MockDataRow(3, 333333, 3333333333333, 3333333333.33m, 33333333.33d, 3333.33, true, false, Guid.NewGuid(), "33333", DateTime.Now, '3', Convert.ToByte("33")),
            };

            var table = new MockDataTable(columns, rows);

            reader.SetupWithReturn(table);
        }
    }
}