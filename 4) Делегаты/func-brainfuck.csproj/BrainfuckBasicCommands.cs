

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', machine => { write((char)machine.Memory[machine.MemoryPointer]); });
            vm.RegisterCommand(',', machine => { machine.Memory[machine.MemoryPointer] = (byte)read(); });
            vm.RegisterCommand('+', machine =>
            {
                if (machine.Memory[machine.MemoryPointer] == 255) machine.Memory[machine.MemoryPointer] = 0;
                else machine.Memory[machine.MemoryPointer]++;
            });
            vm.RegisterCommand('-', machine =>
            {
                if (machine.Memory[machine.MemoryPointer] == 0) machine.Memory[machine.MemoryPointer] = 255;
                else machine.Memory[machine.MemoryPointer]--;
            });
            vm.RegisterCommand('>', machine =>
            {
                if (machine.MemoryPointer == machine.Memory.Length - 1)
                    machine.MemoryPointer = 0;
                else
                    machine.MemoryPointer++;
            });
            vm.RegisterCommand('<', machine =>
            {
                if (machine.MemoryPointer == 0)
                    machine.MemoryPointer = machine.Memory.Length - 1;
                else
                    machine.MemoryPointer--;
            });
            RegisterSymbols(vm);
        }

        public static void RegisterSymbols(IVirtualMachine vm)
        {
            var constantChars =
                "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890".ToCharArray();
            foreach (var с in constantChars)
                vm.RegisterCommand(с, b => { b.Memory[b.MemoryPointer] = Convert.ToByte(с); });
        }
    }
}