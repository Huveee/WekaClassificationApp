using System;
using System.Windows.Forms;

namespace WekaClassificationApp
{
	internal static class Program
	{
		[STAThread]
		static void Main()
		{
			// Enable Windows Forms visual styles
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// Start the application with Form1
			Application.Run(new Form1());
		}
	}
}