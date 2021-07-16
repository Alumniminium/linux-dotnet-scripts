#!/usr/bin/env dotnet-script

#load "shell.csx"

using System.Xml.Serialization;
using Microsoft.VisualBasic;

public static class xtrandr
{
    public static (int w, int h) GetResolution(string monitor)
    {
        var output = shell.run2("xrandr", "");
        var monitorIdx = output.IndexOf(monitor);
        var ines = output.Substring(monitorIdx).Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var line = ines.First(l=> l.Contains("*"));
        line = line.Trim().Split(' ')[0];
        var xyString = line.Split('x');

        return (int.Parse(xyString[0]),int.Parse(xyString[1]));
    }
}