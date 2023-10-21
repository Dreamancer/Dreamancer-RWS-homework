# Load the paths from command line, if there are none provided we're gonna use default paths
if ($args.Length -ge 2) {
    $configSource = $args[0]
    $exePath = $args[1]
} else {
    $configSource = Resolve-Path -Path "./AltConfigs/"
    $exePath = Resolve-Path -Path "Moravia.Homework.exe"
}

Write-Host "Application path: '$exePath'"
Write-Host "Configs path: '$configSource'"

if (Test-Path $configSource -PathType Container) {
    Get-ChildItem -Path $configSource | ForEach-Object {
        $filePath = $_.FullName
        # Start 'Moravia.Homework.exe' with the file path as the first parameter
        Start-Process -FilePath $exePath -ArgumentList """$filePath""" 
    }
} else {
    Write-Host "Source directory not found: $configSource"
}