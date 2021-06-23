using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        public CreatePrizeForm()
        {
            InitializeComponent();
        }

        private void CreatePrizeForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PrizeModel model = new PrizeModel(
                                                    placeNumberValue.Text,
                                                    placeNameValue.Text,
                                                    prizeAmountValue.Text,
                                                    prizePercentageValue.Text
                                                 );

                GlobalConfig.Connection.CreatePrize(model);

                placeNumberValue.Text = "";
                placeNameValue.Text = "";
                prizeAmountValue.Text = "0";
                prizePercentageValue.Text = "0";

            }
            else
            {
                MessageBox.Show("This form has invalid information.");
            }
            
        }


        private bool ValidateForm()
        {
            bool output = true;

            int placeNumber = 0;
            bool placeNumberValidNumber = int.TryParse(placeNumberValue.Text, out placeNumber);

            if(!placeNumberValidNumber)
            {
                output = false;
            }

            if(placeNumber < 1)
            {
                output = false;
            }

            if (placeNameValue.Text.Length == 0)
            {
                output = false;
            }

            decimal prizeAmount = 0;
            int prizePercentage = 0;

            bool prizeAmountValidNumber = decimal.TryParse(prizeAmountValue.Text, out prizeAmount);
            bool prizePercentageValidNumber = int.TryParse(prizePercentageValue.Text, out prizePercentage);


            if (!prizeAmountValidNumber || !prizePercentageValidNumber)
            {
                output = false;
            }

            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                output = false;
            }

            if (prizePercentage < 0 || prizePercentage > 100)
            {
                output = false;
            }




            return output;

        }
    }
}
    

