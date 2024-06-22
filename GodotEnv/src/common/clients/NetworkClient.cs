namespace Chickensoft.GodotEnv.Common.Clients;

using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CliFx.Exceptions;
using Downloader;
using DownloadProgressChangedEventArgs = Downloader.DownloadProgressChangedEventArgs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public interface INetworkClient {
  IDownloadService DownloadService { get; }
  DownloadConfiguration DownloadConfiguration { get; }

  Task DownloadFileAsync(
    string url,
    string destinationDirectory,
    string filename,
    IProgress<DownloadProgressChangedEventArgs> progress,
    CancellationToken token
  );

  Task DownloadFileAsync(
    string url,
    string destinationDirectory,
    string filename,
    CancellationToken token
  );

  public Task<HttpResponseMessage> WebRequestGetAsync(string url);
  public Task<HttpResponseMessage> WebRequestGetAsyncWithProxy(string url, string? proxyUrl = null);
}

public class NetworkClient : INetworkClient {
  public IDownloadService DownloadService { get; }
  public DownloadConfiguration DownloadConfiguration { get; }

  private static HttpClient? _client;

  public NetworkClient(
    IDownloadService downloadService,
    DownloadConfiguration downloadConfiguration
  ) {
    DownloadService = downloadService;
    DownloadConfiguration = downloadConfiguration;
  }

  public async Task<HttpResponseMessage> WebRequestGetAsync(string url) {
    _client ??= new HttpClient();
    return await _client.GetAsync(url);
  }

  public async Task<HttpResponseMessage> WebRequestGetAsyncWithProxy(string url, string? proxyUrl = null) {
    var proxy = string.IsNullOrEmpty(proxyUrl) ? WebRequest.GetSystemWebProxy() : new WebProxy(proxyUrl);
    var serviceProvider = new ServiceCollection()
      .AddHttpClient(Options.DefaultName)
      .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { Proxy = proxy })
      .Services
      .BuildServiceProvider();

    var factory = serviceProvider.GetService<IHttpClientFactory>();
    var client = factory.CreateClient();

    return await client.GetAsync(url);
  }

  public async Task DownloadFileAsync(
    string url,
    string destinationDirectory,
    string filename,
    IProgress<DownloadProgressChangedEventArgs> progress,
    CancellationToken token
  ) {
    var download = DownloadBuilder
      .New()
      .WithUrl(url)
      .WithDirectory(destinationDirectory)
      .WithFileName(filename)
      .WithConfiguration(DownloadConfiguration)
      .Build();

    token.Register(
      () => {
        if (download.Status == DownloadStatus.Running) {
          download.Stop();
        }
      }
    );

    void internalProgress(
      object? sender, DownloadProgressChangedEventArgs args
    ) => progress.Report(args);

    void done(
      object? sender, System.ComponentModel.AsyncCompletedEventArgs args
    ) {
      if (args.Cancelled) {
        throw new CommandException(
          "🚨 Download cancelled!"
        );
      }

      if (args.Error != null) {
        throw new CommandException(
          $"Download failed. {args.Error.Message}"
        );
      }
    }

    download.DownloadProgressChanged += internalProgress;
    download.DownloadFileCompleted += done;

    await download.StartAsync(token);

    download.DownloadProgressChanged -= internalProgress;
    download.DownloadFileCompleted -= done;
  }

  public async Task DownloadFileAsync(
    string url,
    string destinationDirectory,
    string filename,
    CancellationToken token
  ) {
    var download = DownloadBuilder
      .New()
      .WithUrl(url)
      .WithDirectory(destinationDirectory)
      .WithFileName(filename)
      .WithConfiguration(DownloadConfiguration)
      .Build();

    token.Register(
      () => {
        if (download.Status == DownloadStatus.Running) {
          download.Stop();
        }
      }
    );

    await download.StartAsync(token);
  }
}
