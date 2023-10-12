$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\ImageData.json"
Register-MLType "$modelPath\ImagePrediction.json"

# Import the model (created in 2-train.ps1)
$model = Import-MLModel -Path "$modelPath\model.zip"

# Get a prediction for a single item
$testObject = @{ ImagePath = '<path_to_image_file>' } | New-MLObject -Type ImageData
$testObject | Invoke-MLModel -Model $model -OutputType ImagePrediction

# Get predictions for multiple items at once
$testList = @(
  @{ ImagePath = '<path_to_image_file>' } | New-MLObject -Type ImageData
  @{ ImagePath = '<path_to_image_file>' } | New-MLObject -Type ImageData
  @{ ImagePath = '<path_to_image_file>' } | New-MLObject -Type ImageData
  @{ ImagePath = '<path_to_image_file>' } | New-MLObject -Type ImageData
)
$testList | Invoke-MLModel -Model $model -OutputType ImagePrediction

# Get predictions for a whole dataview. This returns a dataview that you can convert to an enumerable (or use that dataview in further machine learning models).
$data = Import-MLData -Type ImageData -Path "$dataPath\images\test-tags.tsv"
$transformed = Invoke-MLModel -Model $model -Data $data
$transformed | ConvertTo-Enumerable -Type ImagePrediction