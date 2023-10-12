$modelPath = '<path_to_your_model_directory>'

# Make sure the model path exists
if (-not (Test-Path -Path $modelPath -PathType Container)) { New-Item -Path $modelPath -ItemType Directory -Force | Out-Null }

# Type MovieRating
@{
  Name = 'MovieRating'
  Properties = @{
    UserId = @{ Type = 'float'; LoadColumn = 0 }
    MovieId = @{ Type = 'float'; LoadColumn = 1 }
    Label = @{ Type = 'float'; LoadColumn = 2 }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\MovieRating.json"

# Type SentimentPrediction
# UserId and MovieId are added to retain the source information, but are not necessary
@{
  Name = 'MovieRatingPrediction'
  Properties = @{
    UserId = @{ Type = 'float' }
    MovieId = @{ Type = 'float' }
    Label = @{ Type = 'float' }
    Score = @{ Type = 'float' }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\MovieRatingPrediction.json"
