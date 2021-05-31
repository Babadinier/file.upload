using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace lib.file.upload.domain.test
{
    public class ChunckedFileBuilderShould
    {
        [Theory]
        [InlineData(512_000)]
        [InlineData(380_000)]
        public void Returns_Only_One_ChunckedFile_When_Content_Is_Less_Or_Equals_Than_500Ko_With_Same_Name(int size)
        {
            var file = new byte[size];

            var chunckedFileBuilder = new ChunckedFileBuilder();

            var name = "fileName.pdf";
            var chunckedFiles = chunckedFileBuilder.Build(file, name);

            Assert.NotEmpty(chunckedFiles);
            Assert.Single(chunckedFiles);
            Assert.Equal(name, chunckedFiles.First().Name);
        }

        [Theory]
        [InlineData(750_000)]
        [InlineData(514_000)]
        public void Returns_Mulitple_ChunckFile_By_500Mb_When_Content_Is_More_Than_500Ko_With_Good_Naming(int size)
        {
            var file = new byte[size];

            var chunckedFileBuilder = new ChunckedFileBuilder();

            var chunckedFiles = chunckedFileBuilder.Build(file, "fileName.pdf");

            Assert.NotEmpty(chunckedFiles);
            Assert.Equal(2, chunckedFiles.Count());
            Assert.True(chunckedFiles.First().Name.Contains("fileName.pdf.1."));
            Assert.True(chunckedFiles.Skip(1).First().Name.Contains("fileName.pdf.2."));
        }

        [Fact]
        public void TestName()
        {
            var file = System.IO.File.ReadAllBytes("{FILE_TO_UPLOAD}");

            var chunckedFileBuilder = new ChunckedFileBuilder();

            var chunckedFiles = chunckedFileBuilder.Build(file, "fileName.pdf").ToList();
            foreach (var item in chunckedFiles)
            {
                File.WriteAllBytes($@"DIRECTORY_SAVE_PARTS\{item.Name}", item.Content);
            }

            var chunckedFile = chunckedFiles.First();
            var _ = chunckedFile.Name.Split('.');

            var files = Directory.GetFiles(@"DIRECTORY_PARTS_SAVED", $"*.{_.Last()}");

            var fileParts = new List<byte[]>();
            foreach (var item in files.OrderBy(x => x))
            {
                fileParts.Add(File.ReadAllBytes(item));
                File.Delete(item);
            }

            var fileContent = fileParts.SelectMany(x => x).ToArray();

            File.WriteAllBytes($@"DIRECTORY_SAVE_FILE\{_.First()}.{_.Skip(1).First()}", fileContent);
        }
    }
}
