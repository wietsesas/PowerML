# Add-DetectIidChangePointTransform

Detect change points in independent and identically distributed (IID) time series data using adaptive kernel density estimations and martingale scores.

## Description

Detect change points in independent and identically distributed (IID) time series data using adaptive kernel density estimations and martingale scores.

## Syntax

```
Add-DetectIidChangePointTransform [-OutputColumn] <String> [-InputColumn] <String> [-Confidence] <Double> [-ChangeHistoryLength] <Int32> [-Martingale <MartingaleType>] [-Eps <Double>] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. The column data is a vector of Double. The vector contains 4 elements: alert (non-zero value means a change point), raw score, p-Value and martingale score.

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

The confidence for change point detection in the range [0, 100].

```yaml
Type: System.Double
Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ChangeHistoryLength

The length of the sliding window on p-values for computing the martingale score.

```yaml
Type: System.Int32
Required: True
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Martingale

The martingale used for scoring.

```yaml
Type: Microsoft.ML.Transforms.TimeSeries.MartingaleType
Required: False
Position: named
Default value: Power
Accept pipeline input: False
Accept wildcard characters: False
```

### -Eps

The epsilon parameter for the Power martingale.

```yaml
Type: System.Double
Required: False
Position: named
Default value: 0.1
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


