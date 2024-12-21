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
            new OutputFileConfig { inputFolderPath = @"C:\Users\Marshall\source\repos\AI-File-Classifier\Data\Inventory Report\",
                                   outputFilePath = @"C:\Users\Marshall\source\repos\AI-File-Classifier\Data\ExtractedData\InventoryReport-Output.txt"},
            new OutputFileConfig { inputFolderPath = @"C:\Users\Marshall\source\repos\AI-File-Classifier\Data\Invoices\Images\",
                                   outputFilePath = @"C:\Users\Marshall\source\repos\AI-File-Classifier\Data\ExtractedData\InvoicesImages-Output.txt"},
            new OutputFileConfig { inputFolderPath = @"C:\Users\Marshall\source\repos\AI-File-Classifier\Data\Invoices\Text\",
                                   outputFilePath = @"C:\Users\Marshall\source\repos\AI-File-Classifier\Data\ExtractedData\InvoicesText-Output.txt"},
            new OutputFileConfig { inputFolderPath = @"C:\Users\Marshall\source\repos\AI-File-Classifier\Data\Orders\",
                                   outputFilePath = @"C:\Users\Marshall\source\repos\AI-File-Classifier\Data\ExtractedData\Orders-Output.txt"},
            new OutputFileConfig { inputFolderPath = @"C:\Users\Marshall\source\repos\AI-File-Classifier\Data\Reciepts\",
                                   outputFilePath = @"C:\Users\Marshall\source\repos\AI-File-Classifier\Data\ExtractedData\Reciepts-Output.txt"},
            new OutputFileConfig { inputFolderPath = @"C:\Users\Marshall\source\repos\AI-File-Classifier\Data\Shipping\",
                                   outputFilePath = @"C:\Users\Marshall\source\repos\AI-File-Classifier\Data\ExtractedData\Shipping-Output.txt"}
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