namespace HackAssembler
{
    public class BinaryCoder
    {
        public BinaryCoder()
        {

        }


        // a-instruction
        public string BinaryAInstruction(string decimalValue)
        {
            // parse the string decimal value to an int decimal value
            int intVal = int.Parse(decimalValue);
            // convert it to binary, then pad it with leading 0's so that the string contains 15 characters (which will represent the 15-bit value)
            string binary15 = Convert.ToString(intVal, 2).PadLeft(15, '0');
            // prefix with a "0", since this specifies that is an A-instruction - so in total it will be 16-bits
            return "0" + binary15;
        }

        // c-instructions
        public string BinaryDest(string destField)
        {
            return CInstructionTables.DestTable[destField];
        }

        public string BinaryComp(string compField)
        {
            return CInstructionTables.CompTable[compField];

        }

        public string BinaryJump(string jumpField)
        {
            return CInstructionTables.JumpTable[jumpField];
        }
    }

    public static class CInstructionTables
    {
        public static readonly Dictionary<string, string> DestTable;
        public static readonly Dictionary<string, string> CompTable;
        public static readonly Dictionary<string, string> JumpTable;

        // From C# doc: A static constructor is used to initialize any static data, or to perform a particular action that needs to be performed only once
        static CInstructionTables()
        {
            DestTable = new Dictionary<string, string>
            {
                { "null", "000" },
                { "M", "001" },
                { "D", "010" },
                { "DM", "011" },
                { "MD", "011" },
                { "A", "100" },
                { "AM", "101" },
                { "AD", "110" },
                { "ADM", "111" },
                { "AMD", "111" },
            };

            CompTable = new Dictionary<string, string>
            {
                { "0", "0101010" },
                { "1", "0111111" },
                { "-1", "0111010" },
                { "D", "0001100" },
                { "A", "0110000" },
                { "M", "1110000" },
                { "!D", "0001101" },
                { "!A", "0110001" },
                { "!M", "1110001" },
                { "-D", "0001111" },
                { "-A", "0110011" },
                { "-M", "1110011" },
                { "D+1", "0011111" },
                { "A+1", "0110111" },
                { "M+1", "1110111" },
                { "D-1", "0001110" },
                { "A-1", "0110010" },
                { "M-1", "1110010" },
                { "D+A", "0000010" },
                { "D+M", "1000010" },
                { "D-A", "0010011" },
                { "D-M", "1010011" },
                { "A-D", "0000111" },
                { "M-D", "1000111" },
                { "D&A", "0000000" },
                { "D&M", "1000000" },
                { "D|A", "0010101" },
                { "D|M", "1010101" },
            };

            JumpTable = new Dictionary<string, string>
            {
                { "null", "000" },
                { "JGT", "001" },
                { "JEQ", "010" },
                { "JGE", "011" },
                { "JLT", "100" },
                { "JNE", "101" },
                { "JLE", "110" },
                { "JMP", "111" },
            };
        }
    }
}
