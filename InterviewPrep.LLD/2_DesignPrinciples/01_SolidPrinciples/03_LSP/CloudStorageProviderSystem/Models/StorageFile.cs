

namespace InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Models
{
    public class StorageFile
    {
        public Guid FileId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string StorageProvider { get; set; } = string.Empty;

        public string FileUrl { get; set; } = string.Empty;

        public DateTime UploadedOn { get; set; }
    }
}

//Why not return UploadRequest?

//Because

//Request

//↓

//goes inside

//Result

//↓

//comes outside

//Returning UploadRequest would expose input rather than the stored resource.