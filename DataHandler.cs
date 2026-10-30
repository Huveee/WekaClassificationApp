using System;
using System.Windows.Forms;
using weka.core;
using weka.filters;
using weka.filters.unsupervised.attribute;
using weka.classifiers;
using java.io;

namespace WekaClassificationApp
{
    public static class DataHandler
    {
        public static Instances LoadDataset(string filePath)
        {
            try
            {
                File inputFile = new File(filePath);
                FileReader fileReader = new FileReader(inputFile);
                Instances data = new Instances(fileReader);

                data.setClassIndex(data.numAttributes() - 1);

                var stringToNominalFilter = new StringToNominal();
                stringToNominalFilter.setAttributeRange("first-last");
                stringToNominalFilter.setInputFormat(data);
                Instances transformedData = Filter.useFilter(data, stringToNominalFilter);

                return transformedData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dataset: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static Instances TransformDataset(Instances dataset, string algorithm)
        {
            try
            {
                Instances transformedDataset = new Instances(dataset);

                if (algorithm == "Naive Bayes")
                {
                    var numericToNominalFilter = new Discretize();
                    numericToNominalFilter.setInputFormat(transformedDataset);
                    transformedDataset = Filter.useFilter(transformedDataset, numericToNominalFilter);
                }

                if (algorithm == "Logistic Regression" ||
                    algorithm == "K-Nearest Neighbors" ||
                    algorithm == "Artificial Neural Network" ||
                    algorithm == "Support Vector Machine")
                {
                    var nominalToNumericFilter = new NominalToBinary();
                    nominalToNumericFilter.setInputFormat(transformedDataset);
                    transformedDataset = Filter.useFilter(transformedDataset, nominalToNumericFilter);

                    var normalizeFilter = new Normalize();
                    normalizeFilter.setInputFormat(transformedDataset);
                    transformedDataset = Filter.useFilter(transformedDataset, normalizeFilter);
                }

                return transformedDataset;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during data transformation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static Instance TransformInstance(Instance instance, Instances trainingDataset)
        {
            Instance transformedInstance = new DenseInstance(trainingDataset.numAttributes());
            transformedInstance.setDataset(trainingDataset);

            for (int i = 0; i < trainingDataset.numAttributes(); i++)
            {
                if (i < instance.numAttributes())
                {
                    transformedInstance.setValue(i, instance.value(i));
                }
                else
                {
                    if (trainingDataset.attribute(i).isNominal())
                    {
                        transformedInstance.setValue(i, 0);
                    }
                    else if (trainingDataset.attribute(i).isNumeric())
                    {
                        transformedInstance.setValue(i, 0.0);
                    }
                }
            }
            return transformedInstance;
        }

        public static string PredictWithAlgorithm(string algorithm, Instances dataset, Instance instance)
        {
            try
            {
                if (dataset.numAttributes() != instance.numAttributes())
                {
                    throw new Exception($"Attribute count mismatch: Dataset has {dataset.numAttributes()} attributes, but instance has {instance.numAttributes()} attributes.");
                }

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
                System.Console.WriteLine($"Error during prediction: {ex.Message}");
                System.Console.WriteLine(ex.StackTrace);
                return "Error";
            }
        }


        /// <summary>
        /// Classifies the dataset using Naive Bayes.
        /// </summary>
        public static (double Accuracy, weka.classifiers.Classifier Model) ClassifyWithNaiveBayes(Instances dataset)
        {
            try
            {
                // Dataset'i dönüştür
                var transformedDataset = TransformDataset(dataset, "Naive Bayes");

                // Naive Bayes sınıflandırıcıyı oluştur
                var classifier = new weka.classifiers.bayes.NaiveBayes();
                classifier.buildClassifier(transformedDataset);

                // Modeli değerlendir
                var eval = new weka.classifiers.Evaluation(transformedDataset);
                eval.crossValidateModel(classifier, transformedDataset, 10, new java.util.Random(1));

                return (eval.pctCorrect(), classifier);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error classifying dataset with Naive Bayes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (0, null);
            }
        }


        /// <summary>
        /// Classifies the dataset using K-Nearest Neighbors.
        /// </summary>
        public static (double Accuracy, weka.classifiers.Classifier Model) ClassifyWithKNN(Instances dataset, int k = 3)
        {
            try
            {
                var classifier = new weka.classifiers.lazy.IBk();
                classifier.setKNN(k);
                classifier.buildClassifier(dataset);

                var eval = new weka.classifiers.Evaluation(dataset);
                eval.crossValidateModel(classifier, dataset, 10, new java.util.Random(1));

                return (eval.pctCorrect(), classifier);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error classifying dataset with K-Nearest Neighbors: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (0, null);
            }
        }

        /// <summary>
        /// Classifies the dataset using Decision Tree.
        /// </summary>
        public static (double Accuracy, weka.classifiers.Classifier Model) ClassifyWithDecisionTree(Instances dataset)
        {
            try
            {
                var classifier = new weka.classifiers.trees.J48();
                classifier.buildClassifier(dataset);

                var eval = new weka.classifiers.Evaluation(dataset);
                eval.crossValidateModel(classifier, dataset, 10, new java.util.Random(1));

                return (eval.pctCorrect(), classifier);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error classifying dataset with Decision Tree: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (0, null);
            }
        }

        /// <summary>
        /// Classifies the dataset using Artificial Neural Network.
        /// </summary>
        public static (double Accuracy, weka.classifiers.Classifier Model) ClassifyWithANN(Instances dataset)
        {
            try
            {
                var classifier = new weka.classifiers.functions.MultilayerPerceptron();
                classifier.buildClassifier(dataset);

                var eval = new weka.classifiers.Evaluation(dataset);
                eval.crossValidateModel(classifier, dataset, 10, new java.util.Random(1));

                return (eval.pctCorrect(), classifier);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error classifying dataset with Artificial Neural Network: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (0, null);
            }
        }

        /// <summary>
        /// Classifies the dataset using Support Vector Machine.
        /// </summary>
        public static (double Accuracy, weka.classifiers.Classifier Model) ClassifyWithSVM(Instances dataset)
        {
            try
            {
                var classifier = new weka.classifiers.functions.SMO();
                classifier.buildClassifier(dataset);

                var eval = new weka.classifiers.Evaluation(dataset);
                eval.crossValidateModel(classifier, dataset, 10, new java.util.Random(1));

                return (eval.pctCorrect(), classifier);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error classifying dataset with Support Vector Machine: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (0, null);
            }
        }

        /// <summary>
        /// Classifies the dataset using Logistic Regression.
        /// </summary>
        public static (double Accuracy, weka.classifiers.Classifier Model) ClassifyWithLogisticRegression(Instances dataset)
        {
            try
            {
                var classifier = new weka.classifiers.functions.Logistic();
                classifier.buildClassifier(dataset);

                var eval = new weka.classifiers.Evaluation(dataset);
                eval.crossValidateModel(classifier, dataset, 10, new java.util.Random(1));

                return (eval.pctCorrect(), classifier);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error classifying dataset with Logistic Regression: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (0, null);
            }
        }
    }
}
