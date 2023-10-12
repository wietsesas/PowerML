# Add-ApplyOnnxModelTransform

Transform the input data with an imported ONNX model.

## Description

Transform the input data with an imported ONNX model.

## Syntax

```
Add-ApplyOnnxModelTransform [-OutputColumn] <String> [-InputColumn] <String> [-Path] <String> [-ShapeDictionary <IDictionary<String, Int32[]>>] [-GpuDeviceId <Nullable<Int32>>] [-FallbackToCpu] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
Add-ApplyOnnxModelTransform -OutputColumns <String[]> -InputColumns <String[]> [-Path] <String> [-ShapeDictionary <IDictionary<String, Int32[]>>] [-GpuDeviceId <Nullable<Int32>>] [-FallbackToCpu] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

The output column resulting from the transformation.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputColumns

The output columns resulting from the transformation.

```yaml
Type: System.String[]
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

The input column.

```yaml
Type: System.String
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumns

The input columns.

```yaml
Type: System.String[]
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path

The path of the file containing the ONNX model.

```yaml
Type: System.String
Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShapeDictionary

ONNX shapes to be used over those loaded from modelFile. For keys use names as stated in the ONNX model, e.g. "input". Stating the shapes with this parameter is particularly useful for working with variable dimension inputs and outputs.

```yaml
Type: System.Collections.Generic.IDictionary<System.String, System.Int32[]>
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -GpuDeviceId

Optional GPU device ID to run execution on, null to run on CPU.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -FallbackToCpu

If GPU error, raise exception or fallback to CPU.

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


