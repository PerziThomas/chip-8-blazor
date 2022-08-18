namespace Chip_8_Blazor.Model
{
    public class OpCode
    {
        public ushort Opcode { get; }

        public ushort NNN { get; }
        public byte X { get; }
        public byte Y { get; }
        public byte KK { get; }
        public ushort N { get; }

        public OpCode(ushort opcode)
        {
            Opcode = opcode;

            NNN = (ushort)(opcode & 0x0FFF);
            X = (byte)((opcode & 0x0F00) >> 8);
            Y = (byte)((opcode & 0x00F0) >> 4);
            KK = (byte)(opcode & 0x00FF);
            N = (byte)(opcode & 0x000F);

        }
    }
}
