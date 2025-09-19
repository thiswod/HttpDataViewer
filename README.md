# QueryStringView

## Project Introduction
QueryStringView is a lightweight, efficient URL query string parsing and visualization tool designed for developers and testers. It can quickly parse URL query strings and display them in a structured table format, supporting copy and modification operations, greatly improving development and debugging efficiency.

## Features

1. **Real-time Parsing** - Automatically parse and display key-value pairs after entering query string
2. **Structured Display** - Show serial number, key, value, key length, value length in table format
3. **Right-click Operations** - Support convenient operations of copying keys and modifying values
4. **Performance Optimization** - Use custom SuperListView class with double buffering to improve display performance with large data
5. **Thread Safety** - Use CancellationToken to avoid duplicate display issues during rapid input

## How to Use

1. Download and run the program
2. Enter or paste the URL query string in the top text box (format: key1=value1&key2=value2)
3. The system will automatically parse and display the results in the table below
4. Right-click on cells in the table to copy keys or modify values

## Installation

1. Ensure your computer has [.NET 8.0 or higher](https://dotnet.microsoft.com/download/dotnet/8.0) installed
2. Download the latest release from GitHub
3. Unzip the files and run QueryStringView.exe

## Tech Stack
- C#
- .NET 8.0
- Windows Forms

## Development

If you want to participate in development, you can follow these steps:
1. Clone the code repository
2. Open the solution with Visual Studio 2022 or higher
3. Develop and debug
4. Submit a Pull Request

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Contributions
Contributions are welcome! Please submit issues and suggestions to help us improve this tool!

## Multi-language Support
This project supports multi-language documentation. You can view the following translations:
- [README_ZH.md](README_ZH.md) - Chinese Simplified (简体中文)