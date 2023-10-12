$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\IrisData.json"
Register-MLType "$modelPath\ClusterPrediction.json"

# Load the data
$data = Import-MLData -Type IrisData -Path "$dataPath\iris.data" -Separator	','

# Create the transformer pipeline
$pipeline = Add-ConcatenateTransform -OutputColumn 'Features' -InputColumn 'SepalLength', 'SepalWidth', 'PetalLength', 'PetalWidth' |
  Add-KMeansTrainer -Clusters 3

# Train the model and export to file
$model = $pipeline | Build-MLModel -Data $data
Export-MLModel -Model $model -Path "$modelPath\model.zip" -Schema $data.Schema -Force