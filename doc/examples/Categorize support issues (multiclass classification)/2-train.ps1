$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\GitHubIssue.json"
Register-MLType "$modelPath\IssuePrediction.json"

# Load the data
$data = Import-MLData -Type GitHubIssue -Path "$dataPath\issues_train.tsv" -HasHeader

# Create the transformer pipeline
# Warning: Use Add-CacheCheckpoint for small/medium datasets to lower training time. Do NOT use it (remove Add-CacheCheckpoint) when handling very large datasets.
$pipeline = Add-MapValueToKeyTransform -OutputColumn 'Label' -InputColumn 'Area' |
  Add-FeaturizeTextTransform -OutputColumn 'TitleFeaturized' -InputColumn 'Title' |
  Add-FeaturizeTextTransform -OutputColumn 'DescriptionFeaturized' -InputColumn 'Description' |
  Add-ConcatenateTransform -OutputColumn 'Features' -InputColumns 'TitleFeaturized', 'DescriptionFeaturized' |
  Add-CacheCheckpoint |
  Add-SdcaMaximumEntropyMulticlassTrainer |
  Add-MapKeyToValueTransform -OutputColumn 'PredictedLabel'

# Train the model and export to file
$model = $pipeline | Build-MLModel -Data $data
Export-MLModel -Model $model -Path "$modelPath\model.zip" -Schema $data.Schema -Force

# Evaluate the machine learning model with the test data
$testData = Import-MLData -Type GitHubIssue -Path "$dataPath\issues_test.tsv" -HasHeader
Test-MLModel -Model $model -Data $testData -MulticlassClassification
