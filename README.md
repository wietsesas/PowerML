# PowerML

This module is designed to harness the capabilities of Microsoft's machine learning framework [ML.Net](https://learn.microsoft.com/dotnet/machine-learning/) directly from within your PowerShell scripts and automation tasks.

**Version info**  
- Version: **1.0.0.1**  
- Compatible PowerShell edition: **Core**  
- PowerShell version: **7.3.8**  

**On this page**  
- [Installation](#installation)
- [Getting started](#getting-started)
  - [MLContext](#mlcontext)
  - [Types](#types)
- [Examples](#examples)
- [Cmdlets](#cmdlets)
  - [MLContext](#mlcontext)
  - [Custom types](#custom-types)
  - [Data](#data)
  - [Model](#model)
  - [Transforms](#transforms)
  - [Trainers](#trainers)
- [Contributors](#contributors)

## Installation

This PowerShell module has been published to the PowerShell Gallery. Use the following command to install the module on your local machine:
```pwsh
Install-Module PowerML
```

You can also download the [latest release](https://github.com/wietsesas/PowerML/releases/latest) from this github repository.

## Usage

### MLContext

The common context for all ML.NET operations. It provides a way to create components for data preparation, feature engineering, training, prediction, and model evaluation. It also allows logging, execution control, and the ability to set repeatable random numbers.

**A context is automatically created and cached on the first operation that requires a context.** This means you don't have to create a context or pass it to the cmdlets.

However, if you want the MLContext environment to become deterministic, you have to provide a fixed seed. In this case you can create your own context before calling another cmdlet that needs a context.
```pwsh
New-MLContext -Seed 1234567
```
If you want to work with multiple contexts at once, you can choose to not cache the context and pass the correct context to the subsequent cmdlets.
```pwsh
$context1 = New-MLContext -NoCache
$context2 = New-MLContext -Seed 1234567 -NoCache
# ...
$data1 = Import-MLData -Type '<type_name>' -Path '<path_to_data>' -Context $context1
$data2 = Import-MLData -Type '<type_name>' -Path '<path_to_data>' -Context $context2
```

### Types

ML input and output data is typed. You can use the cmdlet `Register-MLType` to register a new custom type.  
You can pass the type definition as a Hashtable.
```pwsh
Register-MLType -Definition @{
  Name = 'SentimentData'
  Properties = @{
    SentimentText = @{ Type = 'string'; LoadColumn = 0 }
    Sentiment = @{ Type = 'bool'; LoadColumn = 1; ColumnName = 'Label' }
  }
}
```
Or you can load the type from a json file containing the type definition.
```pwsh
Register-MLType -Path '<path_to_json_file>'
```
You can get the registered types with the cmdlet `Get-MLType`.
```pwsh
# Get all registered types
Get-MLType
# Get a specific registered type
Get-MLType -Type '<type_name>'
```
You can create an object of a registered type with the cmdlet `New-MLObject`.
```pwsh
@{
  Property1 = 1
  Property2 = 'Value2'
} | New-MLObject -Type '<type_name>'
```

## Examples

Examples are taken from the original Microsoft tutorials.

- [Analyze sentiment (binary classification)](/doc/examples/Analyze%20sentiment%20%28binary%20classification%29/README.md)
- [Categorize support issues (multiclass classification)](/doc\examples\Categorize%20support%20issues%20%28multiclass%20classification%29/README.md)
- [Predict prices (regression)](/doc/examples/Predict%20prices%20%28regression%29/README.md)
- [Categorize iris flowers (k-means clustering)](/doc/examples/Categorize%20iris%20flowers%20%28k-means%20clustering%29/README.md)
- [Recommend movies (matrix factorization)](/doc/examples/Recommend%20movies%20%28matrix%20factorization%29/README.md)
- [Image classification (transfer learning)](/doc/examples/Image%20Classification%20%28transfer%20learning%29/README.md)
- [Classify images (model composition)](/doc/examples/Classify%20images%20%28model%20composition%29/README.md)
- [Call-volume spikes (anomaly detection)](/doc/examples/Call-volume%20spikes%20%28anomaly%20detection%29/README.md)

## Cmdlets

### MLContext

| Name | Description |
| --- | --- |
| [Get-MLContext](/doc/cmdlets/Get-MLContext.md) | Get the current (cached) MLContext. |
| [New-MLContext](/doc/cmdlets/New-MLContext.md) | Create a new MLContext. |
| [Set-MLContext](/doc/cmdlets/Set-MLContext.md) | Set the current (cached) MLContext. |

### Custom types

| Name | Description |
| --- | --- |
| [Get-MLType](/doc/cmdlets/Get-MLType.md) | Get a registered type. |
| [New-MLObject](/doc/cmdlets/New-MLObject.md) | Create a new object of the specified registered type with the specified properties. |
| [Register-MLType](/doc/cmdlets/Register-MLType.md) | Register a custom type from a type definition. |

### Data

| Name | Description |
| --- | --- |
| [ConvertTo-Enumerable](/doc/cmdlets/ConvertTo-Enumerable.md) | Convert an IDataView to an enumerable list. |
| [Get-BootstrapSample](/doc/cmdlets/Get-BootstrapSample.md) | Take an approximate bootstrap sample of the input data. |
| [Import-MLData](/doc/cmdlets/Import-MLData.md) | Get data from memory, files or database. |
| [Measure-EntireAnomalyBySrCnn](/doc/cmdlets/Measure-EntireAnomalyBySrCnn.md) | Detect timeseries anomalies for entire input using SRCNN algorithm. |
| [Measure-Seasonality](/doc/cmdlets/Measure-Seasonality.md) | Detects this predictable interval (or period) by adopting techniques of fourier analysis. |
| [Select-MLData](/doc/cmdlets/Select-MLData.md) | Select a subset of data in a DataView. |
| [Split-MLData](/doc/cmdlets/Split-MLData.md) | Split the dataset into a train and test set or into cross-validation folds of train and test sets. |

### Model

| Name | Description |
| --- | --- |
| [Build-MLModel](/doc/cmdlets/Build-MLModel.md) | Train a machine learning model (Fit). |
| [Export-MLModel](/doc/cmdlets/Export-MLModel.md) | Export a machine learning model. |
| [Import-MLModel](/doc/cmdlets/Import-MLModel.md) | Import a machine learning model. |
| [Import-TensorFlowModel](/doc/cmdlets/Import-TensorFlowModel.md) | Load TensorFlow model into memory. |
| [Invoke-MLModel](/doc/cmdlets/Invoke-MLModel.md) | Transform data using a machine learning model. |
| [Test-MLModel](/doc/cmdlets/Test-MLModel.md) | Evaluate a machine learning model. |

### Transforms

| Name | Description |
| --- | --- |
| [Add-ApplyOnnxModelTransform](/doc/cmdlets/Add-ApplyOnnxModelTransform.md) | Transform the input data with an imported ONNX model. |
| [Add-ApplyWordEmbeddingTransform](/doc/cmdlets/Add-ApplyWordEmbeddingTransform.md) | Convert vectors of text tokens into sentence vectors using a pre-trained model. |
| [Add-ApproximatedKernelMapTransform](/doc/cmdlets/Add-ApproximatedKernelMapTransform.md) | Map each input vector onto a lower dimensional feature space, where inner products approximate a kernel function, so that the features can be used as inputs to the linear algorithms. |
| [Add-CacheCheckpoint](/doc/cmdlets/Add-CacheCheckpoint.md) | Append a caching checkpoint to an estimator chain. |
| [Add-CalculateFeatureContribution](/doc/cmdlets/Add-CalculateFeatureContribution.md) | Calculate contribution scores for each element of a feature vector. |
| [Add-ConcatenateTransform](/doc/cmdlets/Add-ConcatenateTransform.md) | Concatenate one or more input columns into a new output column. |
| [Add-ConvertToGrayscaleTransform](/doc/cmdlets/Add-ConvertToGrayscaleTransform.md) | Convert an image to grayscale. |
| [Add-ConvertToImageTransform](/doc/cmdlets/Add-ConvertToImageTransform.md) | Convert a vector of pixels to ImageDataViewType. |
| [Add-ConvertTypeTransform](/doc/cmdlets/Add-ConvertTypeTransform.md) | Convert the type of an input column to a new type. |
| [Add-CopyColumnsTransform](/doc/cmdlets/Add-CopyColumnsTransform.md) | Copy and rename one or more input columns. |
| [Add-DetectAnomalyBySrCnnTransform](/doc/cmdlets/Add-DetectAnomalyBySrCnnTransform.md) | Detect anomalies in the input time series data using the Spectral Residual (SR) algorithm. |
| [Add-DetectChangePointBySsaTransform](/doc/cmdlets/Add-DetectChangePointBySsaTransform.md) | Detect change points in time series data using singular spectrum analysis (SSA). |
| [Add-DetectIidChangePointTransform](/doc/cmdlets/Add-DetectIidChangePointTransform.md) | Detect change points in independent and identically distributed (IID) time series data using adaptive kernel density estimations and martingale scores. |
| [Add-DetectIidSpikeTransform](/doc/cmdlets/Add-DetectIidSpikeTransform.md) | Detect spikes in independent and identically distributed (IID) time series data using adaptive kernel density estimations and martingale scores. |
| [Add-DetectSpikeBySsaTransform](/doc/cmdlets/Add-DetectSpikeBySsaTransform.md) | Detect spikes in time series data using singular spectrum analysis (SSA). |
| [Add-DnnFeaturizeImageTransform](/doc/cmdlets/Add-DnnFeaturizeImageTransform.md) | Applies a pre-trained deep neural network (DNN) model to transform an input image into a feature vector. |
| [Add-DropColumnsTransform](/doc/cmdlets/Add-DropColumnsTransform.md) | Drop one or more input columns. |
| [Add-ExpressionTransform](/doc/cmdlets/Add-ExpressionTransform.md) | Apply an expression to transform columns into new ones. |
| [Add-ExtractPixelsTransform](/doc/cmdlets/Add-ExtractPixelsTransform.md) | Convert pixels from input image into a vector of numbers. |
| [Add-FeaturizeTextTransform](/doc/cmdlets/Add-FeaturizeTextTransform.md) | Transform a text column into a float array of normalized ngrams and char-grams counts. |
| [Add-ForecastBySsaTransform](/doc/cmdlets/Add-ForecastBySsaTransform.md) | Singular Spectrum Analysis (SSA) model for univariate time-series forecasting. |
| [Add-HashTransform](/doc/cmdlets/Add-HashTransform.md) | Hash the value in the input column. |
| [Add-IndicateMissingValuesTransform](/doc/cmdlets/Add-IndicateMissingValuesTransform.md) | Create a new boolean output column, the value of which is true when the value in the input column is missing. |
| [Add-IsotonicTransform](/doc/cmdlets/Add-IsotonicTransform.md) | Transforms a binary classifier raw score into a class probability by assigning scores to bins, where the position of boundaries and the size of bins are estimated using the training data. |
| [Add-LatentDirichletAllocationTransform](/doc/cmdlets/Add-LatentDirichletAllocationTransform.md) | Transform a document (represented as a vector of floats) into a vector of floats over a set of topics. |
| [Add-LoadImagesTransform](/doc/cmdlets/Add-LoadImagesTransform.md) | Load images from a folder into memory. |
| [Add-LoadRawImageBytesTransform](/doc/cmdlets/Add-LoadRawImageBytesTransform.md) | Loads images of raw bytes into a new column. |
| [Add-MapKeyToBinaryVectorTransform](/doc/cmdlets/Add-MapKeyToBinaryVectorTransform.md) | Convert keys back to a binary vector of original values. |
| [Add-MapKeyToValueTransform](/doc/cmdlets/Add-MapKeyToValueTransform.md) | Convert keys back to their original values. |
| [Add-MapKeyToVectorTransform](/doc/cmdlets/Add-MapKeyToVectorTransform.md) | Convert keys back to vectors of original values. |
| [Add-MapValueToKeyTransform](/doc/cmdlets/Add-MapValueToKeyTransform.md) | Map values to keys (categories) by creating the mapping from the input data. |
| [Add-MapValueTransform](/doc/cmdlets/Add-MapValueTransform.md) | Map values to keys (categories) based on the supplied dictionary of mappings. |
| [Add-NaiveTransform](/doc/cmdlets/Add-NaiveTransform.md) | Transforms a binary classifier raw score into a class probability by assigning scores to bins, and calculating the probability based on the distribution among the bins. |
| [Add-NormalizeBinningTransform](/doc/cmdlets/Add-NormalizeBinningTransform.md) | Assign the input value to a bin index and divide by the number of bins to produce a float value between 0 and 1. The bin boundaries are calculated to evenly distribute the training data across bins. |
| [Add-NormalizeGlobalContrastTransform](/doc/cmdlets/Add-NormalizeGlobalContrastTransform.md) | Scale each value in a row by subtracting the mean of the row data and divide by either the standard deviation or l2-norm (of the row data), and multiply by a configurable scale factor (default 2). |
| [Add-NormalizeLogMeanVarianceTransform](/doc/cmdlets/Add-NormalizeLogMeanVarianceTransform.md) | Normalize based on the logarithm of the training data. |
| [Add-NormalizeLpNormTransform](/doc/cmdlets/Add-NormalizeLpNormTransform.md) | Scale input vectors by their lp-norm, where p is 1, 2 or infinity. Defaults to the l2 (Euclidean distance) norm. |
| [Add-NormalizeMeanVarianceTransform](/doc/cmdlets/Add-NormalizeMeanVarianceTransform.md) | Subtract the mean (of the training data) and divide by the variance (of the training data). |
| [Add-NormalizeMinMaxTransform](/doc/cmdlets/Add-NormalizeMinMaxTransform.md) | Scale the input by the difference between the minimum and maximum values in the training data. |
| [Add-NormalizeRobustScalingTransform](/doc/cmdlets/Add-NormalizeRobustScalingTransform.md) | Scale each value using statistics that are robust to outliers that will center the data around 0 and scales the data according to the quantile range. |
| [Add-NormalizeSupervisedBinningTransform](/doc/cmdlets/Add-NormalizeSupervisedBinningTransform.md) | Assign the input value to a bin based on its correlation with label column. |
| [Add-NormalizeTextTransform](/doc/cmdlets/Add-NormalizeTextTransform.md) | Change case, remove diacritical marks, punctuation marks, and numbers. |
| [Add-OneHotEncodingTransform](/doc/cmdlets/Add-OneHotEncodingTransform.md) | Convert one or more text columns into one-hot encoded vectors. |
| [Add-OneHotHashEncodingTransform](/doc/cmdlets/Add-OneHotHashEncodingTransform.md) | Convert one or more text columns into hash-based one-hot encoded vectors. |
| [Add-PlattTransform](/doc/cmdlets/Add-PlattTransform.md) | Transforms a binary classifier raw score into a class probability using logistic regression with fixed parameters or parameters estimated using the training data. |
| [Add-ProduceHashedNgramsTransform](/doc/cmdlets/Add-ProduceHashedNgramsTransform.md) | Transform text column into a vector of hashed ngram counts. |
| [Add-ProduceHashedWordBagsTransform](/doc/cmdlets/Add-ProduceHashedWordBagsTransform.md) | Transform text column into a bag of hashed ngram counts. |
| [Add-ProduceNgramsTransform](/doc/cmdlets/Add-ProduceNgramsTransform.md) | Transform text column into a bag of counts of ngrams (sequences of consecutive words). |
| [Add-ProduceWordBagsTransform](/doc/cmdlets/Add-ProduceWordBagsTransform.md) | Transform text column into a bag of counts of ngrams vector. |
| [Add-ProjectToPrincipalComponentsTransform](/doc/cmdlets/Add-ProjectToPrincipalComponentsTransform.md) | Reduce the dimensions of the input feature vector by applying the Principal Component Analysis algorithm. |
| [Add-RemoveStopWordsTransform](/doc/cmdlets/Add-RemoveStopWordsTransform.md) | Remove default (for the specified language) or specified stop words from input columns. |
| [Add-ReplaceMissingValuesTransform](/doc/cmdlets/Add-ReplaceMissingValuesTransform.md) | Create a new output column, the value of which is set to a default value if the value is missing from the input column, and the input value otherwise. |
| [Add-ResizeImagesTransform](/doc/cmdlets/Add-ResizeImagesTransform.md) | Resize images. |
| [Add-ScoreTensorFlowModelTransform](/doc/cmdlets/Add-ScoreTensorFlowModelTransform.md) | Scores a dataset using a pre-trained TensorFlow model. |
| [Add-SelectColumnsTransform](/doc/cmdlets/Add-SelectColumnsTransform.md) | Select one or more columns to keep from the input data. |
| [Add-SelectFeaturesBasedOnCountTransform](/doc/cmdlets/Add-SelectFeaturesBasedOnCountTransform.md) | Select features whose non-default values are greater than a threshold. |
| [Add-SelectFeaturesBasedOnMutualInformationTransform](/doc/cmdlets/Add-SelectFeaturesBasedOnMutualInformationTransform.md) | Select the features on which the data in the label column is most dependent. |
| [Add-TokenizeIntoCharactersAsKeysTransform](/doc/cmdlets/Add-TokenizeIntoCharactersAsKeysTransform.md) | Split one or more text columns into individual characters floats over a set of topics. |
| [Add-TokenizeIntoWordsTransform](/doc/cmdlets/Add-TokenizeIntoWordsTransform.md) | Split one or more text columns into individual words. |

### Trainers

| Name | Description |
| --- | --- |
| [Add-AveragedPerceptronTrainer](/doc/cmdlets/Add-AveragedPerceptronTrainer.md) | Predict a target using a linear binary classification model trained with the averaged perceptron. |
| [Add-FastForestBinaryTrainer](/doc/cmdlets/Add-FastForestBinaryTrainer.md) | Train a decision tree binary classification model using Fast Forest. |
| [Add-FastForestRegressionTrainer](/doc/cmdlets/Add-FastForestRegressionTrainer.md) | Train a decision tree regression model using Fast Forest. |
| [Add-FastTreeBinaryTrainer](/doc/cmdlets/Add-FastTreeBinaryTrainer.md) | Train a decision tree binary classification model using FastTree. |
| [Add-FastTreeRankingTrainer](/doc/cmdlets/Add-FastTreeRankingTrainer.md) | Train a decision tree ranking model using FastTree. |
| [Add-FastTreeRegressionTrainer](/doc/cmdlets/Add-FastTreeRegressionTrainer.md) | Train a decision tree regression model using FastTree. |
| [Add-FastTreeTweedieTrainer](/doc/cmdlets/Add-FastTreeTweedieTrainer.md) | Train a decision tree regression model using Tweedie loss function. This trainer is a generalization of Poisson, compound Poisson, and gamma regression. |
| [Add-FieldAwareFactorizationMachineTrainer](/doc/cmdlets/Add-FieldAwareFactorizationMachineTrainer.md) | Predict a target using a field-aware factorization machine model trained using a stochastic gradient method. |
| [Add-GamBinaryTrainer](/doc/cmdlets/Add-GamBinaryTrainer.md) | Train a binary classification model with generalized additive models (GAM). |
| [Add-GamRegressionTrainer](/doc/cmdlets/Add-GamRegressionTrainer.md) | Train a regression model with generalized additive models (GAM). |
| [Add-ImageClassificationTrainer](/doc/cmdlets/Add-ImageClassificationTrainer.md) | Train a Deep Neural Network (DNN) to classify images. |
| [Add-KMeansTrainer](/doc/cmdlets/Add-KMeansTrainer.md) | Train a KMeans cluster. |
| [Add-LbfgsLogisticRegressionBinaryTrainer](/doc/cmdlets/Add-LbfgsLogisticRegressionBinaryTrainer.md) | Predict a target using a linear logistic regression model trained with L-BFGS method. |
| [Add-LbfgsMaximumEntropyMulticlassTrainer](/doc/cmdlets/Add-LbfgsMaximumEntropyMulticlassTrainer.md) | Predict a target using a maximum entropy multiclass classifier trained with L-BFGS method. |
| [Add-LbfgsPoissonRegressionTrainer](/doc/cmdlets/Add-LbfgsPoissonRegressionTrainer.md) | Train a Poisson regression model. |
| [Add-LdSvmTrainer](/doc/cmdlets/Add-LdSvmTrainer.md) | Predict a target using a non-linear binary classification model trained with Local Deep SVM. |
| [Add-LightGbmBinaryTrainer](/doc/cmdlets/Add-LightGbmBinaryTrainer.md) | Train a boosted decision tree binary classification model using LightGBM. |
| [Add-LightGbmMulticlassTrainer](/doc/cmdlets/Add-LightGbmMulticlassTrainer.md) | Train a boosted decision tree multi-class classification model using LightGBM. |
| [Add-LightGbmRankingTrainer](/doc/cmdlets/Add-LightGbmRankingTrainer.md) | Train a boosted decision tree ranking model using LightGBM. |
| [Add-LightGbmRegressionTrainer](/doc/cmdlets/Add-LightGbmRegressionTrainer.md) | Train a boosted decision tree regression model using LightGBM. |
| [Add-LinearSvmTrainer](/doc/cmdlets/Add-LinearSvmTrainer.md) | Predict a target using a linear binary classification model trained with Linear SVM. |
| [Add-MatrixFactorizationTrainer](/doc/cmdlets/Add-MatrixFactorizationTrainer.md) | Predict elements in a matrix using matrix factorization (also known as a type of collaborative filtering). |
| [Add-NaiveBayesMulticlassTrainer](/doc/cmdlets/Add-NaiveBayesMulticlassTrainer.md) | Predicts a multiclass target using a Naive Bayes model that supports binary feature values. |
| [Add-OlsTrainer](/doc/cmdlets/Add-OlsTrainer.md) | Train a linear regression model using ordinary least squares (OLS) for estimating the parameters of the linear regression model. |
| [Add-OnlineGradientDescentTrainer](/doc/cmdlets/Add-OnlineGradientDescentTrainer.md) | Train a linear regression model using Online Gradient Descent (OGD) for estimating the parameters of the linear regression model. |
| [Add-PriorTrainer](/doc/cmdlets/Add-PriorTrainer.md) | Predicting a target using a binary classification model. |
| [Add-RandomizedPcaTrainer](/doc/cmdlets/Add-RandomizedPcaTrainer.md) | Train an approximate PCA using Randomized SVD algorithm. |
| [Add-SdcaLogisticRegressionBinaryTrainer](/doc/cmdlets/Add-SdcaLogisticRegressionBinaryTrainer.md) | Train a binary logistic regression classification model using the stochastic dual coordinate ascent method. The trained model is calibrated and can produce probability by feeding the output value of the linear function to a PlattCalibrator. |
| [Add-SdcaMaximumEntropyMulticlassTrainer](/doc/cmdlets/Add-SdcaMaximumEntropyMulticlassTrainer.md) | Predict a target using a maximum entropy multiclass classifier. The trained model MaximumEntropyModelParameters produces probabilities of classes. |
| [Add-SdcaNonCalibratedBinaryTrainer](/doc/cmdlets/Add-SdcaNonCalibratedBinaryTrainer.md) | Train a binary logistic regression classification model using the stochastic dual coordinate ascent method. |
| [Add-SdcaNonCalibratedMulticlassTrainer](/doc/cmdlets/Add-SdcaNonCalibratedMulticlassTrainer.md) | Predict a target using a linear multiclass classifier. The trained model LinearMulticlassModelParameters produces probabilities of classes. |
| [Add-SdcaRegressionTrainer](/doc/cmdlets/Add-SdcaRegressionTrainer.md) | Training a regression model using the stochastic dual coordinate ascent method. |
| [Add-SgdCalibratedBinaryTrainer](/doc/cmdlets/Add-SgdCalibratedBinaryTrainer.md) | Train logistic regression using a parallel stochastic gradient method. The trained model is calibrated and can produce probability by feeding the output value of the linear function to a PlattCalibrator. |
| [Add-SgdNonCalibratedBinaryTrainer](/doc/cmdlets/Add-SgdNonCalibratedBinaryTrainer.md) | Train logistic regression using a parallel stochastic gradient method. |
| [Add-SymbolicSgdLogisticRegressionBinaryTrainer](/doc/cmdlets/Add-SymbolicSgdLogisticRegressionBinaryTrainer.md) | Train logistic regression using a parallel stochastic gradient method. |

## Contributors

Feel free to send pull requests or fill out issues when you encounter them.


