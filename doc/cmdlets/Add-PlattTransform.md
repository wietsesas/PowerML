# Add-PlattTransform

Transforms a binary classifier raw score into a class probability using logistic regression with fixed parameters or parameters estimated using the training data.

## Description

Transforms a binary classifier raw score into a class probability using logistic regression with fixed parameters or parameters estimated using the training data.

## Syntax

```
Add-PlattTransform [[-LabelColumn] <String>] [[-ScoreColumn] <String>] [[-ExampleWeightColumn] <String>] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
Add-PlattTransform [-ScoreColumn <String>] -Slope <Double> -Offset <Double> [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -LabelColumn

The name of the label column.

```yaml
Type: System.String
Required: False
Position: 0
Default value: Label
Accept pipeline input: False
Accept wildcard characters: False
```

### -ScoreColumn

The name of the score column.

```yaml
Type: System.String
Required: False
Position: 1
Default value: Score
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExampleWeightColumn

The name of the example weight column (optional).

```yaml
Type: System.String
Required: False
Position: 2
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -Slope

The slope in the function of the exponent of the sigmoid.

```yaml
Type: System.Double
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Offset

The offset in the function of the exponent of the sigmoid.

```yaml
Type: System.Double
Required: True
Position: named
Default value: None
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


