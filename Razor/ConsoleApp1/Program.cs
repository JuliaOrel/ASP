using System;
using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

string connString= "DefaultEndpointsProtocol=https;AccountName=teststoraccount14;AccountKey=ZiHjvVDLZjg3UtfVLGi5nzVrBS62YvmCaN+XmaksNsOok586GpRNo9V1zYw0ZjN4gJsbgRW98CA/+ASt7cP2Mw==;BlobEndpoint=https://teststoraccount14.blob.core.windows.net/;QueueEndpoint=https://teststoraccount14.queue.core.windows.net/;TableEndpoint=https://teststoraccount14.table.core.windows.net/;FileEndpoint=https://teststoraccount14.file.core.windows.net/;";
BlobServiceClient blobServiceClient = new BlobServiceClient(connString);
string containerName = "text-files-container";
string localDirectory = "../../../Data";
string fileName = "textFile" + Guid.NewGuid().ToString() + ".txt";
string localFilePath = Path.Combine(localDirectory, fileName);
await File.WriteAllTextAsync(localFilePath, "Any text");
BlobContainerClient BlobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
await BlobContainerClient.CreateIfNotExistsAsync();
BlobClient blobClient = BlobContainerClient.GetBlobClient(fileName);
Console.WriteLine(blobClient.Uri);
using(FileStream fs=File.OpenRead(localFilePath))
{
    await blobClient.UploadAsync(content: fs, overwrite: true );
}
await foreach (var item in BlobContainerClient.GetBlobsAsync())
{
    Console.WriteLine(item.Name);
    BlobClient client = BlobContainerClient.GetBlobClient(item.Name);
    Console.WriteLine(client.Uri);
    Console.WriteLine(client.Uri.AbsoluteUri);
}
Console.WriteLine("\n");
string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOAD.txt");
var blobDownloadInfo =  blobClient.DownloadTo(downloadFilePath);
using(FileStream fs=File.OpenWrite(downloadFilePath))
{
    //await blobDownloadInfo.Content.ToMemory().CopyTo();
    fs.Write(blobDownloadInfo.Content);
}
Console.ReadLine();
await blobClient.DeleteAsync();
await BlobContainerClient.DeleteAsync();
File.Delete(localFilePath);
File.Delete(downloadFilePath);

//namespace ConsoleApp1
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello World!");
//        }
//    }
//}
