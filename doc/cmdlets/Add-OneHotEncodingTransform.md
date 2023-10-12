# Add-OneHotEncodingTransform

Convert one or more text columns into one-hot encoded vectors.

## Description

Convert one or more text columns into one-hot encoded vectors.

## Syntax

```
Add-OneHotEncodingTransform [-OutputColumn] <String> [[-InputColumn] <String>] [-OutputKind <OutputKind>] [-MaxKeys <Int32>] [-KeyOrdinality <KeyOrdinality>] [-KeyData <IDataView>] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. This column's data type will be a vector of Single if outputKind is Bag, Indicator, and Binary. If outputKind is Key, this column's data type will be a key in the case of a scalar input column or a vector of keys in the case of a vector input column.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of column to convert to one-hot vectors. If set to null, the value of the outputColumnName will be used as source. This column's data type can be scalar or vector of numeric, text, boolean, DateTime or DateTimeOffset,

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputKind

Output kind: Bag (multi-set vector), Ind (indicator vector), Key (index), or Binary encoded indicator vector.

```yaml
Type: Microsoft.ML.Transforms.OutputKind
Required: False
Position: named
Default value: Indicator
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxKeys

Maximum number of terms to keep per column when auto-training.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 1000000
Accept pipeline input: False
Accept wildcard characters: False
```

### -KeyOrdinality

How items should be ordered when vectorized. If ByOccurrence choosen they will be in the order encountered. If ByValue, items are sorted according to their default comparison, for example, text sorting will be case sensitive (for example, 'A' then 'Z' then 'a').

```yaml
Type: Microsoft.ML.Transforms.KeyOrdinality
Required: False
Position: named
Default value: ByOccurrence
Accept pipeline input: False
Accept wildcard characters: False
```

### -KeyData

Specifies an ordering for the encoding. If specified, this should be a single column data view, and the key-values will be taken from that column. If unspecified, the ordering will be determined from the input data upon fitting.

```yaml
Type: Microsoft.ML.IDataView
Required: False
Position: named
Default value: null
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


