#!/usr/bin/env dotnet-script

public class ini
{
    public static char[] ignoredPrefixes = new[] { ' ', '#', '/' };
    private bool _loaded;
    public string filename;
    public Dictionary<string, Dictionary<string, string>> contents;

    public ini(string FileName)
    {
        filename = FileName;
        contents = new();
    }

    public void load()
    {
        using var reader = new StreamReader(filename);
        var section = string.Empty;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (ignoredPrefixes.Any(p => line.StartsWith(p)))
                continue;

            if (line.StartsWith("["))
            {
                var name = line.Replace("[", "").Replace("]", "");
                contents.TryAdd(name, new());
                section = name;
                continue;
            }

            if (string.IsNullOrEmpty(section))
                throw new FormatException($"'{filename}' not valid! No header ([header]). At: '{line}'");

            var kvp = line.Split('=');
            if (!contents[section].TryGetValue(kvp[0], out var val))
                contents[section].TryAdd(kvp[0], kvp[1]);
            else
                val = kvp[1];
        }
        _loaded = true;
    }
    public bool tryget(string section, string key, out string val)
    {
        if (!_loaded)
            load();
        if (contents.TryGetValue(section, out var data))
            return data.TryGetValue(key, out val);

        val = null;
        return false;
    }
}