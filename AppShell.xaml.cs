
using Microsoft.Maui.Controls;

namespace MauiApp3
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("standard", typeof(CalculatorPage));
            Routing.RegisterRoute("scientific", typeof(CalculatorBilimselPage));
        }
    }
}