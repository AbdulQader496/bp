# BP Category Calculator (Razor Pages)

Simple Razor Pages web app that accepts systolic and diastolic blood pressure values and displays a categorized BP result. This project is implemented as an ASP.NET Core Razor Pages app targeting .NET 9 and C# 13.

## Features
- Enter systolic and diastolic values and get a BP category.
- Server-side validation and validation scripts included.
- Category display styled with simple color classes.
- Unit tests project included (`TestProject1`).
- Multiple GitHub Actions workflows for build, quality checks, security scans, and deployment.

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

Or use the Visual Studio Test Explorer to run tests and view results.

## Usage
- Navigate to the home page.
- Enter `Systolic` and `Diastolic` numeric values and Submit.
- The calculated category appears below the form with a colored background. Styling classes are defined in `Pages/Index.cshtml`.

Categories and where they are determined:
- Business logic lives in `BloodPressure.cs`.
- Page UI: `Pages/Index.cshtml`.
- Page model / form handling: `Pages/Index.cshtml.cs`.
- App configuration / routing: `Startup.cs`.

## Repository structure (key paths)
```
.
├── BPCalculator/                 # Razor Pages app
│   ├── Pages/Index.cshtml        # form/UI and category colors
│   ├── Pages/Index.cshtml.cs     # page model and request handling
│   └── BloodPressure.cs          # BP domain logic and category computation
├── TestProject1/                 # unit tests
├── k6Test/performance.js         # k6 load test script
└── .github/workflows/            # CI/CD and security workflows
    ├── master_bp-ca1.yml         # build + publish + deploy to Azure Web App on master
    ├── e2e.yml                   # Selenium end-to-end tests against a locally run build
    ├── bdd.yml                   # Cucumber/Selenium BDD tests
    ├── sonarCloud.yml            # build, test with coverage, and SonarCloud analysis
    ├── odc.yml                   # OWASP Dependency-Check scan (manual trigger)
    └── zap.yml                   # OWASP ZAP baseline scan (manual trigger)
```

## Styling
Category CSS classes (in `Pages/Index.cshtml`) map categories to colors:
- `.bp-category.low`
- `.bp-category.ideal`
- `.bp-category.prehigh`
- `.bp-category.high`

Adjust colors or add classes in `Index.cshtml` as needed.

## Contributing
Contributions welcome. Open a pull request and include a short description of the change. Run tests locally before submitting.

## CI / GitHub Actions overview
- `master_bp-ca1.yml`: build and publish the app, then deploy to the Azure Web App `bp-ca1` on pushes to `master`.
- `sonarCloud.yml`: run build + test with coverage and submit results to SonarCloud on pushes/PRs.
- `e2e.yml` and `bdd.yml`: publish and start the app locally on `windows-latest`, then run Selenium-based E2E and BDD suites from external repos; triggered on pushes/PRs to `master`.
- `odc.yml`: manual OWASP Dependency-Check scan that uploads an HTML report artifact.
- `zap.yml`: manual OWASP ZAP baseline scan against the deployed site defined by `URI`, uploads the HTML report artifact.
- `k6Test/performance.js`: local k6 script for performance testing; run with `k6 run k6Test/performance.js` after pointing it at your target URL.

### Workflow details and how to run them
- Deploy (`master_bp-ca1.yml`): automatically runs on pushes to `master` (or manual `Run workflow`) to build, publish, and deploy to Azure App Service.
- Quality (`sonarCloud.yml`): runs on pushes and PRs to `master`; builds, runs tests with coverage, and sends results to SonarCloud.
- E2E (`e2e.yml`) and BDD (`bdd.yml`): run on pushes/PRs to `master`; each publishes the app locally on the runner, starts it on port 5000, installs Chrome/ChromeDriver/Java, then pulls external Selenium repos (`bpTestSelenium` and `bpTestBDD`) and executes `mvn test`.
- Dependency scan (`odc.yml`): manual trigger only; runs OWASP Dependency-Check and uploads the HTML report as an artifact.
- ZAP baseline (`zap.yml`): manual trigger only; pulls the weekly ZAP image, scans the target `URI` (set in `env`), and uploads the HTML report artifact.
- Performance (`k6Test/performance.js`): not a GitHub Action. Run locally with `k6 run k6Test/performance.js` after setting the target host within the script.

## License
Specify your project license here (e.g., MIT). If you don't want an open source license, add a brief note describing permitted use.

## Notes / Next steps
- Consider adding client-side validation or richer UI feedback.
- If you want to containerize, use `dotnet publish -c Release -o out` then build a Docker image from `out`.
