# CheckSumCompare - Usage Guide

## Overview
CheckSumCompare is a Windows WPF application that allows you to calculate and compare file checksums using various hash algorithms.

## Supported Hash Algorithms
- **MD5** - 128-bit hash (32 hex characters)
- **SHA1** - 160-bit hash (40 hex characters)
- **SHA256** - 256-bit hash (64 hex characters) - Default
- **SHA384** - 384-bit hash (96 hex characters)
- **SHA512** - 512-bit hash (128 hex characters)

## Features

### 1. Calculate File Checksum
- Click "Browse..." to select any file
- Choose your preferred hash algorithm from the dropdown
- Click "Calculate Checksum" to compute the hash
- The checksum will be displayed in hexadecimal format

### 2. Compare Checksums
- After calculating a checksum, you can verify file integrity
- Paste the expected checksum in the "Expected Checksum" field
- Click "Compare" to verify
- Results will show:
  - **Green background with ✓** - Checksums match (file is identical)
  - **Red background with ✗** - Checksums don't match (file is different or corrupted)

### 3. Checksum Format Flexibility
The comparison is format-agnostic and handles:
- Uppercase or lowercase hex characters
- With or without hyphens (e.g., `ABC-DEF` or `ABCDEF`)
- With or without spaces

## Example Workflows

### Verify Downloaded File Integrity
1. Download a file from the internet
2. Copy the checksum provided by the source
3. Open CheckSumCompare
4. Select the downloaded file
5. Choose the same hash algorithm as the source
6. Calculate the checksum
7. Paste the expected checksum and compare

### Generate File Checksum for Distribution
1. Open CheckSumCompare
2. Select your file
3. Choose your preferred algorithm (SHA256 recommended)
4. Calculate the checksum
5. Copy the calculated checksum
6. Share it with your users for verification

## Technical Details

### Performance
- The application uses asynchronous file processing to prevent UI freezing
- Large files are processed efficiently using streaming
- All controls are disabled during calculation to prevent conflicts

### Security
- Uses .NET's built-in cryptographic providers
- All hash calculations are performed using standard .NET libraries
- No external dependencies required

## Building from Source

```bash
# Clone the repository
git clone https://github.com/TipLt/CheckSumCompare.git
cd CheckSumCompare

# Build the project
cd CheckSumCompare
dotnet restore
dotnet build --configuration Release

# Run the application
dotnet run --configuration Release
```

## System Requirements
- Windows 10 or later
- .NET 8.0 Runtime or later
- Minimal disk space (~50 MB)
- No administrator privileges required
