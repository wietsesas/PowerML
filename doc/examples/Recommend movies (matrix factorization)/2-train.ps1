$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\MovieRating.json"
Register-MLType "$modelPath\MovieRatingPrediction.json"

# Load the data
$data = Import-MLData -Type MovieRating -Path "$dataPath\recommendation-ratings-train.csv" -HasHeader -Separator ','

# Create the Model pipeline
$pipeline = Add-MapValueToKeyTransform -OutputColumn 'UserIdEncoded' -InputColumn 'UserId' |
  Add-MapValueToKeyTransform -OutputColumn 'MovieIdEncoded' -InputColumn 'MovieId' |
  Add-MatrixFactorizationTrainer -ColumnIndexColumn 'UserIdEncoded' -RowIndexColumn 'MovieIdEncoded' -Iterations 20 -ApproximationRank 100

# Train the model and export to file
$model = $pipeline | Build-MLModel -Data $data
Export-MLModel -Model $model -Path "$modelPath\model.zip" -Schema $data.Schema -Force

# Test the machine learning model with the test data
$testData = Import-MLData -Type MovieRating -Path "$dataPath\recommendation-ratings-test.csv" -HasHeader -Separator ','
Test-MLModel -Model $model -Regression -Data $testData