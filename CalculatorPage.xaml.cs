
using System.Globalization;

namespace MauiApp3
{

    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();
        }

        private void NumClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;

            var str = cScreen.Text + btn.Text;
            double.TryParse(str, out double value);
            cScreen.Text = value.ToString();
        }

        double number1 = 0;
        string operatorSymbol = "";
        private void OperatorClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            lblHistory.Text = cScreen.Text + " " + btn.Text;
            number1 = double.Parse(cScreen.Text);
            cScreen.Text = "0";
            operatorSymbol = btn.Text;
        }

        private void EqualClicked(object sender, EventArgs e)
        {
            double number2 = double.Parse(cScreen.Text);
            double result = 0;
            switch (operatorSymbol)
            {
                case "+":
                    result = (number1 + number2);
                    break;
                case "-":
                    result = (number1 - number2);
                    break;
                case "x":
                    result = (number1 * number2);
                    break;
                case "÷":
                    result = (number1 / number2);
                    break;
                case "%":
                    result = (number1 * (number2 / 100));
                    break;
                case "1/x":
                    if (number2 == 0)
                    {
                        cScreen.Text = "Tanımsız";
                        return;
                    }
                    result = 1 / number2;
                    break;

            }

            cScreen.Text = result.ToString();
            lblHistory.Text = $"{number1} {operatorSymbol} {number2}";
            number1 = result;
        }

        private void Sqrt_Clicked(object sender, EventArgs e)
        {
            if (!double.TryParse(cScreen.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double val))
            {
                cScreen.Text = "Geçersiz";
                return;
            }

            if (val < 0)
            {
                cScreen.Text = "Geçersiz";
                return;
            }

            var res = Math.Sqrt(val);
            cScreen.Text = FormatNumberForDisplay(res);
            isNewEntry = true;
        }
        private void Clear_Clicked(object sender, EventArgs e)
        {
            cScreen.Text = "0";
            lblHistory.Text = "";
            number1 = 0;
            operatorSymbol = "";
        }

        private void Square_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            lblHistory.Text = cScreen.Text + " " + btn.Text;

            number1 = double.Parse(cScreen.Text);
            var result = number1 * number1;
            cScreen.Text = result.ToString();
        }

        private void BackSpaceClicked(object sender, EventArgs e)
        {
            cScreen.Text = cScreen.Text.Length > 1 ? cScreen.Text[..^1] : "0";
        }

        private void CLR_Clicked(object sender, EventArgs e)
        {
            cScreen.Text = "0";

        }

        private double memoryValue = 0.0;
        private bool memoryHasValue = false;

        private void UpdateMemoryIndicator()
        {

            if (lblMemoryIndicator != null)
                lblMemoryIndicator.Text = memoryHasValue ? "M" : "";
        }

        private string FormatNumberForDisplay(double value)
        {
            return value.ToString("G12", CultureInfo.InvariantCulture);
        }


        private void MS_Clicked(object sender, EventArgs e)
        {
            if (!double.TryParse(cScreen.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double current))
            {
                return;
            }

            memoryValue = current;
            memoryHasValue = true;
            UpdateMemoryIndicator();

        }

        private void MC_Clicked(object sender, EventArgs e)
        {
            memoryValue = 0.0;
            memoryHasValue = false;
            UpdateMemoryIndicator();

        }

        private void MPlus_Clicked(object sender, EventArgs e)
        {
            if (!double.TryParse(cScreen.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double current))
                return;

            memoryValue += current;
            memoryHasValue = true;
            UpdateMemoryIndicator();
        }

        private void MR_Clicked(object sender, EventArgs e)
        {
            if (!memoryHasValue)
            {
                return;
            }
        }

        private void MMinus_Clicked(object sender, EventArgs e)
        {
            if (!double.TryParse(cScreen.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double current))
                return;

            memoryValue -= current;
            memoryHasValue = true;
            UpdateMemoryIndicator();
        }


        private async void Mv_Clicked(object sender, EventArgs e)
        {
            if (!memoryHasValue)
            {
                await DisplayAlert("Bellek", "Bellek boş.", "Tamam");
                return;
            }


            lblHistory.Text = $"Memory: {memoryValue.ToString("G12")}";
        }


        private async void PMClicked(object sender, EventArgs e)
        {
            var text = cScreen.Text ?? "0";

            if (!double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                return;
            }

            value = -value;
            cScreen.Text = FormatNumberForDisplay(value);
        }

        private bool isNewEntry;
            private void DecimalClicked(object sender, EventArgs e)
        {
            var dec = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator; 

            if (isNewEntry || string.IsNullOrEmpty(cScreen.Text))
            {
                cScreen.Text = "0" + dec;
                isNewEntry = false;
                return;
            }

            if (!cScreen.Text.Contains(dec))
            {
                cScreen.Text += dec;
            }
        }
    
    }

}