using System;
using System.Windows.Forms;

namespace Ex05.UI
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			GameRunner.RunApplication();
		}
	}
}
