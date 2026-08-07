using InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.Models;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.Interfaces
{
    public interface ICloudStorageService
    {
        UploadResult Upload(FileUploadRequest request);
    }
}

//Why only one method?

//This project focuses on demonstrating the Adapter Pattern.

//In a real system you might have:

//Upload()

//Download()

//Delete()

//Copy()

//Move()

//GenerateSignedUrl()

//Exists()

//But adding all of them would distract from the pattern.