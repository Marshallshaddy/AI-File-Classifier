using AI_File_Classifier;
using System.IO;
using System.Runtime.InteropServices;

//Run this file seperate from the project solution
class Program
{
    static void Main(string[] args)
    {

        var inputOutpuPairs = new[]
        {
            new OutputFileConfig { inputFolderPath = @"/Data/RawData/Inventory Report/",
                                   outputFilePath = @"Data/ExtractedData/InventoryReport-Output.txt"},
            new OutputFileConfig { inputFolderPath = @"Data/RawData/Invoices/Images/",
                                   outputFilePath = @"Data/ExtractedData/InvoicesImages-Output.txt"},
            new OutputFileConfig { inputFolderPath = @"Data/RawData/Invoices/Text/",
                                   outputFilePath = @"Data/ExtractedData/InvoicesText-Output.txt"},
            new OutputFileConfig { inputFolderPath = @"Data/RawData/Orders/",
                                   outputFilePath = @"Data/ExtractedData/Orders-Output.txt"},
            new OutputFileConfig { inputFolderPath = @"Data/RawData/Reciepts/",
                                   outputFilePath = @"Data/ExtractedData/Reciepts-Output.txt"},
            new OutputFileConfig { inputFolderPath = @"Data/RawData/Shipping/",
                                   outputFilePath = @"Data/ExtractedData/Shipping-Output.txt"}
        };

        foreach (var config in inputOutpuPairs)
        {
            ImageTextExtractor extractor = new ImageTextExtractor(config.outputFilePath);
            extractor.ProcessImagesAndPDFs(config.inputFolderPath);

            if (config.CopyIfNewer)
            {
                // Implement copy logic here
                string sourceFile = Path.Combine(config.inputFolderPath, config.outputFilePath);
                string destinationFile = Path.Combine(Environment.CurrentDirectory, config.outputFilePath);

                if (File.Exists(sourceFile) && (File.GetLastWriteTime(sourceFile) > File.GetLastWriteTime(destinationFile)))
                {
                    File.Copy(sourceFile, destinationFile, true);
                }
            }
            Console.WriteLine("Text extraction completed. Check the output file for results.");
        }

    }
}