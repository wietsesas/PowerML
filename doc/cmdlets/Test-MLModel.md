# Test-MLModel

Evaluate a machine learning model.

## Description

Evaluate a machine learning model.

## Syntax

```
Test-MLModel [-Model] <TransformerChain<ITransformer>> [-Data] <IDataView> -BinaryClassification [-LabelColumn <String>] [-ScoreColumn <String>] [-ProbabilityColumn <String>] [-PredictedLabelColumn <String>] [-Context <MLContext>] [<CommonParameters>]
Test-MLModel [-Model] <TransformerChain<ITransformer>> [-Data] <IDataView> -MulticlassClassification [-LabelColumn <String>] [-ScoreColumn <String>] [-PredictedLabelColumn <String>] [-TopKPredictionCount <Int32>] [-Context <MLContext>] [<CommonParameters>]
Test-MLModel [-Model] <TransformerChain<ITransformer>> [-Data] <IDataView> -Regression [-LabelColumn <String>] [-ScoreColumn <String>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -Model

The transformer to evaluate.

```yaml
Type: Microsoft.ML.Data.TransformerChain<Microsoft.ML.ITransformer>
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Data

The data to evaluate the model.

```yaml
Type: Microsoft.ML.IDataView
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BinaryClassification

Test a binary classification model.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: True
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -MulticlassClassification

Test a multiclass classification model.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: True
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Regression

Test a regression model.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: True
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -LabelColumn

The label column name.

```yaml
Type: System.String
Required: False
Position: named
Default value: Label
Accept pipeline input: False
Accept wildcard characters: False
```

### -ScoreColumn

The score column name.

```yaml
Type: System.String
Required: False
Position: named
Default value: Score
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProbabilityColumn

The probability column name.

```yaml
Type: System.String
Required: False
Position: named
Default value: Probability
Accept pipeline input: False
Accept wildcard characters: False
```

### -PredictedLabelColumn

The predicted label column name

```yaml
Type: System.String
Required: False
Position: named
Default value: PredictedLabel
Accept pipeline input: False
Accept wildcard characters: False
```

### -TopKPredictionCount

TopKPredictionCount

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 0
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
| None | This cmdlet does not accept pipeline input. |

## Outputs

| Type | Description |
|:---|:---|
| Microsoft.ML.Data.CalibratedBinaryClassificationMetrics | This cmdlet returns evaluation metrics. |
| Microsoft.ML.Data.MulticlassClassificationMetrics | This cmdlet returns evaluation metrics. |
| Microsoft.ML.Data.RegressionMetrics | This cmdlet returns evaluation metrics. |


