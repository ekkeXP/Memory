namespace Memory.Maui_App.Views;

public partial class ConfirmPage : ContentPage
{
	GamePageViewModel GPVM;
	//Constructor
	public ConfirmPage(GamePageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = GPVM = viewModel;
	}
	
	//OnAppearing method
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		enteredname.Text = GPVM.EnteredName;
		enteredcards.Text = GPVM.EnteredCardAmount;
    }
}
