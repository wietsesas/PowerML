# Get-MLType

Get a registered type.

## Description

Get a registered type.

## Syntax

```
Get-MLType [[-Type] <String>] [<CommonParameters>]
```

## Parameters

### -Type

The type to fetch from the registered types. Leave empty to get all types.

```yaml
Type: System.String
Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### Common parameters

This cmdlet supports the common parameters: Verbose, Debug, ErrorAction, ErrorVariable, WarningAction, WarningVariable, OutBuffer, PipelineVariable, and OutVariable. For more information, see [about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## Inputs

| Type | Description |
|:---|:---|
| System.String | You can pipe the name of the type to fetch to this cmdlet. |

## Outputs

| Type | Description |
|:---|:---|
| System.Type | This cmdlet returns the requested type(s). |


