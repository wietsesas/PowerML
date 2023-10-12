# Add-DetectAnomalyBySrCnnTransform

Detect anomalies in the input time series data using the Spectral Residual (SR) algorithm.

## Description

Detect anomalies in the input time series data using the Spectral Residual (SR) algorithm.

## Syntax

```
Add-DetectAnomalyBySrCnnTransform [-OutputColumn] <String> [-InputColumn] <String> [-WindowSize <Int32>] [-BackAddWindowSize <Int32>] [-LookaheadWindowSize <Int32>] [-AveragingWindowSize <Int32>] [-JudgementWindowSize <Int32>] [-Threshold <Double>] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. The column data is a vector of Double. The vector contains 3 elements: alert (1 means anomaly while 0 means normal), raw score, and magnitude of spectual residual.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of column to transform. The column data must be Single.

```yaml
Type: System.String
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WindowSize

The size of the sliding window for computing spectral residual.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 64
Accept pipeline input: False
Accept wildcard characters: False
```

### -BackAddWindowSize

The number of points to add back of training window. No more than windowSize, usually keep default value.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 5
Accept pipeline input: False
Accept wildcard characters: False
```

### -LookaheadWindowSize

The number of pervious points used in prediction. No more than windowSize, usually keep default value.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 5
Accept pipeline input: False
Accept wildcard characters: False
```

### -AveragingWindowSize

The size of sliding window to generate a saliency map for the series. No more than windowSize, usually keep default value.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 3
Accept pipeline input: False
Accept wildcard characters: False
```

### -JudgementWindowSize

The size of sliding window to calculate the anomaly score for each data point. No more than windowSize.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 21
Accept pipeline input: False
Accept wildcard characters: False
```

### -Threshold

The threshold to determine anomaly, score larger than the threshold is considered as anomaly. Should be in (0,1)

```yaml
Type: System.Double
Required: False
Position: named
Default value: 0.3
Accept pipeline input: False
Accept wildcard characters: False
```

### -AppendTo

Append the created estimator to the end of this chain.

```yaml
Type: Microsoft.ML.Data.EstimatorChain<Microsoft.ML.ITransformer>
Required: False
Position: named
Default value: null
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -AppendScope

The scope allows for 'tagging' the estimators (and subsequently transformers) in the chain to be used 'only for training', 'for training and evaluation' etc.

```yaml
Type: Microsoft.ML.Data.TransformerScope
Required: False
Position: named
Default value: Everything
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
| Microsoft.ML.Data.EstimatorChain<Microsoft.ML.ITransformer> | You can pipe the EstimatorChain to append to this cmdlet. |

## Outputs

| Type | Description |
|:---|:---|
| Microsoft.ML.Data.EstimatorChain<Microsoft.ML.ITransformer> | This cmdlet returns the appended EstimatorChain. |


