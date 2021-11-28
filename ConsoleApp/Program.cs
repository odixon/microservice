using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace ConsoleApp
{

    class Program
    {
        public static void Main(string[] args)
        {
            CommandLineApplication.Execute<Program>(args);
        }

        [Option(ShortName = "id", LongName = "playlist", Description = "The playlist id")]
        public long PlaylistId { get; }

        [Option(ShortName = "pt", LongName = "platform", Description = "The platform type: KuGouMusic|KuWoMusic|QianQianMusic|QQMusic")]
        public Music.SDK.Models.Enums.PlatformType? PlatformType { get; }

        [Option(ShortName = "n", LongName = "name", Description = "Song name")]
        public string? Name { get; }

        private async Task OnExecuteAsync()
        {
            var platform = this.PlatformType ?? Music.SDK.Models.Enums.PlatformType.KuGouMusic;
            if (!string.IsNullOrWhiteSpace(Name))
            {
                await DownloadMusic(Name, platform);
                return;
            }
            Console.WriteLine($"PlaylistId: {PlaylistId}, Platform: {PlatformType}");
            await DownloadMusic(PlaylistId, platform);
        }

        static async Task<int> DownloadMusic(long playListId, Music.SDK.Models.Enums.PlatformType platformType)
        {
            var musicClient = new Music_SDK.MusicClient();
            var playList = await musicClient.GetPlayList(platformType, playListId);
            var songsCount = playList?.Songs.Count ?? 0;
            Console.WriteLine($"Total songs count: {songsCount}");
            var webClient = new WebClient();
            
            var dir = EnsureDirectory(platformType, playList?.PlayListName ?? playListId.ToString());

            for (var i = 0; i < songsCount; i++)
            {
                var item = playList?.Songs[i];
                if (item == null) continue;
                try
                {
                    var playUrl = await musicClient.GetSongPlayUrl(platformType, item);
                    if (string.IsNullOrEmpty(playUrl)) continue;

                    var path = System.IO.Path.Combine(dir, item.SongName);
                    await webClient.DownloadFileTaskAsync(playUrl, $"{path}.mp3");
                    var lyric = await musicClient.GetLyricById(platformType, item.SongId.ToString(), item.SongAlbumId.ToString());
                    System.IO.File.WriteAllText($"{path}.lrc", lyric.ToString());
                    Console.WriteLine($"{item.SongName}.mp3 downloaded.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred when download the song: {item.SongName}, error: {ex.Message}.");
                }
                finally
                {
                    Console.WriteLine($"Finished: {Math.Round((i + 1M) / songsCount * 100, 2)}%");
                }
            }
            webClient.Dispose();
            return songsCount;
        }

        static async Task DownloadMusic(string name, Music.SDK.Models.Enums.PlatformType platformType)
        {
            var webClient = new WebClient();
            var musicClient = new Music_SDK.MusicClient();
            var songs = await musicClient.SearchSong(platformType, name);
            Console.WriteLine($"Total songs count: {songs.Count}");

            foreach (var item in songs)
            {
                var playUrl = await musicClient.GetSongPlayUrl(platformType, item);
                if (string.IsNullOrEmpty(playUrl)) continue;
                var songName = $"{item.SongName}.{songs.IndexOf(item)}.mp3";

                await webClient.DownloadFileTaskAsync(playUrl, System.IO.Path.Combine(EnsureDirectory(platformType), songName));
                Console.WriteLine($"{songName} downloaded.");
            }
            webClient.Dispose();
            Console.WriteLine("All songs downloaded.");
        }

        static string EnsureDirectory(Music.SDK.Models.Enums.PlatformType platformType, params string[] subDirs)
        {
            var path = $"/Users/apple/Desktop/{platformType}";
            if (subDirs?.Any() ?? false)
            {
                path = $"{path}/{string.Join("/", subDirs)}";
            }

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            return path;
        }

        static void MoqApplicationBuilder()
        {
            var builder = new ApplicationBuilder();
            builder.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware1 Begin");
                await next();
                await context.Response.WriteAsync("Middleware1 End");
            }).Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware2 Begin");
                await next();
                await context.Response.WriteAsync("Middleware2 End");
            }).Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware3 Begin");
                await next();
                await context.Response.WriteAsync("Middleware3 End");
            })
            .Build().Run();
        }
    }
}
