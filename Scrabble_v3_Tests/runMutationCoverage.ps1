dotnet stryker
$resultsPath = $PSScriptRoot + "\StrykerOutput"
$latestResults = gci $resultsPath | ? { $_.PSIsContainer } | sort CreationTime -desc | select -f 1
$report = "$($resultsPath)\$($latestResults)\reports\mutation-report.html"
$destination = $PSScriptRoot + "\MutationCoverageReport\"
Copy-Item $report -Destination $destination
Remove-Item $resultsPath -Recurse