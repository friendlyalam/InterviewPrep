
namespace InterviewPrep.LLD.OOPS
{
    //=========================================================
    // Abstract Base Class
    //=========================================================
    public abstract class Document
    {
        //-----------------------------------------
        // Properties
        //-----------------------------------------

        public Guid DocumentId { get; }

        public string FileName { get; }

        public long FileSize { get; }

        public DateTime UploadedOn { get; }

        //-----------------------------------------
        // Event
        //-----------------------------------------

        public event Action<string>? DocumentProcessed;

        //-----------------------------------------
        // Constructor
        //-----------------------------------------

        protected Document(string fileName, long fileSize)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name is required.");

            if (fileSize <= 0)
                throw new ArgumentException("Invalid file size.");

            DocumentId = Guid.NewGuid();
            FileName = fileName;
            FileSize = fileSize;
            UploadedOn = DateTime.UtcNow;
        }

        //-----------------------------------------
        // Template Method
        //-----------------------------------------

        public void Process()
        {
            Validate();

            CheckPermission();

            ProcessDocument();

            SaveMetadata();

            GenerateAuditLog();

            NotifyUser();

            DocumentProcessed?.Invoke(
                $"{FileName} processed successfully.");
        }

        //-----------------------------------------
        // Hidden Common Logic
        //-----------------------------------------

        private void Validate()
        {
            Console.WriteLine("Validating document...");
        }

        private void CheckPermission()
        {
            Console.WriteLine("Checking user permission...");
        }

        private void SaveMetadata()
        {
            Console.WriteLine("Saving document metadata...");
        }

        private void GenerateAuditLog()
        {
            Console.WriteLine("Generating audit log...");
        }

        private void NotifyUser()
        {
            Console.WriteLine("Sending notification...");
        }

        //-----------------------------------------
        // Abstract Method
        //-----------------------------------------

        protected abstract void ProcessDocument();
    }

    //=========================================================
    // PDF
    //=========================================================

    public class PdfDocument : Document
    {
        public PdfDocument(string fileName, long fileSize)
            : base(fileName, fileSize)
        {
        }

        protected override void ProcessDocument()
        {
            Console.WriteLine("Extracting PDF pages...");
        }
    }

    //=========================================================
    // Word
    //=========================================================

    public class WordDocument : Document
    {
        public WordDocument(string fileName, long fileSize)
            : base(fileName, fileSize)
        {
        }

        protected override void ProcessDocument()
        {
            Console.WriteLine("Reading Word document...");
        }
    }

    //=========================================================
    // Excel
    //=========================================================

    public class ExcelDocument : Document
    {
        public ExcelDocument(string fileName, long fileSize)
            : base(fileName, fileSize)
        {
        }

        protected override void ProcessDocument()
        {
            Console.WriteLine("Importing Excel worksheet...");
        }
    }
}

#region Program explanation
//Why This Is a Product-Company Example

//This resembles systems built in:

//Microsoft SharePoint
//Google Drive
//Dropbox
//Azure Blob Storage document pipelines
//Enterprise Document Management Systems

//Instead of "Animal" or "Shape", it models a realistic business workflow.
 #endregion