# Example: Categorize support issues (multiclass classification)

Original example: https://learn.microsoft.com/en-us/dotnet/machine-learning/tutorials/image-classification

## Data

- Download The [project assets directory zip file](https://github.com/dotnet/samples/blob/main/machine-learning/tutorials/TransferLearningTF/image-classifier-assets.zip), and unzip into your data folder.
- Download the [Inception model](https://storage.googleapis.com/download.tensorflow.org/models/inception5h.zip), and unzip. Copy the contents of the inception5h directory just unzipped into your data folder `inception` directory. This directory contains the model and additional support files needed for this tutorial.

## Usage

- Create the necessary data types ([1-types.ps1](1-types.ps1)).
- Train the machine learning model ([2-train.ps1](2-train.ps1)).
- Use the machine learning model ([3-use.ps1](3-use.ps1))
