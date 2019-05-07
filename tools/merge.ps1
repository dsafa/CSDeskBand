$srcDir = $PSScriptRoot + "/../src/CSDeskBand"
$alldirs = $srcDir, ($srcDir + "/Interop"), ($srcDir + "/ContextMenu")
$alldirs | ForEach-Object { $sourceFiles += Get-ChildItem -Path $_ -File -Filter "*.cs" }

$outputFile = $PSScriptRoot + "/../output/CSDeskBand.cs"
Set-Content -Path $outputFile -Value (Get-Content -Path ($PSScriptRoot + "/outputheader.txt"))
$sourceFiles | ForEach-Object { Add-Content -Path $outputFile -Value (Get-Content $_.FullName) }
Add-Content -Path $outputFile -Value (Get-Content -Path ($PSScriptRoot + "/outputfooter.txt"))