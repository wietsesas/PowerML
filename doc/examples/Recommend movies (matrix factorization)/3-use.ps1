$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\MovieRating.json"
Register-MLType "$modelPath\MovieRatingPrediction.json"

# Import the model (created in 2-train.ps1)
$model = Import-MLModel -Path "$modelPath\model.zip"

# Get a prediction for a single item
$testObject = @{
  UserId = 6
  MovieId = 10
} | New-MLObject -Type MovieRating
$testObject | Invoke-MLModel -Model $model -OutputType MovieRatingPrediction

# Get predictions for multiple items at once
$testList = @()
$testList += @{ UserId = 6; MovieId = 10 } | New-MLObject -Type MovieRating
$testList += @{ UserId = 4; MovieId = 8 } | New-MLObject -Type MovieRating
$testList | Invoke-MLModel -Model $model -OutputType MovieRatingPrediction

# Get predictions for a whole dataview. This returns a dataview that you can convert to an enumerable (or use that dataview in further machine learning models).
$data = Import-MLData -Type MovieRating -Path "$dataPath\recommendation-ratings-test.csv" -HasHeader -Separator ','
$transformed = Invoke-MLModel -Model $model -Data $data
$transformed | ConvertTo-Enumerable -Type MovieRatingPrediction
