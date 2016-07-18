using Consulgo.Test.ReactiveAutomation.SampleRunner.UiAutomation;
using System;

namespace Consulgo.Test.ReactiveAutomation.SampleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = new FeatureTests();
            f.SmokeTest();
            Console.WriteLine("Waiting for enter");
            Console.ReadLine();
        }
    }
}
