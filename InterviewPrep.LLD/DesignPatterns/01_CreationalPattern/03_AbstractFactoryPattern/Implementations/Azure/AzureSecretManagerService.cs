using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.Azure
{
    public sealed class AzureSecretManagerService : ISecretManagerService
    {
        public CloudProvider Provider => CloudProvider.Azure;

        public void Save(Secret secret)
        {
            Console.WriteLine(
                $"[Azure Key Vault] Secret '{secret.Key}' saved.");
        }

        public Secret Get(string key)
        {
            Console.WriteLine(
                $"[Azure Key Vault] Reading secret '{key}'.");

            return new Secret
            {
                Key = key,
                Value = "AzureSecretValue"
            };
        }
    }
}
