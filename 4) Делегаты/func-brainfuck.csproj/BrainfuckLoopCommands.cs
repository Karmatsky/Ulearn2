using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
        public static Stack<int> Stack = new Stack<int>();
        public static Dictionary<int, int> Bracket = new Dictionary<int, int>();



        public static void RegisterTo(IVirtualMachine vm)
        {
            for (var k = 0; k < vm.Instructions.Length; k++)
            {
                if (vm.Instructions[k] == '[') Stack.Push(k);
                else if (vm.Instructions[k] == ']')
                {
                    Bracket[k] = Stack.Peek();
                    Bracket[Stack.Pop()] = k;
                }
            }



            vm.RegisterCommand('[', machine =>
            {
                if (machine.Memory[machine.MemoryPointer] == 0)
                    machine.InstructionPointer = Bracket[machine.InstructionPointer];
            });



            vm.RegisterCommand(']', machine =>
            {
                if (machine.Memory[machine.MemoryPointer] != 0)
                    machine.InstructionPointer = Bracket[machine.InstructionPointer];
            });
        }
    }

}
