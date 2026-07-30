namespace InterviewPrep.LLD.OOPS.Polymorphism
{
    #region Real Product Scenario

    //    Imagine you're building a Cloud Storage system.

    //Users can upload files in different ways.

    //Upload(file)

    //Upload(file, folder)

    //Upload(file, folder, overwrite)

    //Same operation.

    //Different inputs.

    //Perfect use case for overloading.
    #endregion
    public class StorageService
    {
        public void Upload(string fileName)
        {
            Console.WriteLine($"Uploading {fileName}");
        }

        public void Upload(string fileName, string folder)
        {
            Console.WriteLine($"Uploading {fileName} to {folder}");
        }

        public void Upload(string fileName,
                           string folder,
                           bool overwrite)
        {
            Console.WriteLine(
                $"Uploading {fileName} to {folder} (Overwrite: {overwrite})");
        }
    }
}


#region Why Overloading?
//Why Overloading?

//Without it:

//UploadFile()

//UploadFileToFolder()

//UploadFileWithOverwrite()

//Too many method names.

//With overloading:

//Upload(...)

//Cleaner API.
#endregion