using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary
{
    public class MatchupEntryModel
    {
        /// <summary>
        /// Good practice to write a comment for every file
        /// </summary>
        public TeamModel TeamCompeting { get; set; }
        public double Score { get; set; }
        public MatchupModel ParentMatchup { get; set; }

    }
}
