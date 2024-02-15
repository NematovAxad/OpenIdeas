using Microsoft.AspNetCore.Http;

namespace GeneralApplication.Extensions;

public static class FileSaver
{
    public static string? AddFile(IFormFile newFile, string folder)
    {
        var path = Directory.GetCurrentDirectory();
        path = Path.Combine(path, folder);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        
        byte[] bytes = null;
        using (var binaryReader = new BinaryReader(newFile.OpenReadStream()))
        {
            bytes = binaryReader.ReadBytes((int)newFile.Length);
        }

        var fileTip = newFile.FileName.Split('.').Last();

        var name = Guid.NewGuid().ToString();
        name = name + '.' + fileTip;
        path = Path.Combine(path, name);
        
        if (bytes.Length == 0)
        {
            return null;
        }
        Console.WriteLine(path);
        File.WriteAllBytes(path, bytes);
        return name;
    }
}