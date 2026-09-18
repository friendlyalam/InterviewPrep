

namespace InterviewPrep.LLD.OOPS.Polymorphism
{
    #region Product Scenario

    //    Suppose a legacy SDK contains:

    //public class ReportGenerator
    //    {
    //        public void Generate()
    //        {
    //            Console.WriteLine("Legacy Report");
    //        }
    //    }

    //    Your new module wants a different implementation but cannot change the SDK.

    #endregion
    public class ReportGenerator
    {
        public void Generate()
        {
            Console.WriteLine("Legacy Report");
        }
    }

    public class PdfReportGenerator : ReportGenerator
    {
        public new void Generate()
        {
            Console.WriteLine("PDF Report");
        }
    }
}


#region Output

//Legacy Report

//PDF Report

//Why?

//The first call uses the base reference, so the hidden base method is selected.

//The second call uses the derived reference, so the hidden derived method is called.

#endregion