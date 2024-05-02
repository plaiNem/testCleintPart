using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using testCleintPart.Contracts;
using testCleintPart.Extensions;
using testCleintPart.Models;

namespace testCleintPart.Views
{
    public partial class EditImageWindow : Window
    {
        public ImageData ImageData { get; private set; }
        private readonly IImageApiService _apiService;
        public event Action ImageUpdated;

        public EditImageWindow(IImageApiService apiService, ImageData imageData)
        {
            InitializeComponent();
            _apiService = apiService;
            ImageData = imageData;

            NameTextBox.Text = ImageData.Name;
            DescriptionTextBox.Text = ImageData.Description;
            PreviewImage.Source = ImageExtension.ByteArrayToBitmapImage(ImageData.Image);
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
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ImageData.Name = NameTextBox.Text;
            ImageData.Description = DescriptionTextBox.Text;
            ImageData.Image = ImageExtension.BitmapSourceToByteArray((BitmapSource)PreviewImage.Source);

            bool isSuccessful = await _apiService.EditImage(ImageData);
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