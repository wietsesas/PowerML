# Add-SelectFeaturesBasedOnMutualInformationTransform

Select the features on which the data in the label column is most dependent.

## Description

Select the features on which the data in the label column is most dependent.

## Syntax

```
Add-SelectFeaturesBasedOnMutualInformationTransform [-OutputColumn] <String> [[-InputColumn] <String>] [[-LabelColumn] <String>] [-SlotsInOutput <Int32>] [-Bins <Int32>] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName.

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

### -LabelColumn

The name of the label column.

```yaml
Type: System.String
Required: False
Position: 2
Default value: Label
Accept pipeline input: False
Accept wildcard characters: False
```

### -SlotsInOutput

The maximum number of slots to preserve in the output. The number of slots to preserve is taken across all input columns.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 1000
Accept pipeline input: False
Accept wildcard characters: False
```

### -Bins

Max number of bins used to approximate mutual information between each input column and the label column. Power of 2 recommended.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 256
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


