using System.Runtime.InteropServices;
using CliWrap;
using CliWrap.Buffered;

namespace Devantler.TalosctlCLI;

/// <summary>
/// A class to run talosctl CLI commands.
/// </summary>
public static class Talosctl
{
  /// <summary>
  /// The Talosctl CLI command.
  /// </summary>
  static Command Command => GetCommand();

  internal static Command GetCommand(PlatformID? platformID = default, Architecture? architecture = default, string? runtimeIdentifier = default)
  {
    platformID ??= Environment.OSVersion.Platform;
    architecture ??= RuntimeInformation.ProcessArchitecture;
    runtimeIdentifier ??= RuntimeInformation.RuntimeIdentifier;

    string binary = (platformID, architecture, runtimeIdentifier) switch
    {
      (PlatformID.Unix, Architecture.X64, "osx-x64") => "talosctl-osx-x64",
      (PlatformID.Unix, Architecture.Arm64, "osx-arm64") => "talosctl-osx-arm64",
      (PlatformID.Unix, Architecture.X64, "linux-x64") => "talosctl-linux-x64",
      (PlatformID.Unix, Architecture.Arm64, "linux-arm64") => "talosctl-linux-arm64",
      (PlatformID.Win32NT, Architecture.X64, "win-x64") => "talosctl-win-x64.exe",
      (PlatformID.Win32NT, Architecture.Arm64, "win-arm64") => "talosctl-win-x64.exe",
      _ => throw new PlatformNotSupportedException($"Unsupported platform: {Environment.OSVersion.Platform} {RuntimeInformation.ProcessArchitecture}"),
    };
    string binaryPath = Path.Combine(AppContext.BaseDirectory, binary);
    if (!File.Exists(binaryPath))
    {
      throw new FileNotFoundException($"{binaryPath} not found.");
    }
    if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
      File.SetUnixFileMode(binaryPath, UnixFileMode.UserExecute | UnixFileMode.GroupExecute | UnixFileMode.OtherExecute);
    }
    return Cli.Wrap(binaryPath);
  }

  /// <summary>
  /// Runs the talosctl CLI command with the given arguments.
  /// </summary>
  /// <param name="arguments"></param>
  /// <param name="validation"></param>
  /// <param name="input"></param>
  /// <param name="silent"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public static async Task<(int ExitCode, string Message)> RunAsync(
    string[] arguments,
    CommandResultValidation validation = CommandResultValidation.ZeroExitCode,
    bool input = false,
    bool silent = false,
    CancellationToken cancellationToken = default)
  {
    using var stdInConsole = input ? Stream.Null : Console.OpenStandardInput();
    using var stdOutConsole = silent ? Stream.Null : Console.OpenStandardOutput();
    using var stdErrConsole = silent ? Stream.Null : Console.OpenStandardError();
    var command = Command.WithArguments(arguments)
      .WithValidation(validation)
      .WithStandardInputPipe(PipeSource.FromStream(stdInConsole))
      .WithStandardOutputPipe(PipeTarget.ToStream(stdOutConsole))
      .WithStandardErrorPipe(PipeTarget.ToStream(stdErrConsole));
    var result = await command.ExecuteBufferedAsync(cancellationToken);
    return (result.ExitCode, result.StandardOutput.ToString() + result.StandardError.ToString());
  }
}
