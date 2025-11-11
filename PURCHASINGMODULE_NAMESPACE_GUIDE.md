# PurchasingModule Namespace Cleanup Guide

## Overview
This guide helps you clean up and standardize namespace declarations across all files in the PurchasingModule.

## Current Issues
The PurchasingModule has inconsistent namespace declarations that don't match the folder structure, causing compilation errors.

## Target Namespace Structure

```
KMCI_System/PurchasingModule/
├── ProjectManagementModule/            → KMCI_System.PurchasingModule.ProjectManagementModule
│   ├── ProjectManagement.cs
│   ├── ProjectManagement.Designer.cs
│   ├── AddProjectForm.cs
│   └── ProjectDetailsModule/  → KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule
│       ├── ProjectOverview.cs
│       ├── ProjectDetails.cs
│   ├── BudgetAllocation/           → KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.BudgetAllocation
│       │   ├── BudgetAllocation.cs
│       │   └── AddBudget.cs
│       ├── CreateQuotation/          → KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.CreateQuotation
│     │   ├── CreateQuotation.cs
││   ├── AddProductList.cs
│     │   └── KinglandQuotationPdfGenerator.cs
│    └── ProjectDirectory/   → KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.ProjectDirectory
│           ├── ProjectDirectory.cs
│   └── BudgetDetails.cs
│
├── PurchaseRequestModule/ → KMCI_System.PurchasingModule.PurchaseRequestModule
│   ├── PurchaseRequest.cs
│   ├── PurchaseRequestDetails.cs
│   └── CreatePurchaseRequest.cs
│
└── PurchaseOrderModule/       → KMCI_System.PurchasingModule.PurchaseOrderModule
    ├── PurchaseOrder.cs
    ├── PurchaseOrderList.cs
    ├── CreatePurchaseOrder.cs
├── PurchaseOrderPDFGenerator.cs
    └── PurchaseOrderPDFGenerator2.cs
```

## Common Issues Found

### 1. **Mismatched Namespaces**
   - **Problem**: Designer files using SalesModule namespace instead of PurchasingModule
   - **Example**: `ProjectManagement.Designer.cs` had `namespace KMCI_System.SalesModule`
   - **Fix**: Change to `namespace KMCI_System.PurchasingModule.ProjectManagementModule`

### 2. **Missing Folder in Namespace**
   - **Problem**: Files not including their immediate parent folder in namespace
   - **Example**: File in `ProjectManagementModule` folder using `namespace KMCI_System.PurchasingModule`
   - **Fix**: Should be `namespace KMCI_System.PurchasingModule.ProjectManagementModule`

### 3. **Incorrect Type References**
   - **Problem**: Code referencing types with wrong namespace prefix
   - **Example**: `ProjectManagementModule.ProjectDetailsModule.ProjectOverview` when already in ProjectManagementModule namespace
   - **Fix**: Use `ProjectDetailsModule.ProjectOverview` (relative reference)

## Quick Start

### Step 1: Run the Analysis Script
```powershell
.\namespace_cleanup_purchasingmodule.ps1
```

This will:
- ✅ Scan all .cs files in PurchasingModule
- ✅ Identify namespace mismatches
- ✅ Generate a detailed report (CSV)
- ✅ Create an auto-fix script

### Step 2: Review the Report
Check the generated `namespace_issues_purchasing_report.csv` file to see all namespace issues.

### Step 3: Apply Fixes
**Option A: Manual Fix (Recommended for first time)**
Review each file and update the namespace declarations manually.

**Option B: Automated Fix (Use with caution)**
```powershell
.\auto_fix_purchasing_namespaces.ps1
```

### Step 4: Update Using Directives
After fixing namespaces, update `using` directives in files that reference types from other modules.

### Step 5: Rebuild and Test
```powershell
dotnet clean
dotnet build
```

## Namespace Rules

### Rule 1: Match Folder Structure
**Every folder level becomes a namespace segment**
```
Folder: KMCI_System\PurchasingModule\ProjectManagementModule\
Namespace: namespace KMCI_System.PurchasingModule.ProjectManagementModule
```

### Rule 2: Designer Files Match Parent Files
**Designer.cs must have same namespace as parent .cs file**
```csharp
// ProjectManagement.cs
namespace KMCI_System.PurchasingModule.ProjectManagementModule

// ProjectManagement.Designer.cs
namespace KMCI_System.PurchasingModule.ProjectManagementModule  // MUST MATCH!
```

### Rule 3: Using Directives for Cross-Module References
**Add using directives for types from other modules**
```csharp
using KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule;

namespace KMCI_System.PurchasingModule.ProjectManagementModule
{
    public partial class ProjectManagement : UserControl
    {
    private ProjectDetailsModule.ProjectOverview projectDetailsControl;  // ✓ Correct
    }
}
```

## Common Fixes

### Fix 1: ProjectManagement Files
**Before:**
```csharp
// ProjectManagement.Designer.cs
namespace KMCI_System.SalesModule  // ✗ WRONG!
```

**After:**
```csharp
// ProjectManagement.Designer.cs
namespace KMCI_System.PurchasingModule.ProjectManagementModule  // ✓ CORRECT
```

### Fix 2: ProjectOverview Reference
**Before:**
```csharp
namespace KMCI_System.PurchasingModule
{
    public partial class ProjectManagement : UserControl
    {
  private ProjectManagementModule.ProjectDetailsModule.ProjectOverview projectDetailsControl;  // ✗ Overly qualified
    }
}
```

**After:**
```csharp
namespace KMCI_System.PurchasingModule.ProjectManagementModule
{
    public partial class ProjectManagement : UserControl
    {
        private ProjectDetailsModule.ProjectOverview projectDetailsControl;  // ✓ CORRECT
    }
}
```

### Fix 3: Cross-Module References
**Before:**
```csharp
namespace KMCI_System.PurchasingModule.PurchaseRequestModule
{
    public partial class CreatePurchaseRequest : UserControl
    {
    // Missing using directive, fully qualified reference needed
  private KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule.ProjectOverview overview;
    }
}
```

**After:**
```csharp
using KMCI_System.PurchasingModule.ProjectManagementModule.ProjectDetailsModule;

namespace KMCI_System.PurchasingModule.PurchaseRequestModule
{
    public partial class CreatePurchaseRequest : UserControl
    {
  private ProjectOverview overview;  // ✓ Clean reference
    }
}
```

## Files Fixed So Far

### ✅ Already Fixed:
1. `ProjectManagement.cs` - Updated to `KMCI_System.PurchasingModule.ProjectManagementModule`
2. `ProjectManagement.Designer.cs` - Updated to `KMCI_System.PurchasingModule.ProjectManagementModule`

### 📋 Pending Review:
Run the analysis script to get a complete list of files that need attention.

## Troubleshooting

### Error: "Type or namespace name 'X' could not be found"
**Solution**: Add the appropriate `using` directive at the top of the file.

### Error: "The type or namespace name 'X' exists in both 'Y' and 'Z'"
**Solution**: Use fully qualified type names or alias the conflicting types.

### Error: "CS0115: no suitable method found to override"
**Solution**: Ensure the class is declared with the correct base type (UserControl, Form, etc.)

### Build still failing after namespace fixes
**Solution**: 
1. Clean solution: `dotnet clean`
2. Delete `bin` and `obj` folders
3. Rebuild: `dotnet build`

## Best Practices

1. **Always match namespaces to folder structure** - Makes code navigation predictable
2. **Keep Designer.cs and .cs namespaces in sync** - Prevents partial class errors
3. **Use relative namespace references when possible** - Reduces coupling
4. **Add using directives for clarity** - Makes code more readable
5. **Run analysis scripts regularly** - Catches issues early

## Next Steps

1. ✅ Run `namespace_cleanup_purchasingmodule.ps1`
2. ✅ Review generated report
3. ✅ Apply fixes (manual or automated)
4. ✅ Update using directives
5. ✅ Clean and rebuild
6. ✅ Test the application
7. ✅ Commit changes with clear message: "fix: standardize PurchasingModule namespaces"

## Related Documentation
- [SalesModule Namespace Cleanup Guide](./SALESMODULE_NAMESPACE_GUIDE.md) - Similar process for SalesModule
- [AdminModule Namespace Standards](./ADMINMODULE_NAMESPACE_GUIDE.md) - AdminModule conventions

---
**Generated**: $(Get-Date -Format 'yyyy-MM-dd')
**Last Updated**: $(Get-Date -Format 'yyyy-MM-dd')
