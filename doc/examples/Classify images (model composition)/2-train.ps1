$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\ImageData.json"
Register-MLType "$modelPath\ImagePrediction.json"

# Load the data
$data = Import-MLData -Type ImageData -Path "$dataPath\images\tags.tsv"

# Create the transformation pipeline
$pipeline = Add-LoadImagesTransform -OutputColumn 'input' -InputColumn 'ImagePath' -Path "$dataPath\images" |
  Add-ResizeImagesTransform -OutputColumn 'input' -Height 224 -Width 224 |
  Add-ExtractPixelsTransform -OutputColumn 'input' -InterleaveColors -Offset 117 |
  Add-ScoreTensorFlowModelTransform -Path "$dataPath\inception\tensorflow_inception_graph.pb" -OutputColumn 'softmax2_pre_activation' -InputColumn 'input' -AddBatchDimension |
  Add-MapValueToKeyTransform -OutputColumn 'LabelKey' -InputColumn 'Label' |
  Add-LbfgsMaximumEntropyMulticlassTrainer -LabelColumn 'LabelKey' -FeatureColumn 'softmax2_pre_activation' |
  Add-MapKeyToValueTransform -OutputColumn 'PredictedLabelValue' -InputColumn 'PredictedLabel' |
  Add-CacheCheckpoint

# Train the model and export to file
$model = $pipeline | Build-MLModel -Data $data
Export-MLModel -Model $model -Path "$modelPath\model.zip" -Schema $data.Schema -Force

# Evaluate the machine learning model with the test data
$testData = Import-MLData -Type ImageData -Path "$dataPath\images\test-tags.tsv"
Test-MLModel -Model $model -Data $testData -MulticlassClassification -LabelColumn 'LabelKey' -PredictedLabelColumn 'PredictedLabel'
