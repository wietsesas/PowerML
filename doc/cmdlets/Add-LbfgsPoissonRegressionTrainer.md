# Add-LbfgsPoissonRegressionTrainer

Train a Poisson regression model.

## Description

Train a Poisson regression model.

## Syntax

```
Add-LbfgsPoissonRegressionTrainer [[-LabelColumn] <String>] [[-FeatureColumn] <String>] [[-ExampleWeightColumn] <String>] [-L1Regularization <Single>] [-L2Regularization <Single>] [-OptimizationTolerance <Single>] [-HistorySize <Int32>] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -LabelColumn

The name of the label column. The column data must be Single.

```yaml
Type: System.String
Required: False
Position: 0
Default value: Label
Accept pipeline input: False
Accept wildcard characters: False
```

### -FeatureColumn

The name of the feature column. The column data must be a known-sized vector of Single.

```yaml
Type: System.String
Required: False
Position: 1
Default value: Features
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

### -L1Regularization

The L1 regularization hyperparameter. Higher values will tend to lead to more sparse model.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 1
Accept pipeline input: False
Accept wildcard characters: False
```

### -L2Regularization

The L2 weight for regularization.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 1
Accept pipeline input: False
Accept wildcard characters: False
```

### -OptimizationTolerance

Threshold for optimizer convergence.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 1E-07
Accept pipeline input: False
Accept wildcard characters: False
```

### -HistorySize

Memory size for LbfgsLogisticRegressionBinaryTrainer. Low=faster, less accurate.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 20
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


