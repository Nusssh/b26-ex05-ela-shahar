namespace Ex05.Logic
{
	internal class Game
	{
		private readonly Board r_Board;
		private readonly Player r_Player1;
		private readonly Player r_Player2;
		private readonly ComputerMoveGenerator r_ComputerMoveGenerator;
		private Player m_CurrentPlayer;

		public Game(int i_BoardSize, string i_Player1Name, string i_Player2Name, bool i_IsVsComputer)
		{
			const bool v_IsComputerPlayer = true;
			r_Board = new Board(i_BoardSize);
			r_Player1 = new Player(i_Player1Name, eCellSymbol.X, !v_IsComputerPlayer);
			r_Player2 = new Player(i_Player2Name, eCellSymbol.O, i_IsVsComputer);
			r_ComputerMoveGenerator = new ComputerMoveGenerator();
			m_CurrentPlayer = r_Player1;
		}

		public Board Board
		{
			get
			{
				return r_Board;
			}
		}

		public Player Player1
		{
			get
			{
				return r_Player1;
			}
		}

		public Player Player2
		{
			get
			{
				return r_Player2;
			}
		}

		public Player CurrentPlayer
		{
			get
			{
				return m_CurrentPlayer;
			}
		}

		public bool IsMoveValid(Move i_Move)
		{
			bool isMoveValid = r_Board.IsCellEmpty(i_Move.Row, i_Move.Col);

			return isMoveValid;
		}

		public bool DidCurrentPlayerLose()
		{
			bool currentPlayerLost = r_Board.HasFullSequence(m_CurrentPlayer.Symbol);

			return currentPlayerLost;
		}

		public eRoundResult TryPlayTurn(Move i_Move)
		{
			eRoundResult roundResult = eRoundResult.InvalidMove;
			bool moveWasMade = tryMakeMove(i_Move);

			if (moveWasMade)
			{
				if (DidCurrentPlayerLose())
				{
					GetOpponent().AddPoint();
					roundResult = eRoundResult.CurrentPlayerLost;
				}
				else if (IsDraw())
				{
					roundResult = eRoundResult.Draw;
				}
				else
				{
					SwitchTurn();
					roundResult = eRoundResult.InProgress;
				}
			}

			return roundResult;
		}

		public bool TryGenerateComputerMove(out Move o_Move)
		{
			bool moveWasGenerated = r_ComputerMoveGenerator.TryFindBestMove(r_Board, m_CurrentPlayer.Symbol, out o_Move);

			return moveWasGenerated;
		}

		public void SwitchTurn()
		{
			if (m_CurrentPlayer == r_Player1)
			{
				m_CurrentPlayer = r_Player2;
			}
			else
			{
				m_CurrentPlayer = r_Player1;
			}
		}

		public bool IsDraw()
		{
			bool isDraw = r_Board.IsFull() && !DidCurrentPlayerLose();

			return isDraw;
		}

		public Player GetOpponent()
		{
			Player opponent = r_Player1;

			if (m_CurrentPlayer == r_Player1)
			{
				opponent = r_Player2;
			}

			return opponent;
		}

		public void StartNewRound()
		{
			r_Board.Clear();
			m_CurrentPlayer = r_Player1;
		}

		public void ForfeitCurrentPlayer()
		{
			GetOpponent().AddPoint();
		}

		private bool tryMakeMove(Move i_Move)
		{
			bool moveWasMade = r_Board.TrySetCell(i_Move.Row, i_Move.Col, m_CurrentPlayer.Symbol);

			return moveWasMade;
		}
	}
}
