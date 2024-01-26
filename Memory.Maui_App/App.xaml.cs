namespace Memory.Maui_App;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new AppShell();
	}

    private async void OnMenuItemClicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(UserOptionsPage)}");
    }
}
