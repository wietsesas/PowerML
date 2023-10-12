$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\IrisData.json"
Register-MLType "$modelPath\ClusterPrediction.json"

# Import the model (created in 2-train.ps1)
$model = Import-MLModel -Path "$modelPath\model.zip"

# Get a prediction for a single item
$testObject = @{
  SepalLength = 5.1
  SepalWidth = 3.5
  PetalLength = 1.4
  PetalWidth = 0.2
} | New-MLObject -Type IrisData
$testObject | Invoke-MLModel -Model $model -OutputType ClusterPrediction

# Get predictions for multiple items at once
$testList = @()
$testList += @{ SepalLength = 5.1; SepalWidth = 3.5; PetalLength = 1.4; PetalWidth = 0.2 } | New-MLObject -Type IrisData
$testList += @{ SepalLength = 7.0; SepalWidth = 3.7; PetalLength = 0.5; PetalWidth = 1.4 } | New-MLObject -Type IrisData
$testList += @{ SepalLength = 6.9; SepalWidth = 2.9; PetalLength = 2.1; PetalWidth = 0.7 } | New-MLObject -Type IrisData
$testList += @{ SepalLength = 4.3; SepalWidth = 3.2; PetalLength = 1.3; PetalWidth = 1.1 } | New-MLObject -Type IrisData
$testList += @{ SepalLength = 6.2; SepalWidth = 2.3; PetalLength = 0.8; PetalWidth = 0.9 } | New-MLObject -Type IrisData
$testList | Invoke-MLModel -Model $model -OutputType ClusterPrediction

# Get predictions for a whole dataview. This returns a dataview that you can convert to an enumerable (or use that dataview in further machine learning models).
$data = Import-MLData -Type IrisData -Path "$dataPath\iris.data" -Separator ','
$transformed = Invoke-MLModel -Model $model -Data $data
$transformed | ConvertTo-Enumerable -Type ClusterPrediction