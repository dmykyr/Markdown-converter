# Markdown Converter

## Description
Markdown Converter is a C# project that allows you to convert markdown file into html.


## Installation

1. Clone the repo to your local machine:
`git clone https://github.com/dmykyr/Markdown-converter.git`

2. Restore the .NET packages:
`dotnet restore`

3. Build the project:
`dotnet build`


## Usage
* Run the project with the path to the markdown file as an argument:

`dotnet run <path-to-markdown-file>`

* If you want to write the output to a file, use the `--out` option followed by the output file path:

`dotnet run <path-to-markdown-file> --out <output-file-path>`