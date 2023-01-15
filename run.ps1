# Move to the selected directory
$dir = "src\" + $args[0];
Set-Location $dir;

$noSpecificLanguageSelected = $args[1].length -eq 0;
if(-not $noSpecificLanguageSelected)
{
    $languagesToRun = $args[1] -split ",";
}

if($noSpecificLanguageSelected -Or $languagesToRun -Contains "C")
{
    # Run the C version
    Write-Output "C:"
    gcc .\c\main.c -O3 -o .\c\main; .\c\main.exe; # -O3 compiles in release mode (highest level of optimizations)
    Write-Output "----------------"
}

if($noSpecificLanguageSelected -Or $languagesToRun -Contains "Rust")
{
    # Run the Rust version using cargo
    Write-Output "Rust:"
    Set-Location ".\rs";
    cargo run --release -q; # -q flag for "quiet", to not output the compilation logs 
    Write-Output "----------------"
    cd..;
}

if($noSpecificLanguageSelected -Or $languagesToRun -Contains "C#")
{
    # Run the C# version using the dotnet cli
    Write-Output "C#:"
    Set-Location ".\c#";
    dotnet run /clp:ErrorsOnly --configuration Release; # Suppressing warning messages with /clp:ErrorsOnly.
    Write-Output "----------------"
    cd..;
}

cd..;
cd..;