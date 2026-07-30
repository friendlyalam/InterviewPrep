namespace InterviewPrep.LLD.OOPS.Interfaces.DocumentExample
{ 
    public interface IDocument
    {
        void Open();
        void Save();
    }

    public interface IPrintables
    {
        void Print();
    }

    public interface IExportable
    {
        void Export(string format);
    }

    public interface IAuditable
    {
        void Audit(string action);
    }
}
//Notice how each interface has one responsibility.

//This follows the Interface Segregation Principle (ISP).