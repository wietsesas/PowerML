$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\TaxiTrip.json"
Register-MLType "$modelPath\TaxiTripFarePrediction.json"

# Import the model (created in 2-train.ps1)
$model = Import-MLModel -Path "$modelPath\model.zip"

# Get a prediction for a single item
$testObject = @{
  VendorId = 'VTS'
  RateCode = '1'
  PassengerCount = 1
  TripTime = 1140
  TripDistance = 3.75
  PaymentType = "CRD"
  FareAmount = 0 # To predict.
} | New-MLObject -Type TaxiTrip
$testObject | Invoke-MLModel -Model $model -OutputType TaxiTripFarePrediction

# Get predictions for multiple items at once
$testList = @()
$testList += @{ VendorId = 'VTS'; RateCode = '1'; PassengerCount = 1; TripTime = 1140; TripDistance = 3.75; PaymentType = 'CRD' } | New-MLObject -Type TaxiTrip
$testList += @{ VendorId = 'CMT'; RateCode = '1'; PassengerCount = 3; TripTime = 2450; TripDistance = 4.75; PaymentType = 'CRD' } | New-MLObject -Type TaxiTrip
$testList += @{ VendorId = 'VTS'; RateCode = '2'; PassengerCount = 2; TripTime = 970; TripDistance = 2; PaymentType = 'CSH' } | New-MLObject -Type TaxiTrip
$testList += @{ VendorId = 'CMT'; RateCode = '2'; PassengerCount = 2; TripTime = 5360; TripDistance = 6.9; PaymentType = 'CSH' } | New-MLObject -Type TaxiTrip
$testList | Invoke-MLModel -Model $model -OutputType TaxiTripFarePrediction

# Get predictions for a whole dataview. This returns a dataview that you can convert to an enumerable (or use that dataview in further machine learning models).
$data = Import-MLData -Type TaxiTrip -Path "$dataPath\taxi-fare-test.csv" -HasHeader -Separator ','
$transformed = Invoke-MLModel -Model $model -Data $data
$transformed | ConvertTo-Enumerable -Type TaxiTripFarePrediction