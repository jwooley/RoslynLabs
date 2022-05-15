using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using VerifyCS = Analyzer1.Test.CSharpCodeFixVerifier<
    Analyzer1.Analyzer1Analyzer,
    Analyzer1.Analyzer1CodeFixProvider>;

namespace Analyzer1.Test
{
    [TestClass]
    public class Analyzer1UnitTest
    {

        [TestMethod]
        public async Task MvcClassWithoutConstructorFixes()
        {
            var test = @"
using System.Web.Mvc;
namespace WebApplicationCS.Controllers
{
    public class HomeControllerTest : Controller
    {
        public ActionResult Index()
        {
            return new ActionResult();
        }
    }
}
namespace System.Web.Mvc
{
    public class Controller { }
    public class ActionResult { }
}
";

            var expected = VerifyCS.Diagnostic("Lab001")
                .WithArguments("HomeControllerTest")
                .WithSpan(5, 18, 5, 36)
                .WithSeverity(Microsoft.CodeAnalysis.DiagnosticSeverity.Error);

            await VerifyCS.VerifyAnalyzerAsync(test, expected);
            var fixtest = @"
using System.Web.Mvc;
namespace WebApplicationCS.Controllers
{
    public class HomeTestController : Controller
    {
        public ActionResult Index()
        {
            return new ActionResult();
        }
    }
}
namespace System.Web.Mvc
{
    public class Controller { }
    public class ActionResult { }
}
";
            await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
        }

        [TestMethod]
        public async Task MvcClassValid()
        {
            var test = @"
using System.Web.Mvc;
namespace WebApplicationCS.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        public ActionResult Index()
        {
            return new ActionResult();
        }
    }
}
namespace System.Web.Mvc
{
    public class Controller { }
    public class ActionResult { }
}
";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }
        
        [TestMethod]
        public async Task MvcClassWithConstructorFixes()
        {
            var test = @"
using System.Web.Mvc;
namespace WebApplicationCS.Controllers
{
    public class HomeControllerTest : Controller
    {
        public HomeControllerTest()
        {
        }
        public ActionResult Index()
        {
            return new ActionResult();
        }
    }
}
namespace System.Web.Mvc
{
    public class Controller { }
    public class ActionResult { }
}
";

            var expected = VerifyCS.Diagnostic("Lab001")
                .WithArguments("HomeControllerTest")
                .WithSpan(5, 18, 5, 36)
                .WithSeverity(Microsoft.CodeAnalysis.DiagnosticSeverity.Error);

            await VerifyCS.VerifyAnalyzerAsync(test, expected);
            var fixtest = @"
using System.Web.Mvc;
namespace WebApplicationCS.Controllers
{
    public class HomeTestController : Controller
    {
        public HomeTestController()
        {
        }
        public ActionResult Index()
        {
            return new ActionResult();
        }
    }
}
namespace System.Web.Mvc
{
    public class Controller { }
    public class ActionResult { }
}
";
            await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
        }
    }
}
