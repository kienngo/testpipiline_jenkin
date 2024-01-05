// 起動プログラム

using Wiseman.PJC.Gen2.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// 業務フレームワーク構成追加
builder.AddFrameworkConfig();

var app = builder.Build();

// デバッグ実行時パスを追加
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    // デバッグ実行時サービス名/バージョン/コントローラ名でアクセスできるように設定します。
    app.UsePathBase("/GroupSettings/v1.0");
    app.UseRouting();
}

// 業務フレームワーク構成追加
app.UseFrameworkConfig();
app.UseHttpsRedirection();
app.Run();