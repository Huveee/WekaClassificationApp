using System;
using System.Windows.Forms;
using weka.core;
using weka.filters;
using weka.filters.unsupervised.attribute;
using weka.classifiers;

namespace WekaClassificationApp
{
	public static class DataHandler
	{
		/// <summary>
		/// Loads a dataset from the specified file path.
		/// </summary>
		/// <param name="filePath">Path to the dataset file.</param>
		/// <returns>The loaded dataset as Instances.</returns>
		public static Instances LoadDataset(string filePath)
		{
			try
			{
				java.io.File inputFile = new java.io.File(filePath);
				Console.WriteLine($"File path: {filePath}");
				Console.WriteLine($"File exists: {inputFile.exists()}");
				java.io.FileReader fileReader = new java.io.FileReader(inputFile);
				Instances data = new Instances(fileReader);
				data.setClassIndex(data.numAttributes() - 1);
				return data;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error loading dataset: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
		}

		/// <summary>
		/// Transforms the dataset for the specified algorithm.
		/// </summary>
		/// <param name="dataset">The dataset to transform.</param>
		/// <param name="algorithm">The algorithm for which the dataset is being transformed.</param>
		/// <returns>The transformed dataset.</returns>
		public static Instances TransformDataset(Instances dataset, string algorithm)
		{
			Instances transformedDataset = new Instances(dataset);

			try
			{
				if (algorithm == "Naive Bayes")
				{
					// Convert numeric to nominal
					var discretizeFilter = new Discretize();
					discretizeFilter.setInputFormat(transformedDataset);
					transformedDataset = Filter.useFilter(transformedDataset, discretizeFilter);
				}
				else if (algorithm == "Logistic Regression" ||
						 algorithm == "K-Nearest Neighbors" ||
						 algorithm == "Artificial Neural Network" ||
						 algorithm == "Support Vector Machine")
				{
					// Normalize numeric attributes
					var normalizeFilter = new Normalize();
					normalizeFilter.setInputFormat(transformedDataset);
					transformedDataset = Filter.useFilter(transformedDataset, normalizeFilter);

					// Convert nominal to numeric
					var nominalToBinaryFilter = new NominalToBinary();
					nominalToBinaryFilter.setInputFormat(transformedDataset);
					transformedDataset = Filter.useFilter(transformedDataset, nominalToBinaryFilter);
				}

				// Decision Tree: No transformation needed
				return transformedDataset;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error during data transformation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
		}

		/// <summary>
		/// Classifies the dataset using Naive Bayes.
		/// </summary>
		public static (double Accuracy, weka.classifiers.Classifier Model) ClassifyWithNaiveBayes(Instances dataset)
		{
			var classifier = new weka.classifiers.bayes.NaiveBayes();
			classifier.buildClassifier(dataset);

			var eval = new weka.classifiers.Evaluation(dataset);
			eval.crossValidateModel(classifier, dataset, 10, new java.util.Random(1));

			return (eval.pctCorrect(), classifier);
		}

		/// <summary>
		/// Classifies the dataset using Logistic Regression.
		/// </summary>
		public static (double Accuracy, weka.classifiers.Classifier Model) ClassifyWithLogisticRegression(Instances dataset)
		{
			var classifier = new weka.classifiers.functions.Logistic();
			classifier.buildClassifier(dataset);

			var eval = new weka.classifiers.Evaluation(dataset);
			eval.crossValidateModel(classifier, dataset, 10, new java.util.Random(1));

			return (eval.pctCorrect(), classifier);
		}

		/// <summary>
		/// Classifies the dataset using K-Nearest Neighbors.
		/// </summary>
		public static (double Accuracy, weka.classifiers.Classifier Model) ClassifyWithKNN(Instances dataset, int k)
		{
			var classifier = new weka.classifiers.lazy.IBk();
			classifier.setKNN(k); // Set the number of neighbors
			classifier.buildClassifier(dataset);

			var eval = new weka.classifiers.Evaluation(dataset);
			eval.crossValidateModel(classifier, dataset, 10, new java.util.Random(1));

			return (eval.pctCorrect(), classifier);
		}

		/// <summary>
		/// Classifies the dataset using Decision Tree.
		/// </summary>
		public static (double Accuracy, weka.classifiers.Classifier Model) ClassifyWithDecisionTree(Instances dataset)
		{
			var classifier = new weka.classifiers.trees.J48();
			classifier.buildClassifier(dataset);

			var eval = new weka.classifiers.Evaluation(dataset);
			eval.crossValidateModel(classifier, dataset, 10, new java.util.Random(1));

			return (eval.pctCorrect(), classifier);
		}

		/// <summary>
		/// Classifies the dataset using Artificial Neural Network.
		/// </summary>
		public static (double Accuracy, weka.classifiers.Classifier Model) ClassifyWithANN(Instances dataset)
		{
			var classifier = new weka.classifiers.functions.MultilayerPerceptron();
			classifier.buildClassifier(dataset);

			var eval = new weka.classifiers.Evaluation(dataset);
			eval.crossValidateModel(classifier, dataset, 10, new java.util.Random(1));

			return (eval.pctCorrect(), classifier);
		}

		/// <summary>
		/// Classifies the dataset using Support Vector Machine.
		/// </summary>
		public static (double Accuracy, weka.classifiers.Classifier Model) ClassifyWithSVM(Instances dataset)
		{
			var classifier = new weka.classifiers.functions.SMO();
			classifier.buildClassifier(dataset);

			var eval = new weka.classifiers.Evaluation(dataset);
			eval.crossValidateModel(classifier, dataset, 10, new java.util.Random(1));

			return (eval.pctCorrect(), classifier);
		}

		/// <summary>
		/// Predicts the output for a new instance using the given algorithm and dataset.
		/// </summary>
		public static string PredictWithAlgorithm(string algorithm, Instances dataset, Instance instance)
		{
			try
			{
				weka.classifiers.Classifier classifier;
				switch (algorithm)
				{
					case "Naive Bayes":
						classifier = new weka.classifiers.bayes.NaiveBayes();
						break;

					case "Logistic Regression":
						classifier = new weka.classifiers.functions.Logistic();
						break;

					case "K-Nearest Neighbors":
						classifier = new weka.classifiers.lazy.IBk();
						break;

					case "Decision Tree":
						classifier = new weka.classifiers.trees.J48();
						break;

					case "Artificial Neural Network":
						classifier = new weka.classifiers.functions.MultilayerPerceptron();
						break;

					case "Support Vector Machine":
						classifier = new weka.classifiers.functions.SMO();
						break;

					default:
						throw new ArgumentException("Unsupported algorithm.");
				}

				classifier.buildClassifier(dataset);
				double predictedClassIndex = classifier.classifyInstance(instance);
				return dataset.classAttribute().value((int)predictedClassIndex);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error during prediction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return "Error";
			}
		}
	}
}
