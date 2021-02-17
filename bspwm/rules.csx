#!/usr/bin/env dotnet-script

#load "../io/formats/ini.csx"
#load "../io/logger.csx"
#load "../wrappers/bspc.csx"

using System.IO;


public static bool ApplyRulesForFirst(string WM_CLASS, string WM_NAME, string WM_TYPE)
{
    WM_CLASS = string.Join("_", WM_CLASS.Split(Path.GetInvalidFileNameChars()));
    WM_NAME = string.Join("_", WM_NAME.Split(Path.GetInvalidFileNameChars()));

    var paths = new[]
    {
        Path.Combine(WM_CLASS, $"{WM_NAME}.ini"),
        Path.Combine(WM_CLASS, $"{WM_CLASS}.ini"),
        $"{WM_NAME}.ini",
        $"{WM_CLASS}.ini",
        $"{WM_TYPE}.ini"
    };

    for (int i = 0; i < paths.Length; i++)
        if (LoadFile(paths[i]))
            return true;
    return false;
}

public static bool LoadFile(string filename)
{
    var ruleFile = Path.Combine("/home",Environment.UserName, ".config/bspwm/externalRules/rules/", filename);
    logger.WriteLine("Trying " + ruleFile);

    if (File.Exists(ruleFile))
    {
        logger.WriteLine("Found " + ruleFile);
        var file = new ini(ruleFile);

        foreach (var section in file.contents)
            foreach(var data in section.Value)
                Flags[data.Key] = data.Value;
        
        return true;
    }
    return false;
}

public static void PrintDebugInfo(IList<string> Args, string WM_CLASS, string WM_NAME, string WM_TYPE)
{
    logger.WriteLine();
    logger.WriteLine("###### Debug Info Start");
    
    logger.Write("Args:");
    for (int i = 0; i < Args.Count - 1; i++)
        logger.Write($" {Args[i]}");
    logger.WriteLine();

    logger.WriteLine("WM_TYPE: " + WM_TYPE);
    logger.WriteLine("WM_CLASS: " + WM_CLASS);
    logger.WriteLine("WM_NAME: " + WM_NAME);
    logger.WriteLine();

    logger.Write("Flags:");
    foreach (var flag in Flags.Where(flag=>!string.IsNullOrEmpty(flag.Value)))
        logger.Write($" {flag.Key}={flag.Value}");
    logger.WriteLine();

    logger.WriteLine("###### Debug Info End");
    logger.WriteLine();
}