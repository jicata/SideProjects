using System;
using System.IO;
using NUnit.Framework;


[SetUpFixture]
public class SetUpClass
{
    [OneTimeSetUp]
    public void RedirectConsoleOutputBeforeEveryTest()
    {
        TextWriter writer = new StringWriter();
        Console.SetOut(writer);
    }
}

