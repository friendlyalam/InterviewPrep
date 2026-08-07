
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Models
{
    public sealed class CloudFile
    {
        public string FileName { get; init; } = string.Empty;

        public byte[] Content { get; init; } = Array.Empty<byte>();
    }
}

//Why sealed?

//This is a DTO (Data Transfer Object).

//It represents data only.

//We don't expect anyone to inherit from it.

//Many enterprise teams mark DTOs as sealed to prevent unnecessary inheritance and to communicate intent clearly.
