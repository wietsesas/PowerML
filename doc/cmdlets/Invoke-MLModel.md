# Invoke-MLModel

Transform data using a machine learning model.

## Description

Transform data using a machine learning model.

## Syntax

```
Invoke-MLModel [-Model] <TransformerChain<ITransformer>> [-Data] <IDataView> [-Context <MLContext>] [<CommonParameters>]
Invoke-MLModel [-Model] <TransformerChain<ITransformer>> -InputObject <Object> -OutputType <String> [-InputType <String>] [-InputSchema <SchemaDefinition>] [-OutputSchema <SchemaDefinition>] [-IgnoreMissingColumns] [-NoCache] [-Context <MLContext>] [<CommonParameters>]
```

## Parameters

### -Model

The transformer to transform the data.

```yaml
Type: Microsoft.ML.Data.TransformerChain<Microsoft.ML.ITransformer>
Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Data

The data to transform.

```yaml
Type: Microsoft.ML.IDataView
Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject

The data for which to get predictions.

```yaml
Type: System.Object
Required: True
Position: named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -OutputType

The output type.

```yaml
Type: System.String
Required: True
Position: named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputType

The data type.

```yaml
Type: System.String
Required: False
Position: named
Default value: Type from Object
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputSchema

The input schema definition.

```yaml
Type: Microsoft.ML.Data.SchemaDefinition
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutputSchema

The output schema definition

```yaml
Type: Microsoft.ML.Data.SchemaDefinition
Required: False
Position: named
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -IgnoreMissingColumns

Ignore missing columns.

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoCache

Do not cache the prediction function.

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
| System.Object | You can pipe objects of InputType to this cmdlet. |

## Outputs

| Type | Description |
|:---|:---|
| Microsoft.ML.IDataView | This cmdlet returns the transformed data. |
| System.Object | This cmdlet returns the transformed data as objects of type OutputType. |


