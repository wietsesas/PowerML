# Add-MatrixFactorizationTrainer

Predict elements in a matrix using matrix factorization (also known as a type of collaborative filtering).

## Description

Predict elements in a matrix using matrix factorization (also known as a type of collaborative filtering).

## Syntax

```
Add-MatrixFactorizationTrainer [[-LabelColumn] <String>] [-ColumnIndexColumn] <String> [-RowIndexColumn] <String> [-ApproximationRank <Int32>] [-LearningRate <Double>] [-Iterations <Int32>] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -LabelColumn

The name of the label column. The column data must be Single.

```yaml
Type: System.String
Required: False
Position: 0
Default value: Label
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnIndexColumn

The name of the column hosting the matrix's column IDs. The column data must be KeyDataViewType.

```yaml
Type: System.String
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RowIndexColumn

The name of the column hosting the matrix's row IDs. The column data must be KeyDataViewType.

```yaml
Type: System.String
Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ApproximationRank

Rank of approximation matrices.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 8
Accept pipeline input: False
Accept wildcard characters: False
```

### -LearningRate

Initial learning rate. It specifies the speed of the training algorithm.

```yaml
Type: System.Double
Required: False
Position: named
Default value: 0.1
Accept pipeline input: False
Accept wildcard characters: False
```

### -Iterations

The number of training iterations.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 20
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


