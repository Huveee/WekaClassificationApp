# WekaClassificationApp

A Windows Forms desktop app that finds the best-performing classifier for a dataset and uses it to predict new instances. Built with C# on .NET Framework 4.8, using Weka through IKVM.

## How it works

1. Load an ARFF dataset. The last attribute is treated as the class.
2. The app evaluates these algorithms with 10-fold cross-validation:
   - Naive Bayes
   - Logistic Regression
   - K-Nearest Neighbors (k = 3)
   - Decision Tree (J48)
   - Multilayer Perceptron (only for datasets under 1,000 instances)
   - Support Vector Machine (SMO)
3. It shows the most accurate algorithm and its accuracy.
4. It generates an input field for each attribute, so you can enter values and predict their class.

## Building

Open `WekaClassificationApp.sln` in Visual Studio.

`weka.dll` and the `IKVM.*.dll` files are not in this repository, and the project's references point to folders on the original development machine. Get those DLLs and update the references before building.
