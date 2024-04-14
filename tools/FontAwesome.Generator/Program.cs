using Flurl.Http;
using P41.FontAwesome.Generator;
using System.IO.Compression;
using System.Text;

var currentDir = Environment.CurrentDirectory;
using var http = new FlurlClient().WithHeader("User-Agent", "request");

// Download latest
var fontAwesomeLatest = await http.Request("https://api.github.com/repos/FortAwesome/Font-Awesome/releases/latest").GetJsonAsync<GitHubReleaseResponse>();
var downloadAsset = fontAwesomeLatest!.Assets.First(x => x.Name.Contains("-web"));
var zipPath = await http.Request(downloadAsset.BrowserDownloadUrl).DownloadFileAsync(currentDir, downloadAsset.Name);

// Extract zip
using var zip = new ZipArchive(File.OpenRead(zipPath));
zip.ExtractToDirectory(currentDir, true);

// Keep zip and set out dirs
var zipOutDir = Path.Combine(currentDir, Path.GetFileNameWithoutExtension(downloadAsset.Name));
var outDir = Path.Combine(currentDir, "..", "..", "src", "FontAwesome", "Icons");

// Generate files for all svgs in the zip
foreach (var directory in Directory.GetDirectories(Path.Combine(zipOutDir, "svgs")))
{
    var info = new DirectoryInfo(directory);
    var nameSb = new StringBuilder((info.Name));
    nameSb[0] = char.ToUpper(nameSb[0]);
    nameSb.Insert(0, "Fa");

    IconGenerator.Generate(nameSb.ToString(), directory, outDir);
}
