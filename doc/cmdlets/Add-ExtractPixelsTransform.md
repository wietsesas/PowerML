# Add-ExtractPixelsTransform

Convert pixels from input image into a vector of numbers.

## Description

Convert pixels from input image into a vector of numbers.

## Syntax

```
Add-ExtractPixelsTransform [-OutputColumn] <String> [[-InputColumn] <String>] [-Colors <ColorBits>] [-ColorsOrder <ColorsOrder>] [-InterleaveColors] [-Scale <Single>] [-Offset <Single>] [-AsByteArray] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. This column's data type will be a known-sized vector of Single or Byte depending on outputAsFloatArray.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of the column with images. This estimator operates over MLImage.

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -Colors

The colors to extract from the image.

```yaml
Type: Microsoft.ML.Transforms.Image.ColorBits
Required: False
Position: named
Default value: Rgb
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColorsOrder

The order in which to extract colors from pixel.

```yaml
Type: Microsoft.ML.Transforms.Image.ColorsOrder
Required: False
Position: named
Default value: ARGB
Accept pipeline input: False
Accept wildcard characters: False
```

### -InterleaveColors

Whether the pixels are interleaved, meaning whether they are in ColorsOrder order, or separated in the planar form: all the values for one color for all pixels, then all the values for another color and so on.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scale

Scale each pixel's color value by this amount. Applied to color value after offsetImage.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 1
Accept pipeline input: False
Accept wildcard characters: False
```

### -Offset

Offset each pixel's color value by this amount. Applied to color value before scaleImage.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -AsByteArray

Output array as byte array. Output as byte array ignores offsetImage and scaleImage.

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


