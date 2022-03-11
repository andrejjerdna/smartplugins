using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Phases
{
    /// <summary>
    /// Phase manager class
    /// </summary>
    public class PhasesManager
    {
        /// <summary>
        /// Create phases with parameters.
        /// </summary>
        /// <param name="phaseNumber">Phase number</param>
        /// <param name="phaseName">Phase name</param>
        public bool CreatePhases(int[] phaseNumber, string[] phaseName)
        {
            return CreatePhases(phaseNumber, phaseName, "", 0, "", "", "", "");
        }


        /// <summary>
        /// Create phases with parameters.
        /// </summary>
        /// <param name="phaseNumber">Phase number</param>
        /// <param name="phaseName">Phase name</param>
        /// <param name="phaseComment">Phase comment</param>
        /// <param name="isCurrentPhase">Is current phase(1-true)</param>
        /// <param name="layoutNumber">Layout number</param>
        /// <param name="author">Author</param>
        /// <param name="objectNameRus">Building name Rus</param>
        /// <param name="objectNameEng">Building name Eng</param>
        public bool CreatePhases(int[] phaseNumber, string[] phaseName, string phaseComment, int isCurrentPhase,
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
        /// Create phase with parameters.
        /// </summary>
        /// <param name="phaseNumber">Phase number</param>
        /// <param name="phaseName">Phase name</param>
        public bool CreatePhase(int phaseNumber, string phaseName)
        {
            return CreatePhase(phaseNumber, phaseName, "", 0, "", "", "", "");
        }

        /// <summary>
        /// Create phase with parameters.
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
        public bool CreatePhase(int phaseNumber, string phaseName, string phaseComment, int isCurrentPhase,
            string layoutNumber, string author, string objectNameRus, string objectNameEng)
        {
            var flag = false;
            var model = new Model();
            if (model.GetConnectionStatus())
            {
                var phase = new Phase(phaseNumber, phaseName, phaseComment, isCurrentPhase);
                if (phase.Insert())
                {
                    phase.SetUserProperty("NLO_comment", layoutNumber);
                    phase.SetUserProperty("PHASE_D_COMMENT", author);
                    phase.SetUserProperty("PHASE_N_RU_COMMENT", objectNameRus);
                    phase.SetUserProperty("PHASE_N_EN_COMMENT", objectNameEng);
                    //phase.PhaseComment = phaseComment;
                    phase.SetUserProperty("comment", phaseComment);

                    phase.Modify();

                    flag = true;
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
        public bool DeletePhase(int phaseNumber)
        {
            var flag = false;
            var model = new Model();
            var ph = new Phase(phaseNumber);

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
        public bool PhaseIsExist(int phaseNumber)
        {
            var model = new Model();

            if (model.GetConnectionStatus())
            {

                var phaseCollection = model.GetPhases();

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
