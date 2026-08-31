namespace InterviewPrep.LLD.OOPS.Inheritence
{

    #region Inheritance approach
    //    Product Company Example - Cloud Storage System

    //Imagine your company is building something similar to:

    //Google Drive
    //OneDrive
    //Dropbox
    //Azure Blob Storage

    //The application stores different types of files.

    //                    File

    //        ┌──────────┼───────────┐

    //        ▼          ▼           ▼

    // ImageFile VideoFile    PdfFile

    //Every file has common information.

    //FileName

    //Size

    //CreatedOn

    //Upload()

    //Download()

    //But each file type behaves differently.

    //Image → Generate Thumbnail

    //Video → Compress Video

    //PDF → Extract Text

    //This is a true "IS-A" relationship.

    //An ImageFile IS A File.

    //A VideoFile IS A File.

    //A PdfFile IS A File.

    //This makes inheritance appropriate.
    #endregion
    //=========================================================
    // Base Class
    //=========================================================

    public class File
    {
        public string FileName { get; }

        public long SizeInKb { get; }

        public DateTime CreatedOn { get; }

        public File(string fileName, long sizeInKb)
        {
            FileName = fileName;
            SizeInKb = sizeInKb;
            CreatedOn = DateTime.Now;
        }

        public void Upload()
        {
            Console.WriteLine($"{FileName} uploaded.");
        }

        public void Download()
        {
            Console.WriteLine($"{FileName} downloaded.");
        }

        public virtual void Preview()
        {
            Console.WriteLine("Generic file preview.");
        }
    }

    //=========================================================
    // Image File
    //=========================================================

    public class ImageFile : File
    {
        public string Resolution { get; }

        public ImageFile(string fileName,
                         long size,
                         string resolution)
            : base(fileName, size)
        {
            Resolution = resolution;
        }

        public void GenerateThumbnail()
        {
            Console.WriteLine("Thumbnail generated.");
        }

        public override void Preview()
        {
            Console.WriteLine("Displaying image preview.");
        }
    }

    //=========================================================
    // Video File
    //=========================================================

    public class VideoFile : File
    {
        public int Duration { get; }

        public VideoFile(string fileName,
                         long size,
                         int duration)
            : base(fileName, size)
        {
            Duration = duration;
        }

        public void CompressVideo()
        {
            Console.WriteLine("Video compressed.");
        }

        public override void Preview()
        {
            Console.WriteLine("Playing video preview.");
        }
    }
}

#region Program explanation

//Why is Inheritance used here?

//Without inheritance, every class would need to repeat the same code:

//ImageFile

//FileName

//Size

//CreatedOn

//Upload()

//Download()

//-------------------------

//VideoFile

//FileName

//Size

//CreatedOn

//Upload()

//Download()

//-------------------------

//PdfFile

//FileName

//Size

//CreatedOn

//Upload()

//Download()

//This duplicates code.

//With inheritance:

//                     File
//        ------------------------------ -
//        FileName
//        SizeInKb
//        CreatedOn
//        Upload()
//        Download()
//        Preview()
//        -------------------------------
//             ▲          ▲          ▲
//             │          │          │
//        ImageFile   VideoFile   PdfFile

//The common functionality is written once in the base class.

//Each derived class only adds what makes it unique.

//Why not Composition?

//This is a favourite product-company interview question.

//Suppose someone says:

//ImageFile HAS A File

//That doesn't make sense.

//An image file is a file.

//Similarly:

//Car IS A Vehicle ✅
//Dog IS AN Animal ✅
//PdfFile IS A File ✅

//But:

//Car HAS AN Engine ✅ (Composition)
//Order HAS A PaymentService ✅ (Composition)
//Hospital HAS Doctors ✅ (Aggregation/Association depending on ownership)

//The relationship determines the design.

//What does the derived class inherit?

//ImageFile automatically gets:

//FileName

//SizeInKb

//CreatedOn

//Upload()

//Download()

//Preview()   (can override)

//It only adds:

//Resolution

//GenerateThumbnail()
//Memory Layout

//When you create:

//ImageFile image = new ImageFile("Holiday.jpg", 2048, "1920x1080");

//The object in memory contains both base-class and derived -class members :

//ImageFile Object
//----------------------------------

//FileName

//SizeInKb

//CreatedOn

//Upload()

//Download()

//Preview()

//----------------------------------

//Resolution

//GenerateThumbnail()

//There is one object, not separate File and ImageFile objects.

//Why would a Product Company choose this design?

//Because it provides:

//Code reuse – common file operations are implemented once.
//Extensibility – adding AudioFile or ZipFile requires minimal code.
//Runtime polymorphism – each file type can provide its own Preview() implementation.
//Maintainability – changes to common behaviour happen in one place.
#endregion
