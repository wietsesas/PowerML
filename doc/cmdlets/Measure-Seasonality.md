# Measure-Seasonality

Detects this predictable interval (or period) by adopting techniques of fourier analysis.

## Description

In time series data, seasonality (or periodicity) is the presence of variations that occur at specific regular intervals, such as weekly, monthly, or quarterly.
This method detects this predictable interval (or period) by adopting techniques of fourier analysis. Assuming the input values have the same time interval (e.g., sensor data collected at every second ordered by timestamps), this method takes a list of time-series data, and returns the regular period for the input seasonal data, if a predictable fluctuation or pattern can be found that recurs or repeats over this period throughout the input values.
Returns -1 if no such pattern is found, that is, the input values do not follow a seasonal fluctuation.

## Syntax

```
Measure-Seasonality -Data <IDataView> [-InputColumn] <String> [-WindowSize <Int32>] [-RandomnessThreshold <Double>] [-Context <MLContext>] [<CommonParameters>]
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

### -InputColumn

Name of column to process. The column data must be Double.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WindowSize

An upper bound on the number of values to be considered in the input values. When set to -1, use the whole input to fit model; when set to a positive integer, only the first windowSize number of values will be considered. The default value is -1.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: -1
Accept pipeline input: False
Accept wildcard characters: False
```

### -RandomnessThreshold

Randomness threshold that specifies how confidently the input values follow a predictable pattern recurring as seasonal data. The range is between [0, 1]. The default value is 0.95.

```yaml
Type: System.Double
Required: False
Position: named
Default value: 0.95
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
| System.Int32 | This cmdlet returns the regular interval for the input as seasonal data, otherwise return -1. |


