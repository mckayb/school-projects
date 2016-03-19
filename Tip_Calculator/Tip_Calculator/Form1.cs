using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tip_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void computeTipButton_Click(object sender, EventArgs e)
        {
            string billTotal = billTotalInput.Text;
            double billTotalDouble = Convert.ToDouble(billTotal);

            string tipPct = tipPercentInput.Text;
            double tipPctDouble = Convert.ToDouble(tipPct);
            tipPctDouble /= 100;

            double computedTipDouble = billTotalDouble * tipPctDouble;
            string computedTip = computedTipDouble.ToString("C", CultureInfo.CurrentCulture);
            computedTipInput.Text = computedTip;


            double totalCostDouble = billTotalDouble + computedTipDouble;
            string totalCost = totalCostDouble.ToString("C", CultureInfo.CurrentCulture);
            totalCostInput.Text = totalCost;
           
        }

        private void tipPercentInput_TextChanged(object sender, EventArgs e)
        {
            string tipPct = tipPercentInput.Text;

            double tipPctDouble;
            if (tipPct != "" && Double.TryParse(tipPct, out tipPctDouble) ) {
                if (tipPctDouble < 0 || tipPctDouble > 100)
                {
                    MessageBox.Show("Tip Percentage must be between 0 and 100.");
                    tipPercentInput.Text = "20";
                }
            }
        }
    }
}
