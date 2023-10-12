$dataPath = '<path_to_your_data_directory>'
$modelPath = '<path_to_your_model_directory>'

# Register the data types (created in 1-types.ps1)
Register-MLType "$modelPath\GitHubIssue.json"
Register-MLType "$modelPath\IssuePrediction.json"

# Import the model (created in 2-train.ps1)
$model = Import-MLModel -Path "$modelPath\model.zip"

# Get a prediction for a single item
$testObject = @{
  Id = '24611'
  Area = '' # To predict
  Title = 'Ignore the type in SGEN if it contains any property that only have private setter'
  Description = 'Fix #19723 @shmao @zhenlan @mconnew'
} | New-MLObject -Type GitHubIssue
$testObject | Invoke-MLModel -Model $model -OutputType IssuePrediction

# Get predictions for multiple items at once
$testList = @()
$testList += @{ Id = '99996'; Area = 'unknown'; Title = 'Ignore the type in SGEN if it contains any property that only have private setter'; Description = 'Fix #19723 @shmao @zhenlan @mconnew' } | New-MLObject -Type GitHubIssue
$testList += @{ Id = '99997'; Area = 'unknown'; Title = 'Mark packages as stable for 2.0.3'; Description = 'cc: @weshaggard' } | New-MLObject -Type GitHubIssue
$testList += @{ Id = '99998'; Area = 'unknown'; Title = 'Remove search trimming	Trimming prevents access to valid Windows paths. We''ve removed trimming from the rest of our API surface, this is the last place we have it.'; Description = 'Fixes #21096' } | New-MLObject -Type GitHubIssue
$testList += @{ Id = '99999'; Area = 'unknown'; Title = '"Test: System.Collections.Tests.Perf_ArrayList/Indexer failed with ""System.OutOfMemoryException"""'; Description = 'Opened on behalf of @Jiayili1  The test `System.Collections.Tests.Perf_ArrayList/Indexer` has failed.  System.OutOfMemoryException : Exception of type ''System.OutOfMemoryException'' was thrown.          Stack Trace:           Build : Master - 20171013.01 (Core Tests) Failing configurations: - Windows.81.Amd64-x86   - Release - Windows.7.Amd64-x86   - Release - Windows.10.Amd64.Core-x86   - Release - Windows.10.Amd64-x86   - Release  Detail: https://mc.dot.net/#/product/netcore/master/source/official~2Fcorefx~2Fmaster~2F/type/test~2Ffunctional~2Fcli~2F/build/20171013.01/workItem/System.Collections.NonGeneric.Performance.Tests/analysis/xunit/System.Collections.Tests.Perf_ArrayList~2FIndexer' } | New-MLObject -Type GitHubIssue
$testList | Invoke-MLModel -Model $model -OutputType IssuePrediction

# Get predictions for a whole dataview. This returns a dataview that you can convert to an enumerable (or use that dataview in further machine learning models).
$data = Import-MLData -Type GitHubIssue -Path "$dataPath\issues_test.tsv" -HasHeader
$transformed = Invoke-MLModel -Model $model -Data $data
$transformed | ConvertTo-Enumerable -Type IssuePrediction