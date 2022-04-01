### A simple Moq extensions library that helps in mocking `IDataReader`

Build | MoqExtensions.DataReader |
--- | --- |
[![Nuget (Prod Release)](https://github.com/alfusinigoj/moq_datareader/actions/workflows/prod-release-pipeline.yml/badge.svg)](https://github.com/alfusinigoj/moq_datareader/actions/workflows/prod-release-pipeline.yml) | [![NuGet](https://img.shields.io/nuget/v/MoqExtensions.DataReader.svg?style=flat-square)](http://www.nuget.org/packages/MoqExtensions.DataReader)

### Salient features
- Works with [Moq](https://github.com/Moq/moq4/wiki/Quickstart)
- Provides extensions to mock `IDataReader` using [Moq](https://github.com/Moq/moq4/wiki/Quickstart), especially when unit testing `Repositories`
- Supports most of the data types `(string, int, short, long, float, decimal, double, guid, datetime, bool, char and byte)`

### Package
- Extensions package - [MoqExtensions.DataReader](https://www.nuget.org/packages/MoqExtensions.DataReader)

### Usage Instructions
- Install package [MoqExtensions.DataReader](https://www.nuget.org/packages/MoqExtensions.DataReader)
- Create a `DataTable` as below with the `data` and `schema` as supposed to be returned by the query used by `DataReader`
  
```c#
    var mockDataTable = new MockDataTable();

    //Add Columns (schema)
    mockDataTable.Columns.Add(new MockDataColumn("Column1", typeof(string)));
    mockDataTable.Columns.Add(new MockDataColumn("Column2", typeof(string)));
    mockDataTable.Columns.Add(new MockDataColumn("Column3", typeof(string), true));

    //Add Rows (data) - column order is important here
    mockDataTable.Rows.Add(new MockDataRow("Row1_Column1_Value", "Row1_Column2_Value", "Row1_Column3_Value"));
    mockDataTable.Rows.Add(new MockDataRow("Row2_Column1_Value", "Row2_Column2_Value", "Row2_Column3_Value"));
    mockDataTable.Rows.Add(new MockDataRow("Row3_Column1_Value", "Row3_Column2_Value", "Row3_Column3_Value"));
    mockDataTable.Rows.Add(new MockDataRow("Row4_Column1_Value", "Row4_Column2_Value", "Row4_Column3_Value"));
```

- Create a mock datareader as below using [Moq](https://github.com/Moq/moq4/wiki/Quickstart) and setup using the extension method `SetupWithReturn`
  
```c#
    var reader = new Mock<IDataReader>();

    //Setup reader to return the data from datatable
    reader.SetupWithReturn(mockDataTable);
```

- You should be all set to use your mocked reader for unit testing a `Repository` or any class that uses `IDataReader`. Sample implementation given below.

```c#
    while (reader.Read())
    {
        models.Add(new Model
        {
            Column1 = reader.GetString(reader.GetOrdinal("Column1")),
            Column2 = reader.GetString(reader.GetOrdinal("Column2")),
            Column3 = Convert.IsDBNull(reader.GetOrdinal("Column3")) ? null : reader.GetString(reader.GetOrdinal("Column3")),
        });
    }   
```

### Issues
- Kindly raise any issues under [GitHub Issues](https://github.com/alfusinigoj/moq_datareader/issues)

### Release Info

#### 1.0.1
- Added additional framework support `(net45;netstandard2.0;netstandard2.1;net5.0)`

#### 1.0.0
- Initial Release

### Contributions are welcome!
