$modelPath = '<path_to_your_model_directory>'

# Make sure the model path exists
if (-not (Test-Path -Path $modelPath -PathType Container)) { New-Item -Path $modelPath -ItemType Directory -Force | Out-Null }

# Type PhoneCallsData
@{
  Name = 'PhoneCallsData'
  Properties = @{
    Timestamp = @{ Type = 'string'; LoadColumn = 0 }
    Value = @{ Type = 'double'; LoadColumn = 1 }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\PhoneCallsData.json"

# Type PhoneCallsPrediction
@{
  Name = 'PhoneCallsPrediction'
  Properties = @{
    Prediction  = @{ Type = 'double[]'; VectorType = @(7) }
  }
} | ConvertTo-Json -Depth 3 | Out-File -FilePath "$modelPath\PhoneCallsPrediction.json"