$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\PhoneCallsData.json"
Register-MLType "$modelPath\PhoneCallsPrediction.json"

# Load the data and split into a train and test set
$data = Import-MLData -Type PhoneCallsData -Path "$dataPath\phone-calls.csv" -HasHeader -Separator ','

# Detect seasonality
$period = $data | Measure-Seasonality -InputColumn 'Value'
Write-Host "Period of the series is: $period."

# Detect anomaly
$predictions = $data | Measure-EntireAnomalyBySrCnn -OutputColumn 'Prediction' -InputColumn 'Value' -Threshold 0.3 -Sensitivity 64 -DetectMode AnomalyAndMargin -Period $period |
  ConvertTo-Enumerable -Type PhoneCallsPrediction

# Display results
$index = 0
$predictions | ForEach-Object {
  $item = $PSItem
  if ($item -ne $null) {
    [PSCustomObject]@{
      Index = $index
      Anomaly = $item.Prediction[0]
      ExpectedValue  = $item.Prediction[3]
      LowerBoundary = $item.Prediction[5]
      UpperBoundary = $item.Prediction[6]
      IsAnomaly = $item.Prediction[0] -eq 1
    }
  }
  $index++
}