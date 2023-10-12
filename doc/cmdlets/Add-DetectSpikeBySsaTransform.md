# Add-DetectSpikeBySsaTransform

Detect spikes in time series data using singular spectrum analysis (SSA).

## Description

Detect spikes in time series data using singular spectrum analysis (SSA).

## Syntax

```
Add-DetectSpikeBySsaTransform [-OutputColumn] <String> [-InputColumn] <String> [-Confidence] <Double> [-PValueHistoryLength] <Int32> [-TrainingWindowSize] <Int32> [-SeasonalityWindowSize] <Int32> [-Side <AnomalySide>] [-ErrorFunction <ErrorFunction>] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. The column data is a vector of Double. The vector contains 3 elements: alert (non-zero value means a spike), raw score, and p-value.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of column to transform. The column data must be Single. If set to null, the value of the outputColumnName will be used as source.

```yaml
Type: System.String
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confidence

The confidence for spike detection in the range [0, 100].

```yaml
Type: System.Double
Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PValueHistoryLength

The size of the sliding window for computing the p-value.

```yaml
Type: System.Int32
Required: True
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TrainingWindowSize

The number of points from the beginning of the sequence used for training.

```yaml
Type: System.Int32
Required: True
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SeasonalityWindowSize

An upper bound on the largest relevant seasonality in the input time-series.

```yaml
Type: System.Int32
Required: True
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Side

The argument that determines whether to detect positive or negative anomalies, or both.

```yaml
Type: Microsoft.ML.Transforms.TimeSeries.AnomalySide
Required: False
Position: named
Default value: TwoSided
Accept pipeline input: False
Accept wildcard characters: False
```

### -ErrorFunction

The function used to compute the error between the expected and the observed value.

```yaml
Type: Microsoft.ML.Transforms.TimeSeries.ErrorFunction
Required: False
Position: named
Default value: SignedDifference
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


