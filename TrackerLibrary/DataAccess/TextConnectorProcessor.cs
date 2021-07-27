using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName)  //PrizeModels.csv
        {
            // example C:\Users\vconstantini2605\Desktop\Learning\C#\Data\TournamentTracker\PrizeModels.csv

            return $"{ConfigurationManager.AppSettings["filePath2"]}\\{fileName}";
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }


        public static List<PrizeModel> ConvertToPrizeModel(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = int.Parse(cols[4]);

                output.Add(p);

            }

            return output;
        }

        public static List<TeamModel> ConvertToTeamModel(this List<String> lines, string fileName)
        {
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = fileName.FullFilePath().LoadFile().ConverToPersonModel();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TeamModel p = new TeamModel();
                p.Id = int.Parse(cols[0]);
                p.TeamName = cols[1];

                string[] personIds = cols[2].Split('|');

                foreach (string  id  in personIds)
                {
                    p.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());
                        
                }

                output.Add(p);

            }

            return output;

        }


        public static List<PersonModel> ConverToPersonModel(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel p = new PersonModel();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAdress = cols[3];
                p.CellphoneNumber = cols[4];

                output.Add(p);
            }

            return output;
        }

        public static List<TournamentModel> ConvertToTournamentModels(this List<string> lines, string teamFileName, string personFileName, string prizeFileName)
        {
            List<TournamentModel> output = new List<TournamentModel>();
            List<TeamModel> teams = new List<TeamModel>();
            teams = teamFileName.FullFilePath().LoadFile().ConvertToTeamModel(personFileName);

            List<PrizeModel> prizes = prizeFileName.FullFilePath().LoadFile().ConvertToPrizeModel();

            //id, TournamentName, EntryFee, id_teams/id_teams/id_teams,  id_prize/id_prize/id_prize,  id^id^id^id/id^id list of list

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TournamentModel t = new TournamentModel();
                t.Id = int.Parse(cols[0]);
                t.TournamentName = cols[1];
                t.EntryFee = decimal.Parse(cols[2]);

                string[] teamsId = cols[3].Split('|');

                foreach (string id in teamsId)
                {
                    t.EnteredTeams.Add(teams.Where(x => x.Id == int.Parse(id)).First());
                }

                string[] prizesId = cols[4].Split('|');

                foreach (string id in prizesId)
                {
                    t.Prizes.Add(prizes.Where(x => x.Id == int.Parse(id)).First());
                }


                // TO DO Rounds 

                output.Add(t);

            }


            return output;
        }

        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models)
            {
                lines.Add($"{ p.Id },{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }


        public static void SaveToPersonFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel model in models)
            {
                lines.Add($"{model.Id},{model.FirstName},{model.LastName},{model.EmailAdress},{model.CellphoneNumber}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTeamModel(this List<TeamModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TeamModel team in models)
            {
                lines.Add($"{team.Id},{team.TeamName},{ConvertPeopleListToString(team.TeamMembers)}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }


        public static void SaveToTournamentModel(this List<TournamentModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TournamentModel tournamentModel in models)
            {
                lines.Add($@"{tournamentModel.Id},
                             {tournamentModel.TournamentName},
                             {tournamentModel.EntryFee},
                             {ConvertTeamsListToString(tournamentModel.EnteredTeams)},
                             {ConvertPrizesListToString(tournamentModel.Prizes)},
                             {ConvertListOfMatchupsListToString(tournamentModel.Rounds)}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }




        private static string ConvertTeamsListToString(List<TeamModel> teams)
        {
            string teamIds = "";

            if (teams.Count == 0)
            {
                return "";
            }

            foreach (TeamModel  team in teams)
            {
                teamIds += $"{team.Id}|";
            }

            teamIds = teamIds.Substring(0, teamIds.Length - 1); //delete the last pipe from last id

            return teamIds;
        }

        private static string ConvertPrizesListToString(List<PrizeModel> prizes)
        {
            string prizeIds = "";

            if (prizes.Count == 0)
            {
                return "";
            }

            foreach (PrizeModel prize in prizes)
            {
                prizeIds += $"{prize.Id}|";
            }

            prizeIds = prizeIds.Substring(0, prizeIds.Length - 1); //delete the last pipe from last id

            return prizeIds;
        }



        private static string ConvertListOfMatchupsListToString(List<List<MatchupModel>> matchups)
        {
            string matchupIds = "";

            if (matchups.Count == 0)
            {
                return "";
            }

            foreach (List<MatchupModel> matchup in matchups)
            {
                matchupIds += $"{ConvertMatchupsListToString(matchup)}|";
            }

            matchupIds = matchupIds.Substring(0, matchupIds.Length - 1); //delete the last pipe from last id

            return matchupIds;
        }

        private static string ConvertMatchupsListToString(List<MatchupModel> matchups)
        {
            string matchupIds = "";

            if (matchups.Count == 0)
            {
                return "";
            }

            foreach (MatchupModel matchup in matchups)
            {
                matchupIds += $"{matchup.Id}^";
            }

            matchupIds = matchupIds.Substring(0, matchupIds.Length - 1); //delete the last pipe from last id

            return matchupIds;
        }

        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            string personIds = "";

            if (people.Count == 0)
            {
                return "";
            }

            foreach (PersonModel person in people)
            {
                personIds += $"{person.Id}|";
            }

            personIds = personIds.Substring(0, personIds.Length - 1); //delete the last pipe from last id

            return personIds;
        }

        

    }
}
