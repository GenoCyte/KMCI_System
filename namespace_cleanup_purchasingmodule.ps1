# PowerShell Script to Clean Up Namespaces in PurchasingModule
# This script helps identify and fix namespace issues

Write-Host "PurchasingModule Namespace Cleanup Utility" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host ""

$basePath = "C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule"

Write-Host "Analyzing files in: $basePath" -ForegroundColor Cyan
Write-Host ""

# Get all .cs files (excluding .Designer.cs for initial scan)
$csFiles = Get-ChildItem -Path $basePath -Filter "*.cs" -Recurse | Where-Object { $_.Name -notlike "*.Designer.cs" }
$designerFiles = Get-ChildItem -Path $basePath -Filter "*.Designer.cs" -Recurse

Write-Host "Found $($csFiles.Count) C# files (excluding Designer files)" -ForegroundColor Yellow
Write-Host "Found $($designerFiles.Count) Designer files" -ForegroundColor Yellow
Write-Host ""

$namespaceIssues = @()

# Function to calculate expected namespace
function Get-ExpectedNamespace {
    param($filePath, $baseDirectory)
    
    $relativePath = $filePath.Replace($baseDirectory, "").TrimStart("\")
    $expectedNamespace = "KMCI_System.PurchasingModule"
    
    if ($relativePath) {
  $folderParts = $relativePath.Split([IO.Path]::DirectorySeparatorChar)
        $expectedNamespace += "." + ($folderParts -join ".")
    }
  
    return $expectedNamespace
}

Write-Host "Scanning regular C# files..." -ForegroundColor Cyan
Write-Host ""

foreach ($file in $csFiles) {
    $content = Get-Content $file.FullName -Raw
    
    # Extract current namespace
    if ($content -match 'namespace\s+([\w\.]+)') {
        $currentNamespace = $matches[1]
      
        # Calculate expected namespace based on folder structure
   $expectedNamespace = Get-ExpectedNamespace -filePath $file.DirectoryName -baseDirectory $basePath
        
        if ($currentNamespace -ne $expectedNamespace) {
            $namespaceIssues += [PSCustomObject]@{
    File = $file.FullName.Replace($basePath, "PurchasingModule")
           CurrentNamespace = $currentNamespace
     ExpectedNamespace = $expectedNamespace
      Status = "MISMATCH"
     Type = "Regular"
  }
        Write-Host "? " -ForegroundColor Red -NoNewline
 Write-Host $file.Name -ForegroundColor Yellow -NoNewline
            Write-Host " (Expected: $expectedNamespace)" -ForegroundColor Gray
        } else {
 Write-Host "? " -ForegroundColor Green -NoNewline
            Write-Host $file.Name -ForegroundColor White
        }
    } else {
     $expectedNamespace = Get-ExpectedNamespace -filePath $file.DirectoryName -baseDirectory $basePath
        $namespaceIssues += [PSCustomObject]@{
 File = $file.FullName.Replace($basePath, "PurchasingModule")
      CurrentNamespace = "NOT FOUND"
   ExpectedNamespace = $expectedNamespace
            Status = "MISSING"
         Type = "Regular"
        }
        Write-Host "? " -ForegroundColor Yellow -NoNewline
      Write-Host $file.Name -ForegroundColor Yellow -NoNewline
     Write-Host " (No namespace found)" -ForegroundColor Gray
    }
}

Write-Host ""
Write-Host "Scanning Designer files..." -ForegroundColor Cyan
Write-Host ""

foreach ($file in $designerFiles) {
    $content = Get-Content $file.FullName -Raw
    
    # Extract current namespace
    if ($content -match 'namespace\s+([\w\.]+)') {
        $currentNamespace = $matches[1]
        
      # Calculate expected namespace based on folder structure
        $expectedNamespace = Get-ExpectedNamespace -filePath $file.DirectoryName -baseDirectory $basePath
        
        if ($currentNamespace -ne $expectedNamespace) {
         $namespaceIssues += [PSCustomObject]@{
          File = $file.FullName.Replace($basePath, "PurchasingModule")
        CurrentNamespace = $currentNamespace
        ExpectedNamespace = $expectedNamespace
        Status = "MISMATCH"
                Type = "Designer"
            }
            Write-Host "? " -ForegroundColor Red -NoNewline
     Write-Host $file.Name -ForegroundColor Yellow -NoNewline
       Write-Host " (Expected: $expectedNamespace)" -ForegroundColor Gray
  } else {
      Write-Host "? " -ForegroundColor Green -NoNewline
            Write-Host $file.Name -ForegroundColor White
        }
    }
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Yellow
Write-Host "Namespace Issues Found:" -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Yellow

if ($namespaceIssues.Count -eq 0) {
    Write-Host "? No namespace issues found! All files are correctly namespaced." -ForegroundColor Green
} else {
    # Group by type
  $regularIssues = $namespaceIssues | Where-Object { $_.Type -eq "Regular" }
    $designerIssues = $namespaceIssues | Where-Object { $_.Type -eq "Designer" }
 
    if ($regularIssues.Count -gt 0) {
        Write-Host ""
        Write-Host "Regular C# Files ($($regularIssues.Count) issues):" -ForegroundColor Cyan
        Write-Host "----------------------------------------" -ForegroundColor Cyan
        $regularIssues | Format-Table -Property File, CurrentNamespace, ExpectedNamespace -AutoSize -Wrap
  }
    
    if ($designerIssues.Count -gt 0) {
 Write-Host ""
  Write-Host "Designer Files ($($designerIssues.Count) issues):" -ForegroundColor Magenta
      Write-Host "----------------------------------------" -ForegroundColor Magenta
    $designerIssues | Format-Table -Property File, CurrentNamespace, ExpectedNamespace -AutoSize -Wrap
    }
    
    Write-Host ""
  Write-Host "========================================" -ForegroundColor Yellow
    Write-Host "Summary:" -ForegroundColor Yellow
    Write-Host "========================================" -ForegroundColor Yellow
    Write-Host "Total files with namespace issues: $($namespaceIssues.Count)" -ForegroundColor Red
    Write-Host "  - Regular files: $($regularIssues.Count)" -ForegroundColor Yellow
    Write-Host "  - Designer files: $($designerIssues.Count)" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Namespace Cleanup Rules:" -ForegroundColor Cyan
    Write-Host "1. ProjectManagementModule files ? KMCI_System.PurchasingModule.ProjectManagementModule" -ForegroundColor White
 Write-Host "2. ProjectDetailsModule files ? KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule" -ForegroundColor White
    Write-Host "3. PurchaseRequestModule files ? KMCI_System.PurchasingModule.PurchaseRequestModule" -ForegroundColor White
    Write-Host "4. PurchaseOrderModule files ? KMCI_System.PurchasingModule.PurchaseOrderModule" -ForegroundColor White
    Write-Host "5. Sub-modules (BudgetAllocation, CreateQuotation, etc.) follow parent namespace" -ForegroundColor White
    Write-Host ""
}

# Export to CSV for manual review
$reportPath = Join-Path $PSScriptRoot "namespace_issues_purchasing_report.csv"
$namespaceIssues | Export-Csv -Path $reportPath -NoTypeInformation
Write-Host "Detailed report exported to: $reportPath" -ForegroundColor Green

# Create a batch fix file
Write-Host ""
Write-Host "Generating automated fix suggestions..." -ForegroundColor Cyan

$fixScript = @"
# Automated Namespace Fix Script for PurchasingModule
# Generated on $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')
# Review and run with caution!

Write-Host 'PurchasingModule Namespace Auto-Fix Utility' -ForegroundColor Green
Write-Host '===========================================' -ForegroundColor Green
Write-Host ''

`$fixes = @(
"@

foreach ($issue in $namespaceIssues | Where-Object { $_.Status -eq "MISMATCH" }) {
    $fullPath = $issue.File.Replace("PurchasingModule", $basePath)
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

$fixScriptPath = Join-Path $PSScriptRoot "auto_fix_purchasing_namespaces.ps1"
Set-Content -Path $fixScriptPath -Value $fixScript
Write-Host "Auto-fix script created: $fixScriptPath" -ForegroundColor Green
Write-Host ""
Write-Host "To apply fixes automatically, run:" -ForegroundColor Yellow
Write-Host "  .\auto_fix_purchasing_namespaces.ps1" -ForegroundColor White
Write-Host ""
Write-Host "? IMPORTANT: Review the changes before committing!" -ForegroundColor Red
