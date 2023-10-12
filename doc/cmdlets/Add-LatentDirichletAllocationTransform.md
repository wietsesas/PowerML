# Add-LatentDirichletAllocationTransform

Transform a document (represented as a vector of floats) into a vector of floats over a set of topics.

## Description

Transform a document (represented as a vector of floats) into a vector of floats over a set of topics.

## Syntax

```
Add-LatentDirichletAllocationTransform [-OutputColumn] <String> [[-InputColumn] <String>] [-Topics <Int32>] [-AlphaSum <Single>] [-Beta <Single>] [-SamplingSteps <Int32>] [-MaxIterations <Int32>] [-LikelihoodInterval <Int32>] [-Threads <Int32>] [-MaxTokensPerDocument <Int32>] [-SummaryTermsPerTopic <Int32>] [-BurninIterations <Int32>] [-ResetRandomGenerator] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. This estimator outputs a vector of Single.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of the column to transform. If set to null, the value of the outputColumnName will be used as source. This estimator operates over a vector of Single.

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -Topics

The number of topics.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 100
Accept pipeline input: False
Accept wildcard characters: False
```

### -AlphaSum

Dirichlet prior on document-topic vectors.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 100
Accept pipeline input: False
Accept wildcard characters: False
```

### -Beta

Dirichlet prior on vocab-topic vectors.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 0.01
Accept pipeline input: False
Accept wildcard characters: False
```

### -SamplingSteps

Number of Metropolis Hasting step.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 4
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxIterations

Number of iterations.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 200
Accept pipeline input: False
Accept wildcard characters: False
```

### -LikelihoodInterval

Compute log likelihood over local dataset on this iteration interval.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 5
Accept pipeline input: False
Accept wildcard characters: False
```

### -Threads

The number of training threads. Default value depends on number of logical processors.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxTokensPerDocument

The threshold of maximum count of tokens per doc.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 512
Accept pipeline input: False
Accept wildcard characters: False
```

### -SummaryTermsPerTopic

The number of words to summarize the topic.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 10
Accept pipeline input: False
Accept wildcard characters: False
```

### -BurninIterations

The number of burn-in iterations.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 10
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResetRandomGenerator

Reset the random number generator for each document.

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


