using System;
using System.Windows.Forms;
using weka.core;

namespace WekaClassificationApp
{
	public partial class Form1 : Form
	{
		private Instances currentDataset; // Holds the loaded dataset
		private string bestAlgorithm;    // Stores the best algorithm name

		public Form1()
		{
			InitializeComponent();
		}

		private (string Algorithm, double Accuracy) FindBestAlgorithm(Instances dataset)
		{
			double bestAccuracy = 0.0;
			string bestAlgorithm = "";

			// Test Naive Bayes
			var naiveBayesResult = DataHandler.ClassifyWithNaiveBayes(dataset);
			if (naiveBayesResult.Accuracy > bestAccuracy)
			{
				bestAccuracy = naiveBayesResult.Accuracy;
				bestAlgorithm = "Naive Bayes";
			}

			// Test Logistic Regression
			var logisticResult = DataHandler.ClassifyWithLogisticRegression(dataset);
			if (logisticResult.Accuracy > bestAccuracy)
			{
				bestAccuracy = logisticResult.Accuracy;
				bestAlgorithm = "Logistic Regression";
			}

			// Test K-Nearest Neighbors
			var knnResult = DataHandler.ClassifyWithKNN(dataset, 3); // Example: K=3
			if (knnResult.Accuracy > bestAccuracy)
			{
				bestAccuracy = knnResult.Accuracy;
				bestAlgorithm = "K-Nearest Neighbors";
			}

			// Test Decision Tree
			var decisionTreeResult = DataHandler.ClassifyWithDecisionTree(dataset);
			if (decisionTreeResult.Accuracy > bestAccuracy)
			{
				bestAccuracy = decisionTreeResult.Accuracy;
				bestAlgorithm = "Decision Tree";
			}

			// Test Artificial Neural Network
			var annResult = DataHandler.ClassifyWithANN(dataset);
			if (annResult.Accuracy > bestAccuracy)
			{
				bestAccuracy = annResult.Accuracy;
				bestAlgorithm = "Artificial Neural Network";
			}

			// Test Support Vector Machine
			var svmResult = DataHandler.ClassifyWithSVM(dataset);
			if (svmResult.Accuracy > bestAccuracy)
			{
				bestAccuracy = svmResult.Accuracy;
				bestAlgorithm = "Support Vector Machine";
			}

			return (bestAlgorithm, bestAccuracy);
		}


		private void GenerateAttributeInputs(Instances dataset)
		{
			panelAttributes.Controls.Clear();

			for (int i = 0; i < dataset.numAttributes() - 1; i++) // Exclude class attribute
			{
				var attribute = dataset.attribute(i);

				if (attribute.isNominal())
				{
					var comboBox = new ComboBox
					{
						Name = $"comboBoxAttr{i}",
						Width = 150
					};

					// Add all nominal values as options
					for (int j = 0; j < attribute.numValues(); j++)
					{
						comboBox.Items.Add(attribute.value(j));
					}

					panelAttributes.Controls.Add(new Label { Text = attribute.name() });
					panelAttributes.Controls.Add(comboBox);
				}
				else if (attribute.isNumeric())
				{
					var textBox = new TextBox
					{
						Name = $"textBoxAttr{i}",
						Width = 150
					};

					panelAttributes.Controls.Add(new Label { Text = attribute.name() });
					panelAttributes.Controls.Add(textBox);
				}
			}
		}

		private void btnLoadDataset_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "ARFF files (*.arff)|*.arff|CSV files (*.csv)|*.csv";

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string filePath = openFileDialog.FileName;

				try
				{
					// Load the dataset
					currentDataset = DataHandler.LoadDataset(filePath);

					// Automatically find the best algorithm
					var (algorithm, accuracy) = FindBestAlgorithm(currentDataset);

					// Display the best algorithm and its accuracy
					bestAlgorithm = algorithm;
					txtBestAlgorithm.Text = bestAlgorithm;
					txtAccuracy.Text = $"Accuracy: {accuracy:F2}%";

					// Generate dynamic inputs for dataset attributes
					GenerateAttributeInputs(currentDataset);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error loading dataset: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnDiscover_Click(object sender, EventArgs e)
		{
			if (currentDataset == null || string.IsNullOrEmpty(bestAlgorithm))
			{
				MessageBox.Show("Please load a dataset and find the best algorithm first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				// Create a new instance for prediction
				var inputInstance = new DenseInstance(currentDataset.numAttributes());
				inputInstance.setDataset(currentDataset);

				// Retrieve user inputs for each attribute
				for (int i = 0; i < currentDataset.numAttributes() - 1; i++) // Exclude class attribute
				{
					var attribute = currentDataset.attribute(i);

					if (attribute.isNominal())
					{
						var comboBox = panelAttributes.Controls.Find($"comboBoxAttr{i}", true)[0] as ComboBox;
						inputInstance.setValue(attribute, comboBox.SelectedItem.ToString());
					}
					else if (attribute.isNumeric())
					{
						var textBox = panelAttributes.Controls.Find($"textBoxAttr{i}", true)[0] as TextBox;
						inputInstance.setValue(attribute, double.Parse(textBox.Text));
					}
				}

				// Transform the dataset for the best algorithm
				var transformedDataset = DataHandler.TransformDataset(currentDataset, bestAlgorithm);

				// Predict the output using the backend
				string predictedClass = DataHandler.PredictWithAlgorithm(bestAlgorithm, transformedDataset, inputInstance);

				// Display the prediction result
				txtOutput.Text = $"Predicted Class: {predictedClass}";
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error during prediction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
