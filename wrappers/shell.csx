#!/usr/bin/env dotnet-script

public static class shell
{
    public static string run2(string command, string args = "", bool useShell = false, bool background = false)
    {
        ProcessStartInfo info;
        info = new ProcessStartInfo(command, args);
        info.EnvironmentVariables["DISPLAY"] = ":0";
        info.UseShellExecute = useShell;

        info.RedirectStandardError = !background;
        info.RedirectStandardOutput = !background;

        var process = Process.Start(info);

        if (background)
            return string.Empty;

        var error = process.StandardError.ReadToEnd();
        var output = process.StandardOutput.ReadToEnd();

        return $"StdOut: {Environment.NewLine}{output}{Environment.NewLine}{Environment.NewLine}StdErr: {Environment.NewLine}{error}";
    }
}
