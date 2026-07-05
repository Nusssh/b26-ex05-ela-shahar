namespace Ex05.Logic
{
	internal class Player
	{
		private readonly string r_Name;
		private readonly eCellSymbol r_Symbol;
		private readonly bool r_IsComputer;
		private int m_Score;

		public Player(string i_Name, eCellSymbol i_Symbol, bool i_IsComputer)
		{
			r_Name = i_Name;
			r_Symbol = i_Symbol;
			r_IsComputer = i_IsComputer;
			m_Score = 0;
		}

		public string Name
		{
			get
			{
				return r_Name;
			}
		}

		public eCellSymbol Symbol
		{
			get
			{
				return r_Symbol;
			}
		}

		public int Score
		{
			get
			{
				return m_Score;
			}
		}

		public bool IsComputer
		{
			get
			{
				return r_IsComputer;
			}
		}

		public void AddPoint()
		{
			m_Score++;
		}
	}
}
