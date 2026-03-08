namespace ComputerSimulator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Cpu : IComponent
    {
        private readonly int MEMORY_SIZE = 256;

        public byte IP;
        byte[] Memory;

        public Cpu()
        {
            IP = 0;
            Memory = new byte[MEMORY_SIZE];
        }

        public void Tick()
        {
            Instruction instruction = FetchInstruction();
        }

        byte[] prefixes = new byte[] { 0xF0, 0xF2, 0xF3 }; // Example prefixes

        private Instruction FetchInstruction()
        {
            // Read next byte
            byte instructionByte = Memory[IP++];

            switch(instructionByte)
            {
                case 0x00: // NOP
                    return new Instruction(0);
                case 0x01: // ADD (ax, imm)
                    return new Instruction(1, Memory[(IP ++)].ToString(), Memory[(IP++)].ToString());
                case 0x02: // SUB
                    return new Instruction(2, Memory[(IP++)].ToString(), Memory[(IP++)].ToString());
                case 0x03: // JMP
                    return new Instruction(3, Memory[(IP++)].ToString());
                case 0x04: // MOV (reg, imm)
                    return new Instruction(4, Memory[(IP++)].ToString());
                default:
                    throw new InvalidOperationException($"Unknown instruction: {instructionByte}");
            }
        }
    }
}
