# Add-MapValueTransform

Map values to keys (categories) based on the supplied dictionary of mappings.

## Description

Map values to keys (categories) based on the supplied dictionary of mappings.

## Syntax

```
Add-MapValueTransform [-OutputColumn] <String> [[-InputColumn] <String>] -LookupMap <IDataView> -KeyColumn <Column> -ValueColumn <Column> [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. The data types can be primitives or vectors of numeric, text, boolean, DateTime, DateTimeOffset or DataViewRowId types.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of the column to transform. If set to null, the value of the outputColumnName will be used as source. The data types can be primitives or vectors of numeric, text, boolean, DateTime, DateTimeOffset or DataViewRowId types.

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -LookupMap

An instance of IDataView that contains the keyColumn and valueColumn columns.

```yaml
Type: Microsoft.ML.IDataView
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -KeyColumn

The key column in lookupMap.

```yaml
Type: Microsoft.ML.Column
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ValueColumn

The value column in lookupMap.

```yaml
Type: Microsoft.ML.Column
Required: True
Position: named
Default value: None
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


