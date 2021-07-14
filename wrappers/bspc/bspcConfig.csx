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
    public static int PointerMotionInterval 
    {
        get =>int.Parse(getValue("pointer_motion_interval"));
        set => setValue("pointer_motion_interval",value);
    }
    public static float SplitRatio 
    {
        get =>int.Parse(getValue("split_ratio"));
        set => setValue("split_ratio",value);
    }
    public static bool SingleMonocle 
    {
        get =>bool.Parse(getValue("single_monocle"));
        set => setValue("single_monocle",value.ToString().ToLowerInvariant());
    }
    public static bool BorderlessMonocle 
    {
        get =>bool.Parse(getValue("borderless_monocle"));
        set => setValue("borderless_monocle",value.ToString().ToLowerInvariant());
    }
    public static bool HistoryAwareFocus 
    {
        get =>bool.Parse(getValue("history_aware_focus"));
        set => setValue("history_aware_focus",value.ToString().ToLowerInvariant());
    }
    public static bool FocusByDistance 
    {
        get =>bool.Parse(getValue("focus_by_distance"));
        set => setValue("focus_by_distance",value.ToString().ToLowerInvariant());
    }
    public static bool FocusFollowsPointer 
    {
        get =>bool.Parse(getValue("focus_follows_pointer"));
        set => setValue("focus_follows_pointer",value.ToString().ToLowerInvariant());
    }
    public static bool RemoveDisabledMonitors
    {
        get => bool.Parse(getValue("remove_disabled_monitors"));
        set => setValue("remove_disabled_monitors", value.ToString().ToLowerInvariant());
    }
    public static bool RemoveUnpluggedMonitors
    {
        get => bool.Parse(getValue("remove_unplugged_monitors"));
        set => setValue("remove_unplugged_monitors", value.ToString().ToLowerInvariant());
    }
    public static bool MergeOverlappingMonitors 
    {
        get =>bool.Parse(getValue("merge_overlapping_monitors"));
        set => setValue("merge_overlapping_monitors",value.ToString().ToLowerInvariant());
    }
    public static string NormalBorderColor 
    {
        get => getValue("normal_border_color");
        set => setValue("normal_border_color",value);
    }
    public static string ActiveBorderColor 
    {
        get => getValue("active_border_color");
        set => setValue("active_border_color",value);
    }
    public static string FocusedBorderColor 
    {
        get => getValue("focused_border_color");
        set => setValue("focused_border_color",value);
    }
    public static string PreselFeedbackColor 
    {
        get => getValue("presel_feedback_color");
        set => setValue("presel_feedback_color",value);
    }
    public static string UrgentBorderColor 
    {
        get => getValue("urgent_border_color");
        set => setValue("urgent_border_color",value);
    }

    public static void setValue(string ConfigName, object ConfigValue) 
        => bspcCmd("config", ConfigName, ConfigValue);
    public static string getValue(string ConfigName) 
        => bspcCmd("config", ConfigName).Trim();
    public static string bspcCmd(string type, string sub = "", object val = null) 
    {
        WriteLine($"executing: bspc {type} {sub} {val}");
        return shell.run2("bspc", $"{type} {sub} {val}");
    }
}
