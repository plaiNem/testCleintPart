using System.Windows;
using testCleintPart.Contracts;
using testCleintPart.Models;
using testCleintPart.Services;
using testCleintPart.ViewModels;
using testCleintPart.Views;

namespace testCleintPart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageViewModel _viewModel;
        private readonly IImageApiService _apiService;

        public MainWindow()
        {
            _viewModel = new ImageViewModel();
            DataContext = _viewModel;
            _apiService = new ImageApiService();
        }

        private async void GetImages_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Images = await _apiService.GetAllImages();
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            var addImageWindow = new AddImageWindow(_apiService);
            addImageWindow.ImageUpdated += async () => _viewModel.Images = await _apiService.GetAllImages();
            if (addImageWindow.ShowDialog() == true)
            {
                _ = addImageWindow.ImageData;
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedImageData = (ImageData)ImagesListBox.SelectedItem;

            if (selectedImageData != null)
            {
                var editImageWindow = new EditImageWindow(_apiService, selectedImageData);
                editImageWindow.ImageUpdated += async () => _viewModel.Images = await _apiService.GetAllImages();
                if (editImageWindow.ShowDialog() == true)
                {
                    _ = editImageWindow.ImageData;
                }
            }
            else
            {
                MessageBox.Show("Choose the image to change!");
            }
        }


        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedImageData = (ImageData)ImagesListBox.SelectedItem;
            if (selectedImageData != null)
            {

                bool isSuccessful = await _apiService.DeleteImage(selectedImageData.ImageDataPath);
                if (isSuccessful)
                {
                    _viewModel.Images = await _apiService.GetAllImages();
                }
                else
                {
                    MessageBox.Show("Error, try again!");
                }
            }
            else
            {
                MessageBox.Show("Choose the image to change!");
            }
        }
    }
}