using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AzureWebJobsEx
{
    public class Functions
    {
        public static void HandlerQueueMessage(
            [QueueTrigger("some-messages")] string message, ILogger logger)
        {
            logger.LogInformation(message);
            
        }

        //public static void HandlerBlobImage(
        //    [BlobTrigger("image-container/{filename}")] Stream inputStream,
        //    [Blob("container-copy/copy-{filename}", FileAccess.Write)] Stream outBlob,
        //    ILogger logger)
        //    {
        //        logger.LogCritical($"Length: { inputStream.Length}");
        //        inputStream.CopyTo(outBlob);
        //    }

        public static async Task HandlerMinBlobImage(
           [BlobTrigger("image-container/{filename}")] Stream inBlob, string filename,
            [Blob("image-container-min/min-{filename}", FileAccess.Write)] Stream outBlob,
            ILogger logger)
        {
            
            using (Image image = Image.Load(inBlob))
            {
                //1500*1000
                //w==1.5
                //h==0.666
                double x = (double)image.Width / (double)image.Height;
                double y = (double)image.Height / (double)image.Width;
                image.Mutate(i => i.Resize((int)x * 100, (int)y * 100));
                await image.SaveAsync(outBlob, GetEncoder(Path.GetExtension(filename)));
            }
        }

        private static IImageEncoder GetEncoder(string extension)
        {
            IImageEncoder encoder = null;
            extension = extension.Replace(".", "");
            bool isSupported = Regex.IsMatch(extension, "png|jpg|gif", RegexOptions.IgnoreCase);
            if(isSupported)
            {
                switch(extension.ToLower())
                {
                    case "png":
                        encoder = new PngEncoder();
                        break;
                    case "jpeg":
                        encoder = new JpegEncoder();
                        break;
                    case "jpg":
                        encoder = new JpegEncoder();
                        break;
                    case "gif":
                        encoder = new GifEncoder();
                        break;
                    default:
                        break;
                }
            }
            return encoder;
        }
    }
}
