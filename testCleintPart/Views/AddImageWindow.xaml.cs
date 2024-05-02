using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using testCleintPart.Contracts;
using testCleintPart.Extensions;
using testCleintPart.Models;

namespace testCleintPart.Views
{
    /// <summary>
    /// Interaction logic for AddImageWindow.xaml
    /// </summary>
    public partial class AddImageWindow : Window
    {
        public ImageData ImageData { get; private set; }
        private readonly IImageApiService _apiService;
        public event Action ImageUpdated;


        public AddImageWindow(IImageApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                PreviewImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                ImageData = new ImageData
                {
                    Name = NameTextBox.Text,
                    Description = DescriptionTextBox.Text,
                    Image = ImageExtension.BitmapSourceToByteArray((BitmapSource)PreviewImage.Source)
                };
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var imageData = new ImageData
            {
                ImageDataPath = ImageData.ImageDataPath,
                Name = ImageData.Name,
                Description = ImageData.Description,
                Image = ImageExtension.BitmapSourceToByteArray((BitmapSource)PreviewImage.Source)
            };

            DialogResult = true;

            bool isSuccessful = await _apiService.SendImage(imageData);
            if (isSuccessful)
            {
                ImageUpdated?.Invoke();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error, try again!");
            }
        }
    }
}
