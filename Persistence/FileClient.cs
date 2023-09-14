using System.Text.Json;

namespace Persistence
{
    public class FileClient : IFileClient
    {
        private readonly string _fileName;

        public FileClient(string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerable<T> ReadFile<T>()
        {
            try
            {
                if (!File.Exists(_fileName))
                {
                    throw new FileNotFoundException($"File not found: {_fileName}");
                }

                var jsonItems = File.ReadAllLines(_fileName);

                return jsonItems.Select(jsonItem => JsonSerializer.Deserialize<T>(jsonItem));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error reading file: {_fileName}", ex);
            }
        }
    }
}