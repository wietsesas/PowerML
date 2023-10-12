# Add-ForecastBySsaTransform

Singular Spectrum Analysis (SSA) model for univariate time-series forecasting.

## Description

Singular Spectrum Analysis (SSA) model for univariate time-series forecasting.

## Syntax

```
Add-ForecastBySsaTransform [-OutputColumn] <String> [-InputColumn] <String> -WindowSize <Int32> -SeriesLength <Int32> -TrainSize <Int32> -Horizon <Int32> [-IsAdaptive] [-DiscountFactor <Single>] [-RankSelectionMethod <RankSelectionMethod>] [-Rank <Nullable<Int32>>] [-MaxRank <Nullable<Int32>>] [-ShouldNotStabilize] [-ShouldMaintainInfo] [-MaxGrowth <Nullable<GrowthRatio>>] [-ConfidenceLowerBoundColumn <String>] [-ConfidenceUpperBoundColumn <String>] [-ConfidenceLevel <Single>] [-VariableHorizon] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of column to transform. If set to null, the value of the outputColumnName will be used as source. The vector contains Alert, Raw Score, P-Value as first three values.

```yaml
Type: System.String
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WindowSize

The length of the window on the series for building the trajectory matrix (parameter L).

```yaml
Type: System.Int32
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SeriesLength

The length of series that is kept in buffer for modeling (parameter N).

```yaml
Type: System.Int32
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TrainSize

The length of series from the beginning used for training.

```yaml
Type: System.Int32
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Horizon

The number of values to forecast.

```yaml
Type: System.Int32
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsAdaptive

The flag determining whether the model is adaptive.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -DiscountFactor

The discount factor in [0,1] used for online updates.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 1
Accept pipeline input: False
Accept wildcard characters: False
```

### -RankSelectionMethod

The rank selection method.

```yaml
Type: Microsoft.ML.Transforms.TimeSeries.RankSelectionMethod
Required: False
Position: named
Default value: Exact
Accept pipeline input: False
Accept wildcard characters: False
```

### -Rank

The desired rank of the subspace used for SSA projection (parameter r). This parameter should be in the range in [1, windowSize]. If set to null, the rank is automatically determined based on prediction error minimization.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxRank

The maximum rank considered during the rank selection process. If not provided (i.e. set to null), it is set to windowSize - 1.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShouldNotStabilize

The flag determining whether the model should be stabilized.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShouldMaintainInfo

The flag determining whether the meta information for the model needs to be maintained.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaxGrowth

The maximum growth on the exponential trend.

```yaml
Type: Microsoft.ML.Transforms.TimeSeries.GrowthRatio
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConfidenceLowerBoundColumn

The name of the confidence interval lower bound column. If not specified then confidence intervals will not be calculated.

```yaml
Type: System.String
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConfidenceUpperBoundColumn

The name of the confidence interval upper bound column. If not specified then confidence intervals will not be calculated.

```yaml
Type: System.String
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -ConfidenceLevel

The confidence level for forecasting.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 0.95
Accept pipeline input: False
Accept wildcard characters: False
```

### -VariableHorizon

Set this to true if horizon will change after training(at prediction time).

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


