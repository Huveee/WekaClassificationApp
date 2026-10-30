namespace WekaClassificationApp
{
	partial class Form1
	{
		/// <summary>
		///Gerekli tasarımcı değişkeni.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///Kullanılan tüm kaynakları temizleyin.
		/// </summary>
		///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer üretilen kod

		/// <summary>
		/// Tasarımcı desteği için gerekli metot - bu metodun 
		///içeriğini kod düzenleyici ile değiştirmeyin.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnLoadDataset = new System.Windows.Forms.Button();
			this.txtBestAlgorithm = new System.Windows.Forms.TextBox();
			this.txtDatasetFile = new System.Windows.Forms.TextBox();
			this.txtAccuracy = new System.Windows.Forms.TextBox();
			this.panelAttributes = new System.Windows.Forms.Panel();
			this.btnDiscover = new System.Windows.Forms.Button();
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.lblDataset = new System.Windows.Forms.Label();
			this.lblBestAlgorithm = new System.Windows.Forms.Label();
			this.lblAccuracy = new System.Windows.Forms.Label();
			this.lblAttributes = new System.Windows.Forms.Label();
			this.lblPrediction = new System.Windows.Forms.Label();
			this.groupBoxDataset = new System.Windows.Forms.GroupBox();
			this.groupBoxResults = new System.Windows.Forms.GroupBox();
			this.groupBoxPrediction = new System.Windows.Forms.GroupBox();
			
			// Initialize groupboxes first
			this.groupBoxDataset.SuspendLayout();
			this.groupBoxResults.SuspendLayout();
			this.groupBoxPrediction.SuspendLayout();
			this.SuspendLayout();

			// Dataset GroupBox
			this.groupBoxDataset.Controls.Add(this.lblDataset);
			this.groupBoxDataset.Controls.Add(this.txtDatasetFile);
			this.groupBoxDataset.Controls.Add(this.btnLoadDataset);
			this.groupBoxDataset.Location = new System.Drawing.Point(12, 12);
			this.groupBoxDataset.Name = "groupBoxDataset";
			this.groupBoxDataset.Size = new System.Drawing.Size(776, 80);
			this.groupBoxDataset.TabIndex = 7;
			this.groupBoxDataset.Text = "Dataset Selection";
			
			// Results GroupBox
			this.groupBoxResults.Controls.Add(this.lblBestAlgorithm);
			this.groupBoxResults.Controls.Add(this.txtBestAlgorithm);
			this.groupBoxResults.Controls.Add(this.lblAccuracy);
			this.groupBoxResults.Controls.Add(this.txtAccuracy);
			this.groupBoxResults.Location = new System.Drawing.Point(12, 98);
			this.groupBoxResults.Name = "groupBoxResults";
			this.groupBoxResults.Size = new System.Drawing.Size(776, 80);
			this.groupBoxResults.TabIndex = 8;
			this.groupBoxResults.Text = "Analysis Results";

			// Prediction GroupBox
			this.groupBoxPrediction.Controls.Add(this.lblAttributes);
			this.groupBoxPrediction.Controls.Add(this.panelAttributes);
			this.groupBoxPrediction.Controls.Add(this.btnDiscover);
			this.groupBoxPrediction.Controls.Add(this.lblPrediction);
			this.groupBoxPrediction.Controls.Add(this.txtOutput);
			this.groupBoxPrediction.Location = new System.Drawing.Point(12, 184);
			this.groupBoxPrediction.Name = "groupBoxPrediction";
			this.groupBoxPrediction.Size = new System.Drawing.Size(776, 254);
			this.groupBoxPrediction.TabIndex = 9;
			this.groupBoxPrediction.Text = "Make Prediction";

			// Labels
			this.lblDataset.AutoSize = true;
			this.lblDataset.Location = new System.Drawing.Point(16, 32);
			this.lblDataset.Name = "lblDataset";
			this.lblDataset.Size = new System.Drawing.Size(80, 16);
			this.lblDataset.TabIndex = 7;
			this.lblDataset.Text = "Dataset File:";

			this.lblBestAlgorithm.AutoSize = true;
			this.lblBestAlgorithm.Location = new System.Drawing.Point(16, 32);
			this.lblBestAlgorithm.Name = "lblBestAlgorithm";
			this.lblBestAlgorithm.Size = new System.Drawing.Size(95, 16);
			this.lblBestAlgorithm.TabIndex = 8;
			this.lblBestAlgorithm.Text = "Best Algorithm:";

			this.lblAccuracy.AutoSize = true;
			this.lblAccuracy.Location = new System.Drawing.Point(580, 32);
			this.lblAccuracy.Name = "lblAccuracy";
			this.lblAccuracy.Size = new System.Drawing.Size(66, 16);
			this.lblAccuracy.TabIndex = 9;
			this.lblAccuracy.Text = "Accuracy:";

			this.lblAttributes.AutoSize = true;
			this.lblAttributes.Location = new System.Drawing.Point(16, 32);
			this.lblAttributes.Name = "lblAttributes";
			this.lblAttributes.Size = new System.Drawing.Size(127, 16);
			this.lblAttributes.TabIndex = 10;
			this.lblAttributes.Text = "Instance Attributes:";

			this.lblPrediction.AutoSize = true;
			this.lblPrediction.Location = new System.Drawing.Point(16, 190);
			this.lblPrediction.Name = "lblPrediction";
			this.lblPrediction.Size = new System.Drawing.Size(71, 16);
			this.lblPrediction.TabIndex = 11;
			this.lblPrediction.Text = "Prediction:";

			// Buttons
			this.btnLoadDataset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnLoadDataset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLoadDataset.ForeColor = System.Drawing.Color.White;
			this.btnLoadDataset.Location = new System.Drawing.Point(583, 29);
			this.btnLoadDataset.Name = "btnLoadDataset";
			this.btnLoadDataset.Size = new System.Drawing.Size(175, 30);
			this.btnLoadDataset.TabIndex = 0;
			this.btnLoadDataset.Text = "Browse Dataset";
			this.btnLoadDataset.UseVisualStyleBackColor = false;
			this.btnLoadDataset.Click += new System.EventHandler(this.btnLoadDataset_Click);

			this.btnDiscover.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnDiscover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDiscover.ForeColor = System.Drawing.Color.White;
			this.btnDiscover.Location = new System.Drawing.Point(19, 150);
			this.btnDiscover.Name = "btnDiscover";
			this.btnDiscover.Size = new System.Drawing.Size(175, 30);
			this.btnDiscover.TabIndex = 5;
			this.btnDiscover.Text = "Discover";
			this.btnDiscover.UseVisualStyleBackColor = false;
			this.btnDiscover.Click += new System.EventHandler(this.btnDiscover_Click);

			// TextBoxes
			this.txtDatasetFile.BackColor = System.Drawing.SystemColors.Window;
			this.txtDatasetFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDatasetFile.Location = new System.Drawing.Point(102, 30);
			this.txtDatasetFile.Name = "txtDatasetFile";
			this.txtDatasetFile.ReadOnly = true;
			this.txtDatasetFile.Size = new System.Drawing.Size(475, 22);
			this.txtDatasetFile.TabIndex = 2;

			this.txtBestAlgorithm.BackColor = System.Drawing.SystemColors.Window;
			this.txtBestAlgorithm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtBestAlgorithm.Location = new System.Drawing.Point(117, 30);
			this.txtBestAlgorithm.Name = "txtBestAlgorithm";
			this.txtBestAlgorithm.ReadOnly = true;
			this.txtBestAlgorithm.Size = new System.Drawing.Size(460, 22);
			this.txtBestAlgorithm.TabIndex = 1;

			this.txtAccuracy.BackColor = System.Drawing.SystemColors.Window;
			this.txtAccuracy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtAccuracy.Location = new System.Drawing.Point(652, 30);
			this.txtAccuracy.Name = "txtAccuracy";
			this.txtAccuracy.ReadOnly = true;
			this.txtAccuracy.Size = new System.Drawing.Size(106, 22);
			this.txtAccuracy.TabIndex = 3;

			this.txtOutput.BackColor = System.Drawing.SystemColors.Window;
			this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtOutput.Location = new System.Drawing.Point(93, 188);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.Size = new System.Drawing.Size(665, 22);
			this.txtOutput.TabIndex = 6;

			// Panel
			this.panelAttributes.AutoScroll = true;
			this.panelAttributes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelAttributes.Location = new System.Drawing.Point(19, 51);
			this.panelAttributes.Name = "panelAttributes";
			this.panelAttributes.Size = new System.Drawing.Size(739, 93);
			this.panelAttributes.TabIndex = 4;

			// Form
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.groupBoxPrediction);
			this.Controls.Add(this.groupBoxResults);
			this.Controls.Add(this.groupBoxDataset);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.Name = "Form1";
			this.Text = "Weka Classification App";
			this.groupBoxDataset.ResumeLayout(false);
			this.groupBoxDataset.PerformLayout();
			this.groupBoxResults.ResumeLayout(false);
			this.groupBoxResults.PerformLayout();
			this.groupBoxPrediction.ResumeLayout(false);
			this.groupBoxPrediction.PerformLayout();
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Button btnLoadDataset;
		private System.Windows.Forms.TextBox txtBestAlgorithm;
		private System.Windows.Forms.TextBox txtDatasetFile;
		private System.Windows.Forms.TextBox txtAccuracy;
		private System.Windows.Forms.Panel panelAttributes;
		private System.Windows.Forms.Button btnDiscover;
		private System.Windows.Forms.TextBox txtOutput;
		private System.Windows.Forms.Label lblDataset;
		private System.Windows.Forms.Label lblBestAlgorithm;
		private System.Windows.Forms.Label lblAccuracy;
		private System.Windows.Forms.Label lblAttributes;
		private System.Windows.Forms.Label lblPrediction;
		private System.Windows.Forms.GroupBox groupBoxDataset;
		private System.Windows.Forms.GroupBox groupBoxResults;
		private System.Windows.Forms.GroupBox groupBoxPrediction;
	}
}

