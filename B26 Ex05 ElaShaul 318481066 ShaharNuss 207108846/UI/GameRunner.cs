using System.Windows.Forms;

namespace Ex05.UI
{
	internal static class GameRunner
	{
		internal static void RunApplication()
		{
			FormGameSettings settingsForm = new FormGameSettings();

			settingsForm.ShowDialog();

			if (settingsForm.ShouldStartGame)
			{
				runGame(settingsForm);
			}
		}

		private static void runGame(FormGameSettings i_SettingsForm)
		{
			FormGame gameForm = new FormGame(
				i_SettingsForm.Player1Name,
				i_SettingsForm.Player2Name,
				i_SettingsForm.BoardSize,
				i_SettingsForm.IsVsComputer);

			Application.Run(gameForm);
		}
	}
}
