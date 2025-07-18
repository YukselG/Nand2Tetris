using HackAssembler;

public class Assembler
{
    // public FileStream file;
    public string filePath = "";
    List<string> assemblyLines;
    List<string> binaryLines;
    public Parser parser;
    public BinaryCoder binaryCoder;

    public Assembler(string path)
    {
        // set filepath from command line argument
        filePath = path;
        Console.WriteLine("filepath = " + filePath);

        // read assembly file to list
        assemblyLines = StripWhiteSpace(filePath);
        Console.WriteLine(assemblyLines[0]);

        binaryLines = new List<string>();

        parser = new Parser(assemblyLines.Count);

        binaryCoder = new BinaryCoder();

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
        string binaryValue = "";
        int index = 0;
        while (parser.HasMoreLines())
        {
            parser.Advance(assemblyLines, index);

            InstructionType instructionType = parser.ParseInstructionType();

            if (instructionType == InstructionType.A_INSTRUCTION)
            {
                string decimalValue = parser.aInstruction();

                binaryValue = binaryCoder.BinaryAInstruction(decimalValue);
            }
            else if (instructionType == InstructionType.C_INSTRUCTION)
            {
                string destDecValue = parser.dest();
                string compDecValue = parser.comp();
                string jumpDecValue = parser.jump();

                string destBinaryValue = binaryCoder.BinaryDest(destDecValue);
                string compBinaryValue = binaryCoder.BinaryComp(compDecValue);
                string jumpBinarValue = binaryCoder.BinaryJump(jumpDecValue);

                binaryValue = "111" + compBinaryValue + destBinaryValue + jumpBinarValue;
            }

            AddToBinaryLines(binaryValue);

            index++;
        }
    }

    public void AddToBinaryLines(string binaryValues)
    {
        binaryLines.Add(binaryValues);
    }

    public void CreateAndAddToOutputFile()
    {
        // dynamically name the output file using the input file name, with new file extension of ".hack" 
        string outputDirectory = @"C:\Users\Guray\Desktop\coding\Fra Git\Nand2Tetris\Course-1-Solutions-Hardware\06\Output-Files";

        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);

        string outputPath = Path.Combine(outputDirectory, fileNameWithoutExtension + ".hack");

        Console.WriteLine(outputPath);

        // Concatenate all elements in the collection, insert the "\n" separator between each element
        // use \n for new line so its unix line ending and not getting carriage return symbols (using Environment.NewLine)
        var finalOutput = string.Join("\n", binaryLines);
        File.WriteAllText(outputPath, finalOutput);

        return;
    }


    static void Main(string[] args)
    {
        Assembler asm = new Assembler(args[0]);

        asm.SecondPass();

        asm.CreateAndAddToOutputFile();
    }

}
