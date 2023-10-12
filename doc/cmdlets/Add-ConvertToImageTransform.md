# Add-ConvertToImageTransform

Convert a vector of pixels to ImageDataViewType.

## Description

Convert a vector of pixels to ImageDataViewType.

## Syntax

```
Add-ConvertToImageTransform [-OutputColumn] <String> [[-InputColumn] <String>] [-Height] <Int32> [-Width] <Int32> [-Colors <ColorBits>] [-ColorsOrder <ColorsOrder>] [-InterleaveColors] [-Scale <Single>] [-Offset <Single>] [-DefaultAlpha <Int32>] [-DefaultRed <Int32>] [-DefaultGreen <Int32>] [-DefaultBlue <Int32>] [-AppendTo <EstimatorChain<ITransformer>>] [-AppendScope <TransformerScope>] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -OutputColumn

Name of the column resulting from the transformation of inputColumnName. This column's data type will be MLImage.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputColumn

Name of the column with data to be converted to image. This estimator operates over known-sized vector of Single, Double and Byte.

```yaml
Type: System.String
Required: False
Position: 1
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -Height

The height of the output images.

```yaml
Type: System.Int32
Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Width

The width of the output images.

```yaml
Type: System.Int32
Required: True
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Colors

Specifies which ImagePixelExtractingEstimator.ColorBits are in present the input pixel vectors. The order of colors is specified in orderOfColors.

```yaml
Type: Microsoft.ML.Transforms.Image.ColorBits
Required: False
Position: named
Default value: Rgb
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColorsOrder

The order in which colors are presented in the input vector.

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

The values are scaled by this value before being converted to pixels. Applied to vector value before offsetImage.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 1
Accept pipeline input: False
Accept wildcard characters: False
```

### -Offset

The offset is subtracted before converting the values to pixels. Applied to vector value after scaleImage.

```yaml
Type: System.Single
Required: False
Position: named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultAlpha

Default value for alpha color, would be overridden if colorsPresent contains Alpha.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 255
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultRed

Default value for red color, would be overridden if colorsPresent contains Red.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultGreen

Default value for green color, would be overridden if colorsPresent contains Green.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultBlue

Default value for blue color, would be overridden if colorsPresent contains Blue.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: 0
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


