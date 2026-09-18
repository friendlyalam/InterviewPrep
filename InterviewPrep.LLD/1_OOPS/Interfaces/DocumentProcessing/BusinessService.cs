using System.Buffers.Text;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;

namespace InterviewPrep.LLD.OOPS.Interfaces.DocumentExample
{
    public class DocumentManager
    {
        private readonly IDocument _document;
        public DocumentManager(IDocument document)
        {
            _document = document;
        }

        public void Process()
        {
            _document.Open();

            _document.Save();
        }
    }
}

//Notice:

//The manager doesn't know:

//PDF
//Word
//Excel

//It only knows:

//IDocument

//This is Dependency Inversion Principle (DIP).

