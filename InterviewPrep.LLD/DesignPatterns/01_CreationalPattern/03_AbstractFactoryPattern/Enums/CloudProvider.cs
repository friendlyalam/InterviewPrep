
namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums
{
    public enum CloudProvider
    {
        Azure,
        Aws,
        GoogleCloud
    }
}

//Why Enum Instead of String?

//❌ Avoid:

//provider = "Azure"

//Someone could accidentally write:

//azure
//AZURE
//Azur

//Enums provide compile-time safety and make the code easier to refactor.