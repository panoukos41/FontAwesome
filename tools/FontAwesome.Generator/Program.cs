using Flurl.Http;
using P41.FontAwesome.Generator;
using System.IO.Compression;
using System.Text;

if (args is not [var fontAwesomeDir])
{
    throw new ArgumentException("Expected first argument as FontAwesome.csproj root dir path.");
}

var currentDir = Environment.CurrentDirectory;

// Keep zip and set out dirs
var outDir = Path.Combine(fontAwesomeDir, "Icons");
outDir = Path.GetFullPath(outDir);

// Download latest
using var http = new FlurlClient().WithHeader("User-Agent", "request");
var fontAwesomeLatest = await http.Request("https://api.github.com/repos/FortAwesome/Font-Awesome/releases/latest").GetJsonAsync<GitHubReleaseResponse>();
var downloadAsset = fontAwesomeLatest!.Assets.First(x => x.Name.Contains("-web"));
var zipPath = await http.Request(downloadAsset.BrowserDownloadUrl).DownloadFileAsync(currentDir, downloadAsset.Name);

// Extract zip
using var zip = new ZipArchive(File.OpenRead(zipPath));
zip.ExtractToDirectory(currentDir, true);

var zipOutDir = Path.Combine(currentDir, Path.GetFileNameWithoutExtension(downloadAsset.Name));

// Generate files for all svgs in the zip
foreach (var directory in Directory.GetDirectories(Path.Combine(zipOutDir, "svgs")))
{
    var info = new DirectoryInfo(directory);
    var nameSb = new StringBuilder((info.Name));
    nameSb[0] = char.ToUpper(nameSb[0]);
    nameSb.Insert(0, "Fa");

    IconGenerator.Generate(nameSb.ToString(), directory, outDir);
}
