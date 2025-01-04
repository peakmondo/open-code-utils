using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class CsvBatcher {

    public static async Task RunIt(){
        try
        {
            var csvData = await ReadCsvFromDiagnosticsAsync("data.csv");

            foreach (var row in csvData)
            {
                Console.WriteLine($"Row: {string.Join(", ", row)}");
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static async Task<List<List<string>>> ReadCsvFromDiagnosticsAsync(string fileName)
    {
        // Define the path relative to the project directory
        string diagnosticFolder = Path.Combine(Directory.GetCurrentDirectory(), "DiagnosticFiles");
        
        // Combine the path with the file name
        string fullPath = Path.Combine(diagnosticFolder, fileName);

        // Ensure the file exists
        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException($"The file '{fileName}' was not found in the 'DiagnosticFiles' directory.", fullPath);
        }

        // Read all lines from the file
        var lines = await File.ReadAllLinesAsync(fullPath);

        // Convert each line into a list of column values
        var result = lines
            .Select(line => line.Split(',').ToList())
            .ToList();

        return result;
    }
}


