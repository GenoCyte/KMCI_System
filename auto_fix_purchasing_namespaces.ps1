# Automated Namespace Fix Script for PurchasingModule
# Generated on 2025-11-11 15:53:48
# Review and run with caution!

Write-Host 'PurchasingModule Namespace Auto-Fix Utility' -ForegroundColor Green
Write-Host '===========================================' -ForegroundColor Green
Write-Host ''

$fixes = @(
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\PurchasingForm.cs'
        From = 'KMCI_System'
      To = 'KMCI_System.PurchasingModule'
    },
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\ProjectManagementModule\AddProjectForm.cs'
        From = 'KMCI_System.PurchasingModule'
      To = 'KMCI_System.PurchasingModule.ProjectManagementModule'
    },
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\ProjectManagementModule\ProjectDetailsModule\ProjectDirectory\PurchaseRequestDetails2.cs'
        From = 'KMCI_System.PurchasingModule'
      To = 'KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory'
    },
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\PurchasingForm.Designer.cs'
        From = 'KMCI_System'
      To = 'KMCI_System.PurchasingModule'
    },
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\ProjectManagementModule\ProjectDetailsModule\ProjectDetails.Designer.cs'
        From = 'KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule'
      To = 'KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule'
    },
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\ProjectManagementModule\ProjectDetailsModule\ProjectOverview.Designer.cs'
        From = 'KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule'
      To = 'KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule'
    },
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\ProjectManagementModule\ProjectDetailsModule\BudgetAllocation\AddBudget.Designer.cs'
        From = 'KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule.BudgetAllocation'
      To = 'KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.BudgetAllocation'
    },
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\ProjectManagementModule\ProjectDetailsModule\BudgetAllocation\BudgetAllocation.Designer.cs'
        From = 'KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule.BudgetAllocation'
      To = 'KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.BudgetAllocation'
    },
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\ProjectManagementModule\ProjectDetailsModule\CreateQuotation\AddProductList.Designer.cs'
        From = 'KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule.CreateQuotation'
      To = 'KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.CreateQuotation'
    },
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\ProjectManagementModule\ProjectDetailsModule\CreateQuotation\CreateQuotation.Designer.cs'
        From = 'KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule.CreateQuotation'
      To = 'KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.CreateQuotation'
    },
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\ProjectManagementModule\ProjectDetailsModule\ProjectDirectory\BudgetDetails.Designer.cs'
        From = 'KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory'
      To = 'KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory'
    },
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\ProjectManagementModule\ProjectDetailsModule\ProjectDirectory\ProjectDirectory.Designer.cs'
        From = 'KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory'
      To = 'KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory'
    },
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\ProjectManagementModule\ProjectDetailsModule\ProjectDirectory\PurchaseRequestDetails2.Designer.cs'
        From = 'KMCI_System.PurchasingModule'
      To = 'KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory'
    },
    @{
        File = 'C:\Users\Lenovo\Documents\Projects\KMCI_System\KMCI_System\PurchasingModule\ProjectManagementModule\ProjectDetailsModule\ProjectDirectory\QuotationDetails.Designer.cs'
        From = 'KMCI_System.SalesModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory'
      To = 'KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory'
    },
)

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
