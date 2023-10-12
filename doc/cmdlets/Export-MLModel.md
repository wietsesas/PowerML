# Export-MLModel

Export a machine learning model.

## Description

Export a machine learning model.

## Syntax

```
Export-MLModel [-Model] <TransformerChain<ITransformer>> [-Path] <String> -Schema <DataViewSchema> [-Force] [-PassThru] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -Model

The model to save to file.

```yaml
Type: Microsoft.ML.Data.TransformerChain<Microsoft.ML.ITransformer>
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path

The location where to save the model.

```yaml
Type: System.String
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Schema

The data schema.

```yaml
Type: Microsoft.ML.DataViewSchema
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force

Overwrite existing files.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru

Return the model.

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
| Microsoft.ML.Data.TransformerChain<Microsoft.ML.ITransformer> | This cmdlet returns the TransformerChain if -PassThru is used. |


