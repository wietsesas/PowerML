# Add-ApproximatedKernelMapTransform

Map each input vector onto a lower dimensional feature space, where inner products approximate a kernel function, so that the features can be used as inputs to the linear algorithms.

## Description

Map each input vector onto a lower dimensional feature space, where inner products approximate a kernel function, so that the features can be used as inputs to the linear algorithms.

## Syntax

```
Add-ApproximatedKernelMapTransform [-OutputColumn] <String> [[-InputColumn] <String>] [-Rank <Int32>] [-UseCosAndSinBases] [-Generator <KernelBase>] [-Seed <Nullable<Int32>>] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. The data type on this column will be a known-sized vector of Single.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of column to transform. If set to null, the value of the outputColumnName will be used as source. This estimator operates on known-sized vector of Single data type.

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -Rank

The dimension of the feature space to map the input to.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 1000
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseCosAndSinBases

If true, use both of cos and sin basis functions to create two features for every random Fourier frequency. Otherwise, only cos bases would be used. Note that if set to true, the dimension of the output feature space will be 2*rank.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Generator

The argument that indicates which kernel to use. The two available implementations are GaussianKernel and LaplacianKernel.

```yaml
Type: Microsoft.ML.Transforms.KernelBase
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -Seed

The seed of the random number generator for generating the new features (if unspecified, the global random is used).

```yaml
Type: System.Int32
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


