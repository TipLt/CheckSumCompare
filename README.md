# CheckSumCompare

A WPF application for calculating and comparing file checksums.

## Features

- Calculate checksums for any file
- Support for multiple hash algorithms:
  - MD5
  - SHA1
  - SHA256
  - SHA384
  - SHA512
- Compare calculated checksums with expected values
- Visual feedback for match/mismatch results
- User-friendly interface

## Requirements

- .NET 8.0 or later
- Windows OS

## Building

```bash
cd CheckSumCompare
dotnet restore
dotnet build
```

## Running

```bash
cd CheckSumCompare
dotnet run
```

## Usage

1. Click "Browse..." to select a file
2. Select the hash algorithm from the dropdown (default: SHA256)
3. Click "Calculate Checksum" to compute the file's hash
4. (Optional) Enter the expected checksum and click "Compare" to verify file integrity