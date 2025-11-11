# PowerShell Script to Clean Up Namespaces in SalesModule
# This script helps identify and fix namespace issues

Write-Host "SalesModule Namespace Cleanup Utility" -ForegroundColor Green
Write-Host "======================================" -ForegroundColor Green
Write-Host ""

$basePath = "C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\SalesModule"

Write-Host "Analyzing files in: $basePath" -ForegroundColor Cyan
Write-Host ""

# Get all .cs files (excluding .Designer.cs for now)
$csFiles = Get-ChildItem -Path $basePath -Filter "*.cs" -Recurse | Where-Object { $_.Name -notlike "*.Designer.cs" }

Write-Host "Found $($csFiles.Count) C# files (excluding Designer files)" -ForegroundColor Yellow
Write-Host ""

$namespaceIssues = @()

foreach ($file in $csFiles) {
    $content = Get-Content $file.FullName -Raw
    
    # Extract current namespace
    if ($content -match 'namespace\s+([\w\.]+)') {
    $currentNamespace = $matches[1]
        
      # Calculate expected namespace based on folder structure
 $relativePath = $file.DirectoryName.Replace($basePath, "").TrimStart("\")
        $expectedNamespace = "KMCI_System.SalesModule"
        
        if ($relativePath) {
 $folderParts = $relativePath.Split([IO.Path]::DirectorySeparatorChar)
            $expectedNamespace += "." + ($folderParts -join ".")
   }
        
        if ($currentNamespace -ne $expectedNamespace) {
            $namespaceIssues += [PSCustomObject]@{
                File = $file.FullName.Replace($basePath, "SalesModule")
   CurrentNamespace = $currentNamespace
    ExpectedNamespace = $expectedNamespace
                Status = "MISMATCH"
        }
 } else {
            Write-Host "? " -ForegroundColor Green -NoNewline
          Write-Host $file.Name -ForegroundColor White
        }
    } else {
        $namespaceIssues += [PSCustomObject]@{
        File = $file.FullName.Replace($basePath, "SalesModule")
      CurrentNamespace = "NOT FOUND"
        ExpectedNamespace = "N/A"
            Status = "MISSING"
}
    }
}

Write-Host ""
Write-Host "======================================" -ForegroundColor Yellow
Write-Host "Namespace Issues Found:" -ForegroundColor Yellow
Write-Host "======================================" -ForegroundColor Yellow

if ($namespaceIssues.Count -eq 0) {
    Write-Host "No namespace issues found! All files are correctly namespaced." -ForegroundColor Green
} else {
 $namespaceIssues | Format-Table -AutoSize -Wrap
    
    Write-Host ""
    Write-Host "Total files with namespace issues: $($namespaceIssues.Count)" -ForegroundColor Red
    Write-Host ""
    Write-Host "Namespace Cleanup Rules:" -ForegroundColor Cyan
    Write-Host "1. CompanyManagementModule files ? KMCI_System.SalesModule.CompanyManagementModule" -ForegroundColor White
    Write-Host "2. ProductManagementModule files ? KMCI_System.SalesModule.ProductManagementModule" -ForegroundColor White
    Write-Host "3. ProjectManagementModule files ? KMCI_System.SalesModule.ProjectManagementModule" -ForegroundColor White
    Write-Host "4. ProjectDetailsModule files ? KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule" -ForegroundColor White
    Write-Host ""
}

# Export to CSV for manual review
$reportPath = Join-Path $PSScriptRoot "namespace_issues_report.csv"
$namespaceIssues | Export-Csv -Path $reportPath -NoTypeInformation
Write-Host "Detailed report exported to: $reportPath" -ForegroundColor Green

# Create a batch fix file
Write-Host ""
Write-Host "Generating automated fix suggestions..." -ForegroundColor Cyan

$fixScript = @"
# Automated Namespace Fix Script for SalesModule
# Generated on $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')
# Review and run with caution!

Write-Host 'SalesModule Namespace Auto-Fix Utility' -ForegroundColor Green
Write-Host '========================================' -ForegroundColor Green
Write-Host ''

`$fixes = @(
"@

foreach ($issue in $namespaceIssues | Where-Object { $_.Status -eq "MISMATCH" }) {
    $fullPath = $issue.File.Replace("SalesModule", $basePath)
    $fromNamespace = $issue.CurrentNamespace
    $toNamespace = $issue.ExpectedNamespace
    
    $fixScript += @"
    @{
   File = '$fullPath'
        From = '$fromNamespace'
     To = '$toNamespace'
    },
"@
}

$fixScript += @"
)

`$fixedCount = 0
foreach (`$fix in `$fixes) {
    if (Test-Path `$fix.File) {
        `$content = Get-Content `$fix.File -Raw
   `$escapedFrom = [regex]::Escape(`$fix.From)
        `$pattern = "namespace\s+`$escapedFrom"
        `$replacement = "namespace `$(`$fix.To)"
        `$newContent = `$content -replace `$pattern, `$replacement
  
  if (`$content -ne `$newContent) {
    Set-Content -Path `$fix.File -Value `$newContent -NoNewline
     Write-Host "? Fixed: `$(`$fix.File.Split('\')[-1])" -ForegroundColor Green
            `$fixedCount++
    } else {
            Write-Host "? Skipped: `$(`$fix.File.Split('\')[-1]) (no changes needed)" -ForegroundColor Yellow
        }
    } else {
        Write-Host "? Not found: `$(`$fix.File.Split('\')[-1])" -ForegroundColor Red
    }
}

Write-Host ''
Write-Host "Fixed `$fixedCount file(s)" -ForegroundColor Green
Write-Host "Please rebuild your solution to verify the changes." -ForegroundColor Cyan
"@

$fixScriptPath = Join-Path $PSScriptRoot "auto_fix_sales_namespaces.ps1"
Set-Content -Path $fixScriptPath -Value $fixScript
Write-Host "Auto-fix script created: $fixScriptPath" -ForegroundColor Green
Write-Host ""
Write-Host "To apply fixes automatically, run:" -ForegroundColor Yellow
Write-Host "  .\auto_fix_sales_namespaces.ps1" -ForegroundColor White
Write-Host ""
Write-Host "? IMPORTANT: Review the changes before committing!" -ForegroundColor Red
