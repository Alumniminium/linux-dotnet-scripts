#!/usr/bin/env dotnet-script

#load "../shell.csx"
public static class bspcConfig
{
    public static int BorderThickness 
    {
        get =>int.Parse(getValue("border_width"));
        set => setValue("border_width",value);
    }
    public static int WindowGap 
    {
        get =>int.Parse(getValue("window_gap"));
        set => setValue("window_gap",value);
    }
    public static int TopPadding 
    {
        get =>int.Parse(getValue("top_padding"));
        set => setValue("top_padding",value);
    }
    public static float SplitRatio 
    {
        get =>int.Parse(getValue("split_ratio"));
        set => setValue("split_ratio",value);
    }


    public static void setValue(string ConfigName, object ConfigValue) 
        => bspcCmd("config", ConfigName, ConfigValue);
    public static string getValue(string ConfigName) 
        => bspcCmd("config", ConfigName).Trim();
    static string bspcCmd(string type, string sub = "", object val = null) 
        => shell.run($"bspc {type} {sub} {val}");
}