#!/usr/bin/env dotnet-script

public class ini
{
    public static char[] ignoredPrefixes = new[] { '-', '#', '/' };
    private bool _loaded;
    public string filename;
    public Dictionary<string, Dictionary<string, string>> contents;

    public ini(string FileName,bool preload = false)
    {
        filename = FileName;
        contents = new();
        if(preload)
            load();
    }

    public void load()
    {
        using var reader = new StreamReader(filename);
        var section = string.Empty;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if(string.IsNullOrEmpty(line))
                continue;

            if (ignoredPrefixes.Any(p => line.StartsWith(p)))
                continue;

            if (line.StartsWith("["))
            {
                var name = line.Replace("[", "").Replace("]", "");
                contents.TryAdd(name, new());
                section = name;
                Console.WriteLine("Found new Section: " + section);
                continue;
            }

            if (string.IsNullOrEmpty(section))
                throw new FormatException($"'{filename}' not valid! No header ([header]). At: '{line}'");

            var kvp = line.Split('=');
            if (!contents[section].TryGetValue(kvp[0], out var val))
                contents[section].TryAdd(kvp[0], kvp[1]);
            else
                val = kvp[1];
            Console.WriteLine(line);
        }
        _loaded = true;
    }
    public bool tryGet(string section, string key, out string val)
    {
        if (!_loaded)
            load();
        if (contents.TryGetValue(section, out var data))
            return data.TryGetValue(key, out val);

        val = null;
        return false;
    }
}