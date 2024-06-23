using System;
using CommandPattern.Core.Contracts;

namespace CommandPattern.Core.Commands;

public class ExitCommand : ICommand
{
    private const int DefaultExitCode = 0;
    public string Execute(string[] args)
    {
        Environment.Exit(DefaultExitCode);

        return null;
    }
}