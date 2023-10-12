# Add-AveragedPerceptronTrainer

Predict a target using a linear binary classification model trained with the averaged perceptron.

## Description

Predict a target using a linear binary classification model trained with the averaged perceptron.

## Syntax

```
Add-AveragedPerceptronTrainer [[-LabelColumn] <String>] [[-FeatureColumn] <String>] [-LossFunction <IClassificationLoss>] [-LearningRate <Single>] [-DecreaseLearningRate] [-L2Regularization <Single>] [-Iterations <Int32>] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -LabelColumn

The name of the label column. The column data must be Boolean.

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

### -LossFunction

The loss function minimized in the training process. If null, HingeLoss would be used and lead to a max-margin averaged perceptron trainer.

```yaml
Type: Microsoft.ML.Trainers.IClassificationLoss
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -LearningRate

The initial learning rate used by SGD.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 1
Accept pipeline input: False
Accept wildcard characters: False
```

### -DecreaseLearningRate

True to decrease the learningRate as iterations progress; otherwise, false.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -L2Regularization

The L2 weight for regularization.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -Iterations

Number of passes through the training dataset.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 10
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


