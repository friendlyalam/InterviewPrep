
namespace InterviewPrep.LLD.SolidPrinciples.LSP.CloudStorageProviderSystem.Models
{
    public class UploadRequest
    {
        public string FileName { get; set; } = string.Empty;

        public byte[] FileContent { get; set; }

        public string ContentType { get; set; } = string.Empty;

        public long FileSizeInBytes { get; set; }
    }
}

//Why do we need UploadRequest?

//Without it

//developers usually write

//Upload(
//    string fileName,
//    byte[] content,
//    string type,
//    long size,
//    bool overwrite,
//    ...
//);

//Eventually

//Upload()

//gets 8-10 parameters.

//Bad design.

//Instead

//everything belongs to one model.