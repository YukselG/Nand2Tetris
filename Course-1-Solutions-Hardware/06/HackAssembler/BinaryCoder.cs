namespace HackAssembler
{
    public class BinaryCoder
    {
        public BinaryCoder()
        {

        }

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
                { "A", "100" },
                { "AM", "101" },
                { "AD", "110" },
                { "ADM", "111" },
            };

            CompTable = new Dictionary<string, string>
            {
                { "0", "101010" },
                { "1", "111111" },
                { "-1", "111010" },
                { "D", "001100" },
                { "A", "110000" },
                { "M", "110000" },
                { "!D", "001101" },
                { "!A", "110001" },
                { "!M", "110001" },
                { "-D", "001111" },
                { "-A", "110011" },
                { "-M", "110011" },
                { "D+1", "011111" },
                { "A+1", "110111" },
                { "M+1", "110111" },
                { "D-1", "001110" },
                { "A-1", "110010" },
                { "M-1", "110010" },
                { "D+A", "000010" },
                { "D+M", "000010" },
                { "D-A", "010011" },
                { "D-M", "010011" },
                { "A-D", "000111" },
                { "M-D", "000111" },
                { "D&A", "000000" },
                { "D&M", "000000" },
                { "D|A", "010101" },
                { "D|M", "010101" },
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
