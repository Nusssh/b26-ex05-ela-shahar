using System;
using System.Collections.Generic;

namespace Ex05.Logic
{
	internal class ComputerMoveGenerator
	{
		private static readonly Random sr_Random = new Random();

		public bool TryFindBestMove(Board i_Board, eCellSymbol i_ComputerSymbol, out Move o_Move)
		{
			List<Move> bestMoves = new List<Move>();
			List<Move> emptyCells = new List<Move>();
			int lowestDangerScore = int.MaxValue;
			o_Move = new Move(0, 0);

			for (int row = 0; row < i_Board.Size; row++)
			{
				for (int col = 0; col < i_Board.Size; col++)
				{
					Move currentMove = new Move(row, col);

					if (i_Board.IsCellEmpty(row, col))
					{
						emptyCells.Add(currentMove);

						if (!wouldCreateFullSequence(i_Board, currentMove, i_ComputerSymbol))
						{
							int dangerScore = calculateDangerScore(i_Board, currentMove, i_ComputerSymbol);

							if (dangerScore < lowestDangerScore)
							{
								lowestDangerScore = dangerScore;
								bestMoves.Clear();
								bestMoves.Add(currentMove);
							}
							else if (dangerScore == lowestDangerScore)
							{
								bestMoves.Add(currentMove);
							}
						}
					}
				}
			}

			bool moveWasFound = emptyCells.Count > 0;

			if (moveWasFound)
			{
				List<Move> movesToChooseFrom;

				if (bestMoves.Count > 0)
				{
					movesToChooseFrom = bestMoves;
				}
				else
				{
					movesToChooseFrom = emptyCells;
				}

				int randomIndex = sr_Random.Next(movesToChooseFrom.Count);

				o_Move = movesToChooseFrom[randomIndex];
			}

			return moveWasFound;
		}

		private bool wouldCreateFullSequence(Board i_Board, Move i_Move, eCellSymbol i_Symbol)
		{
			bool wouldCreateFullSequence = wouldCreateFullRow(i_Board, i_Move, i_Symbol)
				|| wouldCreateFullColumn(i_Board, i_Move, i_Symbol)
				|| wouldCreateFullMainDiagonal(i_Board, i_Move, i_Symbol)
				|| wouldCreateFullSecondaryDiagonal(i_Board, i_Move, i_Symbol);

			return wouldCreateFullSequence;
		}

		private bool wouldCreateFullRow(Board i_Board, Move i_Move, eCellSymbol i_Symbol)
		{
			bool wouldCreateFullRow = true;

			for (int col = 0; col < i_Board.Size && wouldCreateFullRow; col++)
			{
				if (col != i_Move.Col && i_Board.GetCell(i_Move.Row, col) != i_Symbol)
				{
					wouldCreateFullRow = false;
				}
			}

			return wouldCreateFullRow;
		}

		private bool wouldCreateFullColumn(Board i_Board, Move i_Move, eCellSymbol i_Symbol)
		{
			bool wouldCreateFullColumn = true;

			for (int row = 0; row < i_Board.Size && wouldCreateFullColumn; row++)
			{
				if (row != i_Move.Row && i_Board.GetCell(row, i_Move.Col) != i_Symbol)
				{
					wouldCreateFullColumn = false;
				}
			}

			return wouldCreateFullColumn;
		}

		private bool wouldCreateFullMainDiagonal(Board i_Board, Move i_Move, eCellSymbol i_Symbol)
		{
			bool wouldCreateFullMainDiagonal = i_Move.Row == i_Move.Col;

			for (int index = 0; index < i_Board.Size && wouldCreateFullMainDiagonal; index++)
			{
				if (index != i_Move.Row && i_Board.GetCell(index, index) != i_Symbol)
				{
					wouldCreateFullMainDiagonal = false;
				}
			}

			return wouldCreateFullMainDiagonal;
		}

		private bool wouldCreateFullSecondaryDiagonal(Board i_Board, Move i_Move, eCellSymbol i_Symbol)
		{
			bool wouldCreateFullSecondaryDiagonal = i_Move.Row + i_Move.Col == i_Board.Size - 1;

			for (int row = 0; row < i_Board.Size && wouldCreateFullSecondaryDiagonal; row++)
			{
				int col = i_Board.Size - 1 - row;

				if (row != i_Move.Row && i_Board.GetCell(row, col) != i_Symbol)
				{
					wouldCreateFullSecondaryDiagonal = false;
				}
			}

			return wouldCreateFullSecondaryDiagonal;
		}

		private int calculateDangerScore(Board i_Board, Move i_Move, eCellSymbol i_Symbol)
		{
			int rowScore = countSymbolsInRow(i_Board, i_Move.Row, i_Symbol);
			int columnScore = countSymbolsInColumn(i_Board, i_Move.Col, i_Symbol);
			int mainDiagonalScore = 0;
			int secondaryDiagonalScore = 0;

			if (i_Move.Row == i_Move.Col)
			{
				mainDiagonalScore = countSymbolsInMainDiagonal(i_Board, i_Symbol);
			}

			if (i_Move.Row + i_Move.Col == i_Board.Size - 1)
			{
				secondaryDiagonalScore = countSymbolsInSecondaryDiagonal(i_Board, i_Symbol);
			}

			int dangerScore = Math.Max(Math.Max(rowScore, columnScore), Math.Max(mainDiagonalScore, secondaryDiagonalScore));

			return dangerScore;
		}

		private int countSymbolsInRow(Board i_Board, int i_Row, eCellSymbol i_Symbol)
		{
			int symbolsCounter = 0;

			for (int col = 0; col < i_Board.Size; col++)
			{
				if (i_Board.GetCell(i_Row, col) == i_Symbol)
				{
					symbolsCounter++;
				}
			}

			return symbolsCounter;
		}

		private int countSymbolsInColumn(Board i_Board, int i_Col, eCellSymbol i_Symbol)
		{
			int symbolsCounter = 0;

			for (int row = 0; row < i_Board.Size; row++)
			{
				if (i_Board.GetCell(row, i_Col) == i_Symbol)
				{
					symbolsCounter++;
				}
			}

			return symbolsCounter;
		}

		private int countSymbolsInMainDiagonal(Board i_Board, eCellSymbol i_Symbol)
		{
			int symbolsCounter = 0;

			for (int index = 0; index < i_Board.Size; index++)
			{
				if (i_Board.GetCell(index, index) == i_Symbol)
				{
					symbolsCounter++;
				}
			}

			return symbolsCounter;
		}

		private int countSymbolsInSecondaryDiagonal(Board i_Board, eCellSymbol i_Symbol)
		{
			int symbolsCounter = 0;

			for (int row = 0; row < i_Board.Size; row++)
			{
				int col = i_Board.Size - 1 - row;

				if (i_Board.GetCell(row, col) == i_Symbol)
				{
					symbolsCounter++;
				}
			}

			return symbolsCounter;
		}
	}
}
