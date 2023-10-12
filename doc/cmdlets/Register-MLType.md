# Register-MLType

Register a custom type from a type definition.

## Description

Register a custom type from a type definition.

## Syntax

```
Register-MLType [-Path] <String> [-Encoding <Encoding>] [-PassThru] [<CommonParameters>]
Register-MLType -Definition <IDictionary> [-PassThru] [<CommonParameters>]
```

## Parameters

### -Path

The path of the type definition file.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Definition

The type definition from which to create the type.

```yaml
Type: System.Collections.IDictionary
Required: True
Position: named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Encoding

The encoding of the type definition file.

```yaml
Type: System.Text.Encoding
Required: False
Position: named
Default value: UTF8
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru

Return the registered type.

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
| System.Collections.IDictionary | You can pipe a dictionary with the type definition to this cmdlet. |

## Outputs

| Type | Description |
|:---|:---|
| System.Type | This cmdlet returns the registered type if -PassThru is used. |


