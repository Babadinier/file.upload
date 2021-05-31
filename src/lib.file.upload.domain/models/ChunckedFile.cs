using System;

namespace lib.file.upload.domain.models
{
    public class ChunckedFile
    {
        public string Name { get; private set; }
        public byte[] Content { get; private set; }

        public ChunckedFile(string name, byte[] content)
        {
            SetName(name);
            SetContent(content);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
        }

        public void SetContent(byte[] content)
        {
            if (content == null || content.Length == 0)
            {
                throw new ArgumentNullException(nameof(content));
            }
            Content = content;
        }
    }
}