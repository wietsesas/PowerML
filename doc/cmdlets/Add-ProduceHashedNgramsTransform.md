# Add-ProduceHashedNgramsTransform

Transform text column into a vector of hashed ngram counts.

## Description

Transform text column into a vector of hashed ngram counts.

## Syntax

```
Add-ProduceHashedNgramsTransform [-OutputColumn] <String> [[-InputColumn] <String>] [-Bits <Int32>] [-NgramLength <Int32>] [-SkipLength <Int32>] [-Seed <UInt32>] [-MaxInverts <Int32>] [-DontUseAllLengths] [-DontUseOrderedHashing] [-RehashUnigrams] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
Add-ProduceHashedNgramsTransform [-OutputColumn] <String> -InputColumns <String[]> [-Bits <Int32>] [-NgramLength <Int32>] [-SkipLength <Int32>] [-Seed <UInt32>] [-MaxInverts <Int32>] [-DontUseAllLengths] [-DontUseOrderedHashing] [-RehashUnigrams] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. This column's data type will be vector of Single.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of the column resulting from the transformation of inputColumnName. This column's data type will be vector of Single.

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumns

Name of the multiple columns to take the data from. This estimator operates over vector of key type.

```yaml
Type: System.String[]
Required: True
Position: named
Default value: None
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

### -NgramLength

Ngram length.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 2
Accept pipeline input: False
Accept wildcard characters: False
```

### -SkipLength

Maximum number of tokens to skip when constructing an n-gram.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 0
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

During hashing we construct mappings between original values and the produced hash values. Text representation of original values are stored in the slot names of the annotations for the new column.Hashing, as such, can map many initial values to one. maximumNumberOfInverts specifies the upper bound of the number of distinct input values mapping to a hash that should be retained. 0 does not retain any input values. -1 retains all input values mapping to each hash.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -DontUseAllLengths

Whether to include all n-gram lengths up to ngramLength or only ngramLength.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -DontUseOrderedHashing

Whether the position of each source column should be included in the hash (when there are multiple source columns).

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -RehashUnigrams

Whether to rehash unigrams.

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


