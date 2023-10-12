$modelPath = '<path_to_your_model_directory>'

# Make sure the model path exists
if (-not (Test-Path -Path $modelPath -PathType Container)) { New-Item -Path $modelPath -ItemType Directory -Force | Out-Null }

# Type IrisData
@{
  Name = 'IrisData'
  Properties = @{
    SepalLength = @{ Type = 'float'; LoadColumn = 0 }
    SepalWidth = @{ Type = 'float'; LoadColumn = 1 }
    PetalLength = @{ Type = 'float'; LoadColumn = 2 }
    PetalWidth = @{ Type = 'float'; LoadColumn = 3 }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\IrisData.json"

# Type ClusterPrediction
@{
  Name = 'ClusterPrediction'
  Properties = @{
    PredictedClusterId = @{ Type = 'uint'; ColumnName = 'PredictedLabel' }
    Distances = @{ Type = 'float[]'; ColumnName = 'Score' }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\ClusterPrediction.json"