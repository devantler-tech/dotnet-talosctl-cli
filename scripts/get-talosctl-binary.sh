#!/bin/bash
set -e

get() {
  local url=$1
  local binary=$2
  local target_dir=$3
  local target_name=$4
  local archiveType=$5

  echo "Downloading $target_name from $url"
  if [ "$archiveType" = "tar" ]; then
    curl -LJ "$url" | tar xvz -C "$target_dir" "$binary"
    mv "$target_dir/$binary" "${target_dir}/$target_name"
  elif [ "$archiveType" = "zip" ]; then
    curl -LJ "$url" -o "$target_dir/$target_name.zip"
    unzip -o "$target_dir/$target_name.zip" -d "$target_dir"
    mv "$target_dir/$binary" "${target_dir}/$target_name"
    rm "$target_dir/$target_name.zip"
  elif [ "$archiveType" = false ]; then
    curl -LJ "$url" -o "$target_dir/$target_name"
  fi
  chmod +x "$target_dir/$target_name"
}

get "https://getbin.io/siderolabs/talos?os=darwin&arch=amd64" "talosctl" "src/Devantler.TalosctlCLI/runtimes/osx-x64/native" "talosctl-osx-x64" false
get "https://getbin.io/siderolabs/talos?os=darwin&arch=arm64" "talosctl" "src/Devantler.TalosctlCLI/runtimes/osx-arm64/native" "talosctl-osx-arm64" false
get "https://getbin.io/siderolabs/talos?os=linux&arch=amd64" "talosctl" "src/Devantler.TalosctlCLI/runtimes/linux-x64/native" "talosctl-linux-x64" false
get "https://getbin.io/siderolabs/talos?os=linux&arch=arm64" "talosctl" "src/Devantler.TalosctlCLI/runtimes/linux-arm64/native" "talosctl-linux-arm64" false
get "https://getbin.io/siderolabs/talos?os=windows&arch=amd64" "talosctl.exe" "src/Devantler.TalosctlCLI/runtimes/win-x64/native" "talosctl-win-x64.exe" false
get "https://getbin.io/siderolabs/talos?os=windows&arch=arm64" "talosctl.exe" "src/Devantler.TalosctlCLI/runtimes/win-arm64/native" "talosctl-win-arm64.exe" false
