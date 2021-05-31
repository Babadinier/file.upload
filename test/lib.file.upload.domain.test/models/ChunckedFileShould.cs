using System;
using lib.file.upload.domain.models;
using Xunit;

namespace lib.file.upload.domain.test
{
    public class ChunckedFileShould
    {
        [Fact]
        public void Throw_Exception_When_Name_Is_Null()
        {
            string name = null;

            Assert.Throws<ArgumentNullException>(() => new ChunckedFile(name, new byte[0]));
        }

        [Fact]
        public void Throw_Exception_When_Name_Is_Empty()
        {
            var name = string.Empty;

            Assert.Throws<ArgumentNullException>(() => new ChunckedFile(name, new byte[0]));
        }

        [Fact]
        public void Throw_Exception_When_Name_Contains_Only_Whitespace()
        {
            var name = "    ";

            Assert.Throws<ArgumentNullException>(() => new ChunckedFile(name, new byte[0]));
        }

        [Fact]
        public void Throw_Exception_When_Content_Is_Null()
        {
            byte[] content = null;

            Assert.Throws<ArgumentNullException>(() => new ChunckedFile("name", content));
        }

        [Fact]
        public void Throw_Exception_When_Content_Has_No_Data()
        {
            var content = new byte[0];

            Assert.Throws<ArgumentNullException>(() => new ChunckedFile("name", content));
        }
    }
}