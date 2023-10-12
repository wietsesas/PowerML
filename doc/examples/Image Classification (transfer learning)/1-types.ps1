$modelPath = '<path_to_your_model_directory>'

# Make sure the model path exists
if (-not (Test-Path -Path $modelPath -PathType Container)) { New-Item -Path $modelPath -ItemType Directory -Force | Out-Null }

# Type ImageData
@{
  Name = 'ImageData'
  Properties = @{
    Image = @{ Type = 'byte[]'; }
    LabelAsKey = @{ Type = 'uint' }
    ImagePath = @{ Type = 'string'; }
    Label = @{ Type = 'string' }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\ImageData.json"

# Type ImagePrediction
# ImagePath is added to retain the source information, but is not necessary
@{
  Name = 'ImagePrediction'
  Properties = @{
    ImagePath = @{ Type = 'string'; }
    PredictedLabel = @{ Type = 'string' }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\ImagePrediction.json"
