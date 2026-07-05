using System;
using System.Drawing;
using System.Windows.Forms;
using Ex05.Logic;

namespace Ex05.UI
{
	public partial class FormGame : Form
	{
		private const int k_ButtonSize = 70;
		private const int k_Margin = 10;

		private readonly Game m_Game;
		private readonly Font r_RegularFont;
		private readonly Font r_BoldFont;
		private Button[,] m_Buttons;

		public FormGame(string i_Player1Name, string i_Player2Name, int i_BoardSize, bool i_IsVsComputer)
		{
			InitializeComponent();
			this.Text = "Tic Tac Toe Reversed";
			r_RegularFont = labelPlayer1Score.Font;
			r_BoldFont = new Font(r_RegularFont, FontStyle.Bold);
			m_Game = new Game(i_BoardSize, i_Player1Name, i_Player2Name, i_IsVsComputer);
			m_Game.Board.CellChanged += board_CellChanged;
			initializeBoard(i_BoardSize);
			setFormSize(i_BoardSize);
			updateScoreLabels();
		}

		private void initializeBoard(int i_BoardSize)
		{
			m_Buttons = new Button[i_BoardSize, i_BoardSize];

			for (int i = 0; i < i_BoardSize; i++)
			{
				for (int j = 0; j < i_BoardSize; j++)
				{
					Button button = new Button();

					button.Size = new Size(k_ButtonSize, k_ButtonSize);
					button.Location = new Point(k_Margin + j * k_ButtonSize, k_Margin + i * k_ButtonSize);
					button.Tag = new Point(i, j);
					button.TabStop = false;
					button.Click += gameButton_Click;
					m_Buttons[i, j] = button;
					this.Controls.Add(button);
				}
			}
		}

		private void setFormSize(int i_BoardSize)
		{
			int gridSize = i_BoardSize * k_ButtonSize;
			int formWidth = k_Margin * 2 + gridSize;
			int labelsY = k_Margin + gridSize + k_Margin;
			int formHeight = labelsY + 30 + k_Margin;

			this.ClientSize = new Size(formWidth, formHeight);
			labelPlayer1Score.Location = new Point(k_Margin, labelsY);
			labelPlayer1Score.Size = new Size(gridSize / 2 - k_Margin, 25);
			labelPlayer2Score.Location = new Point(k_Margin + gridSize / 2, labelsY);
			labelPlayer2Score.Size = new Size(gridSize / 2 - k_Margin, 25);
		}

		private void gameButton_Click(object sender, EventArgs e)
		{
			Button clickedButton = (Button)sender;
			Point position = (Point)clickedButton.Tag;
			Move move = new Move(position.X, position.Y);
			eRoundResult result = m_Game.TryPlayTurn(move);

			handleRoundResult(result);
		}

		private void board_CellChanged(int i_Row, int i_Col, eCellSymbol i_Symbol)
		{
			m_Buttons[i_Row, i_Col].Text = i_Symbol.ToString();
			m_Buttons[i_Row, i_Col].Enabled = false;
		}

		private void handleRoundResult(eRoundResult i_Result)
		{
			if (i_Result == eRoundResult.InProgress)
			{
				updateScoreLabels();

				if (m_Game.CurrentPlayer.IsComputer)
				{
					playComputerTurn();
				}
			}
			else if (i_Result == eRoundResult.CurrentPlayerLost || i_Result == eRoundResult.Draw)
			{
				updateScoreLabels();
				showEndOfRoundDialog(i_Result);
			}
		}

		private void playComputerTurn()
		{
			Move computerMove;
			bool moveWasGenerated = m_Game.TryGenerateComputerMove(out computerMove);

			if (moveWasGenerated)
			{
				eRoundResult result = m_Game.TryPlayTurn(computerMove);

				handleRoundResult(result);
			}
		}

		private void showEndOfRoundDialog(eRoundResult i_Result)
		{
			string title;
			string message;

			if (i_Result == eRoundResult.Draw)
			{
				title = "A Tie!";
				message = "Tie!" + Environment.NewLine + "Would you like to play another round?";
			}
			else
			{
				string winnerName = m_Game.GetOpponent().Name;

				title = "A Win!";
				message = "The winner is " + winnerName + "!" + Environment.NewLine + "Would you like to play another round?";
			}

			DialogResult dialogResult = MessageBox.Show(message, title, MessageBoxButtons.YesNo);

			if (dialogResult == DialogResult.Yes)
			{
				m_Game.StartNewRound();
				resetBoard();
				updateScoreLabels();
			}
			else
			{
				this.Close();
			}
		}

		private void resetBoard()
		{
			int boardSize = m_Buttons.GetLength(0);

			for (int i = 0; i < boardSize; i++)
			{
				for (int j = 0; j < boardSize; j++)
				{
					m_Buttons[i, j].Text = string.Empty;
					m_Buttons[i, j].Enabled = true;
				}
			}
		}

		private void updateScoreLabels()
		{
			labelPlayer1Score.Text = string.Format("{0}: {1}", m_Game.Player1.Name, m_Game.Player1.Score);
			labelPlayer2Score.Text = string.Format("{0}: {1}", m_Game.Player2.Name, m_Game.Player2.Score);
			highlightCurrentPlayer();
		}

		private void highlightCurrentPlayer()
		{
			if (m_Game.CurrentPlayer == m_Game.Player1)
			{
				labelPlayer1Score.Font = r_BoldFont;
				labelPlayer2Score.Font = r_RegularFont;
			}
			else
			{
				labelPlayer1Score.Font = r_RegularFont;
				labelPlayer2Score.Font = r_BoldFont;
			}
		}
	}
}
