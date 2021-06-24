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
        public PrizeModel CreatePrize(PrizeModel model)
        {
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModel();

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


    }
}
