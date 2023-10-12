# Get-BootstrapSample

Take an approximate bootstrap sample of the input data.

## Description

Take an approximate bootstrap sample of the input data.

## Syntax

```
Get-BootstrapSample -Data <IDataView> [-Seed <Nullable<Int32>>] [-Complement] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -Data

The input data.

```yaml
Type: Microsoft.ML.IDataView
Required: True
Position: named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Seed

The random seed. If unspecified, the random state will be instead derived from the MLContext.

```yaml
Type: System.Int32
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -Complement

Whether this is the out-of-bag sample, that is, all those rows that are not selected by the transform. Can be used to create a complementary pair of samples by using the same seed.

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
| Microsoft.ML.IDataView | You can pipe a data view to this cmdlet. |

## Outputs

| Type | Description |
|:---|:---|
| Microsoft.ML.IDataView | This cmdlet returns a DataView. |


