namespace HackAssembler
{
    public class SymbolTable
    {
        private Dictionary<string, int> table;

        public SymbolTable()
        {
            table = new Dictionary<string, int>();
            AddPredefinedSymbols();
        }

        private void AddPredefinedSymbols()
        {
            table.Add("R0", 0);
            table.Add("R1", 1);
            table.Add("R2", 2);
            table.Add("R3", 3);
            table.Add("R4", 4);
            table.Add("R5", 5);
            table.Add("R6", 6);
            table.Add("R7", 7);
            table.Add("R8", 8);
            table.Add("R9", 9);
            table.Add("R10", 10);
            table.Add("R11", 11);
            table.Add("R12", 12);
            table.Add("R13", 13);
            table.Add("R14", 14);
            table.Add("R15", 15);
            table.Add("SCREEN", 16384);
            table.Add("KBD", 24576);
            table.Add("SP", 0);
            table.Add("LCL", 1);
            table.Add("ARG", 2);
            table.Add("THIS", 3);
            table.Add("THAT", 4);
        }

        public void AddSymbol(string symbol, int address)
        {
            table.Add(symbol, address);
        }

        public bool ContainsSymbol(string symbol)
        {
            if (table.ContainsKey(symbol))
            {
                return true;
            }
            else { return false; }
        }

        public int GetAddress(string symbol)
        {
            if (ContainsSymbol(symbol) == true)
            {
                return table[symbol];
            }
            else { return 0; }
        }
    }
}
