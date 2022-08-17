namespace Chip_8_Blazor.Model
{
    public class ChipEight
    {
        public ushort opcode { get; private set; } // Store opcode, 2 Bytes
        private byte[] memory = new byte[4096];
        private byte[] v = new byte[16];

        private ushort index;
        private ushort pc;

        private byte[] gfx = new byte[64 * 32]; // Screen pixels

        private byte delayTimer;
        private byte soundTimer;

        private byte[] stack = new byte[16];
        private byte stackPointer;

        private byte[] keyboard = new byte[16];

        public ChipEight()
        {
            for(int i = 0; i < memory.Length; i++)
            {
                memory[i] = 0xFF;
            }

            memory[0x0011] = 0xAA;
            memory[0x0011 + 1] = 0x12;

            pc = 0x0011;
        }

        public void Cycle()
        {
            opcode = (ushort)(memory[pc] << 8 | memory[pc + 1]);
        }
    }
}
