$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\SentimentData.json"
Register-MLType "$modelPath\SentimentPrediction.json"

# Load the data and split into a train and test set
$data = Import-MLData -Type SentimentData -Path "$dataPath\amazon_cells_labelled.txt"
$splitData = $data | Split-MLData -TestFraction 0.2
$trainData = $splitData.TrainSet
$testData = $splitData.TestSet

# Create the transformer pipeline
$pipeline = Add-FeaturizeTextTransform -OutputColumn 'Features' -InputColumn 'SentimentText' |
  Add-SdcaLogisticRegressionBinaryTrainer

# Train the model and export to file
$model = $pipeline | Build-MLModel -Data $trainData
Export-MLModel -Model $model -Path "$modelPath\model.zip" -Schema $trainData.Schema -Force

# Evaluate the machine learning model with the test data
Test-MLModel -Model $model -BinaryClassification -Data $testData