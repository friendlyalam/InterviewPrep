using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.GoogleCloud
{
    public sealed class GoogleSecretManagerService : ISecretManagerService
    {
        public CloudProvider Provider => CloudProvider.GoogleCloud;

        public void Save(Secret secret)
        {
            Console.WriteLine(
                $"[Google Secret Manager] Secret '{secret.Key}' saved.");
        }

        public Secret Get(string key)
        {
            Console.WriteLine(
                $"[Google Secret Manager] Reading secret '{key}'.");

            return new Secret
            {
                Key = key,
                Value = "GoogleSecretValue"
            };
        }
    }
}
