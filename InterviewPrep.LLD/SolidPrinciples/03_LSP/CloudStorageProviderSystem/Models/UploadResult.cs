
namespace InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Models
{
    public class UploadResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public StorageFile File { get; set; }
    }
}

//Why another class?

//Because tomorrow

//business may ask

//Upload successful

//Virus scan pending

//Checksum

//Version

//Storage Region

//We simply extend

//UploadResult

//No method signatures change.