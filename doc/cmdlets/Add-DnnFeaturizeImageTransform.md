# Add-DnnFeaturizeImageTransform

Applies a pre-trained deep neural network (DNN) model to transform an input image into a feature vector.

## Description

Applies a pre-trained deep neural network (DNN) model to transform an input image into a feature vector.

## Syntax

```
Add-DnnFeaturizeImageTransform [-OutputColumn] <String> [[-InputColumn] <String>] [-ModelFactory] <Func<DnnImageFeaturizerInput, EstimatorChain<ColumnCopyingTransformer>>> [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

The name of the column resulting from the transformation of inputColumnName.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of column to transform. If set to null, the value of the outputColumnName will be used as source.

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -ModelFactory

An extension method on the DnnImageModelSelector that creates a chain of two OnnxScoringEstimator (one for preprocessing and one with a pretrained image DNN) with specific models included in a package together with that extension method.

```yaml
Type: System.Func<Microsoft.ML.Transforms.Onnx.DnnImageFeaturizerInput, Microsoft.ML.Data.EstimatorChain<Microsoft.ML.Transforms.ColumnCopyingTransformer>>
Required: True
Position: 2
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


