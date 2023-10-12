$modelPath = '<path_to_your_model_directory>'

# Make sure the model path exists
if (-not (Test-Path -Path $modelPath -PathType Container)) { New-Item -Path $modelPath -ItemType Directory -Force | Out-Null }

# Type TaxiTrip
@{
  Name = 'TaxiTrip'
  Properties = @{
    VendorId = @{ Type = 'string'; LoadColumn = 0 }
    RateCode = @{ Type = 'string'; LoadColumn = 1 }
    PassengerCount  = @{ Type = 'float'; LoadColumn = 2 }
    TripTime  = @{ Type = 'float'; LoadColumn = 3 }
    TripDistance  = @{ Type = 'float'; LoadColumn = 4 }
    PaymentType  = @{ Type = 'string'; LoadColumn = 5 }
    FareAmount  = @{ Type = 'float'; LoadColumn = 6 }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\TaxiTrip.json"

# Type TaxiTripFarePrediction
# VendorId, RateCode, PassengerCount, TripTime, TripDistance and PaymentType is added to retain the source information, but is not necessary
@{
  Name = 'TaxiTripFarePrediction'
  Properties = @{
    VendorId = @{ Type = 'string' }
    RateCode = @{ Type = 'string' }
    PassengerCount  = @{ Type = 'float' }
    TripTime  = @{ Type = 'float' }
    TripDistance  = @{ Type = 'float' }
    PaymentType  = @{ Type = 'string' }
    FareAmount = @{ Type = 'float'; ColumnName = 'Score' }
  }
} | ConvertTo-Json | Out-File -FilePath "$modelPath\TaxiTripFarePrediction.json"
