$modelPath = '<path_to_your_model_directory>'

# Make sure the model path exists
if (-not (Test-Path -Path $modelPath -PathType Container)) { New-Item -Path $modelPath -ItemType Directory -Force | Out-Null }

# Type SentimentData
@{
  Name = 'SentimentData'
  Properties = @{
    SentimentText = @{ Type = 'string'; LoadColumn = 0 }
    Sentiment = @{ Type = 'bool'; LoadColumn = 1; ColumnName = 'Label' }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\SentimentData.json"

# Type SentimentPrediction
# SentimentText is added to retain the source information, but is not necessary
@{
  Name = 'SentimentPrediction'
  Properties = @{
    SentimentText = @{ Type = 'string'; }
    Prediction = @{ Type = 'bool'; ColumnName = 'PredictedLabel' }
    Probability = @{ Type = 'float' }
    Score = @{ Type = 'float' }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\SentimentPrediction.json"
