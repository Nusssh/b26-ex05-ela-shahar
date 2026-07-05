using System;
using System.Windows.Forms;

namespace Ex05.UI
{
	public partial class FormGameSettings : Form
	{
		private bool m_IsUpdatingSize;
		private bool m_ShouldStartGame;

		public FormGameSettings()
		{
			InitializeComponent();
			m_IsUpdatingSize = false;
			m_ShouldStartGame = false;
		}

		public bool ShouldStartGame
		{
			get
			{
				return m_ShouldStartGame;
			}
		}

		public string Player1Name
		{
			get
			{
				return textBoxPlayer1.Text;
			}
		}

		public string Player2Name
		{
			get
			{
				string player2Name;

				if (checkBoxPlayer2.Checked)
				{
					player2Name = textBoxPlayer2.Text;
				}
				else
				{
					player2Name = "Computer";
				}

				return player2Name;
			}
		}

		public int BoardSize
		{
			get
			{
				return (int)numericUpDownRows.Value;
			}
		}

		public bool IsVsComputer
		{
			get
			{
				return !checkBoxPlayer2.Checked;
			}
		}

		private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxPlayer2.Checked)
			{
				textBoxPlayer2.Enabled = true;
				textBoxPlayer2.Text = string.Empty;
			}
			else
			{
				textBoxPlayer2.Enabled = false;
				textBoxPlayer2.Text = "[Computer]";
			}
		}

		private void numericUpDownRows_ValueChanged(object sender, EventArgs e)
		{
			if (!m_IsUpdatingSize)
			{
				m_IsUpdatingSize = true;
				numericUpDownCols.Value = numericUpDownRows.Value;
				m_IsUpdatingSize = false;
			}
		}

		private void numericUpDownCols_ValueChanged(object sender, EventArgs e)
		{
			if (!m_IsUpdatingSize)
			{
				m_IsUpdatingSize = true;
				numericUpDownRows.Value = numericUpDownCols.Value;
				m_IsUpdatingSize = false;
			}
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			m_ShouldStartGame = true;
			this.Close();
		}

	}
}
