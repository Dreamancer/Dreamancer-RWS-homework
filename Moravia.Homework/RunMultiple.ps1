# Load the paths from command line, if there are none provided we're gonna use default paths
if ($args.Length -ge 2) {
    $source = $args[0]
    $exePath = $args[1]
} else {
    $source = "./AltConfigs/"
    $exePath = "Moravia.Homework.exe"
}


if (Test-Path $source -PathType Container) {
    Get-ChildItem -Path $source | ForEach-Object {
        $filePath = $_.FullName
        Write-Host $filePath
        # Start 'Moravia.Homework.exe' with the file path as the first parameter
        Start-Process -FilePath $exePath -ArgumentList """$filePath"""
    }
} else {
    Write-Host "Source directory not found: $source"
}