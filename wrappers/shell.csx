#!/usr/bin/env dotnet-script
#r "nuget: CliWrap, 3.3.0"

using CliWrap;
using CliWrap.Buffered;

public static class shell
{
    public static string run(string command)
    {
        var parts = command.Split(' ',StringSplitOptions.RemoveEmptyEntries);
        var cmd = Cli.Wrap(parts[0])
                     .WithArguments(parts[1..])
                     .WithValidation(CommandResultValidation.None)
                     .ExecuteBufferedAsync()
                     .GetAwaiter()
                     .GetResult();
        if(string.IsNullOrEmpty(cmd.StandardOutput))
            return cmd.StandardError;

        return cmd.StandardOutput;
    }
    public static async Task<string> runAsync(string command)
    {
        var parts = command.Split(' ',StringSplitOptions.RemoveEmptyEntries);
        var cmd = await Cli.Wrap(parts[0])
                     .WithArguments(parts[1..])
                     .WithValidation(CommandResultValidation.None)
                     .ExecuteBufferedAsync();
        if(string.IsNullOrEmpty(cmd.StandardOutput))
            return cmd.StandardError;

        return cmd.StandardOutput;
    }
}
