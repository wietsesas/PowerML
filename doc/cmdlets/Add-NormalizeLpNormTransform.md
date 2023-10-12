# Add-NormalizeLpNormTransform

Scale input vectors by their lp-norm, where p is 1, 2 or infinity. Defaults to the l2 (Euclidean distance) norm.

## Description

Scale input vectors by their lp-norm, where p is 1, 2 or infinity. Defaults to the l2 (Euclidean distance) norm.

## Syntax

```
Add-NormalizeLpNormTransform [-OutputColumn] <String> [[-InputColumn] <String>] [-Norm <NormFunction>] [-EnsureZeroMean] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. This column's data type will be the same as the input column's data type.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of the column to normalize. If set to null, the value of the outputColumnName will be used as source. This estimator operates over known-sized vectors of Single.

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -Norm

Type of norm to use to normalize each sample. The indicated norm of the resulting vector will be normalized to one.

```yaml
Type: Microsoft.ML.Transforms.NormFunction
Required: False
Position: named
Default value: L2
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnsureZeroMean

If true, subtract mean from each value before normalizing and use the raw input otherwise.

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


