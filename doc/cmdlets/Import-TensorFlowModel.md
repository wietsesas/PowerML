# Import-TensorFlowModel

Load TensorFlow model into memory.

## Description

Load TensorFlow model into memory. This is the convenience method that allows the model to be loaded once and subsequently use it for querying schema and creation of TensorFlowEstimator.

## Syntax

```
Import-TensorFlowModel [-Path] <String> [-OutputAsBatched] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -Path

The location of the TensorFlow model to load.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputAsBatched

If the first dimension of the output is unknown, should it be treated as batched or not.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
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
| Microsoft.ML.Transforms.TensorFlowModel |  |


