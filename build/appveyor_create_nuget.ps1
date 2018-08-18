# This script will create nuget packages based on the tags.

# Regex for tags in the form of vX.X.X+<core|wpf|win>
# core = CSDeskBand, wpf = CSDeskband.Wpf, win = CSDeskBand.win
# Example:
# if we are updating CSDeskBand library we will create a tag v2.0.0+core
[Regex]$version_regex = "v(?i)(?:0|[1-9]+)\.(?:0|[1-9]+).(?:0|[1-9]+)(?:-[0-9a-z-]+(?:\.[0-9a-z-]+)*)?(\+(?<metadata>[0-9a-z-]+)(?:\.[0-9a-z-]+)*)?"
$git_tags = & git tag -l --points-at HEAD
if (!$git_tags) {
    Write-Host "No tags found"
    exit 1
}

# Put all the tags into a list
$tags = $git_tags.Split([Environment]::NewLine).Trim() | % {$version_regex.Match($_)} | ? {$_.Success}

$core_version= $null
$wpf_version = $null
$win_version = $null

# Extracts the version from a tag
function Get-Semver1 {
    param([Parameter(ValueFromPipeline)] $text)
    return $text.Split("v")[1].Split("+")[0]
}

# Go through all the tags and check if what we are updating
foreach ($tag in $tags) {
    switch($tag.Groups["metadata"].Value) {
        "wpf" {$wpf_version = $tag.Value | Get-Semver1}
        "win" {$win_version = $tag.Value | Get-Semver1}
        "core" {$core_version = $tag.Value | Get-Semver1}
    }
}

# Create matching nuget packages and output to the script folder
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
if ($core_version) {
    Write-Host "Building CSDeskband" $core_version
    & nuget pack $scriptDir/../CSDeskband/CSDeskband.csproj -Properties Configuration=Release -Version $core_version -OutputDirectory $scriptDir
}

if ($win_version) {
    Write-Host "Building CSDeskband.win" $win_version
    & nuget pack $scriptDir/../CSDeskband.Win/CSDeskband.Win.csproj -Properties Configuration=Release -Version $win_version -OutputDirectory $scriptDir
}

if ($wpf_version) {
    Write-Host "Building CSDeskband.wpf" $wpf_version
    & nuget pack $scriptDir/../CSDeskband.Wpf/CSDeskband.Wpf.csproj -Properties Configuration=Release -Version $wpf_version -OutputDirectory $scriptDir
}