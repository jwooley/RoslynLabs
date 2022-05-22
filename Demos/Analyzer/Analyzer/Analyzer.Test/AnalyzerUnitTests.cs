using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
//using VerifyCS = Analyzer.Test.CSharpCodeFixVerifier<
//    Analyzer.AnalyzerAnalyzer,
//    Analyzer.AnalyzerCodeFixProvider>;
using Verify = Analyzer.Test.CSharpAnalyzerVerifier<Analyzer.AnalyzerAnalyzer>;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis;

namespace Analyzer.Test
{
    [TestClass]
    public class AnalyzerUnitTest
    {
        //No diagnostics expected to show up
        //[TestMethod]
        //public async Task TestMethod1()
        //{
        //    var test = @"";
        //    await Verify.VerifyAnalyzerAsync(test);
        //}

        //Diagnostic and CodeFix both triggered and checked for
    //    [TestMethod]
    //    public async Task TestMethod2()
    //    {
    //        var test = @"
    //using System;
    //using System.Collections.Generic;
    //using System.Linq;
    //using System.Text;
    //using System.Threading.Tasks;
    //using System.Diagnostics;

    //namespace ConsoleApplication1
    //{
    //    class {|#0:TypeName|}
    //    {   
    //    }
    //}";

    //        var fixtest = @"
    //using System;
    //using System.Collections.Generic;
    //using System.Linq;
    //using System.Text;
    //using System.Threading.Tasks;
    //using System.Diagnostics;

    //namespace ConsoleApplication1
    //{
    //    class TYPENAME
    //    {   
    //    }
    //}";

    //        var expected = VerifyCS.Diagnostic("Analyzer").WithLocation(0).WithArguments("TypeName");
    //        await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
    //    }

        [TestMethod]
        public async Task EmptyFileShouldNotRaiseDiagnostic()
        {
            var test = @"";
            await Verify.VerifyAnalyzerAsync(test);
        }

        [TestMethod]
        public async Task VariableWithOneCharacterShouldRaiseDiagnostic()
        {
            var test = @"
    namespace ConsoleApplication1
    {
        class TypeName
        {  
			void foo()
			{
				var x = 1;
			}
        }
    }";
            var expected = Verify.Diagnostic("Demo001")
                .WithSpan(8,9,8,10)
                .WithMessage("Identifier 'x' is too short");

            await Verify.VerifyAnalyzerAsync(test, expected);
        }


        [TestMethod]
        public async Task ForeachVariableWithOneCharacterShouldRaiseDiagnosticAsync()
        {
            var test = @"
    using System;
    namespace ConsoleApplication1
    {
        class TypeName
        {  
			void foo()
			{
				foreach(var x in new int[0])
				{
					Console.WriteLine(x);
				}
			}
        }
    }";
            var expected = Verify.Diagnostic("Demo001")
                .WithSpan(9, 17, 9, 18)
                .WithMessage("Identifier 'x' is too short");

            await Verify.VerifyAnalyzerAsync(test, expected);
        }

        [TestMethod]
        public async Task VariableWithMoreThanOneCharacterShouldntRaiseDiagnostic()
        {
            var test = @"
    namespace ConsoleApplication1
    {
        class TypeName
        {  
			void foo()
			{
				var x1 = 1;
			}
        }
    }";
        await Verify.VerifyAnalyzerAsync(test);
        }
    }
}