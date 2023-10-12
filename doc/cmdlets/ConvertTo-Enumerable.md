# ConvertTo-Enumerable

Convert an IDataView to an enumerable list.

## Description

Convert an IDataView to an enumerable list.

## Syntax

```
ConvertTo-Enumerable [-Type] <String> -Data <IDataView> [-Skip <Int32>] [-SkipLast <Int32>] [-Take <Int32>] [-TakeLast <Int32>] [-ElementAt <Int32>] [-First] [-Last] [-IgnoreMissingColumns] [-Context <MLContext>] [<CommonParameters>]
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

The input data.

```yaml
Type: Microsoft.ML.IDataView
Required: True
Position: named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Skip

Skips a specified number of elements from the start of the data.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipLast

Skips a specified number of elements from the end of the data.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Take

Returns a specified number of elements from the start of the data.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TakeLast

Returns a specified number of elements from the end of the data.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ElementAt

Returns the element at the specified index in the data.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -First

Returns one element from the start the data.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Last

Returns one element from the end the data.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IgnoreMissingColumns

Whether to ignore the case when a requested column is not present in the data view.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
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
| Microsoft.ML.IDataView | You can pipe a data view to this cmdlet. |

## Outputs

| Type | Description |
|:---|:---|
| System.Collections.Generic.IEnumerable<System.Object> | This cmdlet returns the dataview as an enumerable. |
| System.Object | This cmdlet returns an element of the dataview. |


