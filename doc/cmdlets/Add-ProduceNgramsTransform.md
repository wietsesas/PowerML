# Add-ProduceNgramsTransform

Transform text column into a bag of counts of ngrams (sequences of consecutive words).

## Description

Transform text column into a bag of counts of ngrams (sequences of consecutive words).

## Syntax

```
Add-ProduceNgramsTransform [-OutputColumn] <String> [[-InputColumn] <String>] [-NgramLength <Int32>] [-SkipLength <Int32>] [-MaxNgrams <Int32>] [-Weighting <WeightingCriteria>] [-DontUseAllLengths] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. This column's data type will be a vector of Single.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of the column to transform. If set to null, the value of the outputColumnName will be used as source. This estimator operates over vectors of keys data type.

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
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

Number of tokens to skip between each n-gram. By default no token is skipped.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxNgrams

Maximum number of n-grams to store in the dictionary.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 10000000
Accept pipeline input: False
Accept wildcard characters: False
```

### -Weighting

Statistical measure used to evaluate how important a word or n-gram is to a document in a corpus. When maximumNgramsCount is smaller than the total number of encountered n-grams this measure is used to determine which n-grams to keep.

```yaml
Type: Microsoft.ML.Transforms.Text.WeightingCriteria
Required: False
Position: named
Default value: Tf
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


