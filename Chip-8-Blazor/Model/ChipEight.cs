namespace Chip_8_Blazor.Model
{
    public class ChipEight
    {
        public ushort CurrentOpcode { get; private set; } // Store opcode, 2 Bytes
        private byte[] memory = new byte[4096];
        private byte[] v = new byte[16];

        private Dictionary<byte, Action<OpCode>> opCodes;
        private Dictionary<byte, Action<OpCode>> logicOpCodes;

        private ushort index;
        private ushort pc;

        public byte[] Gfx { get; } = new byte[64 * 32]; // Screen pixels

        private byte delayTimer;
        private byte soundTimer;

        private ushort[] stack = new ushort[16];
        private byte stackPointer;

        private byte[] keyboard = new byte[16];

        public ChipEight()
        {
            pc = 0x200;

            opCodes = new Dictionary<byte, Action<OpCode>>()
            {
                { 0x0, LeadingZeroCodes },
                { 0x1, JumpToAdress },
                { 0x2, JumpToSubroutine },
                { 0x3, SkipIfRegisterEqual },
                { 0x4, SkipIfRegisterNotEqual },
                { 0x5, SkipIfTwoRegistersEqual },
                { 0x6, PutValueInRegister },
                { 0x7, AddToRegister }
            };

            logicOpCodes = new Dictionary<byte, Action<OpCode>>()
            {

            };
        }

        public void Cycle()
        {
            CurrentOpcode = (ushort)(memory[pc] << 8 | memory[pc + 1]);
        }

        private void HandleOpCode()
        {

        }

        private void LeadingZeroCodes(OpCode opCode)
        {
            if (opCode.Opcode == 0x00E0)
            {
                ClearScreen();
                return;
            }

            if (opCode.Opcode == 0x00EE)
            {
                // todo: Return from subroutine
                return;
            }
        }
        private void ClearScreen()
        {
            for (int i = 0; i < Gfx.Length; i++)
            {
                Gfx[i] = 0x0000;
            }
        }

        private void JumpToAdress(OpCode opCode)
        {
            ushort adress = opCode.NNN;
            pc = adress;
        }

        private void JumpToSubroutine(OpCode opCode)
        {
            stack[stackPointer] = pc;
            stackPointer++;

            pc = opCode.NNN;
        }

        private void SkipIfRegisterEqual(OpCode opCode)
        {
            int regIndex = opCode.X;

            if (v[regIndex] == opCode.KK)
            {
                pc = (ushort)(pc + 2);
            }
        }

        private void SkipIfRegisterNotEqual(OpCode opCode)
        {
            int regIndex = opCode.X;

            if (v[regIndex] != opCode.KK)
            {
                pc = (ushort)(pc + 2);
            }
        }

        private void SkipIfTwoRegistersEqual(OpCode opCode)
        {
            if(opCode.N == 0)
            {
                int regIndexOne = opCode.X;
                int regIndexTwo = opCode.Y;

                if (v[regIndexOne] == v[regIndexTwo])
                {
                    pc = (ushort)(pc + 2);
                }
            }
        }

        private void PutValueInRegister(OpCode opCode)
        {
            int regIndex = opCode.X;
            v[regIndex] = opCode.KK;
        }

        private void AddToRegister(OpCode opCode)
        {
            int regIndex = opCode.X;
            v[regIndex] += opCode.KK;
        }

        private void SetRegisterXToY(OpCode opCode)
        {
            int regIndexOne = opCode.X;
            int regIndexTwo = opCode.Y;

            v[regIndexOne] = v[regIndexTwo];
        }

        private void BitwiseOr(OpCode opCode)
        {
            int regIndexOne = opCode.X;
            int regIndexTwo = opCode.Y;

            v[regIndexOne] = (byte)(v[regIndexOne] | v[regIndexTwo]);
        }

        private void BitwiseAnd(OpCode opCode)
        {
            int regIndexOne = opCode.X;
            int regIndexTwo = opCode.Y;

            v[regIndexOne] = (byte)(v[regIndexOne] & v[regIndexTwo]);
        }

    }
}
