using System;
using System.Linq;
using System.Reflection;
using CommandPattern.Core.Contracts;

namespace CommandPattern.Core;

public class CommandInterpreter : ICommandInterpreter
{
    public string Read(string args)
    {
        string[] commandArguments = args.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        string commandName = commandArguments[0];

        Type commandType = Assembly
            .GetEntryAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.Name == $"{commandName}Command");

        if (commandType == null)
        {
            throw new InvalidOperationException("Command not found");
        }

        ICommand commandInstance = Activator.CreateInstance(commandType) as ICommand;

        string result = commandInstance.Execute(commandArguments.Skip(1).ToArray());

        return result;
    }
}