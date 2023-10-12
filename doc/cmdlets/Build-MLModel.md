# Build-MLModel

Train a machine learning model (Fit).

## Description

Train a machine learning model (Fit).

## Syntax

```
Build-MLModel -Pipeline <EstimatorChain<ITransformer>> [-Data] <IDataView> [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -Pipeline

The estimator from which to train the model.

```yaml
Type: Microsoft.ML.Data.EstimatorChain<Microsoft.ML.ITransformer>
Required: True
Position: named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Data

The data to train the model.

```yaml
Type: Microsoft.ML.IDataView
Required: True
Position: 0
Default value: None
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
| Microsoft.ML.Data.EstimatorChain<Microsoft.ML.ITransformer> | You can pipe the EstimatorChain to train to this cmdlet. |

## Outputs

| Type | Description |
|:---|:---|
| Microsoft.ML.Data.TransformerChain<Microsoft.ML.ITransformer> | This cmdlet returns the trained TransformerChain. |


