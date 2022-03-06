using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Phases
{
	/// <summary>
	/// Phase manager class
	/// </summary>
    public static class PhasesManager
    {
		/// <summary>
		/// Create phases with parametrs.
		/// </summary>
		/// <param name="phaseNumber">Phase number</param>
		/// <param name="phaseName">Phase name</param>
		/// <param name="phaseComment">Phase comment</param>
		/// <param name="isCurrentPhase">Is current phase(1-true)</param>
		/// <param name="layoutNumber">Layout number</param>
		/// <param name="author">Author</param>
		/// <param name="objectNameRus">Building name Rus</param>
		/// <param name="objectNameEng">Building name Eng</param>
		public static bool CreatePhases(int[] phaseNumber, string[] phaseName, string phaseComment, int isCurrentPhase,
			string layoutNumber, string author, string objectNameRus, string objectNameEng)
		{
			for (int i = 0; i < phaseNumber.Length; i++)
			{

				if (PhaseIsExist(phaseNumber[i]))
				{
					return false;
				}

				if (!CreatePhase(phaseNumber[i], phaseName[i], phaseComment, isCurrentPhase, layoutNumber,
						author, objectNameRus, objectNameEng))
				{
					return false;
				}
			}
			return true;
		}


		/// <summary>
		/// Create phase with parametrs.
		/// </summary>
		/// <param name="phaseNumber">Phase number</param>
		/// <param name="phaseName">Phase name</param>
		/// <param name="phaseComment">Phase comment</param>
		/// <param name="isCurrentPhase">Is current phase</param>
		/// <param name="layoutNumber">Layout number</param>
		/// <param name="author">Author</param>
		/// <param name="objectNameRus">Building name Rus</param>
		/// <param name="objectNameEng">Building name Eng</param>
		/// <returns>Result of phase create.</returns>
		public static bool CreatePhase(int phaseNumber, string phaseName, string phaseComment, int isCurrentPhase,
			string layoutNumber,string author, string objectNameRus, string objectNameEng)
		{
			bool flag = false;
			Model model = new Model();
			if (model.GetConnectionStatus())
			{
				Phase phase = new Phase(phaseNumber, phaseName, phaseComment, isCurrentPhase);
				if (phase.Insert())
				{
					phase.SetUserProperty("NLO_comment", layoutNumber);
					phase.SetUserProperty("PHASE_D_COMMENT", author);
					phase.SetUserProperty("PHASE_N_RU_COMMENT", objectNameRus);
					phase.SetUserProperty("PHASE_N_EN_COMMENT", objectNameEng);
					//phase.PhaseComment = phaseComment;
					phase.SetUserProperty("comment", phaseComment);

					phase.Modify();
					
					flag= true;
				}
				model.CommitChanges();
			}
			return flag;
		}



		/// <summary>
		/// Phase delete
		/// </summary>
		/// <param name="phaseNumber">Phase number</param>
		/// <returns></returns>
		public static bool DeletePhase(int phaseNumber)
		{
			bool flag = false;
			Model model = new Model();
			Phase ph = new Phase(phaseNumber);

			if (model.GetConnectionStatus())
			{
				if (PhaseIsExist(phaseNumber) && ph.IsCurrentPhase != 1)
				{
					if (ph.Delete())
					{
						flag = true;
					}
					ph.Modify();
				}
				model.CommitChanges();
			}
			return flag;
		}


		/// <summary>
		/// Check phase for exist.
		/// </summary>
		/// <param name="phaseNumber">Phase number</param>
		public static bool PhaseIsExist(int phaseNumber)
		{
			Model model = new Model();

			if (model.GetConnectionStatus())
			{

				PhaseCollection phaseCollection = model.GetPhases();

				foreach (Phase phase in phaseCollection)
				{
					if (phase.PhaseNumber == phaseNumber)
					{
						return true;
					}
				}

				model.CommitChanges();
			}
			return false;
		}
	}
}
