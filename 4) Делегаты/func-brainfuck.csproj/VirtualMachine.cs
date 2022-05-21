using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		Dictionary<char, Action<IVirtualMachine>> registerCommand = new Dictionary<char, Action<IVirtualMachine>>();

		public VirtualMachine(string program, int memorySize)
		{
			Memory = new byte[memorySize];
			Instructions = program;
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			registerCommand.Add(symbol, execute);
		}

		public void Run()
		{
			for (; InstructionPointer < Instructions.Length; InstructionPointer++)
			{
				var command = Instructions[InstructionPointer];
				if (registerCommand.ContainsKey(command))
				{
					registerCommand[command](this);
				}
			}
		}
	}
}