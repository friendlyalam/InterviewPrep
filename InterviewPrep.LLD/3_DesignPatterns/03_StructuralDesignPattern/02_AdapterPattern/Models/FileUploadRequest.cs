using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns._03_StructuralDesignPattern._02_AdapterPattern.Models
{
    public sealed class FileUploadRequest
    {
        public string FileName { get; init; } = string.Empty;

        public byte[] Content { get; init; } = Array.Empty<byte>();

        public string ContentType { get; init; } = "application/octet-stream";

        public string FolderName { get; init; } = string.Empty;
    }
}
