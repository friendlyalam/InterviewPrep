using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Enums;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Interfaces;
using InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.LLD.DesignPatterns.CreationalPattern._03_AbstractFactoryPattern.Implementations.Aws
{
    public sealed class AwsSecretManagerService : ISecretManagerService
    {
        public CloudProvider Provider => CloudProvider.Aws;

        public void Save(Secret secret)
        {
            Console.WriteLine(
                $"[AWS Secrets Manager] Secret '{secret.Key}' saved.");
        }

        public Secret Get(string key)
        {
            Console.WriteLine(
                $"[AWS Secrets Manager] Reading secret '{key}'.");

            return new Secret
            {
                Key = key,
                Value = "AwsSecretValue"
            };
        }
    }
}
