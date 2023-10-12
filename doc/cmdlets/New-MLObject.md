# New-MLObject

Create a new object of the specified registered type with the specified properties.

## Description

Create a new object of the specified registered type with the specified properties.

## Syntax

```
New-MLObject [-Type] <String> [-Properties <IDictionary>] [<CommonParameters>]
```

## Parameters

### -Type

Create an object of this registered type.

```yaml
Type: System.String
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Properties

The properties to assign to the object.

```yaml
Type: System.Collections.IDictionary
Required: False
Position: named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### Common parameters

This cmdlet supports the common parameters: Verbose, Debug, ErrorAction, ErrorVariable, WarningAction, WarningVariable, OutBuffer, PipelineVariable, and OutVariable. For more information, see [about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## Inputs

| Type | Description |
|:---|:---|
| System.Collections.IDictionary | You can pipe a dictionary with the properties for the new object to this cmdlet. |

## Outputs

| Type | Description |
|:---|:---|
| System.Object | This cmdlet returns the newly created object. |


