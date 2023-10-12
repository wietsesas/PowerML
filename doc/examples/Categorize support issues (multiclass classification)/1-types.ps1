$modelPath = '<path_to_your_model_directory>'

# Make sure the model path exists
if (-not (Test-Path -Path $modelPath -PathType Container)) { New-Item -Path $modelPath -ItemType Directory -Force | Out-Null }

# Type GitHubIssue
@{
  Name = 'GitHubIssue'
  Properties = @{
    Id = @{ Type = 'string'; LoadColumn = 0 }
    Area = @{ Type = 'string'; LoadColumn = 1 }
    Title  = @{ Type = 'string'; LoadColumn = 2 }
    Description  = @{ Type = 'string'; LoadColumn = 3 }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\GitHubIssue.json"

# Type IssuePrediction
# Id, Title and Description are added to retain the source information, but are not necessary
@{
  Name = 'IssuePrediction'
  Properties = @{
    Id =  @{ Type = 'string' }
    Area = @{ Type = 'string'; ColumnName = 'PredictedLabel' }
    Title = @{ Type = 'string' }
    Description  = @{ Type = 'string' }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\IssuePrediction.json"