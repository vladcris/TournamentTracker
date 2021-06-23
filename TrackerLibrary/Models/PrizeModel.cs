using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class PrizeModel
    {
        public int Id { get; set; }
        public int PlaceNumber { get; set; }
        public string PlaceName { get; set; }
        public decimal PrizeAmount { get; set; }
        public double PrizePercentage { get; set; }


        public PrizeModel()
        {

        }

        public PrizeModel(string placeNumber, string placeName, string prizeAmount, string prizePercentage)
        {
            int placeNumberFinal = 0;
            int.TryParse(placeNumber, out placeNumberFinal);
            PlaceNumber = placeNumberFinal;

            PlaceName = placeName;

            decimal prizeAmountFinal = 0;
            decimal.TryParse(prizeAmount, out prizeAmountFinal);
            PrizeAmount = prizeAmountFinal;

            double prizePercentageFinal = 0;
            double.TryParse(prizePercentage, out prizePercentageFinal);
            PrizePercentage = prizePercentageFinal;

        }

    }
}
