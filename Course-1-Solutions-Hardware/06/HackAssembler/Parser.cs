namespace HackAssembler
{
    public class Parser
    {
        public int counter { get; set; }
        public string currentInstruction = "";
        public Parser(int counter)
        {
            this.counter = counter;
        }

        // Check if the assembly file has more lines to parse
        public bool HasMoreLines()
        {
            if (counter == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// If there is more lines, get and set the next instruction
        /// </summary
        public string Advance(List<string> lines, int index)
        {
            currentInstruction = lines[index];
            counter--;

            return currentInstruction;
        }

        public InstructionType ParseInstructionType()
        {
            if (currentInstruction[0] == '(')
            {
                return InstructionType.L_INSTRUCTION;
            }
            else if (currentInstruction[0] == '@')
            {
                return InstructionType.A_INSTRUCTION;
            }
            else return InstructionType.C_INSTRUCTION;
        }

        // Returns the instruction’s dest field
        public string dest()
        {
            if (currentInstruction.Contains('='))
            {
                int indexOfEqualSign = currentInstruction.IndexOf('=');
                string destValue = currentInstruction.Substring(0, indexOfEqualSign);
                return destValue;
            }
            else return string.Empty;
        }

        // Returns the instruction’s comp field
        public string comp()
        {
            int indexOfEqualSign = 0;
            int indexOfJumpSeperator = 0;
            int subStringStart = 0;
            int subStringEnd = 0;

            if (currentInstruction.Contains('='))
            {
                indexOfEqualSign = currentInstruction.IndexOf('=');
            }

            // check for jump instruction
            if (currentInstruction.Contains(';'))
            {
                indexOfJumpSeperator = currentInstruction.IndexOf(';');
            }

            if (indexOfEqualSign != 0)
            {
                subStringStart = indexOfEqualSign + 1;
            }

            if (indexOfJumpSeperator != 0)
            {
                subStringEnd = indexOfJumpSeperator - subStringStart;
            }
            else if (indexOfJumpSeperator == 0)
            {
                subStringEnd = currentInstruction.Substring(subStringStart).Length;
            }

            string compValue = currentInstruction.Substring(subStringStart, subStringEnd);

            return compValue;
        }

        public string jump()
        {
            if (currentInstruction.Contains(';'))
            {
                int indexOfEqualSign = currentInstruction.IndexOf(';');
                string jumpValue = currentInstruction.Substring(indexOfEqualSign + 1);
                return jumpValue;
            }
            else return string.Empty;
        }
    }

    /*// TODO: Later
    public string Symbol()
    {
        return "";
    }*/
}

public enum InstructionType
{
    A_INSTRUCTION,
    C_INSTRUCTION,
    L_INSTRUCTION
}