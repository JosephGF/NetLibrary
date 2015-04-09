using System;

public class StartupEventArgs : EventArgs
{
    public string CommandLineArgs { get; internal set; }
    public bool IsFirstInstance { get; internal set; }

    public StartupEventArgs(bool isFirstInstance, string commandLineArgs)
    {
        IsFirstInstance = isFirstInstance;
        CommandLineArgs = commandLineArgs;
    }
}

public class CommandEventArgs : EventArgs
{
    public string CommandName { get; internal set; }

    public CommandEventArgs(string commandName)
    {
        CommandName = commandName;
    }
}