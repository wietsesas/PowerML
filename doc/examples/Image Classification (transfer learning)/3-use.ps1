$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\ImageData.json"
Register-MLType "$modelPath\ImagePrediction.json"

# Import the model (created in 2-train.ps1)
$dataModel = Import-MLModel -Path "$modelPath\dataModel.zip"
$model = Import-MLModel -Path "$modelPath\classificationModel.zip"

# Get a prediction for a single item
$testObject = @{ ImagePath = '<path_to_image_file>' } | New-MLObject -Type ImageData
$testObject | Invoke-MLModel -Model $dataModel -OutputType ImageData |
  Invoke-MLModel -Model $model -OutputType ImagePrediction

# Get predictions for multiple items at once
$testList = @(
  @{ ImagePath = '<path_to_image_file>' } | New-MLObject -Type ImageData
  @{ ImagePath = '<path_to_image_file>' } | New-MLObject -Type ImageData
  @{ ImagePath = '<path_to_image_file>' } | New-MLObject -Type ImageData
  @{ ImagePath = '<path_to_image_file>' } | New-MLObject -Type ImageData
)
$testList | Invoke-MLModel -Model $dataModel -OutputType ImageData |
  Invoke-MLModel -Model $model -OutputType ImagePrediction

# Get predictions for a whole dataview. This returns a dataview that you can convert to an enumerable (or use that dataview in further machine learning models).
$data = Import-MLData -Type ImageData -Data $testList
$transformedData = Invoke-MLModel -Model $dataModel -Data $data
$predictedData = Invoke-MLModel -Model $model -Data $transformedData
$predictedData | ConvertTo-Enumerable -Type ImagePrediction