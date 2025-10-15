namespace MauiApp3
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStandardClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("standard");
        }

        private async void OnScientificClicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync("scientific");
        }

    }
}