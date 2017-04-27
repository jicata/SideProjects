using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;


[SetUpFixture]
public class SetUpClass
{
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        TextWriter writer = new StringWriter();
        Console.SetOut(writer);
    }
}

