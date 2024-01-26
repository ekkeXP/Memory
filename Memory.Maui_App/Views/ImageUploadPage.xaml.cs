using System.Security;

namespace Memory.Maui_App.Views;

public partial class ImageUploadPage : ContentPage
{
    int IMGAmount = 0;
    ImageUploadPageViewModel iupvm;

    //Constructor
    public ImageUploadPage(ImageUploadPageViewModel IUPVM, IMemoryScoreRepository db)
    {
        InitializeComponent();
        BindingContext = iupvm = IUPVM;
        iupvm.DB = db;
        DatabaseLoad();
    }

    //Load the images present in the database
    private async Task DatabaseLoad()
    {
        iupvm.UploadedCardsAmount = 0;
        ImageList.Clear();
        String[] images = iupvm.DB.GetImages();
        foreach (String Url in images)
        {
            if(File.Exists(Url)){
                ImageList.Add(
                    new Image
                    {
                        Source = Url,
                        WidthRequest = 71.4,
                        HeightRequest = 100,
                    }
                );
                iupvm.UploadedCardsAmount++;
            }
            else
            {
                iupvm.DB.DeleteImg(Url);
            }
        }
    }

    //Select (multiple) image(s)
    private async void OnGetPictureClicked(object sender, EventArgs e)
    {
        await UploadIMG2DB();
    }

    //Task to upload the selected images to the database
    private async Task UploadIMG2DB()
    {
        try
        {
            var result = await FilePicker.PickMultipleAsync(new PickOptions
            {
                PickerTitle = "Pick a Photo",
                FileTypes = FilePickerFileType.Images

            });

            if (result != null)
            {
                foreach (FileResult FR in result)
                {
                    if (IMGAmount > 25) continue;
                    var imageStream = await FR.OpenReadAsync();
                    var imageBytes = new byte[imageStream.Length];
                    await imageStream.ReadAsync(imageBytes, 0, imageBytes.Length);


                    string IS = FR.FullPath;

                    iupvm.DB.UploadImage(IS);
                    IMGAmount++;
                }
            }
            DatabaseLoad();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    //Function to convert image to byte array
    public byte[] imageToByteArray(Image imageIn)
    {
        return imageToByteArray(imageIn);
    }
}