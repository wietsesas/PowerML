# Add-CalculateFeatureContribution

Calculate contribution scores for each element of a feature vector.

## Description

Calculate contribution scores for each element of a feature vector.

## Syntax

```
Add-CalculateFeatureContribution [-Transformer] <ISingleFeaturePredictionTransformer<ICalculateFeatureContribution>> [-PositiveContributions <Int32>] [-NegativeContributions <Int32>] [-DontNormalize] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -Transformer

A ISingleFeaturePredictionTransformer that supports Feature Contribution Calculation, and which will also be used for scoring.

```yaml
Type: Microsoft.ML.ISingleFeaturePredictionTransformer<Microsoft.ML.Trainers.ICalculateFeatureContribution>
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PositiveContributions

The number of positive contributions to report, sorted from highest magnitude to lowest magnitude. Note that if there are fewer features with positive contributions than numberOfPositiveContributions, the rest will be returned as zeros.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 10
Accept pipeline input: False
Accept wildcard characters: False
```

### -NegativeContributions

The number of negative contributions to report, sorted from highest magnitude to lowest magnitude. Note that if there are fewer features with negative contributions than numberOfNegativeContributions, the rest will be returned as zeros.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 10
Accept pipeline input: False
Accept wildcard characters: False
```

### -DontNormalize

Whether the feature contributions should be normalized to the [-1, 1] interval.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
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


