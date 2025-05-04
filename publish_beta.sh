#!/bin/bash

set -e

# NuGet API key (replace this placeholder with your actual key)
API_KEY="ADD_API_KEY"

# Base version (e.g. for v0.3.0-beta-202505041830)
BASE_VERSION="0.3.5"
VERSION="${BASE_VERSION}-beta-$(date +%Y%m%d%H%M)"

PROJECT_PATH="./src/Synology.Api.Client/Synology.Api.Client.csproj"
OUTPUT_DIR="./artifacts"

echo "📦 Packing version: $VERSION"

# Clean output directory
rm -rf "$OUTPUT_DIR"
mkdir -p "$OUTPUT_DIR"

# Pack the project
dotnet pack "$PROJECT_PATH" \
  -c Debug \
  -o "$OUTPUT_DIR" \
  --include-symbols \
  -p:PackageVersion="$VERSION" \
  -p:SymbolPackageFormat=snupkg

# Push all .nupkg files (includes .snupkg too)
for package in "$OUTPUT_DIR"/*.nupkg; do
  echo "🚀 Pushing $package"
  dotnet nuget push "$package" \
    --api-key "$API_KEY" \
    --source "https://api.nuget.org/v3/index.json" \
    --skip-duplicate
done

echo "✅ Beta package $VERSION published successfully."