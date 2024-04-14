using System.Text;

namespace P41.FontAwesome.Generator;

internal static class IconGenerator
{
    public static void Generate(string prefix, string svgsPath, string outDir)
    {
        foreach (var file in Directory.GetFiles(svgsPath))
        {
            var name = Path.GetFileName(file);
            var nameSb = new StringBuilder(name);
            nameSb[0] = char.ToUpper(nameSb[0]);

            if (name.Contains('-'))
            {
                var charIndex = name.IndexOf('-');
                while (charIndex is not -1)
                {
                    var next = charIndex + 1;
                    nameSb[next] = char.ToUpper(nameSb[next]);
                    charIndex = name.IndexOf('-', next);
                }
                nameSb.Replace("-", string.Empty);
            }

            var svg = File.ReadAllText(file);

            CreateRazorClass(nameSb.ToString(), prefix, svg, outDir);
        }
    }

    private static void CreateRazorClass(string name, string prefix, string svg, string outDir)
    {
        var fileName = name.Replace(".svg", ".razor").Insert(0, prefix);
        var builder = new StringBuilder();

        builder.AppendLine("@namespace P41.FontAwesome");
        builder.AppendLine("@inherits FontAwesomeIconBase");
        builder.AppendLine();

        var indexOfClosing = builder.Length + svg.IndexOf('>');
        builder.AppendLine(svg);
        builder.Insert(indexOfClosing, " fill=\"currentColor\" @attributes=\"AdditionalAttributes\"");

        builder.Replace("<!--!", "@((MarkupString)\"<!--!");
        builder.Replace("-->", "-- >\")");


        var content = builder.ToString();

        using var fileStream = File.Create(Path.Combine(outDir, fileName));
        using var writer = new StreamWriter(fileStream);
        writer.Write(content);
    }

    private static string GetIconName(string name)
    {
        var builder = new StringBuilder();

        var isNextCharUpper = true;

        foreach (var c in name)
        {
            if (c == '-')
            {
                isNextCharUpper = true;

                continue;
            }

            builder.Append(isNextCharUpper ? char.ToUpper(c) : c);

            isNextCharUpper = false;
        }

        return builder.ToString();
    }
}
