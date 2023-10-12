# Split-MLData

Split the dataset into a train and test set or into cross-validation folds of train and test sets.

## Description

Split the dataset into the train set and test set according to the given fraction, or split the dataset into cross-validation folds of train sets and test sets. Respects the samplingKeyColumnName if provided.

## Syntax

```
Split-MLData -Data <IDataView> [-TestFraction <Double>] [-SamplingKeyColumn <String>] [-Seed <Nullable<Int32>>] [-Context <MLContext>] [<CommonParameters>]
Split-MLData -Data <IDataView> -Folds <Int32> [-SamplingKeyColumn <String>] [-Seed <Nullable<Int32>>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -Data

The dataset to split.

```yaml
Type: Microsoft.ML.IDataView
Required: True
Position: named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TestFraction

The fraction of data to go into the test set.

```yaml
Type: System.Double
Required: False
Position: named
Default value: 0.1
Accept pipeline input: False
Accept wildcard characters: False
```

### -Folds

Number of cross-validation folds.

```yaml
Type: System.Int32
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SamplingKeyColumn

Name of a column to use for grouping rows. If two examples share the same value of the samplingKeyColumnName, they are guaranteed to appear in the same subset (train or test). This can be used to ensure no label leakage from the train to the test set. Note that when performing a Ranking Experiment, the samplingKeyColumnName must be the GroupId column. If null no row grouping will be performed.

```yaml
Type: System.String
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -Seed

Seed for the random number generator used to select rows for the train-test split.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: null
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
| Microsoft.ML.IDataView | You can pipe a data view to this cmdlet. |

## Outputs

| Type | Description |
|:---|:---|
| Microsoft.ML.TrainTestData | This cmdlet returns a TrainTestData object. |


