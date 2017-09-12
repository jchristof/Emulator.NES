﻿using System;
using System.Runtime.CompilerServices;

namespace dotNES.Mappers
{
    [MapperDef(2)]
    class UxROM : BaseMapper
    {
        protected int _bankOffset;

        public UxROM(Emulator emulator) : base(emulator)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override uint ReadByte(uint addr)
        {
            if (addr < 0x8000) return _prgRAM[addr - 0x6000];
            if (addr < 0xC000) return _prgROM[_bankOffset + (addr - 0x8000)];
            return _prgROM[(uint) _prgROM.Length - 0x4000 + (addr - 0xC000)];
        }

        public override void WriteByte(uint addr, uint _val)
        {
            byte val = (byte)_val;

            if (addr < 0x8000)
                _prgRAM[addr - 0x6000] = val;
            else if (addr >= 0x8000)
                _bankOffset = (val & 0xF) * 0x4000;
            else
                throw new NotImplementedException(addr.ToString("X4") + " = " + val.ToString("X2"));
        }
    }
}
