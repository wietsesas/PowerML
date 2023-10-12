# Add-ScoreTensorFlowModelTransform

Scores a dataset using a pre-trained TensorFlow model.

## Description

Scores a dataset using a pre-trained TensorFlow model.

## Syntax

```
Add-ScoreTensorFlowModelTransform [-OutputColumn] <String[]> [-InputColumn] <String[]> -Path <String> [-AddBatchDimension] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
Add-ScoreTensorFlowModelTransform [-OutputColumn] <String[]> [-InputColumn] <String[]> -Model <TensorFlowModel> [-AddBatchDimension] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

The name of the requested model output. The data type is a vector of Single.

```yaml
Type: System.String[]
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

The name of the model input. The data type is a vector of Single.

```yaml
Type: System.String[]
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path

The location of the TensorFlow model to load.

```yaml
Type: System.String
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Model

The TensorFlow model.

```yaml
Type: Microsoft.ML.Transforms.TensorFlowModel
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AddBatchDimension

Add a batch dimension to the input e.g. input = [224, 224, 3] => [-1, 224, 224, 3]. This parameter is used to deal with models that have unknown shape but the internal operators in the model require data to have batch dimension as well.

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


