# BP Category Calculator (Razor Pages)

Simple Razor Pages web app that accepts systolic and diastolic blood pressure values and displays a categorized BP result. This project is implemented as an ASP.NET Core Razor Pages app targeting .NET 9 and C# 13.

## Features
- Enter systolic and diastolic values and get a BP category.
- Server-side validation and validation scripts included.
- Category display styled with simple color classes.
- Unit tests project included (`TestProject1`).
- CI workflow included at `.github/workflows/dotnet.yml`.

## Requirements
- .NET 9 SDK
- Visual Studio 2022 (or later) with ASP.NET and web development workload, or the dotnet CLI.

## Quick start

Clone the repository:

````````
git clone https://github.com/yourusername/BPCalculator.git
````````

Run the app:

````````
cd BPCalculator
dotnet run
````````

Open a browser and navigate to `https://localhost:5001` (or the URL shown in the console).

## Testing

Automated tests are included in the `TestProject1` project. To run tests:

Using the CLI:

````````
dotnet test TestProject1
````````

Or use the Visual Studio __Test Explorer__ to run tests and view results.

## Usage
- Navigate to the home page.
- Enter `Systolic` and `Diastolic` numeric values and Submit.
- The calculated category appears below the form with a colored background. Styling classes are defined in `Pages/Index.cshtml`.

Categories and where they are determined:
- Business logic lives in `BloodPressure.cs`.
- Page UI: `Pages/Index.cshtml`.
- Page model / form handling: `Pages/Index.cshtml.cs`.
- App configuration / routing: `Startup.cs`.

## Project structure (key files)
- `BPCalculator/Pages/Index.cshtml` — form and UI (includes category color CSS).
- `BPCalculator/Pages/Index.cshtml.cs` — page model and request handling.
- `BPCalculator/BloodPressure.cs` — BP domain logic and category computation.
- `BPCalculator/Startup.cs` — app startup configuration.
- `TestProject1/` — unit tests.
- `.github/workflows/dotnet.yml` — CI workflow for build/test.

## Styling
Category CSS classes (in `Pages/Index.cshtml`) map categories to colors:
- `.bp-category.low`
- `.bp-category.ideal`
- `.bp-category.prehigh`
- `.bp-category.high`

Adjust colors or add classes in `Index.cshtml` as needed.

## Contributing
Contributions welcome. Open a pull request and include a short description of the change. Run tests locally before submitting.

## CI / GitHub Actions
A basic GitHub Actions workflow is included at `.github/workflows/dotnet.yml` to build and test the solution on push / PR.

## License
Specify your project license here (e.g., MIT). If you don't want an open source license, add a brief note describing permitted use.

## Notes / Next steps
- Consider adding client-side validation or richer UI feedback.
- If you want to containerize, use `dotnet publish -c Release -o out` then build a Docker image from `out`.
