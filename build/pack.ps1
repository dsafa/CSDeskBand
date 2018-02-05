$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
& nuget pack $scriptDir/../CSDeskband/CSDeskband.csproj -properties Configuration=Release -OutputDirectory $scriptDir
& nuget pack $scriptDir/../CSDeskband.Win/CSDeskband.Win.csproj -properties Configuration=Release -OutputDirectory $scriptDir
& nuget pack $scriptDir/../CSDeskband.Wpf/CSDeskband.Wpf.csproj -properties Configuration=Release -OutputDirectory $scriptDir