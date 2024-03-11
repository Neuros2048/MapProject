
namespace Client.Services;

public class ImageEditor(HttpClient http)
{
    private readonly HttpClient _http = http;

    private async Task<Stream> GetImageStreamAsync()
    {
        return await _http.GetStreamAsync(
            "https://avatars.githubusercontent.com/u/9141961");
        
    }
    /*
    public  async Task<Stream> ImageToGridAsync(Stream inputStream)
    {
        using (Image<Rgba32> originalImage = await Image.LoadAsync<Rgba32>(inputStream))
        {
            // Create a blank image to hold the final result
            using (Image<Rgba32> resultImage = new Image<Rgba32>(originalImage.Width*3, originalImage.Height*3))
            {
                // Divide the image into 3x3 grid
                int cellWidth = originalImage.Width ;
                int cellHeight = originalImage.Height ;
                using Image<Rgba32> croppedImage = originalImage.Clone();
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        // Crop the corresponding part of the original image
                        

                        // Apply the desired operation (e.g., grayscale)
                       

                        // Draw the modified cell image onto the result image
                        resultImage.Mutate(x => x.DrawImage(croppedImage, new Point(col * cellWidth, row * cellHeight), 1f));
                    }
                }

                // Convert the result image to a stream
                var outputStream = new MemoryStream();
                await resultImage.SaveAsync(outputStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
                outputStream.Seek(0, SeekOrigin.Begin);
                return outputStream;
            }
        }
    }*/
    /*
    public async Task<Stream> eddit()
    {
        
        var img = Image.FromStream( await GetImageStreamAsync());
        using (Graphics g = Graphics.FromImage(cellBitmap))
        {
            Rectangle sourceRect = new Rectangle(col * cellWidth, row * cellHeight, cellWidth, cellHeight);
            Rectangle destRect = new Rectangle(0, 0, cellWidth, cellHeight);
            g.DrawImage(originalImage, destRect, sourceRect, GraphicsUnit.Pixel);
        }
        
        
        
        using (Graphics g = Graphics.FromImage(img))
            g.DrawLine(Pens.Black, 10,10, 20,20);
        img.Save("<FILE_PATH>");
    }*/
}