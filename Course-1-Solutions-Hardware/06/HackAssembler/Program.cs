using HackAssembler;

public class Assembler
{
    // public FileStream file;
    public string filePath = "";
    List<string> assemblyLines;
    List<string> binaryLines;
    public Parser parser;
    public BinaryCoder binaryCoder;
    public SymbolTable symbolTable;

    public Assembler(string path)
    {
        // set filepath from command line argument
        filePath = path;
        Console.WriteLine("filepath = " + filePath);

        // read assembly file to list
        assemblyLines = StripWhiteSpace(filePath);

        binaryLines = new List<string>();

        symbolTable = new SymbolTable();

        parser = new Parser(assemblyLines.Count);

        binaryCoder = new BinaryCoder();
    }

    public List<string> StripWhiteSpace(string filePath)
    {
        // read all lines from file
        List<string> allLines = File.ReadLines(filePath).ToList();

        // list for no white space lines
        List<string> lines = new List<string>();
        int index = -1;

        // iterate through all lines to check and skip white space (empty lines, comments etc.)
        foreach (var rawLine in allLines)
        {
            index++;
            // Console.WriteLine("index = " + index);
            // ignore empty lines 
            if (rawLine.Length > 0)
            {
                // trim whitespace at the start and end of string (so fx indentation)
                string line = rawLine.Trim();

                // if line is a comment - ignore the line and continue to next line
                if (line.StartsWith("//"))
                {
                    //Console.WriteLine("inside //");
                    // Console.WriteLine(line[0]);
                    continue;
                }

                // check for inline comments
                int inlineCommentIndex = line.IndexOf("//");
                if (inlineCommentIndex >= 0)
                {
                    // get the substring up to inline comment and trim for whitespaces
                    line = line.Substring(0, inlineCommentIndex).Trim();
                }

                // check if line is not empty (it could fx be empty if there was indented inline comment - taking substring and trimmed that woudl result in empty string)
                if (!string.IsNullOrEmpty(line))
                {
                    //Console.WriteLine("inside not null");
                    // console.WriteLine(line[0]);
                    lines.Add(line);
                }
            }
        }
        return lines;
    }

    // read assemlby file line by line to get labels and labels to symbol table
    public void FirstPass()
    {
        int index = 0;
        int labelIndex = 0;

        while (parser.HasMoreLines())
        {
            parser.Advance(assemblyLines, index);

            // get instruction type
            InstructionType instructionType = parser.ParseInstructionType();

            if (instructionType == InstructionType.L_INSTRUCTION)
            {
                string labelSymbol = parser.GetSymbol();

                symbolTable.AddSymbol(labelSymbol, index - labelIndex);
                labelIndex++;
            }
            index++;
        }
    }

    // read assemlby file line by line again, focusing on instructions and variables
    public void SecondPass()
    {
        Console.WriteLine("inside second pass");
        string binaryValue = "";
        int index = 0;
        int variableMemoryAddres = 16;
        while (parser.HasMoreLines())
        {
            Console.WriteLine("inside second pass while");
            parser.Advance(assemblyLines, index);

            InstructionType instructionType = parser.ParseInstructionType();

            // if the instruction is a label declaration we skip it since this has been taken care of in the first pass
            if (instructionType == InstructionType.L_INSTRUCTION)
            {
                index++;
                continue;
            }

            if (instructionType == InstructionType.A_INSTRUCTION)
            {
                // get symbol from instruction (e.g. @i, @n etc.)
                string symbol = parser.GetSymbol();
                // if symbol doesnt exist, add it to the symbol table
                if (!symbolTable.ContainsSymbol(symbol))
                {
                    symbolTable.AddSymbol(symbol, variableMemoryAddres);
                    variableMemoryAddres++;
                }

                int symbolAddress = symbolTable.GetAddress(symbol);

                // TODO: Have to account for normal a-instructions without symbols as well
                // TODO: Check if normal a-instruction or symbol 

                // below is for symbolless
                // string decimalValue = parser.aInstruction();

                binaryValue = binaryCoder.BinaryAInstruction(symbolAddress.ToString());
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

        asm.FirstPass();

        asm.SecondPass();

        asm.CreateAndAddToOutputFile();
    }

}
