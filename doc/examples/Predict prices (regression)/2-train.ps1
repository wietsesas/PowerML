$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\TaxiTrip.json"
Register-MLType "$modelPath\TaxiTripFarePrediction.json"

# Load the data
$data = Import-MLData -Type TaxiTrip -Path "$dataPath\taxi-fare-train.csv" -HasHeader -Separator ','

# Create the transformation pipeline
$pipeline = Add-CopyColumnsTransform -OutputColumn 'Label' -InputColumn 'FareAmount' |
  Add-OneHotEncodingTransform -OutputColumn 'VendorIdEncoded' -InputColumn 'VendorId' |
  Add-OneHotEncodingTransform -OutputColumn 'RateCodeEncoded' -InputColumn 'RateCode' |
  Add-OneHotEncodingTransform -OutputColumn 'PaymentTypeEncoded' -InputColumn 'PaymentType' |
  Add-ConcatenateTransform -OutputColumn 'Features' -InputColumns 'VendorIdEncoded', 'RateCodeEncoded', 'PassengerCount', 'TripDistance', 'PaymentTypeEncoded' |
  Add-FastTreeRegressionTrainer

# Train the model and export to file
$model = $pipeline | Build-MLModel -Data $data
Export-MLModel -Model $model -Path "$modelPath\model.zip" -Schema $data.Schema -Force

# Evaluate the machine learning model with the test data
$testData = Import-MLData -Type TaxiTrip -Path "$dataPath\taxi-fare-test.csv" -HasHeader -Separator ','
Test-MLModel -Model $model -Data $testData -Regression