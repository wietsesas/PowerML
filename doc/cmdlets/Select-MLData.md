# Select-MLData

Select a subset of data in a DataView.

## Description

Select a subset of data in a DataView.

## Syntax

```
Select-MLData -Data <IDataView> [-ByColumn <String>] [-ByKeyColumnFraction <String>] [-ByMissingValues <String[]>] [-ByColumnLowerBound <Double>] [-ByKeyColumnFractionLowerBound <Double>] [-ByColumnUpperBound <Double>] [-ByKeyColumnFractionUpperBound <Double>] [-Skip <Int64>] [-Take <Int64>] [-Shuffle] [-ShuffleSeed <Nullable<Int32>>] [-ShufflePoolSize <Int32>] [-DontShuffleSource] [-Cache <String[]>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

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

### -ByColumn

The name of a column to use for filtering.

```yaml
Type: System.String
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -ByKeyColumnFraction

The name of a column to use for filtering.

```yaml
Type: System.String
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -ByMissingValues

Name of the columns to filter on. If a row is has a missing value in any of these columns, it will be dropped from the dataset.

```yaml
Type: System.String[]
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -ByColumnLowerBound

The inclusive lower bound for FilterByColumn.

```yaml
Type: System.Double
Required: False
Position: named
Default value: -Infinity
Accept pipeline input: False
Accept wildcard characters: False
```

### -ByKeyColumnFractionLowerBound

The inclusive lower bound for FilterByKeyColumnFraction.

```yaml
Type: System.Double
Required: False
Position: named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -ByColumnUpperBound

The exclusive upper bound for FilterByColumn.

```yaml
Type: System.Double
Required: False
Position: named
Default value: Infinity
Accept pipeline input: False
Accept wildcard characters: False
```

### -ByKeyColumnFractionUpperBound

The exclusive upper bound for FilterByKeyColumnFraction.

```yaml
Type: System.Double
Required: False
Position: named
Default value: 1
Accept pipeline input: False
Accept wildcard characters: False
```

### -Skip

Skip count rows in input.

```yaml
Type: System.Int64
Required: False
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Take

Take count rows from input.

```yaml
Type: System.Int64
Required: False
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Shuffle

Shuffle the rows of input.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShuffleSeed

The random seed. If unspecified, the random seed will be instead derived from the Context.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShufflePoolSize

The number of rows to hold in the pool. Setting this to 1 will turn off pool shuffling and will only perform a shuffle by reading input in a random order.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 1000
Accept pipeline input: False
Accept wildcard characters: False
```

### -DontShuffleSource

If false, the transform will not attempt to read input in a random order and only use pooling to shuffle. This parameter has no effect if the CanShuffle property of input is false.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Cache

The columns that must be cached whenever anything is cached. An empty array or null value means that columns are cached upon their first access.
Only applied when the parameter is used.

```yaml
Type: System.String[]
Required: False
Position: named
Default value: null
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
| Microsoft.ML.IDataView | This cmdlet returns a DataView. |


