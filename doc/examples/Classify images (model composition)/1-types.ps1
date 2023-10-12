$modelPath = '<path_to_your_model_directory>'

# Make sure the model path exists
if (-not (Test-Path -Path $modelPath -PathType Container)) { New-Item -Path $modelPath -ItemType Directory -Force | Out-Null }

# Type ImageData
@{
  Name = 'ImageData'
  Properties = @{
    ImagePath = @{ Type = 'string'; LoadColumn = 0 }
    Label = @{ Type = 'string'; LoadColumn = 1 }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\ImageData.json"

# Type ImagePrediction
# ImagePath, Title and Description are added to retain the source information, but are not necessary
@{
  Name = 'ImagePrediction'
  Properties = @{
    ImagePath = @{ Type = 'string' }
    Label = @{ Type = 'string' }
    Score = @{ Type = 'float[]' }
    PredictedLabelValue = @{ Type = 'string' }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\ImagePrediction.json"