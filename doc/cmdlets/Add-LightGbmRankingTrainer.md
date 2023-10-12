# Add-LightGbmRankingTrainer

Train a boosted decision tree ranking model using LightGBM.

## Description

Train a boosted decision tree ranking model using LightGBM.

## Syntax

```
Add-LightGbmRankingTrainer [[-LabelColumn] <String>] [[-FeatureColumn] <String>] [[-RowGroupColumn] <String>] [[-ExampleWeightColumn] <String>] [-Leaves <Nullable<Int32>>] [-MinExampleCountPerLeaf <Nullable<Int32>>] [-LearningRate <Nullable<Double>>] [-Iterations <Int32>] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -LabelColumn

The name of the label column. The column data must be Single or KeyDataViewType.

```yaml
Type: System.String
Required: False
Position: 0
Default value: Label
Accept pipeline input: False
Accept wildcard characters: False
```

### -FeatureColumn

The name of the feature column. The column data must be a known-sized vector of Single.

```yaml
Type: System.String
Required: False
Position: 1
Default value: Features
Accept pipeline input: False
Accept wildcard characters: False
```

### -RowGroupColumn

The name of the group column.

```yaml
Type: System.String
Required: False
Position: 2
Default value: GroupId
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExampleWeightColumn

The name of the example weight column (optional).

```yaml
Type: System.String
Required: False
Position: 3
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -Leaves

The maximum number of leaves in one tree.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -MinExampleCountPerLeaf

The minimal number of data points required to form a new tree leaf.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -LearningRate

The learning rate.

```yaml
Type: System.Double
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -Iterations

The number of boosting iterations. A new tree is created in each iteration, so this is equivalent to the number of trees.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 100
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


