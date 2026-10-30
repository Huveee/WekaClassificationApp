using System;
using System.Windows.Forms;
using weka.core;
using System.ComponentModel;

namespace WekaClassificationApp
{
	public partial class Form1 : Form
	{
		private Instances currentDataset; // Holds the loaded dataset
		private string bestAlgorithm;    // Stores the best algorithm name
		private BackgroundWorker backgroundWorker;
		private ProgressBar progressBar;

		public Form1()
		{
			InitializeComponent();
			InitializeBackgroundWorker();
			InitializeProgressBar();
		}

		private void InitializeBackgroundWorker()
		{
			backgroundWorker = new BackgroundWorker();
			backgroundWorker.WorkerReportsProgress = true;
			backgroundWorker.DoWork += BackgroundWorker_DoWork;
			backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
			backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
		}

		private void InitializeProgressBar()
		{
			progressBar = new ProgressBar
			{
				Location = new System.Drawing.Point(67, 102),
				Size = new System.Drawing.Size(646, 23),
				Visible = false
			};
			this.groupBoxResults.Controls.Add(progressBar);
		}

		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var dataset = (Instances)e.Argument;
			double bestAccuracy = 0.0;
			string bestAlgo = "";

			// Test Naive Bayes
			try
			{
				backgroundWorker.ReportProgress(0, "Testing Naive Bayes...");
				var naiveBayesResult = DataHandler.ClassifyWithNaiveBayes(dataset);
				if (naiveBayesResult.Accuracy > bestAccuracy)
				{
					bestAccuracy = naiveBayesResult.Accuracy;
					bestAlgo = "Naive Bayes";
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Naive Bayes error: {ex.Message}");
			}

			// Test Logistic Regression
			try
			{
				backgroundWorker.ReportProgress(20, "Testing Logistic Regression...");
				var logisticResult = DataHandler.ClassifyWithLogisticRegression(dataset);
				if (logisticResult.Accuracy > bestAccuracy)
				{
					bestAccuracy = logisticResult.Accuracy;
					bestAlgo = "Logistic Regression";
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Logistic Regression error: {ex.Message}");
			}

			// Test K-Nearest Neighbors
			try
			{
				backgroundWorker.ReportProgress(40, "Testing K-Nearest Neighbors...");
				var knnResult = DataHandler.ClassifyWithKNN(dataset, 3);
				if (knnResult.Accuracy > bestAccuracy)
				{
					bestAccuracy = knnResult.Accuracy;
					bestAlgo = "K-Nearest Neighbors";
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"KNN error: {ex.Message}");
			}

			// Test Decision Tree
			try
			{
				backgroundWorker.ReportProgress(60, "Testing Decision Tree...");
				var decisionTreeResult = DataHandler.ClassifyWithDecisionTree(dataset);
				if (decisionTreeResult.Accuracy > bestAccuracy)
				{
					bestAccuracy = decisionTreeResult.Accuracy;
					bestAlgo = "Decision Tree";
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Decision Tree error: {ex.Message}");
			}

			// Skip ANN if dataset is too large
			if (dataset.numInstances() < 1000)
			{
				try
				{
					backgroundWorker.ReportProgress(80, "Testing Neural Network...");
					
					// Create a timeout task for ANN
					var annTask = System.Threading.Tasks.Task.Run(() => DataHandler.ClassifyWithANN(dataset));
					if (System.Threading.Tasks.Task.WaitAny(new[] { annTask }, 10000) == 0) // 10 second timeout
					{
						var annResult = annTask.Result;
						if (annResult.Accuracy > bestAccuracy)
						{
							bestAccuracy = annResult.Accuracy;
							bestAlgo = "Artificial Neural Network";
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"ANN error: {ex.Message}");
				}
			}

			// Test Support Vector Machine
			try
			{
				backgroundWorker.ReportProgress(90, "Testing SVM...");
				var svmResult = DataHandler.ClassifyWithSVM(dataset);
				if (svmResult.Accuracy > bestAccuracy)
				{
					bestAccuracy = svmResult.Accuracy;
					bestAlgo = "Support Vector Machine";
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"SVM error: {ex.Message}");
			}

			backgroundWorker.ReportProgress(100, "Analysis complete");
			e.Result = (bestAlgo, bestAccuracy);
		}

		private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar.Value = e.ProgressPercentage;
			txtBestAlgorithm.Text = e.UserState.ToString();
		}

		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				MessageBox.Show($"Error during analysis: {e.Error.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				progressBar.Visible = false;
				return;
			}

			var (algorithm, accuracy) = ((string, double))e.Result;
			bestAlgorithm = algorithm;
			
			txtBestAlgorithm.Text = bestAlgorithm;
			txtAccuracy.Text = $"Accuracy: {accuracy:F2}%";

			// Generate dynamic inputs for dataset attributes
			GenerateAttributeInputs(currentDataset);
			
			// Enable controls
			btnLoadDataset.Enabled = true;
			btnDiscover.Enabled = true;
			progressBar.Visible = false;
		}

		private void GenerateAttributeInputs(Instances dataset)
		{
			panelAttributes.Controls.Clear();
			int yOffset = 10;
			const int LABEL_WIDTH = 150;
			const int INPUT_WIDTH = 200;
			const int HEIGHT = 25;
			const int SPACING = 35;

			for (int i = 0; i < dataset.numAttributes() - 1; i++) // Exclude class attribute
			{
				var attribute = dataset.attribute(i);

				// Create label
				var label = new Label
				{
					Text = attribute.name(),
					Location = new System.Drawing.Point(10, yOffset),
					Size = new System.Drawing.Size(LABEL_WIDTH, HEIGHT),
					AutoSize = false,
					TextAlign = System.Drawing.ContentAlignment.MiddleLeft
				};
				panelAttributes.Controls.Add(label);

				if (attribute.isNominal())
				{
					var comboBox = new ComboBox
					{
						Name = $"comboBoxAttr{i}",
						Location = new System.Drawing.Point(LABEL_WIDTH + 20, yOffset),
						Size = new System.Drawing.Size(INPUT_WIDTH, HEIGHT),
						DropDownStyle = ComboBoxStyle.DropDownList,
						Font = new System.Drawing.Font("Microsoft Sans Serif", 9F)
					};

					// Add all nominal values as options
					for (int j = 0; j < attribute.numValues(); j++)
					{
						comboBox.Items.Add(attribute.value(j));
					}
					if (comboBox.Items.Count > 0)
						comboBox.SelectedIndex = 0;

					panelAttributes.Controls.Add(comboBox);
				}
				else if (attribute.isNumeric())
				{
					var textBox = new TextBox
					{
						Name = $"textBoxAttr{i}",
						Location = new System.Drawing.Point(LABEL_WIDTH + 20, yOffset),
						Size = new System.Drawing.Size(INPUT_WIDTH, HEIGHT),
						BorderStyle = BorderStyle.FixedSingle,
						Font = new System.Drawing.Font("Microsoft Sans Serif", 9F)
					};

					// Add numeric validation
					textBox.KeyPress += (sender, e) =>
					{
						if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
						{
							e.Handled = true;
						}

						// Allow only one decimal point
						if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
						{
							e.Handled = true;
						}

						// Allow minus sign only at the start
						if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
						{
							e.Handled = true;
						}
					};

					panelAttributes.Controls.Add(textBox);
				}

				yOffset += SPACING;
			}
		}

		private void btnLoadDataset_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "ARFF files (*.arff)|*.arff|CSV files (*.csv)|*.csv";

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string filePath = openFileDialog.FileName;
				txtDatasetFile.Text = filePath;  // Display the selected file path

				try
				{
					// Disable controls during processing
					btnLoadDataset.Enabled = false;
					btnDiscover.Enabled = false;
					txtBestAlgorithm.Text = "Loading dataset...";
					txtAccuracy.Text = "";
					panelAttributes.Controls.Clear();
					progressBar.Value = 0;
					progressBar.Visible = true;

					// Load the dataset
					currentDataset = DataHandler.LoadDataset(filePath);
					if (currentDataset != null)
					{
						// Start background processing
						backgroundWorker.RunWorkerAsync(currentDataset);
					}
					else
					{
						btnLoadDataset.Enabled = true;
						btnDiscover.Enabled = true;
						txtBestAlgorithm.Text = "";
						progressBar.Visible = false;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error loading dataset: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					btnLoadDataset.Enabled = true;
					btnDiscover.Enabled = true;
					txtBestAlgorithm.Text = "";
					progressBar.Visible = false;
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

                // Transform the instance for the best algorithm
                var transformedInputInstance = DataHandler.TransformInstance(inputInstance, transformedDataset);


                // Predict the output using the backend
                string predictedClass = DataHandler.PredictWithAlgorithm(bestAlgorithm, transformedDataset, transformedInputInstance);

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
