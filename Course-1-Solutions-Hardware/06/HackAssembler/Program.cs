using System;
using System.IO;
using System.Reflection.Metadata;
using System.Text;

public class Assembler
{
    // public FileStream file;
    public string filePath = "";
    List<string> lines;
    public Assembler(string path)
    {
        // set filepath from command line argument
        filePath = path;

        // read assembly file to list
        lines = StripWhiteSpace(filePath);
        Console.WriteLine(lines[0]);

        // TODO: Later - construct symbol table with predefined 
    }

    public List<string> StripWhiteSpace(string filePath)
    { 
        // read all lines from file
        List<string> allLines = File.ReadLines(filePath).ToList();

        // list for no white space lines
        List<string> lines = new List<string>();

        // iterate through all lines to check and skip white space (empty lines, comments etc.)
        foreach (var line in allLines)
        {
            // ignore empty lines 
            if (line.Length > 0)
            {
                // ignore lines starting as comment
                if (line[0] == '/')
                {
                    continue;
                }
                else
                {
                    lines.Add(line);
                }
            }
            
        }
        return lines;
    }

    // read assemlby file line by line to get labels and labels to symbol table
    public void FirstPass()
    {
        // TODO: Later - Add (label) declarations
    }

    // read assemlby file line by line again, focusing on instructions and variables
    public void SecondPass()
    {

    }


    static void Main(string[] args)
    {
        Assembler asm = new Assembler(args[0]);
    }

}
