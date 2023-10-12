$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\SentimentData.json"
Register-MLType "$modelPath\SentimentPrediction.json"

# Import the model (created in 2-train.ps1)
$model = Import-MLModel -Path "$modelPath\model.zip"

# Get a prediction for a single item
$testObject = @{ SentimentText = 'Worst ever' } | New-MLObject -Type SentimentData
$testObject | Invoke-MLModel -Model $model -OutputType SentimentPrediction

# Get predictions for multiple items at once
$testList = @()
$testList += @{ SentimentText = 'Worst ever' } | New-MLObject -Type SentimentData
$testList += @{ SentimentText = 'Better than last time' } | New-MLObject -Type SentimentData
$testList += @{ SentimentText = 'Just ok' } | New-MLObject -Type SentimentData
$testList += @{ SentimentText = 'Very good' } | New-MLObject -Type SentimentData
$testList | Invoke-MLModel -Model $model -OutputType SentimentPrediction

# Get predictions for a whole dataview. This returns a dataview that you can convert to an enumerable (or use that dataview in further machine learning models).
$data = Import-MLData -Type SentimentData -Path "$dataPath\imdb_labelled.txt"
$transformed = Invoke-MLModel -Model $model -Data $data
$transformed | ConvertTo-Enumerable -Type SentimentPrediction