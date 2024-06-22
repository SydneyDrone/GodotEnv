namespace Chickensoft.GodotEnv.Tests.common.clients;

using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Clients;
using Common.Models;
using Downloader;
using Xunit;

public class NetworkClientTest {
  public static IEnumerable<object[]> WebRequestGetTestData => new List<object[]> {
    new object[] {
      "https://jsonplaceholder.typicode.com/todos/1",
      "{\n  \"userId\": 1,\n  \"id\": 1,\n  \"title\": \"delectus aut autem\",\n  \"completed\": false\n}"
    },
    new object[] {
      "https://jsonplaceholder.typicode.com/todos/2",
      "{\n  \"userId\": 1,\n  \"id\": 2,\n  \"title\": \"quis ut nam facilis et officia qui\",\n  \"completed\": false\n}"
    },
    new object[] {
      "https://raw.githubusercontent.com/godotengine/godot-builds/main/releases/godot-4.3-dev6.json",
      "{\n    \"name\": \"4.3-dev6\",\n    \"version\": \"4.3\",\n    \"status\": \"dev6\",\n    \"release_date\": 1714550123,\n    \"git_reference\": \"64520fe6741d8ec3c55e0c9618d3fadcda949f63\",\n\n    \"files\": [\n        {\n            \"filename\": \"godot-4.3-dev6.tar.xz\",\n            \"checksum\": \"517949669a9c5d19a4a7d7702f413125df48f858972d1d6508f531aff518ad80070db0ccfa0184d148fbc203a283f64ea8ab1514e6d9da1f87c3df150949504c\"\n        },\n        {\n            \"filename\": \"godot-4.3-dev6.tar.xz.sha256\",\n            \"checksum\": \"c717b1b589911440930f0b0dd041e7b67b8ff07285a6b0e70b135a86487d18a6f31eb09709f119943692ac213323449a58800231ec07ddd43a7d25fcf2398bd8\"\n        },\n        {\n            \"filename\": \"godot-lib.4.3.dev6.template_release.aar\",\n            \"checksum\": \"9878a7b4541dd80656b64084da22385b537b1b5f22678cb92455962390c2a9b6268637763f1136e4e89d6168e0e2d28a0c44fafb7f93d5895c5151a2b33ebb6a\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_android_editor.aab\",\n            \"checksum\": \"69e73e223dba94c966810ac056ef0d6daf345aae64a700047f41427c65e0691572fd5a1f8fe5844934bbeb4f44720300975032446dca0a654bd12534d111ec34\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_android_editor.apk\",\n            \"checksum\": \"7f4c039f1c2ebbc01bae842091b026e5abad51d33f1303211adfc8318da84fac0fc7de8dfa6666bf482b69a80c0c657d5f6063976a8d0b08e1307bb370c1e62e\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_export_templates.tpz\",\n            \"checksum\": \"d204fd7daea2ebcaf9be3764f0c296c67768fc4a2e98deeecdb50466f19ee7c6f0d2f43bd349b20c55f4792f36944b843102b08c7353df8531c28d45645b2910\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_linux.arm32.zip\",\n            \"checksum\": \"bf6bf42a640cbe12a39bbe86e84ca12ab6084bd61a2a274e6bdd5b0568e7cc8973343d59f4ce61d3e806d9e7805d7ba1c0f52310c1d2df780d5b913d13fe53b8\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_linux.arm64.zip\",\n            \"checksum\": \"ac008f6f14359067db9e3be7cfa3237643fc91bf4d568f5ba7d69295bfe139129ce91f54ef08961bf6a9622f41cadc5d3388d09f3ee3c8a5203ff0406ead0b3b\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_linux.x86_32.zip\",\n            \"checksum\": \"e68d43ba386cac7d311b2158c131c2bb529967db713b545bba8cab2829ac8b1a6bb8bbe4954f1ccabf3d79c230a53e45dfebe64fe0291bf4389e099b789b3a0b\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_linux.x86_64.zip\",\n            \"checksum\": \"fea3449dc0e63a2ee7a41d2f06bad38ee7790ed1996c884dc5e8343422835a1635fe028cd33b4e5ada0ef9234dde1cd9fcec252d82b120c3fb0497de6ede29c9\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_macos.universal.zip\",\n            \"checksum\": \"6cd443c812307f6f14ec5c7376472ff01a6cc74cc1ccb0dcccc91801db49150f412767a290d4cb29279524b8798ca729f9a55242d2abb8e72e5ce96f7428457c\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_web_editor.zip\",\n            \"checksum\": \"479799d7ce3168633ec692fc9d767f611b0102c613ce4c8c2e45ee1b0dc84cccc591d7863ed3bf44b3c11b699b3ac8ee17b15c488a5499bdad1a85a89743b336\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_win32.exe.zip\",\n            \"checksum\": \"3a45cbc84742438eeacdf8d0d09b829b3ca23c38404678d8f77cbcb8673715af9fa008d11e4e53036d88b465bd1b667f39287291445ff387c39de847d5249481\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_win64.exe.zip\",\n            \"checksum\": \"c99ae8f7dbc03639e413e7392d7e0d1bcec15a248a68fe36fffcada1d9d2e5cba9abd6fdeb6a1cf7b513e7efb97a9570c1c24ca0c93e88a69bf976cffa6ef7d1\"\n        },\n        {\n            \"filename\": \"godot-lib.4.3.dev6.mono.template_release.aar\",\n            \"checksum\": \"755a1c6271659d3f46e3b50a44aa92d3930b8e85354bcb50e89decca848f65a350c19bb52602633ec56bc0f6d5f46d0ccd3b4c5c9e8c474e19b248e061b92a66\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_mono_export_templates.tpz\",\n            \"checksum\": \"7fbb57b50686ac7af250fab9f8e47e222afe0cb51f3ff95c299af78e626f3d5fae67c75ca785fa10e8ec07851a94b65f1ea79853b3a3ed9dc6677f5796c0c102\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_mono_linux_arm32.zip\",\n            \"checksum\": \"ad6a9983e093b589f8a45909edb20c7358d7e55aa91235bbc2261f10b08b3530eeb3d971c3c57cdec2421c2d04070afb066cef0d089de07e681db2fc5666ac8c\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_mono_linux_arm64.zip\",\n            \"checksum\": \"1d029216f124035c399de69384aa020022860a8adb4f0520a5cf817b2f53e31acde1ca4da5991dff77b9bd5e2844d1d1d5548848315be9570117cabae7f9c300\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_mono_linux_x86_32.zip\",\n            \"checksum\": \"6ad6acb7e5a04a9b4e6f9e4521bd4090478e9fd87c8f487da0a805094c832ede414438fdfc8d5fe7d02e142252b8eebda27e0661bb5d618352465e552db2f4aa\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_mono_linux_x86_64.zip\",\n            \"checksum\": \"48d7639eb53415f54ab720f3df82c0075a735a30809dca071ccf5c381113e747f2797bcdcfa335246041881a8098581b6135fe27749a7278d0bd4706c22fdb15\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_mono_macos.universal.zip\",\n            \"checksum\": \"261f2c1c303aa904458fd4e6c5c8cec4e65be655271cbac6f8e5d4e09eca37c0e30604089441aeaa9df558d20055569f7794e3a421ed1f33d80ebbc130e005b8\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_mono_win32.zip\",\n            \"checksum\": \"3161886a046a879e87c5b2ee28fbcdef98254b20d5c9e8e55519ab82a56e42cd17cb08ae515ca92ab2cb1233ada09d21f0cca34f97c0e6fd80aeabd18a035e5d\"\n        },\n        {\n            \"filename\": \"Godot_v4.3-dev6_mono_win64.zip\",\n            \"checksum\": \"e0df6076da744f68f5af210622ee051dc43c69302bb217e06bfd95061e11e231035157ad39e375c997bdf03cae7f1ceec30de674bc4860bc26ef1f77ab06ee7a\"\n        }\n    ]\n}\n"
    }
  };

//Test WebRequestGetAsync
  [Theory]
  [MemberData(nameof(WebRequestGetTestData))]
  public async Task TestWebRequestGetAsync(string url, string expectedResponse) {
    var networkClient = new NetworkClient(
      downloadService: new DownloadService(),
      downloadConfiguration: Defaults.DownloadConfiguration
    );

    var response = await networkClient.WebRequestGetAsync(url);
    response.EnsureSuccessStatusCode();

    var jsonResponse = await response.Content.ReadAsStringAsync();
    Assert.Equal(expectedResponse, jsonResponse);
  }

  [Theory]
  [MemberData(nameof(WebRequestGetTestData))]
  public async Task TestWebRequestGetAsyncWithDefaultProxy(string url, string expectedResponse) {
    var networkClient = new NetworkClient(
      downloadService: new DownloadService(),
      downloadConfiguration: Defaults.DownloadConfiguration
    );

    var response = await networkClient.WebRequestGetAsyncWithProxy(url);
    response.EnsureSuccessStatusCode();

    var jsonResponse = await response.Content.ReadAsStringAsync();
    Assert.Equal(expectedResponse, jsonResponse);
  }

  //FIXME:find a better way to inject the proxy url
  [Theory]
  [MemberData(nameof(WebRequestGetTestData))]
  public async Task TestWebRequestGetAsyncWithProxy(string url, string expectedResponse, string proxyUrl = "127.0.0.1:1080") {
    var networkClient = new NetworkClient(
      downloadService: new DownloadService(),
      downloadConfiguration: Defaults.DownloadConfiguration
    );

    var response = await networkClient.WebRequestGetAsyncWithProxy(url,proxyUrl);
    response.EnsureSuccessStatusCode();

    var jsonResponse = await response.Content.ReadAsStringAsync();
    Assert.Equal(expectedResponse, jsonResponse);
  }
}
