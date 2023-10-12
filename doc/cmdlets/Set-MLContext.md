# Set-MLContext

Set the current (cached) MLContext.

## Description

Set the current (cached) MLContext.

## Syntax

```
Set-MLContext [-Context] <MLContext> [-PassThru] [<CommonParameters>]
```

## Parameters

### -Context

The context to set as current (cached) MLContext.

```yaml
Type: Microsoft.ML.MLContext
Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -PassThru

Return the context.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### Common parameters

This cmdlet supports the common parameters: Verbose, Debug, ErrorAction, ErrorVariable, WarningAction, WarningVariable, OutBuffer, PipelineVariable, and OutVariable. For more information, see [about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## Inputs

| Type | Description |
|:---|:---|
| Microsoft.ML.MLContext | You can pipe the MLContext to set to this cmdlet. |

## Outputs

| Type | Description |
|:---|:---|
| Microsoft.ML.MLContext | This cmdlet returns the current (cached) MLContext if -PassThru is used. |


