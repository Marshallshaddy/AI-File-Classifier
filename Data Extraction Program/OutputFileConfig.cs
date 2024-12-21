using System.IO;

namespace AI_File_Classifier
{
    internal class OutputFileConfig
    {
            public string inputFolderPath { get; set; }
            public string outputFilePath { get; set; }
            public bool CopyIfNewer { get; set; } = true;
    }
}
