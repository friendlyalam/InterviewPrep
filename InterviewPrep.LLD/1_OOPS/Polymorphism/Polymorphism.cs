namespace InterviewPrep.LLD.OOPS.Polymorphism
{
    #region Problem Statement

    //Your company is building a cloud storage platform.

    //It stores different types of files.

    //                    CloudFile

    //                        │

    //        ┌───────────────┼────────────────┐

    //        ▼               ▼                ▼

    //     ImageFile VideoFile       DocumentFile

    //Every file supports:

    //Upload
    //Download
    //Preview

    //But each file previews differently.

    //Image

    //Display Image

    //Video

    //Play Video

    //Document

    //Render PDF

    //This is a perfect use case for Runtime Polymorphism.
    #endregion
    //=========================================================
    // Base Class
    //=========================================================

    public abstract class CloudFile
    {
        //------------------------------------------
        // Properties
        //------------------------------------------

        public Guid Id { get; }

        public string FileName { get; }

        public long SizeInKb { get; }

        //------------------------------------------
        // Event
        //------------------------------------------

        public event Action<string>? FileUploaded;

        //------------------------------------------
        // Constructor
        //------------------------------------------

        protected CloudFile(string fileName, long size)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Invalid file name.");

            if (size <= 0)
                throw new ArgumentException("Invalid file size.");

            Id = Guid.NewGuid();

            FileName = fileName;

            SizeInKb = size;
        }

        //------------------------------------------
        // Common Methods
        //------------------------------------------

        public void Upload()
        {
            Console.WriteLine($"{FileName} uploaded.");

            FileUploaded?.Invoke(FileName);
        }

        public void Download()
        {
            Console.WriteLine($"{FileName} downloaded.");
        }

        //------------------------------------------
        // Runtime Polymorphism
        //------------------------------------------

        public abstract void Preview();

        //------------------------------------------
        // Compile-Time Polymorphism
        //------------------------------------------

        public void Share(string email)
        {
            Console.WriteLine($"Shared with {email}");
        }

        public void Share(string email, string permission)
        {
            Console.WriteLine($"Shared with {email} ({permission})");
        }
    }

    //=========================================================
    // Image
    //=========================================================

    public class ImageFiles : CloudFile
    {
        public string Resolution { get; }

        public ImageFiles(
            string name,
            long size,
            string resolution)
            : base(name, size)
        {
            Resolution = resolution;
        }

        public override void Preview()
        {
            Console.WriteLine("Displaying image preview.");
        }

        public void GenerateThumbnail()
        {
            Console.WriteLine("Thumbnail generated.");
        }
    }

    //=========================================================
    // Video
    //=========================================================

    public class VideoFiles : CloudFile
    {
        public int Duration { get; }

        public VideoFiles(
            string name,
            long size,
            int duration)
            : base(name, size)
        {
            Duration = duration;
        }

        public override void Preview()
        {
            Console.WriteLine("Playing video preview.");
        }

        public void Compress()
        {
            Console.WriteLine("Video compressed.");
        }
    }

    //=========================================================
    // Document
    //=========================================================

    public class DocumentFile : CloudFile
    {
        public int PageCount { get; }

        public DocumentFile(
            string name,
            long size,
            int pages)
            : base(name, size)
        {
            PageCount = pages;
        }

        public override void Preview()
        {
            Console.WriteLine("Rendering document preview.");
        }

        public void ExtractText()
        {
            Console.WriteLine("Extracting text...");
        }
    }
}

#region Step-by-Step Execution
//Step 1
//CloudFile[] files

//Here we create an array of CloudFile references.

//Not ImageFile.

//Not VideoFile.

//Why?

//Because we want polymorphism.

//Step 2
//new ImageFile(...)

//Object created.

//Memory

//Heap

//ImageFile Object

//↓

//CloudFile Members

//Id

//FileName

//SizeInKb

//Upload()

//Download()

//↓

//Image Members

//Resolution

//GenerateThumbnail()

//Preview()
//Step 3

//Stored inside

//CloudFile

//reference.

//This is

//Upcasting
//ImageFile

//↓

//CloudFile
//Step 4

//Loop

//foreach(var file in files)

//Current reference type

//CloudFile

//Actual objects

//ImageFile

//VideoFile

//DocumentFile
//Step 5
//file.Preview();

//Question

//Which Preview executes?

//Runtime checks

//Object?

//↓

//ImageFile

//↓

//ImageFile.Preview()

//Next iteration

//Object?

//↓

//VideoFile

//↓

//VideoFile.Preview()

//This is

//Dynamic Dispatch
//Where is Runtime Polymorphism?

//This single line

//file.Preview();

//Produces

//Image Preview

//Video Preview

//Document Preview

//Same method call.

//Different behaviours.

//Where is Compile-Time Polymorphism?
//Share(string email)

//Share(string email,string permission)

//Compiler decides which overload to call based on the arguments.

//Where is Upcasting?
//CloudFile file =
//    new ImageFile(...);

//Reference

//CloudFile

//Object

//ImageFile
//Where is Downcasting?

//Actually we use a better approach:

//if (file is ImageFile image)

//    This performs:

//Type check
//Safe cast
//Variable creation

//all in one statement.

//Why Use Pattern Matching?

//Without it:

//ImageFile image =
//    (ImageFile)file;

//This could throw an InvalidCastException if the object isn't actually an ImageFile.

//Pattern matching avoids unsafe casts.

//| Concept | Used |
//| ------------------------- | ---- |
//| Class | ✅    |
//| Object | ✅    |
//| Encapsulation | ✅    |
//| Inheritance | ✅    |
//| Runtime Polymorphism | ✅    |
//| Compile - Time Polymorphism | ✅    |
//| Method Overriding | ✅    |
//| Method Overloading | ✅    |
//| Upcasting | ✅    |
//| Pattern Matching | ✅    |
//| Events | ✅    |
//| Constructor | ✅    |
//| Exception Handling | ✅    |
//| Properties | ✅    |

//SOLID Principles Used
//SRP

//Each derived class is responsible only for its own file-specific behaviour.

//OCP

//Need support for ZIP files?

//Simply add:

//public class ZipFile : CloudFile
//{
//    public override void Preview()
//    {
//        Console.WriteLine("Previewing ZIP contents.");
//    }
//}

//No existing classes need to change.

//LSP

//This works for every file type:

//CloudFile file = new VideoFile(...);

//or

//CloudFile file = new DocumentFile(...);

//The calling code stays the same.

//DIP

//Higher-level code (Main) depends on the abstraction (CloudFile), not on specific implementations.

//Why Product Companies Like This Design

//Imagine tomorrow your application supports:

//AudioFile

//ZipFile

//PowerPointFile

//CADFile

//ExcelFile

//Would you change the loop?

//foreach(var file in files)
//{
//    file.Preview();
//}

//No.

//You only add a new derived class.

//This is exactly why polymorphism is so valuable in enterprise applications.

//Product Company Interview Questions
//Q1. Why is Preview() abstract?

//Because every file type previews differently, and we want to force each derived class to provide its own implementation.

//Q2. Why is Upload() not abstract?

//Because uploading works the same way for all file types, so we can share one implementation.

//Q3. Why store objects in a CloudFile[]?

//To enable runtime polymorphism and write code that works with any file type.

//Q4. What design pattern does this resemble?

//It demonstrates runtime polymorphism and shares a common workflow through an abstract base class. Unlike the earlier abstraction example, it is not a Template Method pattern because the base class does not define a fixed algorithm that calls overridable steps.

//Q5. What happens when a new file type is added?

//Create a new class that inherits from CloudFile and overrides Preview(). Existing code remains unchanged.

//Interview-Ready Definition

//Polymorphism allows a single reference or method call to work with different object types, enabling the runtime to invoke the correct implementation based on the actual object. This makes software extensible, maintainable, and compliant with object-oriented design principles.SOLID Principles Used
//SRP

//Each derived class is responsible only for its own file-specific behaviour.

//OCP

//Need support for ZIP files?

//Simply add:

//public class ZipFile : CloudFile
//{
//    public override void Preview()
//    {
//        Console.WriteLine("Previewing ZIP contents.");
//    }
//}

//No existing classes need to change.

//LSP

//This works for every file type:

//CloudFile file = new VideoFile(...);

//or

//CloudFile file = new DocumentFile(...);

//The calling code stays the same.

//DIP

//Higher-level code (Main) depends on the abstraction (CloudFile), not on specific implementations.

//Why Product Companies Like This Design

//Imagine tomorrow your application supports:

//AudioFile

//ZipFile

//PowerPointFile

//CADFile

//ExcelFile

//Would you change the loop?

//foreach(var file in files)
//{
//    file.Preview();
//}

//No.

//You only add a new derived class.

//This is exactly why polymorphism is so valuable in enterprise applications.

//Product Company Interview Questions
//Q1. Why is Preview() abstract?

//Because every file type previews differently, and we want to force each derived class to provide its own implementation.

//Q2. Why is Upload() not abstract?

//Because uploading works the same way for all file types, so we can share one implementation.

//Q3. Why store objects in a CloudFile[]?

//To enable runtime polymorphism and write code that works with any file type.

//Q4. What design pattern does this resemble?

//It demonstrates runtime polymorphism and shares a common workflow through an abstract base class. Unlike the earlier abstraction example,
//it is not a Template Method pattern because the base class does not define a fixed algorithm that calls overridable steps.

//Q5. What happens when a new file type is added?

//Create a new class that inherits from CloudFile and overrides Preview(). Existing code remains unchanged.

//Interview-Ready Definition

//Polymorphism allows a single reference or method call to work with different object types, enabling the runtime to invoke the correct
//implementation based on the actual object. This makes software extensible, maintainable, and compliant with object-oriented design principles.

#endregion
