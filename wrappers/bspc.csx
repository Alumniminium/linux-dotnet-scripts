#!/usr/bin/env dotnet-script

#r "nuget: CliWrap, 3.3.0"

using CliWrap;
using CliWrap.Buffered;

public static class bspc
{
    public static Dictionary<string, string> Flags = new Dictionary<string,string>()
    {
        ["monitor"] ="",
        ["desktop"] ="",
        ["node"] ="",
        ["layer"] ="",
        ["split_dir"] ="",
        ["state"] ="",
        ["split_ratio"] ="0",
        ["hidden"] ="",
        ["sticky"] ="",
        ["private"] ="",
        ["locked"] ="",
        ["marked"] ="",
        ["center"] ="",
        ["follow"] ="",
        ["manage"] ="",
        ["focus"] ="",
        ["border"] ="",
        ["rectangle"] =""
    };
    public static bool Floating
    {
        get => Flags["state"] == "floating";
        set => Flags["state"] = value ? "floating" : "tiled";
    }
    public static bool Tiled
    {
        get => Flags["state"] == "tiled";
        set => Flags["state"] = value ? "tiled" : "floating";
    }
    public static bool Centered
    {
        get => Flags["center"] == "on";
        set => Flags["center"] = value ? "on" : "off";
    }
    public static float SplitRatio
    {
        get => float.Parse(Flags["split_ratio"]);
        set => Flags["split_ratio"] = value.ToString();
    }
    public static string SplitDirection
    {
        get => Flags["split_dir"];
        set => Flags["split_dir"] = value;
    }
    public static void Size(int w, int h, int x = 0, int y = 0) => Flags["rectangle"] = $"{w}x{h}+{x}+{y}";
    public static void SizeInverted(int w, int h, int x = 0, int y = 0) => Flags["rectangle"] = $"{w}x{h}-{x}-{y}";
    public static void SelectBiggestNode() => Flags["node"] = "@/";


    public static Dictionary<string, string> ParseFlags(string line)
    {
        var pairs = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var flags = new Dictionary<string, string>();

        foreach (var pair in pairs)
        {
            var kvp = pair.Split('=', StringSplitOptions.RemoveEmptyEntries);
            if (kvp.Length == 2)
                flags.Add(kvp[0], kvp[1]);
            else
                flags.Add(kvp[0], string.Empty);
        }
        return flags;
    }
    public static void ApplyRulesToStdOut()
    {
        var stdo = string.Empty;

        foreach (var flag in Flags)
            if (string.IsNullOrEmpty(flag.Value))
                continue;
            else
                stdo += $"{flag.Key}={flag.Value} ";

        stdo.Trim();
        Console.WriteLine(stdo);
    }
}
public class bspcRule
{
    public Dictionary<string, string> Flags = new Dictionary<string,string>()
    {
        ["monitor"] ="",
        ["desktop"] ="",
        ["node"] ="",
        ["layer"] ="",
        ["split_dir"] ="",
        ["state"] ="",
        ["split_ratio"] ="0",
        ["hidden"] ="",
        ["sticky"] ="",
        ["private"] ="",
        ["locked"] ="",
        ["marked"] ="",
        ["center"] ="",
        ["follow"] ="",
        ["manage"] ="",
        ["focus"] ="",
        ["border"] ="",
        ["rectangle"] =""
    };
    public bspcRule Floating(bool val=true)
    {
        Flags["state"] = val ? "floating" : "tiled";
        return this;
    }
    public bspcRule Tiled(bool val=true)
    {
        Flags["state"] = val ? "tiled" : "floating";
        return this;
    }
    public bspcRule Centered(bool val=true)
    {
        Flags["center"] = val ? "on" : "off";
        return this;
    }
    public bspcRule SplitRatio(float val)
    {
        Flags["split_ratio"] = val.ToString();
        return this;
    }
    public bspcRule SplitDirection(string val)
    {
        Flags["split_dir"] = val;
        return this;
    }
    public bspcRule Size(int w, int h, int x = 0, int y = 0) {
         Flags["rectangle"] = $"{w}x{h}+{x}+{y}";
        return this;
    }
    public bspcRule SizeInverted(int w, int h, int x = 0, int y = 0) {
        Flags["rectangle"] = $"{w}x{h}-{x}-{y}";
        return this;
    }
    public bspcRule SelectBiggestNode() {
         Flags["node"] = "@/";
        return this;
        }
    public string target;

    public bspcRule(string Target) => new bspcRule(Target); 
    public async void Apply()
    {
        var rules = string.Empty;
        foreach(var kvp in Flags)
            rules += $"{kvp.Key}={kvp.Value} ";
        rules = rules.Trim();

        var cmd = await Cli.Wrap("bspc")
                           .WithArguments($"rule -a {target} {rules}")
                           .WithValidation(CommandResultValidation.None)
                           .ExecuteBufferedAsync(); 
    }
    
}