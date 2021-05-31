namespace lib.file.upload.domain.models
{
    public class ChunckedFile
    {
        public string Name { get; }
        public byte[] Content { get; }

        public ChunckedFile(string name, byte[] content)
        {
            Name = name;
            Content = content;
        }
    }
}