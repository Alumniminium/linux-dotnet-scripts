#!/usr/bin/env dotnet-script

#load "shell.csx"

public static class xprop
{
    public static (string WM_CLASS, string WM_NAME, string WM_WINDOW_TYPE) getWindowInfoById(string windowId)
    {
        var output = shell.run2("xprop", $"-id {windowId} _NET_WM_WINDOW_TYPE WM_NAME WM_CLASS");
        var lines = output.Split(Environment.NewLine);
        var WM_WINDOW_TYPE = ParseType(lines).ToUpperInvariant();
        var WM_NAME = ParseName(lines).ToLowerInvariant();
        var WM_CLASS = ParseClass(lines).ToLowerInvariant();
        WM_CLASS = string.Join("_", WM_CLASS.Split(Path.GetInvalidFileNameChars()));
        WM_NAME = string.Join("_", WM_NAME.Split(Path.GetInvalidFileNameChars()));

        return (WM_CLASS, WM_NAME, WM_WINDOW_TYPE);
    }
    static string ParseName(string[] lines)
    {
        if (lines[1].Contains("="))
            return lines[1].Split('=', StringSplitOptions.RemoveEmptyEntries)[1].Replace("\"", "").Trim();
        return string.Empty;
    }

    static string ParseClass(string[] lines)
    {
        if (lines[2].Contains("="))
        {
            var parts = lines[2].Split('=', StringSplitOptions.RemoveEmptyEntries);
            var wanted = parts[1].Replace("\"", "").Trim();
            var classes = wanted.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var longest = "";
            for (int i = 0; i < classes.Length; i++)
            {
                var c = classes[i];
                if (c.Length > longest.Length)
                    longest = c;
            }
            longest = longest.Replace("\"", "").Trim();
            return longest.ToLowerInvariant();
        }
        return string.Empty;
    }

    static string ParseType(string[] lines)
    {
        string type = string.Empty;
        if (lines[0].Contains("="))
            type = lines[0].Split('=', StringSplitOptions.RemoveEmptyEntries)[1].Replace("\"", "").Trim();
        return type;
    }
}