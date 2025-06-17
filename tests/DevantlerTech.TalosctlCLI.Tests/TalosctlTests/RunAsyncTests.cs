using CliWrap;

namespace DevantlerTech.TalosctlCLI.Tests.TalosctlTests;

/// <summary>
/// Tests for the <see cref="Talosctl.RunAsync(string[], CommandResultValidation, bool, bool, CancellationToken)" /> method.
/// </summary>
public class RunAsyncTests
{
  /// <summary>
  /// Tests that the binary can return the version of the flux CLI command.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task RunAsync_Version_ReturnsVersion()
  {
    // Act
    var (exitCode, output) = await Talosctl.RunAsync(["version", "--short", "--client"], CommandResultValidation.ZeroExitCode, false, true);

    // Assert
    Assert.Equal(0, exitCode);
    Assert.Matches(@"^v\d+\.\d+\.\d+$", output.Split('\n')[1].Replace("Talos", "", StringComparison.Ordinal).Trim());
  }
}
