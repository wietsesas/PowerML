# New-MLContext

Create a new MLContext.

## Description

Create a new MLContext.

## Syntax

```
New-MLContext [[-Seed] <Nullable<Int32>>] [-NoCache] [-PassThru] [<CommonParameters>]
```

## Parameters

### -Seed

Seed for MLContext's random number generator.
Many operations in ML.NET require randomness, such as random data shuffling, random sampling, random parameter initialization, random permutation, random feature selection, and many more. MLContext's random number generator is the global source of randomness for all of such random operations.
If a fixed seed is provided by seed, MLContext environment becomes deterministic, meaning that the results are repeatable and will remain the same across multiple runs.For instance in many of ML.NET's API reference example code snippets, a seed is provided. That's because we want the users to get the same output as what's included in example comments, when they run the example on their own machine.
Generally though, repeatability is not a requirement and that's the default behavior. If a seed is not provided by seed, i.e. it's set to null, MLContext environment becomes non - deterministic and outputs change across multiple runs.
There are many operations in ML.NET that don't use any randomness, such as min-max normalization, concatenating columns, missing value indication, etc. The behavior of those operations are deterministic regardless of the seed value.
Also ML.NET trainers don't use randomness *after* the training is finished. So, the predictions from a loaded model don't depend on the seed value.

```yaml
Type: System.Int32
Required: False
Position: 0
Default value: null
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoCache

Do not set the created context as current (cached) context and return the created context (No need to use -PassThru).

```yaml
Type: System.Management.Automation.SwitchParameter
Required: False
Position: named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru

Return the created context.

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
| None | This cmdlet does not accept pipeline input. |

## Outputs

| Type | Description |
|:---|:---|
| Microsoft.ML.MLContext | This cmdlet returns the newly created MLContext if -NoCache or -PassThru is used. |


