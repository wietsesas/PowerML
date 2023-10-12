# Add-NormalizeSupervisedBinningTransform

Assign the input value to a bin based on its correlation with label column.

## Description

Assign the input value to a bin based on its correlation with label column.

## Syntax

```
Add-NormalizeSupervisedBinningTransform [-OutputColumn] <String> [[-InputColumn] <String>] [[-LabelColumn] <String>] [-MaxExamples <Int64>] [-MaxBins <Int32>] [-MinExamplesPerBin <Int32>] [-DontFixZero] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. The data type on this column is the same as the input column.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of the column to transform. If set to null, the value of the outputColumnName will be used as source. The data type on this column should be Single, Double or a known-sized vector of those types.

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -LabelColumn

Name of the label column for supervised binning.

```yaml
Type: System.String
Required: False
Position: 2
Default value: Label
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxExamples

Maximum number of examples used to train the normalizer.

```yaml
Type: System.Int64
Required: False
Position: named
Default value: 1000000000
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxBins

Maximum number of bins (power of 2 recommended).

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 1024
Accept pipeline input: False
Accept wildcard characters: False
```

### -MinExamplesPerBin

Minimum number of examples per bin.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 10
Accept pipeline input: False
Accept wildcard characters: False
```

### -DontFixZero

Whether to map zero to zero, preserving sparsity.

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


