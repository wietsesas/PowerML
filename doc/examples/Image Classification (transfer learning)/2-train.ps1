$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\ImageData.json"
Register-MLType "$modelPath\ImagePrediction.json"

# Load the image paths into objects
$images = $dataPath | Get-ChildItem -File -Recurse | ForEach-Object {
  @{ ImagePath = $PSItem.FullName; Label = $PSItem.Directory.Name } | New-MLObject -Type ImageData
}

# Load and shuffle the data
$data = Import-MLData -Type ImageData -Data $images | Select-MLData -Shuffle

# Create the data transformation pipeline
$dataPipeline = Add-MapValueToKeyTransform -OutputColumn 'LabelAsKey' -InputColumn 'Label' |
  Add-LoadRawImageBytesTransform -OutputColumn 'Image' -InputColumn 'ImagePath' -Path $dataPath
  
# Train the data model and export to file
$dataModel = $dataPipeline | Build-MLModel -Data $data
Export-MLModel -Model $dataModel -Path "$modelPath\dataModel.zip" -Schema $data.Schema -Force

# Preprocess the data and split into a train and test set
$transformedData = Invoke-MLModel -Model $dataModel -Data $data
$splitData = $transformedData | Split-MLData -TestFraction 0.3
$validateionSplitData = $splitData.TestSet | Split-MLData
$trainData = $splitData.TrainSet
$validationData = $validateionSplitData.TrainSet
$testData = $validateionSplitData.TestSet

# Use the ConvertTo-Enumerable cmdlet to check some data
$testData | ConvertTo-Enumerable -Type ImageData

# Create the transformation pipeline
$metricsCallback = { $_ }
$pipeline = Add-ImageClassificationTrainer -FeatureColumn 'Image' -LabelColumn 'LabelAsKey' -ValidationSet $validationData -Arch ResnetV2101 -MetricsCallback $metricsCallback -DontTestOnTrainSet -ReuseTrainSetBottleneckCachedValues -ReuseValidationSetBottleneckCachedValues |
  Add-MapKeyToValueTransform -OutputColumn 'PredictedLabel'

# Train the model and export to file
$model = $pipeline | Build-MLModel -Data $trainData
Export-MLModel -Model $model -Path "$modelPath\classificationModel.zip" -Schema $trainData.Schema -Force

# Evaluate the machine learning model with the test data
Invoke-MLModel -Model $model -Data $testData | ConvertTo-Enumerable -Type ImagePrediction