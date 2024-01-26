namespace Memory.Maui_App;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

    }
    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);

        if (args.Current != null)
        {
            Debug.WriteLine($"AppShell: source={args.Current.Location}, target={args.Target.Location}");
        }
    }
}
