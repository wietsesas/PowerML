# Add-OneHotHashEncodingTransform

Convert one or more text columns into hash-based one-hot encoded vectors.

## Description

Convert one or more text columns into hash-based one-hot encoded vectors.

## Syntax

```
Add-OneHotHashEncodingTransform [-OutputColumn] <String> [[-InputColumn] <String>] [-OutputKind <OutputKind>] [-Bits <Int32>] [-Seed <UInt32>] [-MaxInverts <Int32>] [-DontUseOrderedHashing] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
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

Name of column to transform. If set to null, the value of the outputColumnName will be used as source. This column's data type can be scalar or vector of numeric, text, boolean, DateTime or DateTimeOffset.

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputKind

The conversion mode.

```yaml
Type: Microsoft.ML.Transforms.OutputKind
Required: False
Position: named
Default value: Indicator
Accept pipeline input: False
Accept wildcard characters: False
```

### -Bits

Number of bits to hash into. Must be between 1 and 30, inclusive.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 16
Accept pipeline input: False
Accept wildcard characters: False
```

### -Seed

Hashing seed.

```yaml
Type: System.UInt32
Required: False
Position: named
Default value: 314489979
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxInverts

During hashing we constuct mappings between original values and the produced hash values. Text representation of original values are stored in the slot names of the metadata for the new column.Hashing, as such, can map many initial values to one. maximumNumberOfInverts specifies the upper bound of the number of distinct input values mapping to a hash that should be retained. 0 does not retain any input values. -1 retains all input values mapping to each hash.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -DontUseOrderedHashing

Whether the position of each term should be included in the hash.

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


