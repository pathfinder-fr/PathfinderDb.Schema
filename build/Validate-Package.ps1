[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string] $PackagePath
)

$resolvedPackagePath = (Resolve-Path -LiteralPath $PackagePath).Path
Add-Type -AssemblyName System.IO.Compression.FileSystem

$requiredEntries = @(
    'README.md',
    'LICENSE.txt',
    'CHANGELOG.md',
    'package-icon.png',
    'lib/netstandard2.0/PathfinderDb.Schema.dll',
    'lib/netstandard2.0/PathfinderDb.Schema.xml',
    'docs/xsd/pathfinderDb.xsd',
    'docs/samples/Spell.xml',
    'docs/samples/Feat.xml',
    'docs/samples/Monster.xml'
)

$archive = [System.IO.Compression.ZipFile]::OpenRead($resolvedPackagePath)
try {
    $entryNames = [System.Collections.Generic.HashSet[string]]::new(
        [StringComparer]::OrdinalIgnoreCase
    )

    foreach ($entry in $archive.Entries) {
        [void] $entryNames.Add($entry.FullName.Replace('\', '/'))
    }

    $missingEntries = $requiredEntries | Where-Object { -not $entryNames.Contains($_) }
    if ($missingEntries) {
        throw "Package is missing required entries: $($missingEntries -join ', ')"
    }

    $legacyEntries = $entryNames | Where-Object { $_ -match '^lib/portable-' }
    if ($legacyEntries) {
        throw "Package contains legacy Portable entries: $($legacyEntries -join ', ')"
    }

    Write-Output "Package validation succeeded: $resolvedPackagePath"
}
finally {
    $archive.Dispose()
}
