using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sdib.AspNetCore.RobotsTxt.Abstractions;

namespace Sdib.AspNetCore.RobotsTxt.Tests
{
    [TestClass]
    public class RobotsTxtMiddlewareShould
    {
        private const string WriterContent = "Hello";
        private Mock<RequestDelegate> next;
        private Mock<IRobotTxtContentWriter> writer;
        private RobotsTxtMiddleware middleware;
        private HttpContext context;

        [TestInitialize]
        public void BeforeEach()
        {
            this.writer = new Mock<IRobotTxtContentWriter>();
            this.context = new DefaultHttpContext
            {
                Request = { Path = new PathString("/robots.txt")}
            };
            this.next = new Mock<RequestDelegate>();
            this.middleware = new RobotsTxtMiddleware(next.Object);
            this.writer.Setup(o => o.WriteAsync())
                .ReturnsAsync(WriterContent);
        }

        [TestMethod]
        public async Task ReturnHttp200_StatusCode()
        {
            await this.middleware.InvokeAsync(context, writer.Object);

            Assert.AreEqual((int)HttpStatusCode.OK, context.Response.StatusCode);
        }

        [TestMethod]
        public async Task ReturnTextPlain()
        {
            await this.middleware.InvokeAsync(context, writer.Object);

            Assert.AreEqual("text/plain", context.Response.ContentType);
        }

        [TestMethod]
        public async Task CallNext_WhenRequestPathIsNotRobotsDotTxt()
        {
            this.context.Request.Path = new PathString("/test");

            await this.middleware.InvokeAsync(context, writer.Object);

            next.Verify(nextMiddleware => nextMiddleware(context), Times.Once);
        }

        [TestMethod]
        public async Task NotCallNext_WhenRequestPathIsRobotsDotTxt()
        {
            await this.middleware.InvokeAsync(context, writer.Object);
            
            next.Verify(nextMiddleware => nextMiddleware(context), Times.Never);
        }

        [TestMethod]
        public async Task ReturnWriterContent()
        {
            this.context.Response.Body = new MemoryStream();

            await this.middleware.InvokeAsync(context, writer.Object);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using (var streamReader = new StreamReader(context.Response.Body))
            {
                Assert.AreEqual(WriterContent, await streamReader.ReadToEndAsync());
            }
        }
    }
}