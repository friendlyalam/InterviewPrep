namespace InterviewPrep.LLD.OOPS.Interfaces.DocumentExample
{
    //Suppose Excel files cannot be exported in your business requirement.
    public class ExcelDocument :
    IDocument,
    IPrintables
    {
        public string FileName { get; }

        public ExcelDocument(string fileName)
        {
            FileName = fileName;
        }

        public void Open()
        {
            Console.WriteLine($"Opening Excel : {FileName}");
        }

        public void Save()
        {
            Console.WriteLine($"Saving Excel : {FileName}");
        }

        public void Print()
        {
            Console.WriteLine($"Printing Excel : {FileName}");
        }
    }
}
//Notice:

//We didn't force Excel to implement Export().

//This is exactly why we created multiple interfaces.
