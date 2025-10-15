using System.Globalization;


namespace MauiApp3;

public partial class CalculatorBilimselPage : ContentPage
{

    private bool isTwoSuperscriptActive = false;
    private Color btnTwoOriginalBg;
    private Color btnTwoOriginalText;
    public CalculatorBilimselPage()
    {
        InitializeComponent();
    }

    double number1 = 0;
    double number2 = 0;
    string operatorSymbol = "";
    private void Clr_Clicked(object sender, EventArgs e)
    {


        CScreen.Text = "0";


    }

    private void CLEAR_Clicked(object sender, EventArgs e)
    {
        CScreen.Text = "0";
        LBlHistory.Text = "";
        number1 = 0;
        operatorSymbol = "";
    }

    private void BACKSpaceClicked(object sender, EventArgs e)
    {
        CScreen.Text = CScreen.Text.Length > 1 ? CScreen.Text[..^1] : "0";
    }

    private void NummClicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;

        var str = CScreen.Text + btn.Text;
        double.TryParse(str, out double value);
        CScreen.Text = value.ToString();
    }

    private void Operator_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        LBlHistory.Text = CScreen.Text + " " + btn.Text;
        number1 = double.Parse(CScreen.Text);
        CScreen.Text = "0";
        operatorSymbol = btn.Text;
    }

    private bool isNewEntry;
    private void Decimal_Clicked(object sender, EventArgs e)
    {
        var dec = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        if (isNewEntry || string.IsNullOrEmpty(CScreen.Text))
        {
            CScreen.Text = "0" + dec;
            isNewEntry = false;
            return;
        }

        if (!CScreen.Text.Contains(dec))
        {
            CScreen.Text += dec;
        }

    }

    private double memoryValue = 0.0;
    private bool memoryHasValue = false;
    

  

    private string FormatNumberForDisplay(double value)
    {
        return value.ToString("G12", CultureInfo.InvariantCulture);
    }



    private void PM_Clicked(object sender, EventArgs e)
    {
        var text = CScreen.Text ?? "0";

        if (!double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
        {
            return;
        }

        value = -value;
        CScreen.Text = FormatNumberForDisplay(value);


    }

    private void Equal_Clicked(object sender, EventArgs e)
    {
        double number2 = double.Parse(CScreen.Text);
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
                    CScreen.Text = "Tanımsız";
                    return;
                }
                result = 1 / number2;
                break;
            case "log":
                if (number2 <= 0) { CScreen.Text = "Geçersiz"; return; }
                result = Math.Log10(number2);
                break;
            case "|x|":
                if (!double.TryParse(CScreen.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double value))
                {
                    CScreen.Text = "Geçersiz";
                    return;
                }

                 result = Math.Abs(value);
                
                if (double.IsNaN(result))
                {
                    CScreen.Text = "Geçersiz";
                    return;
                }
                if (double.IsInfinity(result))
                {
                    CScreen.Text = "Error";
                    return;
                }
                break;

            case "mod":
                if (number2 == 0.0)
                {
                    CScreen.Text = "Geçersiz" ;
                    return;
                }
                result = number1 % number2;
                break;
            case "ln": 
                if (number2 <= 0.0)
                {
                    CScreen.Text = "Geçersiz";
                    return;
                }

                result = Math.Log(number2); 
                if (double.IsNaN(result))
                {
                    CScreen.Text = "Geçersiz";
                    return;
                }
                if (double.IsInfinity(result))
                {
                    CScreen.Text = "Error";
                    return;
                }
                break;
            case "π":
                {
                    result = Math.PI ;

                 
                }
                break;

            default:
                result = number2;
                break;


        }

        CScreen.Text = result.ToString();
        LBlHistory.Text = $"{number1} {operatorSymbol} {number2}";
        number1 = result;
    }


    private void UpdateMemoryIndicator()
    {

        if (LBlMemoryIndicator != null)
            LBlMemoryIndicator.Text = memoryHasValue ? "M" : "";
    }

    private void Ms_Clicked(object sender, EventArgs e)
    {
        if (!double.TryParse(CScreen.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double current))
        {
            return;
        }

        memoryValue = current;
        memoryHasValue = true;
        UpdateMemoryIndicator();

    }

    private void Mc_Clicked(object sender, EventArgs e)
    {
        memoryValue = 0.0;
        memoryHasValue = false;
        UpdateMemoryIndicator();

    }

    private void Mp_Clicked(object sender, EventArgs e)
    {
        if (!double.TryParse(CScreen.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double current))
            return;

        memoryValue += current;
        memoryHasValue = true;
        UpdateMemoryIndicator();
    }

    private void Mr_Clicked(object sender, EventArgs e)
    {
        if (!memoryHasValue)
        {
            return;
        }
    }

    private void Mm_Clicked(object sender, EventArgs e)
    {
        if (!double.TryParse(CScreen.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double current))
            return;

        memoryValue -= current;
        memoryHasValue = true;
        UpdateMemoryIndicator();
    }


    private async void MV_Clicked(object sender, EventArgs e)
    {
        if (!memoryHasValue)
        {
            await DisplayAlert("Bellek", "Bellek boş.", "Tamam");
            return;
        }


        LBlHistory.Text = $"Memory: {memoryValue.ToString("G12")}";
    }
    private void exp_Clicked(object sender, EventArgs e)
    {
        if (!double.TryParse(CScreen.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double val))
        {
            CScreen.Text = "Geçersiz";
            return;
        }

        double res = Math.Exp(val);

        if (double.IsNaN(res))
        {
            CScreen.Text = "Geçersiz";
            return;
        }
        if (double.IsInfinity(res))
        {
            CScreen.Text = "Error";
            return;
        }

        CScreen.Text = FormatNumberForDisplay(res);
        if (LBlHistory != null)
            LBlHistory.Text = $"exp({FormatNumberForDisplay(val)}) = {CScreen.Text}";

        isNewEntry = true;
    }

    private void OpenParen_Clicked(object sender, EventArgs e)
    {
        if (isNewEntry)
        {
            CScreen.Text = "(";
            isNewEntry = false;
        }
        else
        {
            CScreen.Text += "(";
        }
    }

    private void CloseParen_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(CScreen.Text))
        {
            return;
        }

        CScreen.Text += ")";
    }

    private bool isDegree = false;       
    private bool isHyperbolic = false;

    private Label CCScreen;
    private Label lblMode;
    private void Trigo_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn == null) return;

        string op = btn.Text?.Trim().ToLowerInvariant() ?? string.Empty;

        if (op == "hyp")
        {
            isHyperbolic = !isHyperbolic;
            UpdateModeLabel();
            return;
        }

        var text = CScreen?.Text ?? string.Empty;
        if (!double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out double inputValue))
        {
            CScreen.Text = "Geçersiz";
            return;
        }

        double result;
        try
        {
            double angleRad = isDegree ? inputValue * Math.PI / 180.0 : inputValue;

            switch (op)
            {
                case "sin":
                    result = isHyperbolic ? Math.Sinh(inputValue) : Math.Sin(angleRad);
                    break;

                case "cos":
                    result = isHyperbolic ? Math.Cosh(inputValue) : Math.Cos(angleRad);
                    break;

                case "tan":
                    {
                        double cosv = Math.Cos(angleRad);
                        if (Math.Abs(cosv) < 1e-12) { CScreen.Text = "Error"; return; }
                        result = Math.Tan(angleRad);
                    }
                    break;

                case "csc": 
                    {
                        double denom = isHyperbolic ? Math.Sinh(inputValue) : Math.Sin(angleRad);
                        if (Math.Abs(denom) < 1e-15)
                        {
                            CScreen.Text = "Error";
                            return;
                        }
                        result = 1.0 / denom;
                    }
                    break;

                case "sec": 
                    {
                        double denom = isHyperbolic ? Math.Cosh(inputValue) : Math.Cos(angleRad);
                        if (Math.Abs(denom) < 1e-15)
                        {
                            CScreen.Text = "Error";
                            return;
                        }
                        result = 1.0 / denom;
                    }
                    break;

                case "cot": 
                    {
                        double denom = isHyperbolic ? Math.Tanh(inputValue) : Math.Tan(angleRad);
                        if (Math.Abs(denom) < 1e-15)
                        {
                            CScreen.Text = "Error";
                            return;
                        }
                        result = 1.0 / denom;
                    }
                    break;

                default:
                    CScreen.Text = "Bilinmiyor";
                    return;
            }

            if (double.IsNaN(result))
            {
                CScreen.Text = "Geçersiz";
                return;
            }
            if (double.IsInfinity(result))
            {
                CScreen.Text = "Error";
                return;
            }

            CScreen.Text = FormatNumberForDisplay(result);
            isNewEntry = true;
        }
        catch
        {
            CScreen.Text = "Geçersiz";
        }
    }

    private void UpdateModeLabel()
    {
        if (lblMode == null) return;
        lblMode.Text = (isDegree ? "DEG" : "RAD") + (isHyperbolic ? " HYP" : "");
    }


    private void TwoSuperscript_Clicked(object sender, EventArgs e)
    {
        isTwoSuperscriptActive = !isTwoSuperscriptActive;


        if (isTwoSuperscriptActive)
        {
            btnTwoSuperscript.BackgroundColor = Color.FromArgb("#2196F3");
            btnTwoSuperscript.TextColor = Colors.White;
        }
        else
        {
            btnTwoSuperscript.BackgroundColor = btnTwoOriginalBg;
            btnTwoSuperscript.TextColor = btnTwoOriginalText;
        }

        btnRow6_primary.IsVisible = !isTwoSuperscriptActive;
        btnRow6_alt.IsVisible = isTwoSuperscriptActive;

        
        btnRow7_primary.IsVisible = !isTwoSuperscriptActive;
        btnRow7_alt.IsVisible = isTwoSuperscriptActive;

      
        btnRow8_primary.IsVisible = !isTwoSuperscriptActive;
        btnRow8_alt.IsVisible = isTwoSuperscriptActive;

        
        btnRow9_primary.IsVisible = !isTwoSuperscriptActive;
        btnRow9_alt.IsVisible = isTwoSuperscriptActive;

        
        btnRow10_primary.IsVisible = !isTwoSuperscriptActive;
        btnRow10_alt.IsVisible = isTwoSuperscriptActive;

        btnRow11_primary.IsVisible = !isTwoSuperscriptActive; 
        btnRow11_alt.IsVisible = isTwoSuperscriptActive;
    }

    private void XPow2_Clicked(object sender, EventArgs e)
    {
        if (!TryGetScreenValue(out double x)) return;
        double res = Math.Pow(x, 2);
        ShowResult(res);
    }

    private void XPow3_Clicked(object sender, EventArgs e)
    {
        if (!TryGetScreenValue(out double x)) return;
        double res = Math.Pow(x, 3);
        ShowResult(res);
    }

    private void Sqrt2_Clicked(object sender, EventArgs e)
    {
        if (!TryGetScreenValue(out double x)) return;
        if (x < 0)
        {
            CScreen.Text = "Geçersiz";
            return;
        }
        double res = Math.Sqrt(x);
        ShowResult(res);
    }

    private void CubeRoot_Clicked(object sender, EventArgs e)
    {
        if (!TryGetScreenValue(out double x)) return;
        double res = CubeRoot(x);
        ShowResult(res);
    }

 
    private async void YPowX_Clicked(object sender, EventArgs e)
    {
        if (!TryGetScreenValue(out double x)) return;

        string yInput = await DisplayPromptAsync("y değeri", "y değerini girin:", accept: "Tamam", cancel: "İptal", keyboard: Keyboard.Numeric);
        if (string.IsNullOrWhiteSpace(yInput)) return;

        if (!double.TryParse(yInput, NumberStyles.Any, CultureInfo.CurrentCulture, out double y))
        {
            CScreen.Text = "Geçersiz";
            return;
        }

        double res = Math.Pow(y, x);
        ShowResult(res);
    }

    private void Tenx_Clicked(object sender, EventArgs e)
    {
        if (!TryGetScreenValue(out double x)) return;
        double res = Math.Pow(10.0, x);
        ShowResult(res);
    }

    private void TwoPowX_Clicked(object sender, EventArgs e)
    {
        if (!TryGetScreenValue(out double x)) return;
        double res = Math.Pow(2.0, x);
        ShowResult(res);
    }

    
    private async void LogBaseY_Clicked(object sender, EventArgs e)
    {
        if (!TryGetScreenValue(out double x)) return;

        string yInput = await DisplayPromptAsync("log tabanı", "log tabanı y değerini girin:", accept: "Tamam", cancel: "İptal", keyboard: Keyboard.Numeric);
        if (string.IsNullOrWhiteSpace(yInput)) return;

        if (!double.TryParse(yInput, NumberStyles.Any, CultureInfo.CurrentCulture, out double y))
        {
            CScreen.Text = "Geçersiz";
            return;
        }

        if (x <= 0 || y <= 0 || Math.Abs(y - 1.0) < double.Epsilon)
        {
            CScreen.Text = "Geçersiz";
            return;
        }

        double res = Math.Log(x) / Math.Log(y);
        ShowResult(res);
    }

    private void Exp_Clicked(object sender, EventArgs e)
    {
        if (!TryGetScreenValue(out double x)) return;
        double res = Math.Exp(x);
        ShowResult(res);
    }

    private bool TryGetScreenValue(out double value)
    {
        value = 0.0;
        var txt = CScreen?.Text ?? string.Empty;
        if (!double.TryParse(txt, NumberStyles.Any, CultureInfo.CurrentCulture, out value))
        {
            CScreen.Text = "Geçersiz";
            return false;
        }
        return true;
    }

    private void ShowResult(double res)
    {
        if (double.IsNaN(res))
        {
            CScreen.Text = "Geçersiz";
        }
        else if (double.IsInfinity(res))
        {
            CScreen.Text = "Error";
        }
        else
        {
            CScreen.Text = FormatNumberForDisplay(res);
            isNewEntry = true;
        }
    }

    private double CubeRoot(double v)
    {
        if (v == 0.0) return 0.0;
        return Math.Sign(v) * Math.Pow(Math.Abs(v), 1.0 / 3.0);
    }

    private void F_E_Clicked(object sender, EventArgs e)
    {
        CScreen.Text = "0,e+0";
    }
}
