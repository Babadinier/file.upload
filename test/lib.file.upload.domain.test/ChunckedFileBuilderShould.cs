using System.Linq;
using Xunit;

namespace lib.file.upload.domain.test
{
    public class ChunckedFileBuilderShould
    {
        [Fact]
        public void Test1()
        {
            var file = System.IO.File.ReadAllBytes(@"C:\Users\gbadinier\Pictures\Expertime.pdf");

            var chunckedFileBuilder = new ChunckedFileBuilder();

            var chunckedFiles = chunckedFileBuilder.Build(file, "fileName");

            var result = chunckedFiles.OrderByDescending(x => x.Name).ToList();

            System.Console.WriteLine();
        }
    }
}
