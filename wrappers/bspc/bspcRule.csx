#!/usr/bin/env dotnet-script

#load "../shell.csx"

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
    public bspcRule Size(int w, int h, int x = 0, int y = 0) 
    {
         Flags["rectangle"] = $"{w}x{h}+{x}+{y}";
        return this;
    }
    public bspcRule SizeInverted(int w, int h, int x = 0, int y = 0) 
    {
        Flags["rectangle"] = $"{w}x{h}-{x}-{y}";
        return this;
    }
    public bspcRule SelectBiggestNode() 
    {
        Flags["node"] = "@/";
        return this;
    }

    public bspcRule Desktop(int id) 
    {
        Flags["desktop"] = $"^{id}";
        return this;
    }
    public bspcRule DontFollow() 
    {
        Flags["follow"] = "off";
        return this;
    }
    public string target;

    public bspcRule(string Target)
    { 
        target = Target;
    }

    
    public void Apply()
    {
        var rules = string.Empty;
        foreach(var kvp in Flags)
            rules += $"{kvp.Key}={kvp.Value} ";
        rules = rules.Trim();

        shell.run($"bspc rule -a {target} {rules}"); 
    }    
}