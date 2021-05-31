using System;
using System.Collections.Generic;
using lib.file.upload.domain.models;

namespace lib.file.upload.domain
{
    public class ChunckedFileBuilder
    {
        private const int ChunckedMaxSize = 500 * 1024;

        public IEnumerable<ChunckedFile> Build(byte[] content, string fileName)
        {
            if (content.Length < ChunckedMaxSize)
            {
                yield return new ChunckedFile(fileName, content);
                yield break;
            }

            var treatedSize = 0;
            var indexPart = 1;
            while (treatedSize < content.Length)
            {
                var chunckedSizeContent = content.Length - treatedSize < ChunckedMaxSize ? 
                    content.Length - treatedSize : ChunckedMaxSize;

                byte[] chunckedFileContent = new byte[chunckedSizeContent];

                Buffer.BlockCopy(content, treatedSize, chunckedFileContent, 0, chunckedSizeContent);
                treatedSize += chunckedSizeContent;

                var guidFile = Guid.NewGuid();

                yield return new ChunckedFile($"{fileName}.{indexPart++}.{guidFile}", chunckedFileContent);
            }
        }
    }
}