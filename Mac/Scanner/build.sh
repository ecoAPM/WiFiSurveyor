#!/usr/bin/env bash
set -euo pipefail

scanner_dir=$(cd "$(dirname "$0")" && pwd)
scanner_output=$1
scanner_temp=$(mktemp -d)
trap 'rm -rf "$scanner_temp"' EXIT

mkdir -p "$(dirname "$scanner_output")"
for arch in arm64 x86_64; do
    xcrun swiftc -O -target "$arch-apple-macos11.0" "$scanner_dir/main.swift" -o "$scanner_temp/$arch"
done
xcrun lipo -create "$scanner_temp/arm64" "$scanner_temp/x86_64" -output "$scanner_output"
