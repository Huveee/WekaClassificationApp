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
			this.SuspendLayout();
			// 
			// btnLoadDataset
			// 
			this.btnLoadDataset.Location = new System.Drawing.Point(555, 25);
			this.btnLoadDataset.Name = "btnLoadDataset";
			this.btnLoadDataset.Size = new System.Drawing.Size(158, 23);
			this.btnLoadDataset.TabIndex = 0;
			this.btnLoadDataset.Text = "Browse Dataset";
			this.btnLoadDataset.UseVisualStyleBackColor = true;
			this.btnLoadDataset.Click += new System.EventHandler(this.btnLoadDataset_Click);
			// 
			// txtBestAlgorithm
			// 
			this.txtBestAlgorithm.BackColor = System.Drawing.SystemColors.Window;
			this.txtBestAlgorithm.Location = new System.Drawing.Point(67, 74);
			this.txtBestAlgorithm.Name = "txtBestAlgorithm";
			this.txtBestAlgorithm.ReadOnly = true;
			this.txtBestAlgorithm.Size = new System.Drawing.Size(501, 22);
			this.txtBestAlgorithm.TabIndex = 1;
			// 
			// txtDatasetFile
			// 
			this.txtDatasetFile.BackColor = System.Drawing.SystemColors.HighlightText;
			this.txtDatasetFile.Location = new System.Drawing.Point(67, 26);
			this.txtDatasetFile.Name = "txtDatasetFile";
			this.txtDatasetFile.ReadOnly = true;
			this.txtDatasetFile.Size = new System.Drawing.Size(472, 22);
			this.txtDatasetFile.TabIndex = 2;
			// 
			// txtAccuracy
			// 
			this.txtAccuracy.BackColor = System.Drawing.SystemColors.Window;
			this.txtAccuracy.Location = new System.Drawing.Point(574, 74);
			this.txtAccuracy.Name = "txtAccuracy";
			this.txtAccuracy.ReadOnly = true;
			this.txtAccuracy.Size = new System.Drawing.Size(139, 22);
			this.txtAccuracy.TabIndex = 3;
			// 
			// panelAttributes
			// 
			this.panelAttributes.AutoScroll = true;
			this.panelAttributes.Location = new System.Drawing.Point(67, 152);
			this.panelAttributes.Name = "panelAttributes";
			this.panelAttributes.Size = new System.Drawing.Size(251, 161);
			this.panelAttributes.TabIndex = 4;
			// 
			// btnDiscover
			// 
			this.btnDiscover.Location = new System.Drawing.Point(67, 343);
			this.btnDiscover.Name = "btnDiscover";
			this.btnDiscover.Size = new System.Drawing.Size(131, 23);
			this.btnDiscover.TabIndex = 5;
			this.btnDiscover.Text = "Discover";
			this.btnDiscover.UseVisualStyleBackColor = true;
			this.btnDiscover.Click += new System.EventHandler(this.btnDiscover_Click);
			// 
			// txtOutput
			// 
			this.txtOutput.BackColor = System.Drawing.SystemColors.Window;
			this.txtOutput.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.txtOutput.Location = new System.Drawing.Point(67, 382);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.Size = new System.Drawing.Size(131, 22);
			this.txtOutput.TabIndex = 6;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.txtOutput);
			this.Controls.Add(this.btnDiscover);
			this.Controls.Add(this.panelAttributes);
			this.Controls.Add(this.txtAccuracy);
			this.Controls.Add(this.txtDatasetFile);
			this.Controls.Add(this.txtBestAlgorithm);
			this.Controls.Add(this.btnLoadDataset);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLoadDataset;
		private System.Windows.Forms.TextBox txtBestAlgorithm;
		private System.Windows.Forms.TextBox txtDatasetFile;
		private System.Windows.Forms.TextBox txtAccuracy;
		private System.Windows.Forms.Panel panelAttributes;
		private System.Windows.Forms.Button btnDiscover;
		private System.Windows.Forms.TextBox txtOutput;
	}
}

