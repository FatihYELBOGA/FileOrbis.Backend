namespace FileOrbis.IntermediateLayer.Backend.Requests.File_System
{
    public class NavigateRequest
    {
        public int DType { get; set; }
        public string? DirectoryPath { get; set; }
        public string? RelativePath { get; set; }
        public string? FolderId { get; set; }

    }

}
