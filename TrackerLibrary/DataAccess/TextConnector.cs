 using System;
using System.Collections.Generic;
using System.Text;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;
using System.Linq;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {   //TODO Create Prize for text files

        private const string PrizesFile = "PrizeModels.csv";
        private const string PersonFile = "PersonModels.csv";
        private const string TeamsFile = "TeamsModels.csv";

        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> persons = PersonFile.FullFilePath().LoadFile().ConverToPersonModel();

            int currentId = 1;

            if (persons.Count > 0)
            {
                currentId = persons.Max(x => x.Id) + 1;
                
            }

            model.Id = currentId;

            persons.Add(model);

            persons.SaveToPersonFile(PersonFile);

            return model;
        }

        public PrizeModel CreatePrize(PrizeModel model)
        {
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModel();  // load text file

            // int maxId = prizes.Max(x => x.Id); a better way

            int currentId = 1;

            if (prizes.Count > 0)
            {
               currentId =  prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            prizes.Add(model);

            prizes.SaveToPrizeFile(PrizesFile);

            return model;


        }

        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = TeamsFile.FullFilePath().LoadFile().ConvertToTeamModel(PersonFile);

            int currentId = 1;

            if(teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            teams.Add(model);

            teams.SaveToTeamModel(TeamsFile);

            return model;
        }

        public TournamentModel CreateTournament(TournamentModel model)
        {
            throw new NotImplementedException();
        }

        public List<PersonModel> GetPerson_All()
        {
            return PersonFile.FullFilePath().LoadFile().ConverToPersonModel();

        }

        public List<TeamModel> GetTeam_All()
        {
           return TeamsFile.FullFilePath().LoadFile().ConvertToTeamModel(PersonFile);

        }
    }
}
