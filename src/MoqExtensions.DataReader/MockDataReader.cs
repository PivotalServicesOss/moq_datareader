using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Moq
{
    public static class MockDataReader
    {
        public static void SetupWithReturn(this Mock<IDataReader> reader, MockDataTable table)
        {
            var columns = new List<string>();
            var rows = new List<List<Tuple<object, string, bool>>>();
            var rowIndex = 0;

            columns.AddRange(from column in table.Columns select column.Name);
            
            foreach (var row in table.Rows)
            {
                var rowItem = new List<Tuple<object, string, bool>>();
                for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++)
                {
                    rowItem.Add(new Tuple<object, string, bool>(
                        row[columnIndex], 
                        table.Columns[columnIndex].Type.Name, 
                        table.Columns[columnIndex].IsNullable));
                }
                rows.Add(rowItem);
            }

            for (int i = 0; i < columns.Count; i++)
            {
                var name = columns[i];
                reader.Setup(r => r.GetOrdinal(It.Is<string>(n => n == name))).Returns(i);
            }

            reader.Setup(r => r.Read()).Returns(() => rowIndex < rows.Count).Callback(() => SetupNextRow(reader, rows, ref rowIndex));
        }

        private static void SetupNextRow(Mock<IDataReader> reader, List<List<Tuple<object, string, bool>>> rows, ref int rowIndex)
        {
            if(rowIndex >= rows.Count) return;

            var row = rows[rowIndex];

            for (int columnIndex = 0; columnIndex < row.Count; columnIndex++)
            {
                var column = row[columnIndex];
                var index = columnIndex;

                switch (column.Item2.ToLower())
                {
                    case "int16":
                        reader.Setup(r => r.GetInt16(It.Is<int>(x => x == index))).Returns((short)column.Item1);
                    break;
                    case "int32":
                        reader.Setup(r => r.GetInt32(It.Is<int>(x => x == index))).Returns((int)column.Item1);
                    break;
                    case "int64":
                        reader.Setup(r => r.GetInt64(It.Is<int>(x => x == index))).Returns((long)column.Item1);
                    break;
                    case "decimal":
                        reader.Setup(r => r.GetDecimal(It.Is<int>(x => x == index))).Returns((decimal)column.Item1);
                    break;
                    case "double":
                        reader.Setup(r => r.GetDouble(It.Is<int>(x => x == index))).Returns((double)column.Item1);
                    break;
                    case "single":
                        reader.Setup(r => r.GetFloat(It.Is<int>(x => x == index))).Returns((float)column.Item1);
                    break;
                    case "bool":
                    case "boolean":
                        reader.Setup(r => r.GetBoolean(It.Is<int>(x => x == index))).Returns((bool)column.Item1);
                    break;
                    case "guid":
                        reader.Setup(r => r.GetGuid(It.Is<int>(x => x == index))).Returns((Guid)column.Item1);
                    break;
                    case "string":
                        reader.Setup(r => r.GetString(It.Is<int>(x => x == index))).Returns((string)column.Item1);
                    break;
                    case "datetime":
                        reader.Setup(r => r.GetDateTime(It.Is<int>(x => x == index))).Returns((DateTime)column.Item1);
                    break;
                    case "char":
                        reader.Setup(r => r.GetChar(It.Is<int>(x => x == index))).Returns((char)column.Item1);
                    break;
                    case "byte":
                        reader.Setup(r => r.GetByte(It.Is<int>(x => x == index))).Returns((byte)column.Item1);
                    break;
                    default:
                        throw new NotImplementedException(column.Item2.ToLower());
                }
            }
            rowIndex++;
        }
    }
}