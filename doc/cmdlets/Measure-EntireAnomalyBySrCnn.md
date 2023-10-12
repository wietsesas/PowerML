# Measure-EntireAnomalyBySrCnn

Detect timeseries anomalies for entire input using SRCNN algorithm.

## Description

Detect timeseries anomalies for entire input using SRCNN algorithm.

## Syntax

```
Measure-EntireAnomalyBySrCnn -Data <IDataView> [-OutputColumn] <String> [-InputColumn] <String> [-Threshold <Double>] [-BatchSize <Int32>] [-Sensitivity <Double>] [-DetectMode <SrCnnDetectMode>] [-Period <Int32>] [-DeseasonalityMode <SrCnnDeseasonalityMode>] [-Context <MLContext>] [<CommonParameters>]
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

### -OutputColumn

Name of the column resulting from data processing of inputColumnName. The column data is a vector of Double. The length of this vector varies depending on DetectMode.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of column to process. The column data must be Double.

```yaml
Type: System.String
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Threshold

The threshold to determine an anomaly. An anomaly is detected when the calculated SR raw score for a given point is more than the set threshold. This threshold must fall between [0,1]. The default value is 0.3.

```yaml
Type: System.Double
Required: False
Position: named
Default value: 0.3
Accept pipeline input: False
Accept wildcard characters: False
```

### -BatchSize

Divide the input data into batches to fit srcnn model. When set to -1, use the whole input to fit model instead of batch by batch, when set to a positive integer, use this number as batch size. Must be -1 or a positive integer no less than 12. The default value is 1024.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 1024
Accept pipeline input: False
Accept wildcard characters: False
```

### -Sensitivity

Sensitivity of boundaries, only useful when srCnnDetectMode is AnomalyAndMargin. Must be in [0,100]. The default value is 99.

```yaml
Type: System.Double
Required: False
Position: named
Default value: 99
Accept pipeline input: False
Accept wildcard characters: False
```

### -DetectMode

An enum type of SrCnnDetectMode. When set to AnomalyOnly, the output vector would be a 3-element Double vector of (IsAnomaly, RawScore, Mag). When set to AnomalyAndExpectedValue, the output vector would be a 4-element Double vector of (IsAnomaly, RawScore, Mag, ExpectedValue). When set to AnomalyAndMargin, the output vector would be a 7-element Double vector of (IsAnomaly, AnomalyScore, Mag, ExpectedValue, BoundaryUnit, UpperBoundary, LowerBoundary). The RawScore is output by SR to determine whether a point is an anomaly or not, under AnomalyAndMargin mode, when a point is an anomaly, an AnomalyScore will be calculated according to sensitivity setting. The default value is AnomalyOnly.

```yaml
Type: Microsoft.ML.TimeSeries.SrCnnDetectMode
Required: False
Position: named
Default value: AnomalyOnly
Accept pipeline input: False
Accept wildcard characters: False
```

### -Period

The period of the series. The default value is 0.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -DeseasonalityMode

The Deseasonality modes of SrCnn models. The de-seasonality mode is invoked when the period of the series is greater than 0. The default value is Stl.

```yaml
Type: Microsoft.ML.TimeSeries.SrCnnDeseasonalityMode
Required: False
Position: named
Default value: Stl
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
| Microsoft.ML.IDataView | This cmdlet returns the transformed data view. |


