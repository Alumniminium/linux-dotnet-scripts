#!/usr/bin/env dotnet-script

public class logger
{
    static StreamWriter _writer = new StreamWriter("/tmp/extenal_rules.log", true) { AutoFlush = true };

    public static void Write(string txt) => _writer.Write(txt);
    public static void WriteLine(string line) => _writer.WriteLine(line);
    public static void WriteLine() => WriteLine(string.Empty);

}