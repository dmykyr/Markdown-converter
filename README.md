# Markdown Converter

## Description
Markdown Converter is a C# project that allows you to convert markdown file into html.


## Installation

1. Clone the repo to your local machine:
```bash
git clone https://github.com/dmykyr/Markdown-converter.git
```

2. Restore the .NET packages:
```bash
dotnet restore
```

3. Build the project:
```bash
dotnet build
```


## Usage
* Run the project with the path to the markdown file as an argument:

```bash
dotnet run <path-to-markdown-file>
```

* If you want to write the output to a file, use --out option followed by the output file path:
```bash
--out <output-file-path>
```
If --out option is provided, default output format is HTML

* To specify output format (ansi or html), use --format option followed by the format:
```bash
--format <format>
```

If neither --format nor --out options are provided, default format is ANSI.

## Running Tests

To run tests, follow these steps:

1. Navigate to the project directory in your terminal.

2. Run the following command to execute the tests:

```bash
dotnet test
```

This will run all test cases and display the results in the terminal.
Here is possible example how result can look like:
```bash
Passed!  - Failed:     0, Passed:    12, Skipped:     0, Total:    12, Duration: 17 ms - MarkdownConverter.dll (net8.0)
```

In such a project, tests might not seem important, because most of the use cases can be checked through debugging
But in larger projects, tests are very useful and helpful. 
I implemented some of the integration and unit tests in [ficeAdvisor](https://github.com/fictadvisor/fictadvisor) as part of our milestone to cover services with tests and it helped a lot later. 
These tests were useful while we were updating and rewriting services methods.
I think keeping tests up-to-date in such projects is important and neccessary. 
They not only minimize the time spent on code inspection and debugging for developer but also helps to avoid human factor during code review.

[Revert commit](https://github.com/dmykyr/Markdown-converter/commit/ec55864fb57ef1c399c7ea07be6c7cd6e11c3302)

[Failed commit](https://github.com/dmykyr/Markdown-converter/commit/351063d3d9f5cd388bed3c941cc750cf663b485e)
