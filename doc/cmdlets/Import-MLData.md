# Import-MLData

Get data from memory, files or database.

## Description

Get data from memory, files or database.

## Syntax

```
Import-MLData [-Type] <String> -Data <IEnumerable> [-Context <MLContext>] [<CommonParameters>]
Import-MLData [-Type] <String> [-Path] <String[]> [-Separator <Char>] [-HasHeader] [-AllowQuoting] [-TrimWhitespace] [-AllowSparse] [-Context <MLContext>] [<CommonParameters>]
Import-MLData [-Type] <String> -DbProvider <DbProviderFactory> -ConnectionString <String> -Command <String> [-CommandTimeout <Int32>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -Type

The registered data type.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Data

The data from memory.

```yaml
Type: System.Collections.IEnumerable
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path

The path to the data file (wildcards supported).

```yaml
Type: System.String[]
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -Separator

Column separator character.

```yaml
Type: System.Char
Required: False
Position: named
Default value: 	
Accept pipeline input: False
Accept wildcard characters: False
```

### -HasHeader

Whether the file has a header. When true, the loader will skip the first line.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowQuoting

Whether the input may include double-quoted values. This parameter is used to distinguish separator characters in an input value from actual separators. When true, separators within double quotes are treated as part of the input value. When false, all separators, even those whitin quotes, are treated as delimiting a new column. It is also used to distinguish empty values from missing values. When true, missing value are denoted by consecutive separators and empty values by "". When false, empty values are denoted by consecutive separators and missing values by the default missing value for each type documented in DataKind.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -TrimWhitespace

Remove trailing whitespace from lines.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowSparse

Whether the input may include sparse representations. For example, a row containing "5 2:6 4:3" means that there are 5 columns, and the only non-zero are columns 2 and 4, which have values 6 and 3, respectively. Column indices are zero-based, so columns 2 and 4 represent the 3rd and 5th columns. A column may also have dense values followed by sparse values represented in this fashion. For example, a row containing "1 2 5 2:6 4:3" represents two dense columns with values 1 and 2, followed by 5 sparsely represented columns with values 0, 0, 6, 0, and 3. The indices of the sparse columns start from 0, even though 0 represents the third column.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -DbProvider

The factory used to create the DbConnection.

```yaml
Type: System.Data.Common.DbProviderFactory
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConnectionString

The string used to open the connection.

```yaml
Type: System.String
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Command

The text command to run against the data source.

```yaml
Type: System.String
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CommandTimeout

The timeout (in seconds) for the database command.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Context

The context on which to perform the action. If omitted, the current (cached) context will be used.

```yaml
Type: Microsoft.ML.MLContext
Required: False
Position: named
Default value: Current context
Accept pipeline input: False
Accept wildcard characters: False
```

### Common parameters

This cmdlet supports the common parameters: Verbose, Debug, ErrorAction, ErrorVariable, WarningAction, WarningVariable, OutBuffer, PipelineVariable, and OutVariable. For more information, see [about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## Inputs

| Type | Description |
|:---|:---|
| None | This cmdlet does not accept pipeline input. |

## Outputs

| Type | Description |
|:---|:---|
| Microsoft.ML.IDataView | This cmdlet returns a DataView. |


