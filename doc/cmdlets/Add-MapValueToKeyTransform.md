# Add-MapValueToKeyTransform

Map values to keys (categories) by creating the mapping from the input data.

## Description

Map values to keys (categories) by creating the mapping from the input data.

## Syntax

```
Add-MapValueToKeyTransform [-OutputColumn] <String> [[-InputColumn] <String>] [-MaxKeys <Int32>] [-KeyOrdinality <KeyOrdinality>] [-KeyData <IDataView>] [-AddKeyValueAnnotationsAsText] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column containing the keys.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of the column containing the categorical values. If set to null, the value of the outputColumnName is used. The input data types can be numeric, text, boolean, DateTime or DateTimeOffset.

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxKeys

Maximum number of keys to keep per column when training.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 1000000
Accept pipeline input: False
Accept wildcard characters: False
```

### -KeyOrdinality

The order in which keys are assigned. If set to ByOccurrence, keys are assigned in the order encountered. If set to ByValue, values are sorted, and keys are assigned based on the sort order.

```yaml
Type: Microsoft.ML.Transforms.KeyOrdinality
Required: False
Position: named
Default value: ByOccurrence
Accept pipeline input: False
Accept wildcard characters: False
```

### -KeyData

Use a pre-defined mapping between values and keys, instead of building the mapping from the input data during training. If specified, this should be a single column IDataView containing the values. The keys are allocated based on the value of keyOrdinality.

```yaml
Type: Microsoft.ML.IDataView
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -AddKeyValueAnnotationsAsText

If set to true, use text type for values, regardless of the actual input type. When doing the reverse mapping, the values are text rather than the original input type.

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


