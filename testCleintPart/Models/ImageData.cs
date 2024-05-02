namespace testCleintPart.Models
{
    public class ImageData
    {
        public string? ImageDataPath { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public required byte[] Image { get; set; }
    }
}
