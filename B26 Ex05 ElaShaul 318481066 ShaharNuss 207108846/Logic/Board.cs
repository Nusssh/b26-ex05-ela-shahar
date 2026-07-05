namespace Ex05.Logic
{
	internal delegate void CellChangedEventHandler(int i_Row, int i_Col, eCellSymbol i_Symbol);

	internal class Board
	{
		private const eCellSymbol k_EmptyCell = eCellSymbol.Empty;
		private readonly int r_Size;
		private readonly eCellSymbol[,] r_Cells;
		private int m_OccupiedCellsCounter;

		public event CellChangedEventHandler CellChanged;

		public Board(int i_Size)
		{
			r_Size = i_Size;
			r_Cells = new eCellSymbol[i_Size, i_Size];
			m_OccupiedCellsCounter = 0;
			initializeCells();
		}

		public int Size
		{
			get
			{
				return r_Size;
			}
		}

		public bool IsInRange(int i_Row, int i_Col)
		{
			bool isInRange = i_Row >= 0 && i_Row < r_Size && i_Col >= 0 && i_Col < r_Size;

			return isInRange;
		}

		public bool IsCellEmpty(int i_Row, int i_Col)
		{
			bool isCellEmpty = false;

			if (IsInRange(i_Row, i_Col))
			{
				isCellEmpty = r_Cells[i_Row, i_Col] == k_EmptyCell;
			}

			return isCellEmpty;
		}

		public eCellSymbol GetCell(int i_Row, int i_Col)
		{
			eCellSymbol cell = r_Cells[i_Row, i_Col];

			return cell;
		}

		public bool TrySetCell(int i_Row, int i_Col, eCellSymbol i_Symbol)
		{
			bool cellWasSet = IsCellEmpty(i_Row, i_Col);

			if (cellWasSet)
			{
				r_Cells[i_Row, i_Col] = i_Symbol;
				m_OccupiedCellsCounter++;
				OnCellChanged(i_Row, i_Col, i_Symbol);
			}

			return cellWasSet;
		}

		public bool IsFull()
		{
			bool isFull = m_OccupiedCellsCounter == r_Size * r_Size;

			return isFull;
		}

		public bool HasFullSequence(eCellSymbol i_Symbol)
		{
			bool hasFullSequence = hasFullRow(i_Symbol)
				|| hasFullColumn(i_Symbol)
				|| hasFullMainDiagonal(i_Symbol)
				|| hasFullSecondaryDiagonal(i_Symbol);

			return hasFullSequence;
		}

		public void Clear()
		{
			initializeCells();
			m_OccupiedCellsCounter = 0;
		}

		protected virtual void OnCellChanged(int i_Row, int i_Col, eCellSymbol i_Symbol)
		{
			if (CellChanged != null)
			{
				CellChanged.Invoke(i_Row, i_Col, i_Symbol);
			}
		}

		private void initializeCells()
		{
			for (int row = 0; row < r_Size; row++)
			{
				for (int col = 0; col < r_Size; col++)
				{
					r_Cells[row, col] = k_EmptyCell;
				}
			}
		}

		private bool hasFullRow(eCellSymbol i_Symbol)
		{
			bool hasFullRow = false;

			for (int row = 0; row < r_Size && !hasFullRow; row++)
			{
				bool rowHasOnlySymbol = true;

				for (int col = 0; col < r_Size && rowHasOnlySymbol; col++)
				{
					if (r_Cells[row, col] != i_Symbol)
					{
						rowHasOnlySymbol = false;
					}
				}

				if (rowHasOnlySymbol)
				{
					hasFullRow = true;
				}
			}

			return hasFullRow;
		}

		private bool hasFullColumn(eCellSymbol i_Symbol)
		{
			bool hasFullColumn = false;

			for (int col = 0; col < r_Size && !hasFullColumn; col++)
			{
				bool columnHasOnlySymbol = true;

				for (int row = 0; row < r_Size && columnHasOnlySymbol; row++)
				{
					if (r_Cells[row, col] != i_Symbol)
					{
						columnHasOnlySymbol = false;
					}
				}

				if (columnHasOnlySymbol)
				{
					hasFullColumn = true;
				}
			}

			return hasFullColumn;
		}

		private bool hasFullMainDiagonal(eCellSymbol i_Symbol)
		{
			bool hasFullMainDiagonal = true;

			for (int index = 0; index < r_Size && hasFullMainDiagonal; index++)
			{
				if (r_Cells[index, index] != i_Symbol)
				{
					hasFullMainDiagonal = false;
				}
			}

			return hasFullMainDiagonal;
		}

		private bool hasFullSecondaryDiagonal(eCellSymbol i_Symbol)
		{
			bool hasFullSecondaryDiagonal = true;

			for (int row = 0; row < r_Size && hasFullSecondaryDiagonal; row++)
			{
				int col = r_Size - 1 - row;

				if (r_Cells[row, col] != i_Symbol)
				{
					hasFullSecondaryDiagonal = false;
				}
			}

			return hasFullSecondaryDiagonal;
		}
	}
}
