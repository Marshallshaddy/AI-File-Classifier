using System;
using System.IO;
using PDFiumSharp;
using Tesseract;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AI_File_Classifier
{
    public class ImageTextExtractor
    {
        private string _outputFilePath;

        public ImageTextExtractor(string outputFilePath)
        {
            _outputFilePath = outputFilePath;
        }

        public void ProcessImagesAndPDFs(string inputFolder)
        {
            var filePaths = Directory.GetFiles(inputFolder, "*.jpg", SearchOption.TopDirectoryOnly)
                .Concat(Directory.GetFiles(inputFolder, "*.png", SearchOption.TopDirectoryOnly))
                .Concat(Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly))
                .ToArray();

            using (StreamWriter writer = new StreamWriter(_outputFilePath))
            {
                foreach (var filePath in filePaths)
                {
                    string extension = Path.GetExtension(filePath).ToLower();
                    if (extension == ".jpg" || extension == ".png")
                    {
                        string text = ExtractTextFromImage(filePath);
                        WriteExtractedText(writer, filePath, text);
                    }
                    else if (extension == ".pdf")
                    {
                        string text = ExtractTextFromPDF(filePath);
                        WriteExtractedText(writer, filePath, text);
                    }
                }
            }
            Console.WriteLine($"Text from all files has been saved to: {_outputFilePath}");
        }

        private string ExtractTextFromImage(string imagePath)
        {
            try
            {
                using (var engine = new TesseractEngine(@".\tessdata\", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            return page.GetText();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing image {imagePath}: {ex.Message}");
                return string.Empty;
            }
        }

        private string ExtractTextFromPDF(string pdfPath)
        {
            try
            {
                using (var document = new PdfDocument(pdfPath))
                {
                    string extractedText = "";
                    for (int i = 0; i < document.Pages.Count; i++)
                    {
                        using (var page = document.Pages[i])
                        {
                            Console.WriteLine($"Processing page {i + 1} of {pdfPath}");

                            using (var pdfBitmap = new PDFiumBitmap((int)page.Width, (int)page.Height, true))
                            {
                                page.Render(pdfBitmap);

                                // Save the PDFiumBitmap as a PNG in memory and extract text
                                using (var memoryStreamBmp = new MemoryStream())
                                {
                                    pdfBitmap.Save(memoryStreamBmp); // Save the PDFiumBitmap to a memory stream

                                    using (var imageBmp = System.Drawing.Image.FromStream(memoryStreamBmp))
                                    {
                                        using (var memoryStreamPng = new MemoryStream())
                                        {
                                            imageBmp.Save(memoryStreamPng, System.Drawing.Imaging.ImageFormat.Png);

                                            // Use the PNG data to extract text
                                            using (var tesseractEngine = new TesseractEngine(@".\tessdata\", "eng", EngineMode.Default))
                                            {
                                                memoryStreamPng.Position = 0;
                                                using (var pix = Pix.LoadFromMemory(memoryStreamPng.ToArray()))
                                                {
                                                    using (var pageText = tesseractEngine.Process(pix))
                                                    {
                                                        string pageTextContent = pageText.GetText();
                                                        extractedText += pageTextContent;

                                                        Console.WriteLine($"Extracted text from page {i + 1}: {pageTextContent}");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return extractedText;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing PDF {pdfPath}: {ex.Message}");
                return string.Empty;
            }
        }

        private void WriteExtractedText(StreamWriter writer, string filePath, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                writer.WriteLine($"--- Text from file: {Path.GetFileName(filePath)} ---");
                writer.WriteLine(text);
                writer.WriteLine();
            }
            else
            {
                writer.WriteLine($"--- Text from file: {Path.GetFileName(filePath)} ---");
                writer.WriteLine("No text extracted or an error occurred.");
                writer.WriteLine();
            }
        }
    }
}
