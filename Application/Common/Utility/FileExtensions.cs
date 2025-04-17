using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Common.Utility
{
    public static class FileExtensions
    {
        public static byte[] ConvertImageToByteArray(IFormFile imageFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Copy the content of the image file into the memory stream
                imageFile.CopyTo(memoryStream);

                // Convert the memory stream to a byte array
                return memoryStream.ToArray();
            }
        }
        //public static async Task<Image> ByteArrayToImageAsync(byte[] byteArray)
        //{
        //    using var ms = new MemoryStream(byteArray);
        //    return Image.Load(ms);
        //}
        public static string ByteArrayToImageBase64(byte[] byteArray)
        {
            string base64Image = Convert.ToBase64String(byteArray);
            string imageSrc = $"data:image/jpeg;base64,{base64Image}";
            return imageSrc;
        }
        public static byte[] FromImageToByteArray(string array)
        {
            string base64String = array;

            // Step 1: Check if the string contains the Base64 prefix and remove it
            if (base64String.Contains("data:image") && base64String.Contains("base64,"))
            {
                base64String = base64String.Split(',')[1];  // Remove the prefix
            }

            // Step 2: Ensure the Base64 string length is a multiple of 4
            int mod4 = base64String.Length % 4;
            if (mod4 > 0)
            {
                base64String = base64String.PadRight(base64String.Length + (4 - mod4), '=');
            }
            byte[] imageBytes = Convert.FromBase64String(base64String);
            return imageBytes;
        }
    }
}
