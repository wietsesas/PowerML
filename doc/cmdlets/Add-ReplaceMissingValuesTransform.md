# Add-ReplaceMissingValuesTransform

Create a new output column, the value of which is set to a default value if the value is missing from the input column, and the input value otherwise.

## Description

Create a new output column, the value of which is set to a default value if the value is missing from the input column, and the input value otherwise.

## Syntax

```
Add-ReplaceMissingValuesTransform [-OutputColumn] <String> [[-InputColumn] <String>] [-ReplacementMode <ReplacementMode>] [-DontImputeBySlot] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. This column's data type will be the same as that of the input column.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of the column to copy the data from. This estimator operates over scalar or vector of Single or Double.

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -ReplacementMode

The type of replacement to use as specified in MissingValueReplacingEstimator.ReplacementMode

```yaml
Type: Microsoft.ML.Transforms.ReplacementMode
Required: False
Position: named
Default value: DefaultValue
Accept pipeline input: False
Accept wildcard characters: False
```

### -DontImputeBySlot

Per-slot imputation of replacement is performed. Otherwise, replacement value is imputed for the entire vector column. This setting is ignored for scalars and variable vectors, where imputation is always for the entire column.

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


