using Xunit;
using FluentAssertions;
using System.Data;
using System.Collections.Generic;
using System;
using Moq;

namespace PivotalServices.MoqExtensions.DataReader.Tests
{
    public class MockDataReaderTests
    {
        readonly Mock<IDbConnection> connection;
        readonly Mock<IDbCommand> command;

        public MockDataReaderTests()
        {
            connection = new Mock<IDbConnection>();
            command = new Mock<IDbCommand>();
        }

        [Fact]
        public void Should_Return_Correct_Value_Based_On_Index()
        {
            //Arrange
            var expectedModels = GetExpectedModels();

            //SetupAndGetMockDataReader method describes how to setup the mock IDataReader
            var reader = SetupAndGetMockDataReader(expectedModels);

            connection.Setup(c => c.CreateCommand()).Returns(command.Object);
            command.Setup(c => c.ExecuteReader()).Returns(reader.Object);

            //Act
            var repository = new StubRepository(connection.Object);
            var models = repository.GetStubModels();

            //Assert
            models.Should().BeEquivalentTo(expectedModels);
        }

        private static Mock<IDataReader> SetupAndGetMockDataReader(List<StubModel> expectedModels)
        {
            var reader = new Mock<IDataReader>();

            //Create Mock DataColumns here, new MockDataColumn(string columnName, Type dataType, bool isNullable
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
                new MockDataColumn("nullable_string_Column", typeof(string), true),
                new MockDataColumn("datetime_Column", typeof(DateTime)),
                new MockDataColumn("char_Column", typeof(char)),
                new MockDataColumn("byte_Column", typeof(byte)),
            };

            //Create Mock DataRows here new MockDataRow(params object[] rowValuesInColumnsOrder)
            var rows = new List<MockDataRow>();
            foreach (var expectedModel in expectedModels)
            {
                rows.Add(
                    new MockDataRow(
                        expectedModel.Short,
                        expectedModel.Integer,
                        expectedModel.Long,
                        expectedModel.Decimal,
                        expectedModel.Double,
                        expectedModel.Float,
                        expectedModel.Bool,
                        expectedModel.Boolean,
                        expectedModel.Guid,
                        expectedModel.String,
                        expectedModel.NullableString,
                        expectedModel.DateTime,
                        expectedModel.Char,
                        expectedModel.Byte
                        )
                );
            }

            //Create a datatable using mocked columns and rows
            var table = new MockDataTable(columns, rows);

            //Setup reader to return the data from datatable
            reader.SetupWithReturn(table);
            return reader;
        }

        private static List<StubModel> GetExpectedModels()
        {
            return new List<StubModel>
            {
                new StubModel
                {
                    Short = 1,
                    Integer = 111111,
                    Long = 1111111111111,
                    Decimal = 1111111111.11m,
                    Double = 11111111.11d,
                    Float = 1111.11f,
                    Bool = true,
                    Boolean = true,
                    Guid = Guid.NewGuid(),
                    String = "11111",
                    NullableString = null,
                    DateTime = DateTime.Now,
                    Char = '1',
                    Byte = Convert.ToByte("11"),
                },
                new StubModel
                {
                    Short = 2,
                    Integer = 222222,
                    Long = 222222222222222,
                    Decimal = 22222222222.11m,
                    Double = 22222222222.11d,
                    Float = 2222.11f,
                    Bool = true,
                    Boolean = true,
                    Guid = Guid.NewGuid(),
                    String = "222222222",
                    NullableString = "22222222222",
                    DateTime = DateTime.Now,
                    Char = '2',
                    Byte = Convert.ToByte("22"),
                },
                new StubModel
                {
                    Short = 3,
                    Integer = 33333333,
                    Long = 3333333333333333,
                    Decimal = 333333333333.11m,
                    Double = 33333333.11d,
                    Float = 333333333.11f,
                    Bool = true,
                    Boolean = false,
                    Guid = Guid.NewGuid(),
                    String = "3333333333",
                    NullableString = null,
                    DateTime = DateTime.Now,
                    Char = '3',
                    Byte = Convert.ToByte("33"),
                }
            };
        }

        public class StubRepository
        {
            private readonly IDbConnection connection;

            public StubRepository(IDbConnection connection)
            {
                this.connection = connection;
            }

            public List<StubModel> GetStubModels()
            {
                var cmd = connection.CreateCommand();
                using var reader = cmd.ExecuteReader();

                var models = new List<StubModel>();
                while (reader.Read())
                {
                    models.Add(new StubModel
                    {
                        Short = reader.GetInt16(reader.GetOrdinal("int16_Column")),
                        Integer = reader.GetInt32(reader.GetOrdinal("int32_Column")),
                        Long = reader.GetInt64(reader.GetOrdinal("int64_Column")),
                        Decimal = reader.GetDecimal(reader.GetOrdinal("decimal_Column")),
                        Double = reader.GetDouble(reader.GetOrdinal("double_Column")),
                        Float = reader.GetFloat(reader.GetOrdinal("float_Column")),
                        Bool = reader.GetBoolean(reader.GetOrdinal("bool_Column")),
                        Boolean = reader.GetBoolean(reader.GetOrdinal("boolean_Column")),
                        Guid = reader.GetGuid(reader.GetOrdinal("guid_Column")),
                        String = reader.GetString(reader.GetOrdinal("string_Column")),
                        NullableString = Convert.IsDBNull(reader.GetOrdinal("nullable_string_Column")) ? null : reader.GetString(reader.GetOrdinal("nullable_string_Column")),
                        DateTime = reader.GetDateTime(reader.GetOrdinal("datetime_Column")),
                        Char = reader.GetChar(reader.GetOrdinal("char_Column")),
                        Byte = reader.GetByte(reader.GetOrdinal("byte_Column"))
                    });
                }
                return models;
            }
        }

        public class StubModel
        {
            public short Short { get; set; }
            public int Integer { get; set; }
            public long Long { get; set; }
            public decimal Decimal { get; set; }
            public double Double { get; set; }
            public float Float { get; set; }
            public bool Bool { get; set; }
            public Boolean Boolean { get; set; }
            public Guid Guid { get; set; }
            public string String { get; set; }
            public string NullableString { get; set; }
            public DateTime DateTime { get; set; }
            public char Char { get; set; }
            public byte Byte { get; set; }
        }
    }
}