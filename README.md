# ③ .NET Talosctl CLI

[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Test](https://github.com/devantler-tech/dotnet-talosctl-cli/actions/workflows/test.yaml/badge.svg)](https://github.com/devantler-tech/dotnet-talosctl-cli/actions/workflows/test.yaml)
[![codecov](https://codecov.io/gh/devantler-tech/dotnet-talosctl-cli/graph/badge.svg?token=RhQPb4fE7z)](https://codecov.io/gh/devantler-tech/dotnet-talosctl-cli)

A simple .NET library that embeds the Talosctl CLI.

## 🚀 Getting Started

## Prerequisites

- .NET 9.0 or later
- [Talosctl CLI](https://www.talos.dev/v1.10/talos-guides/install/talosctl/) installed and available in your system's PATH

## Installation

To get started, you can install the package from NuGet.

```bash
dotnet add package DevantlerTech.TalosctlCLI
```

## 📝 Usage

You can execute the Talosctl CLI commands using the `Talosctl` class.

```csharp
using DevantlerTech.TalosctlCLI;

var (exitCode, output) = await Talosctl.RunAsync(["arg1", "arg2"]);
```
