using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServices.Services
{
    public class AzureComputerVisionService
    {
        private readonly ComputerVisionClient _computerVisionClient;
        public AzureComputerVisionService(string key, string endPoint)
        {
            var credentials = new ApiKeyServiceClientCredentials(key);
            _computerVisionClient = new ComputerVisionClient(credentials)
            {
                Endpoint=endPoint
            };

        }

        public async Task<ImageAnalysis> AnalizeImageAsync(Stream imageStream)
        {
            var features = new List<VisualFeatureTypes?>()
            { 
                VisualFeatureTypes.Categories,
                VisualFeatureTypes.Description,
                VisualFeatureTypes.Adult
            };
            var result = await _computerVisionClient.AnalyzeImageInStreamAsync(imageStream, features);
            return result;
        }

        public async Task<(string, bool)> GetComputerVisionStatus(Stream imageStream)
        {
            ImageAnalysis imageAnalysis = await AnalizeImageAsync(imageStream);
            StringBuilder status = new StringBuilder();

            status.Append("Description:\n");
            foreach (var caption in imageAnalysis.Description.Captions)
            {
                status.Append($"{caption.Text} with {caption.Confidence};\n");
            }

            status.Append("Categories:\n");
            foreach (var category in imageAnalysis.Categories)
            {
                status.Append($"{category.Name} with {category.Score};\n");
            }

            status.Append("Adult Content:\n");
            status.Append($"Image contains XXX content with: " +
                $"{imageAnalysis.Adult.IsAdultContent} with " +
                $"{imageAnalysis.Adult.AdultScore}; \n");

            status.Append($"Image contains Racy content with: " +
                $"{imageAnalysis.Adult.IsRacyContent} with " +
                $"{imageAnalysis.Adult.RacyScore}; \n");

            status.Append($"Image contains Gory content with: " +
               $"{imageAnalysis.Adult.IsGoryContent} with " +
               $"{imageAnalysis.Adult.GoreScore}; \n");

            bool isAdult = imageAnalysis.Adult.IsAdultContent ||
                imageAnalysis.Adult.IsRacyContent ||
                imageAnalysis.Adult.IsGoryContent;
            (string, bool) tupleResult = (status.ToString(), isAdult);
            return tupleResult;
        }
    }
}
