# Add-ImageClassificationTrainer

Train a Deep Neural Network (DNN) to classify images.

## Description

Train a Deep Neural Network (DNN) to classify images.

## Syntax

```
Add-ImageClassificationTrainer [[-LabelColumn] <String>] [[-FeatureColumn] <String>] [[-ScoreColumn] <String>] [[-PredictedLabelColumn] <String>] [-ValidationSet <IDataView>] [-ValidationSetFraction <Nullable<Single>>] [-Arch <Architecture>] [-BatchSize <Int32>] [-Epoch <Int32>] [-LearningRate <Single>] [-MetricsCallback <ScriptBlock>] [-WorkspacePath <String>] [-TrainSetBottleneckCachedValuesPath <String>] [-ValidationSetBottleneckCachedValuesPath <String>] [-FinalModelPrefix <String>] [-DontTestOnTrainSet] [-ReuseTrainSetBottleneckCachedValues] [-ReuseValidationSetBottleneckCachedValues] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -LabelColumn

The name of the label column. The default value is "Label".

```yaml
Type: System.String
Required: False
Position: 0
Default value: Label
Accept pipeline input: False
Accept wildcard characters: False
```

### -FeatureColumn

The name of the feature column. The column data must be a known-sized vector of Single. The default value is "Features".

```yaml
Type: System.String
Required: False
Position: 1
Default value: Features
Accept pipeline input: False
Accept wildcard characters: False
```

### -ScoreColumn

The name of the score column. The default value is "Score".

```yaml
Type: System.String
Required: False
Position: 2
Default value: Score
Accept pipeline input: False
Accept wildcard characters: False
```

### -PredictedLabelColumn

The name of the predicted label column. The default value is "PredictedLabel".

```yaml
Type: System.String
Required: False
Position: 3
Default value: PredictedLabel
Accept pipeline input: False
Accept wildcard characters: False
```

### -ValidationSet

The validation set used while training to improve model quality. The default value is null.

```yaml
Type: Microsoft.ML.IDataView
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -ValidationSetFraction

When validation set is not passed then a fraction of train set is used as validation. To disable this behavior set ValidationSetFraction to null. Accepts value between 0 and 1.0. The default value is 0.1 or 10% of the trainset.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 0.1
Accept pipeline input: False
Accept wildcard characters: False
```

### -Arch

Specifies the model architecture to be used in the case of image classification training using transfer learning. The default value is ResnetV250.

```yaml
Type: Microsoft.ML.Vision.Architecture
Required: False
Position: named
Default value: ResnetV250
Accept pipeline input: False
Accept wildcard characters: False
```

### -BatchSize

Number of samples to use for mini-batch training. The default value is 10.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 10
Accept pipeline input: False
Accept wildcard characters: False
```

### -Epoch

Number of training iterations. The default value is 200.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 200
Accept pipeline input: False
Accept wildcard characters: False
```

### -LearningRate

Learning rate to use during optimization. The default value is 0.01.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 0.01
Accept pipeline input: False
Accept wildcard characters: False
```

### -MetricsCallback

Callback to report statistics on accuracy/cross entropy during training phase. The default value is null.

```yaml
Type: System.Management.Automation.ScriptBlock
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -WorkspacePath

Indicates the path where the image bottleneck cache files and trained model are saved. The default value is a new temporary directory.

```yaml
Type: System.String
Required: False
Position: named
Default value: New temporary directory
Accept pipeline input: False
Accept wildcard characters: False
```

### -TrainSetBottleneckCachedValuesPath

Indicates the file name within the workspace to store trainset bottleneck values for caching. The default value is "trainSetBottleneckFile.csv".

```yaml
Type: System.String
Required: False
Position: named
Default value: trainSetBottleneckFile.csv
Accept pipeline input: False
Accept wildcard characters: False
```

### -ValidationSetBottleneckCachedValuesPath

Indicates the file name within the workspace to store validationset bottleneck values for caching. The default value is "validationSetBottleneckFile.csv".

```yaml
Type: System.String
Required: False
Position: named
Default value: validationSetBottleneckFile.csv
Accept pipeline input: False
Accept wildcard characters: False
```

### -FinalModelPrefix

Final model and checkpoint files/folder prefix for storing graph files. The default value is "custom_retrained_model_based_on_".

```yaml
Type: System.String
Required: False
Position: named
Default value: 200
Accept pipeline input: False
Accept wildcard characters: False
```

### -DontTestOnTrainSet

Indicates to not evaluate the model on train set after every epoch.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReuseTrainSetBottleneckCachedValues

Indicates to not re-compute cached bottleneck trainset values if already available in the bin folder.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReuseValidationSetBottleneckCachedValues

Indicates to not re-compute cached bottleneck validationset values if already available in the bin folder.

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


