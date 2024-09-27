using System.IO;

namespace ChatAdminWPF.Infrastructure.Repositories
{
    public static class KeywordRepository
    {
        // Method to load keywords from a text file
        public static List<string> LoadKeywordsFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                // Read all lines from the file and return them as a list
                return new List<string>(File.ReadAllLines(filePath));
            }

            // Return an empty list if the file does not exist
            return new List<string>();
        }
    }
}
