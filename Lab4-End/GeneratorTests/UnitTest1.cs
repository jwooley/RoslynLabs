using Generators;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using VerifyCS = GeneratorTests.CSharpSourceGeneratorVerifier<Generators.CsvGenerator>;

namespace GeneratorTests
{
    public class UnitTest1
    {
        private const string  generatedAttribute = @"
using System;
namespace CsvSerializer;

[AttributeUsage(AttributeTargets.Class)]
public class CsvSerializableAttribute : Attribute
{
    public CsvSerializableAttribute() {}
}";

        [Fact]
        public async Task TestCsvGeneratorAsync()
        {
            var input = @"
using CsvSerializer;

[CsvSerializable]
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
";

            var generated = @"
using System.Linq;
namespace CsvSerializer;

public static class GeneratedSerializer
{
    #region Person
    public static string ToCsv(this Person input) =>
        $""\""{input.Name}\"", {input.Age}"";

    public static string ToCsvHeader(this Person input) =>
        ""Name, Age"";

    public static string ToCsv(this System.Collections.Generic.IEnumerable<Person> input)
    {
        var sb = new System.Text.StringBuilder();
        if (input.Any())
        {
            sb.AppendLine(input.First().ToCsvHeader());
            foreach (var item in input)
            {
                sb.AppendLine(item.ToCsv());
            }
        }
        return sb.ToString();
    }

    #endregion

}";
            await new VerifyCS.Test
            {
                TestState =
                {
                    Sources = {input},
                    GeneratedSources =
                    {
                        (typeof(CsvGenerator), "CsvSerializableAttribute.g.cs", SourceText.From(generatedAttribute, Encoding.UTF8)),
                        (typeof(CsvGenerator), "CsvSerializer.g.cs", SourceText.From(generated, Encoding.UTF8))
                    }
                }
            }.RunAsync();
        }

        [Fact]
        public async Task CsvGenerator_WhenMultipleClasses_GeneratesInSeparateRegions()
        {
            // The source code to test
            var source = @"
using CsvSerializer;

[CsvSerializable]
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class NotSerialized
{
    public string A {get; set; }
}

[CsvSerializable]
public class Works
{
    public string Col1 {get; set;}
}
";

            var expected = @"
using System.Linq;
namespace CsvSerializer;

public static class GeneratedSerializer
{
    #region Person
    public static string ToCsv(this Person input) =>
        $""\""{input.Name}\"", {input.Age}"";

    public static string ToCsvHeader(this Person input) =>
        ""Name, Age"";

    public static string ToCsv(this System.Collections.Generic.IEnumerable<Person> input)
    {
        var sb = new System.Text.StringBuilder();
        if (input.Any())
        {
            sb.AppendLine(input.First().ToCsvHeader());
            foreach (var item in input)
            {
                sb.AppendLine(item.ToCsv());
            }
        }
        return sb.ToString();
    }

    #endregion

    #region Works
    public static string ToCsv(this Works input) =>
        $""\""{input.Col1}\"""";

    public static string ToCsvHeader(this Works input) =>
        ""Col1"";

    public static string ToCsv(this System.Collections.Generic.IEnumerable<Works> input)
    {
        var sb = new System.Text.StringBuilder();
        if (input.Any())
        {
            sb.AppendLine(input.First().ToCsvHeader());
            foreach (var item in input)
            {
                sb.AppendLine(item.ToCsv());
            }
        }
        return sb.ToString();
    }

    #endregion

}";

            // Pass the source code to our helper and snapshot test the output
            await new VerifyCS.Test
            {
                TestState =
                {
                    Sources = {source},
                    GeneratedSources =
                    {
                        (typeof(CsvGenerator), "CsvSerializableAttribute.g.cs", SourceText.From(generatedAttribute, Encoding.UTF8)),
                        (typeof(CsvGenerator), "CsvSerializer.g.cs", SourceText.From(expected, Encoding.UTF8))
                    }
                }
            }.RunAsync();
        }
    }
}

