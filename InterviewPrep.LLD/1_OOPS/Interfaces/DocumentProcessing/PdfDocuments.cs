namespace InterviewPrep.LLD.OOPS.Interfaces.DocumentExample
{
    public class PdfDocuments :
    IDocument,
    IPrintables,
    IExportable,
    IAuditable
    {
        public string FileName { get; }

        public PdfDocuments(string fileName)
        {
            FileName = fileName;
        }

        public void Open()
        {
            Console.WriteLine($"Opening PDF : {FileName}");
        }

        public void Save()
        {
            Console.WriteLine($"Saving PDF : {FileName}");
        }

        public void Print()
        {
            Console.WriteLine($"Printing PDF : {FileName}");
        }

        public void Export(string format)
        {
            Console.WriteLine($"Exporting PDF to {format}");
        }

        public void Audit(string action)
        {
            Console.WriteLine($"Audit : {action}");
        }
    }
}
