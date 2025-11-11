# Automated Namespace Fix Script for SalesModule
# Generated on 2025-11-11 15:54:26
# Review and run with caution!

Write-Host 'SalesModule Namespace Auto-Fix Utility' -ForegroundColor Green
Write-Host '========================================' -ForegroundColor Green
Write-Host ''

$fixes = @(    @{
   File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\SalesModule\SalesForm.cs'
        From = 'KMCI_System'
     To = 'KMCI_System.SalesModule'
    },    @{
   File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\SalesModule\ProjectManagementModule\AddProjectForm.cs'
        From = 'KMCI_System.SalesModule.ProductManagement'
     To = 'KMCI_System.SalesModule.ProjectManagementModule'
    },    @{
   File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\SalesModule\ProjectManagementModule\ProjectDetailsModule\ProjectDirectory\PurchaseRequestDetails.cs'
        From = 'KMCI_System.SalesModule'
     To = 'KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory'
    },    @{
   File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\SalesModule\SupplierManagementModule\SupplierManagement.cs'
        From = 'KMCI_System.SalesModule'
     To = 'KMCI_System.SalesModule.SupplierManagementModule'
    },)

$fixedCount = 0
foreach ($fix in $fixes) {
    if (Test-Path $fix.File) {
        $content = Get-Content $fix.File -Raw
   $escapedFrom = [regex]::Escape($fix.From)
        $pattern = "namespace\s+$escapedFrom"
        $replacement = "namespace $($fix.To)"
        $newContent = $content -replace $pattern, $replacement
  
  if ($content -ne $newContent) {
    Set-Content -Path $fix.File -Value $newContent -NoNewline
     Write-Host "? Fixed: $($fix.File.Split('\')[-1])" -ForegroundColor Green
            $fixedCount++
    } else {
            Write-Host "? Skipped: $($fix.File.Split('\')[-1]) (no changes needed)" -ForegroundColor Yellow
        }
    } else {
        Write-Host "? Not found: $($fix.File.Split('\')[-1])" -ForegroundColor Red
    }
}

Write-Host ''
Write-Host "Fixed $fixedCount file(s)" -ForegroundColor Green
Write-Host "Please rebuild your solution to verify the changes." -ForegroundColor Cyan
