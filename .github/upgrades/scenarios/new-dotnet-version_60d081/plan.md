# .NET 10.0 Upgrade Plan

## Table of Contents

- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Plans](#project-by-project-plans)
  - [SportsClub.SharedModels](#sportsclubsharedmodels)
  - [SportsClub.Api](#sportsclubapi)
  - [SportsClub.Web](#sportsclubweb)
- [Package Update Reference](#package-update-reference)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Risk Management](#risk-management)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

---

## Executive Summary

### Scenario Description
Upgrade all projects in the SportsClub solution from .NET 9.0 to .NET 10.0 (Long Term Support).

### Scope
**3 projects** require upgrade:
- **SportsClub.SharedModels** - Shared class library (47 LOC)
- **SportsClub.Api** - ASP.NET Core API (568 LOC)
- **SportsClub.Web** - Blazor WebAssembly application (51 LOC)

**Current State**: All projects targeting net9.0  
**Target State**: All projects targeting net10.0

### Selected Strategy
**All-At-Once Strategy** - All projects upgraded simultaneously in single atomic operation.

**Rationale**: 
- Small solution (3 projects, 666 total LOC)
- All currently on .NET 9.0
- Simple, linear dependency structure (1 level deep)
- All packages have clear upgrade paths to 10.0.2
- No security vulnerabilities
- All projects rated Low difficulty

### Discovered Metrics
- **Total Projects**: 3
- **Total LOC**: 666
- **Dependency Depth**: 1 level
- **Package Updates Required**: 3 packages
- **Risk Level**: Low (no high-risk projects)
- **Security Vulnerabilities**: None
- **Behavioral Changes**: 3 in SportsClub.Web (System.Uri API)

### Complexity Assessment
**Simple Solution** - Fast execution with minimal risk

### Iteration Strategy
Fast batch approach:
- Phase 1-2: Foundation (3 iterations - complete) ✅
- Phase 3: All project details in 1-2 consolidated iterations (next)

**Expected Remaining Iterations**: 2-3

---

## Migration Strategy

### Approach Selection: All-At-Once

**Decision: Upgrade all 3 projects simultaneously in a single atomic operation.**

### Justification

This solution is an ideal candidate for All-At-Once upgrade:

✅ **Small solution size**: 3 projects with 666 total LOC  
✅ **Version homogeneity**: All projects currently on net9.0  
✅ **Simple dependencies**: Linear 1-level structure, no circular dependencies  
✅ **Low complexity**: All projects rated Low difficulty  
✅ **Clear package paths**: All 3 packages have straightforward upgrades (9.0.1 → 10.0.2)  
✅ **Low risk**: No security vulnerabilities, no high-risk projects  
✅ **Minimal breaking changes**: Only 3 behavioral changes (System.Uri API in Web project)

### All-At-Once Strategy Rationale

The All-At-Once approach provides:
- **Fastest completion**: Single coordinated update eliminates multi-phase overhead
- **No intermediate states**: Avoids multi-targeting complexity
- **Atomic validation**: One build/test cycle for entire solution
- **Simple coordination**: All projects benefit simultaneously

**Risk Mitigation**: The low complexity and small codebase size make the "big bang" approach low-risk. All changes can be validated together in a single testing pass.

### Execution Principles

**Atomic Operation**: 
- All project files updated together
- All package references updated together
- Single restore + build cycle
- Fix all compilation errors in one pass
- One comprehensive test run

**No Intermediate States**:
- Solution transitions from "all net9.0" to "all net10.0" in one operation
- No multi-targeting needed
- No phased rollout required

### Dependency-Based Ordering

While projects will be updated simultaneously, operations within the atomic upgrade respect dependency order:

1. **Project file updates**: All at once (TargetFramework net9.0 → net10.0)
2. **Package updates**: All at once (version 9.0.1 → 10.0.2)
3. **Restore & build**: Entire solution in dependency order (SharedModels built first automatically)
4. **Fix compilation errors**: Address issues as they appear across all projects
5. **Validate**: Full solution builds with 0 errors

### Parallel vs Sequential Execution

**Project Updates**: Simultaneous - all project files and packages updated together  
**Build Process**: Sequential (automatic) - MSBuild respects project dependencies  
**Testing**: Can be sequential or parallel based on test runner capabilities

### Phase Definition (Logical Grouping)

Though executed atomically, the upgrade can be understood in logical phases:

**Phase 0: Prerequisites** (if needed)
- Verify .NET 10.0 SDK installed
- Validate tooling compatibility

**Phase 1: Atomic Upgrade** 
- Update all 3 project files to net10.0
- Update all 3 packages to 10.0.2
- Restore dependencies
- Build solution
- Fix compilation errors (focus on System.Uri behavioral changes)

**Phase 2: Validation**
- Verify build success (0 errors, 0 warnings)
- Execute any test projects (none identified in assessment)
- Manual validation of Web and Api applications

---

## Detailed Dependency Analysis

### Dependency Graph Summary

The solution has a simple, clean dependency structure with 2 tiers:

```
Level 0 (Foundation):
  └─ SportsClub.SharedModels (no dependencies)

Level 1 (Applications):
  ├─ SportsClub.Api → depends on SharedModels
  └─ SportsClub.Web → depends on SharedModels
```

**Dependency Characteristics:**
- **Linear structure**: No circular dependencies
- **Single foundation**: All projects depend on one shared library
- **Parallel applications**: API and Web projects are independent of each other
- **Shallow depth**: Maximum 1 level of project dependencies

### Project Groupings by Migration Phase

Since this is an **All-At-Once upgrade**, all projects will be updated simultaneously. However, for understanding purposes, the natural dependency order is:

**Foundation Tier** (Level 0):
- SportsClub.SharedModels - 0 dependencies, used by 2 projects

**Application Tier** (Level 1):  
- SportsClub.Api - depends on SharedModels
- SportsClub.Web - depends on SharedModels

### Critical Path Identification

**Critical Path**: SharedModels → (Api, Web)

In the All-At-Once strategy, all three projects will be updated in a single coordinated operation, so the critical path is relevant for understanding dependency relationships but not for sequencing updates.

### Circular Dependencies

**None detected** - The solution has a clean dependency structure with no circular references.

---

## Project-by-Project Plans

[To be filled]

### SportsClub.SharedModels

**Current State**: 
- **Target Framework**: net9.0
- **Project Type**: ClassLibrary
- **Lines of Code**: 47
- **Dependencies**: None (foundation/leaf project)
- **Dependants**: 2 (SportsClub.Api, SportsClub.Web)
- **Packages**: 0 explicit packages
- **Risk Level**: Low

**Target State**: 
- **Target Framework**: net10.0
- **Package Updates**: None required

**Migration Steps**:

1. **Prerequisites**  
   - SharedModels has no dependencies, so no prerequisite projects
   - Ensure .NET 10.0 SDK installed

2. **Framework Update**  
   - Update `SportsClub.SharedModels.csproj`:
     - Change `<TargetFramework>net9.0</TargetFramework>` to `<TargetFramework>net10.0</TargetFramework>`

3. **Package Updates**  
   - No explicit package references to update

4. **Expected Breaking Changes**  
   - None identified in assessment
   - Simple class library with data models

5. **Code Modifications**  
   - No code changes expected
   - Verify implicit usings are still appropriate
   - Confirm nullable reference types still function correctly

6. **Testing Strategy**  
   - Build project to confirm compilation
   - Verify used by dependent projects (Api, Web) without issues
   - No unit test project identified for SharedModels

7. **Validation Checklist**  
   - [ ] Project file updated to net10.0
   - [ ] Project builds without errors
   - [ ] Project builds without warnings
   - [ ] Dependent projects (Api, Web) still reference correctly

### SportsClub.Api

**Current State**: 
- **Target Framework**: net9.0
- **Project Type**: ASP.NET Core Web API
- **Lines of Code**: 568
- **Dependencies**: 1 (SportsClub.SharedModels)
- **Dependants**: None (top-level application)
- **Packages**: 1 explicit (Microsoft.AspNetCore.OpenApi)
- **Risk Level**: Low

**Target State**: 
- **Target Framework**: net10.0
- **Package Updates**: 1 package upgrade

**Migration Steps**:

1. **Prerequisites**  
   - Depends on SportsClub.SharedModels (will be upgraded simultaneously)
   - Ensure .NET 10.0 SDK installed

2. **Framework Update**  
   - Update `SportsClub.Api.csproj`:
     - Change `<TargetFramework>net9.0</TargetFramework>` to `<TargetFramework>net10.0</TargetFramework>`

3. **Package Updates**  
   - Update Microsoft.AspNetCore.OpenApi: 9.0.1 → 10.0.2
   - Edit `SportsClub.Api.csproj`:
     - Change `<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.1" />` to `Version="10.0.2"`

4. **Expected Breaking Changes**  
   - No breaking changes identified in assessment
   - Microsoft.AspNetCore.OpenApi 10.0.2 is a minor version upgrade
   - Standard ASP.NET Core API patterns remain compatible

5. **Code Modifications**  
   - No code changes expected
   - Verify API endpoints still function
   - Confirm OpenAPI/Swagger generation works
   - Review any middleware configuration

6. **Testing Strategy**  
   - Build project to confirm compilation
   - Run API application and test endpoints
   - Verify OpenAPI/Swagger UI loads correctly
   - Test API requests/responses
   - No test project identified in assessment for Api

7. **Validation Checklist**  
   - [ ] Project file updated to net10.0
   - [ ] Microsoft.AspNetCore.OpenApi updated to 10.0.2
   - [ ] dotnet restore succeeds
   - [ ] Project builds without errors
   - [ ] Project builds without warnings
   - [ ] API application starts successfully
   - [ ] OpenAPI/Swagger endpoint accessible
   - [ ] Sample API requests return expected responses

### SportsClub.Web

**Current State**: 
- **Target Framework**: net9.0
- **Project Type**: Blazor WebAssembly
- **Lines of Code**: 51
- **Dependencies**: 1 (SportsClub.SharedModels)
- **Dependants**: None (top-level application)
- **Packages**: 2 explicit (Components.WebAssembly, Components.WebAssembly.DevServer)
- **API Issues**: 3 behavioral changes (System.Uri)
- **Risk Level**: Low

**Target State**: 
- **Target Framework**: net10.0
- **Package Updates**: 2 package upgrades

**Migration Steps**:

1. **Prerequisites**  
   - Depends on SportsClub.SharedModels (will be upgraded simultaneously)
   - Ensure .NET 10.0 SDK installed
   - Ensure compatible browser for WebAssembly testing

2. **Framework Update**  
   - Update `SportsClub.Web.csproj`:
     - Change `<TargetFramework>net9.0</TargetFramework>` to `<TargetFramework>net10.0</TargetFramework>`

3. **Package Updates**  
   - Update Microsoft.AspNetCore.Components.WebAssembly: 9.0.1 → 10.0.2
   - Update Microsoft.AspNetCore.Components.WebAssembly.DevServer: 9.0.1 → 10.0.2
   - Edit `SportsClub.Web.csproj`:
     - Change `<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="9.0.1" />` to `Version="10.0.2"`
     - Change `<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="9.0.1" />` to `Version="10.0.2"`

4. **Expected Breaking Changes**  
   - **System.Uri Behavioral Changes** (3 occurrences in Program.cs, line 8)
     - Location: `SportsClub.Web\Program.cs:8`
     - Code: `builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7019/") });`
     - Impact: System.Uri constructor and type behavior changes in .NET 10
     - Action Required: Review and test URI handling after upgrade

5. **Code Modifications**  
   - **Program.cs Review** (line 8):
     - Current code uses `new Uri("https://localhost:7019/")`
     - Verify URI parsing behavior remains correct
     - Test HttpClient base address configuration
     - Confirm trailing slash handling if relevant

   - **No immediate code changes expected**, but validation required:
     - System.Uri behavioral changes are typically refinements/bug fixes
     - Well-formed URIs (like "https://localhost:7019/") usually unaffected
     - Issues more likely with edge cases (malformed URIs, relative paths)

6. **Testing Strategy**  
   - Build project to confirm compilation
   - Run Blazor WebAssembly application
   - **Critical: Test HttpClient API calls**:
     - Verify base address configuration works
     - Test HTTP requests to API (SportsClub.Api)
     - Confirm navigation and routing work correctly
   - Test all Blazor components load
   - Verify WebAssembly runtime initialization
   - Browser console: check for JavaScript/WASM errors
   - No test project identified in assessment for Web

7. **Validation Checklist**  
   - [ ] Project file updated to net10.0
   - [ ] Microsoft.AspNetCore.Components.WebAssembly updated to 10.0.2
   - [ ] Microsoft.AspNetCore.Components.WebAssembly.DevServer updated to 10.0.2
   - [ ] dotnet restore succeeds
   - [ ] Project builds without errors
   - [ ] Project builds without warnings
   - [ ] Blazor WASM application starts successfully
   - [ ] Application loads in browser without errors
   - [ ] HttpClient base address correctly configured (Program.cs:8)
   - [ ] API calls to SportsClub.Api succeed
   - [ ] Navigation between pages works
   - [ ] No console errors related to URI handling

---

## Package Update Reference

### Common Package Updates

All packages are Microsoft ASP.NET Core packages aligned to .NET 10.0.2:

| Package | Current | Target | Projects Affected | Update Reason |
|---------|---------|--------|-------------------|---------------|
| Microsoft.AspNetCore.OpenApi | 9.0.1 | 10.0.2 | 1 (Api) | Framework compatibility |
| Microsoft.AspNetCore.Components.WebAssembly | 9.0.1 | 10.0.2 | 1 (Web) | Framework compatibility |
| Microsoft.AspNetCore.Components.WebAssembly.DevServer | 9.0.1 | 10.0.2 | 1 (Web) | Framework compatibility |

### Package Update Strategy

**All-At-Once Approach**: All 3 packages will be updated simultaneously in the atomic upgrade operation.

**Version Alignment**: All packages target 10.0.2, ensuring consistency across the solution.

**No Conflicts Expected**: 
- All packages are from Microsoft.AspNetCore.* family
- All target the same version (10.0.2)
- No third-party package dependencies
- No version conflicts anticipated

### Package Details by Project

#### SportsClub.SharedModels
**No package updates** - This project has no explicit package references.

#### SportsClub.Api
**1 package update**:
- Microsoft.AspNetCore.OpenApi: 9.0.1 → 10.0.2
  - Purpose: OpenAPI/Swagger document generation
  - Breaking Changes: None identified
  - Validation: Verify Swagger UI loads after upgrade

#### SportsClub.Web
**2 package updates**:
- Microsoft.AspNetCore.Components.WebAssembly: 9.0.1 → 10.0.2
  - Purpose: Core Blazor WebAssembly runtime
  - Breaking Changes: None identified at package level
  - Validation: Verify WASM app loads and runs

- Microsoft.AspNetCore.Components.WebAssembly.DevServer: 9.0.1 → 10.0.2
  - Purpose: Development server for local debugging
  - Breaking Changes: None identified
  - Validation: Verify `dotnet run` works in development

### Package Update Execution

All package updates will be performed in **Step 2** of the Atomic Upgrade phase:

1. Edit `SportsClub.Api.csproj`: Update Microsoft.AspNetCore.OpenApi to 10.0.2
2. Edit `SportsClub.Web.csproj`: Update both Components.WebAssembly packages to 10.0.2
3. Run `dotnet restore` for entire solution
4. Verify no package conflicts or restore errors

---

## Breaking Changes Catalog

### Framework Breaking Changes

**No binary or source incompatible breaking changes identified** in the assessment for .NET 9 → .NET 10 upgrade.

### Behavioral Changes

#### System.Uri API Behavioral Changes

**Affected Project**: SportsClub.Web  
**Severity**: Low (Potential)  
**Occurrences**: 3 (all in Program.cs, line 8)

**Location**:
```csharp
// File: SportsClub.Web\Program.cs, Line 8
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7019/") });
```

**Affected APIs**:
- `System.Uri` (type)
- `System.Uri(string)` (constructor)

**Description**:  
.NET 10 includes behavioral refinements to System.Uri parsing, construction, and normalization. These are typically bug fixes or edge case improvements rather than breaking changes for well-formed URIs.

**Impact Assessment**:  
- **Low impact expected**: The code uses a well-formed absolute HTTPS URI
- **No code changes required**: URI format is standard and valid
- **Validation required**: Runtime testing to confirm HttpClient base address works

**Action Items**:
1. **No immediate code changes** - URI is well-formed
2. **Test after upgrade**:
   - Verify HttpClient initialization succeeds
   - Test API calls using this HttpClient instance
   - Confirm base address applied correctly to requests
3. **Monitor** for unexpected behavior in:
   - URL resolution
   - Relative path handling
   - Query string parsing

**Mitigation**:  
If issues arise (unlikely):
- Review .NET 10 Uri behavioral change documentation
- Consider alternative URI construction (UriBuilder, string interpolation)
- Add explicit URI validation

### Package Breaking Changes

**No breaking changes identified** for package upgrades:

- **Microsoft.AspNetCore.OpenApi**: 9.0.1 → 10.0.2
  - Minor version upgrade within .NET release cycle
  - API surface remains stable
  - No documented breaking changes

- **Microsoft.AspNetCore.Components.WebAssembly**: 9.0.1 → 10.0.2
  - Minor version upgrade within .NET release cycle
  - Blazor component model remains stable
  - No documented breaking changes

- **Microsoft.AspNetCore.Components.WebAssembly.DevServer**: 9.0.1 → 10.0.2
  - Development tooling package
  - No breaking changes expected

### Expected Compilation Errors

**None anticipated**. The assessment shows:
- 0 binary incompatible changes
- 0 source incompatible changes
- Only behavioral changes (runtime, not compile-time)

### Configuration Changes

**None required**. The solution does not require:
- global.json modifications (no SDK version pinning identified)
- appsettings.json updates
- Middleware configuration changes
- Startup/Program.cs changes (beyond existing code)

### Summary

**Total Breaking Changes**: 0 (zero)  
**Behavioral Changes**: 3 (System.Uri in Web project, low impact)  
**Code Modifications Required**: 0 (zero)  
**Validation Required**: Yes (runtime testing of System.Uri usage)

---

## Risk Management

### Overall Risk Assessment: Low

All three projects are rated Low difficulty with no high-risk factors identified.

### Risk Factors by Project

| Project | Risk Level | Description | Mitigation |
|---------|-----------|-------------|------------|
| SportsClub.SharedModels | 🟢 Low | Simple class library, no packages, 47 LOC | Straightforward TargetFramework update only |
| SportsClub.Api | 🟢 Low | ASP.NET Core API, 1 package update, 568 LOC | Standard package upgrade pattern |
| SportsClub.Web | 🟢 Low | Blazor WASM, 2 package updates, 3 behavioral changes | Review System.Uri usage, test after upgrade |

### Specific Risk Areas

#### Behavioral Changes in SportsClub.Web
**Risk**: System.Uri API behavioral changes (3 occurrences)  
**Impact**: Runtime behavior changes that may affect URL handling  
**Mitigation**: 
- Review all System.Uri constructor and method usages
- Test URL parsing and manipulation functionality
- Validate navigation and HTTP client requests

**Details**: The assessment identified behavioral changes in System.Uri APIs. These are low-severity changes that require runtime validation rather than code modifications.

### Security Vulnerabilities

**None identified** - No security vulnerabilities found in current packages.

### Contingency Plans

**If build fails after framework update**:
- Review compilation errors systematically
- Check for missing using directives
- Verify package restore completed successfully
- Consult .NET 10 breaking changes documentation

**If behavioral changes cause issues**:
- Isolate affected System.Uri usage
- Implement URI validation tests
- Consider alternative URI construction patterns if needed

**If package updates cause conflicts**:
- All packages are Microsoft.AspNetCore.* packages aligned to same version (10.0.2)
- Conflicts are unlikely; if they occur, ensure all ASP.NET Core packages use 10.0.2

### Rollback Strategy

**Branch-based rollback**:
- All work performed on `upgrade-to-NET10` branch
- Original `main` branch remains unchanged
- Can abandon branch if critical issues arise

**Commit-based rollback**:
- Single atomic commit for entire upgrade
- Can revert commit if issues discovered post-merge

---

## Testing & Validation Strategy

### Testing Approach

**No automated test projects identified** in the assessment. Testing will focus on manual validation and smoke testing.

### Phase-by-Phase Testing

#### Phase 0: Prerequisites
**Validation**:
- [ ] Verify .NET 10.0 SDK installed: `dotnet --list-sdks` (should show 10.0.x)
- [ ] Verify branch switched to `upgrade-to-NET10`

#### Phase 1: Atomic Upgrade
**Build Validation** (after all project/package updates):
- [ ] `dotnet restore` succeeds for entire solution
- [ ] `dotnet build` succeeds with 0 errors
- [ ] `dotnet build` produces 0 warnings (or only expected warnings)
- [ ] All 3 projects compile successfully

**Post-Build Checks**:
- [ ] Verify bin/obj folders contain net10.0 artifacts (not net9.0)
- [ ] Check for any missing dependencies or package conflicts

#### Phase 2: Validation
**Application-Level Testing**:

**SportsClub.SharedModels**:
- [ ] Project builds without errors (validated as part of solution build)
- [ ] Referenced successfully by Api and Web projects
- [ ] No runtime model serialization issues

**SportsClub.Api**:
- [ ] Run API: `dotnet run --project SportsClub.Api`
- [ ] API starts without exceptions
- [ ] Swagger UI loads: `https://localhost:7019/swagger` (or configured port)
- [ ] OpenAPI document generates correctly
- [ ] Test sample API endpoints:
  - GET requests return expected data
  - POST requests accept and process data
  - Response serialization works (using SharedModels)
- [ ] No console errors or warnings

**SportsClub.Web**:
- [ ] Run Blazor WASM: `dotnet run --project SportsClub.Web`
- [ ] Application starts without exceptions
- [ ] Browser loads application successfully
- [ ] **Critical: HttpClient base address configuration** (Program.cs:8)
  - [ ] No URI-related errors in browser console
  - [ ] HttpClient initializes correctly
- [ ] Test API integration:
  - [ ] HTTP calls to SportsClub.Api succeed
  - [ ] Data fetched from API displays correctly
  - [ ] SharedModels deserialize properly
- [ ] Navigation between pages works
- [ ] Browser console shows no errors
- [ ] WebAssembly runtime loads without issues

### Smoke Tests (Manual)

**End-to-End Scenario**:
1. Start SportsClub.Api (`dotnet run`)
2. Start SportsClub.Web (`dotnet run`)
3. Navigate to Web application in browser
4. Perform user action that calls API
5. Verify data flow: Web → API → SharedModels → response → Web display
6. Check browser console and API console for errors

**System.Uri Behavioral Validation**:
1. Navigate to Web application
2. Trigger any API call (uses HttpClient with Uri base address)
3. Verify request completes successfully
4. Check browser Network tab: requests go to correct URL
5. Confirm no URI-related exceptions

### Performance Validation

**Optional** (no performance issues expected for minor version upgrade):
- Compare application startup time (net9.0 vs net10.0)
- Check memory usage (no significant changes expected)
- Verify WebAssembly bundle size (should be similar or improved)

### Comprehensive Validation Checklist

#### Build Validation
- [ ] Solution restores successfully
- [ ] Solution builds with 0 errors
- [ ] Solution builds with 0 warnings
- [ ] All projects target net10.0
- [ ] All packages updated to 10.0.2

#### Runtime Validation
- [ ] SportsClub.Api starts and serves requests
- [ ] SportsClub.Web loads in browser
- [ ] HttpClient configuration works (System.Uri)
- [ ] API integration functions correctly
- [ ] No console errors in API or browser

#### Functional Validation
- [ ] User can navigate Web application
- [ ] Data fetches from API successfully
- [ ] SharedModels serialize/deserialize correctly
- [ ] Swagger UI accessible and functional

#### Quality Validation
- [ ] No new compiler warnings
- [ ] No security vulnerabilities (verified post-upgrade)
- [ ] No package dependency conflicts

### Testing Exit Criteria

Testing is complete when:
1. All build validation checks pass
2. All runtime validation checks pass
3. All functional validation checks pass
4. No blocking issues identified
5. System.Uri behavioral changes confirmed non-impacting

---

## Complexity & Effort Assessment

### Per-Project Complexity

| Project | Complexity | LOC | Dependencies | Packages | Risk Factors |
|---------|-----------|-----|--------------|----------|-------------|
| SportsClub.SharedModels | Low | 47 | 0 | 0 | None |
| SportsClub.Api | Low | 568 | 1 | 1 | None |
| SportsClub.Web | Low | 51 | 1 | 2 | 3 behavioral changes |
| **Total** | **Low** | **666** | **1 level** | **3** | **Minimal** |

### Phase Complexity Assessment

**Phase 0: Prerequisites** - Low
- Verify .NET 10.0 SDK installed (1 command)
- No global.json modifications needed (assessment shows no global.json)

**Phase 1: Atomic Upgrade** - Low
- 3 project file edits (TargetFramework property change)
- 3 package version updates (all straightforward 9.0.1 → 10.0.2)
- Single solution restore and build
- Minimal expected compilation errors
- Focus area: System.Uri behavioral changes review

**Phase 2: Validation** - Low
- Build verification (automated)
- No test projects identified in assessment
- Manual smoke testing of Api and Web projects

### Dependency Ordering

**Level 0 (Foundation)**: SportsClub.SharedModels  
- Complexity: Low
- Effort: Minimal (TargetFramework update only)

**Level 1 (Applications)**: SportsClub.Api, SportsClub.Web  
- Complexity: Low
- Effort: Low (TargetFramework + package updates)
- Built after SharedModels automatically

### Resource Requirements

**Skills Required**:
- Basic understanding of .NET project structure
- Familiarity with NuGet package management
- Understanding of Blazor WebAssembly (for validation)

**Parallel Execution Capacity**:
- Not applicable (All-At-Once strategy means single atomic operation)
- Build system will parallelize where possible automatically

**Tooling**:
- .NET 10.0 SDK
- Visual Studio 2022 (17.12+) or VS Code with C# extension
- Git for version control

### Overall Assessment

**Total Complexity**: Low  
**Approach**: All-At-Once upgrade is appropriate and low-risk  
**Estimated Scope**: Small (666 LOC, 3 projects, 3 packages, 1 dependency level)  
**Key Success Factor**: Thorough testing of System.Uri usage in Web project

---

## Source Control Strategy

### Branching Strategy

**Branch Structure**:
- **Main Branch**: `main` (remains untouched, stable production state)
- **Upgrade Branch**: `upgrade-to-NET10` (all upgrade work performed here)

**Branch Workflow**:
1. Create upgrade branch from `main`: ✅ **Already created**
2. Perform all upgrade work on `upgrade-to-NET10`
3. Test and validate on upgrade branch
4. Merge to `main` only after full validation

**Protection**: Original `main` branch remains unchanged until upgrade is validated and approved.

### Commit Strategy

**Recommended: Single Atomic Commit**

Given the All-At-Once strategy and small scope, use **one comprehensive commit** for the entire upgrade:

**Single Commit Structure**:
```
Upgrade solution to .NET 10.0

- Update all projects from net9.0 to net10.0
- Update Microsoft.AspNetCore.OpenApi 9.0.1 → 10.0.2 (Api)
- Update Microsoft.AspNetCore.Components.WebAssembly 9.0.1 → 10.0.2 (Web)
- Update Microsoft.AspNetCore.Components.WebAssembly.DevServer 9.0.1 → 10.0.2 (Web)
- Verified: All builds succeed, applications run correctly
- Validated: System.Uri behavioral changes non-impacting
```

**Files Changed** (3 project files):
- `SportsClub.SharedModels/SportsClub.SharedModels.csproj`
- `SportsClub.Api/SportsClub.Api.csproj`
- `SportsClub.Web/SportsClub.Web.csproj`

**Alternative: Multi-Commit Approach** (if preferred):
1. Commit: "Update project target frameworks to net10.0"
2. Commit: "Update NuGet packages to .NET 10 versions"
3. Commit: "Validate and fix any compilation issues" (if any arise)

**Recommended**: Single commit for simplicity and atomic rollback capability.

### Commit Message Format

Use clear, structured commit messages:

**Format**:
```
<Action>: <Summary>

<Detailed changes>
- <Change 1>
- <Change 2>

<Validation performed>
```

**Example**:
```
Upgrade: Migrate solution to .NET 10.0 LTS

Updated all projects and packages to .NET 10.0:
- All projects: net9.0 → net10.0
- Microsoft.AspNetCore.OpenApi: 9.0.1 → 10.0.2
- Microsoft.AspNetCore.Components.WebAssembly: 9.0.1 → 10.0.2
- Microsoft.AspNetCore.Components.WebAssembly.DevServer: 9.0.1 → 10.0.2

Validation:
- Solution builds with 0 errors
- Api and Web applications start successfully
- System.Uri behavioral changes verified non-impacting
- All smoke tests passing
```

### Review and Merge Process

**Pull Request Requirements**:
1. **Create PR**: `upgrade-to-NET10` → `main`
2. **PR Title**: "Upgrade solution to .NET 10.0 LTS"
3. **PR Description**: Include:
   - Summary of changes (projects, packages)
   - Validation performed (build, runtime, functional)
   - Breaking changes assessment (none identified)
   - Testing results (all passing)

**Review Checklist**:
- [ ] All 3 project files updated correctly
- [ ] Package versions aligned (all 10.0.2)
- [ ] No unintended file changes
- [ ] Commit message clear and complete
- [ ] Build succeeds on upgrade branch
- [ ] Applications tested and functional

**Merge Criteria**:
- All builds pass ✅
- All tests pass ✅
- Applications run correctly ✅
- No blocking issues ✅
- Code review approved ✅

**Merge Method**: 
- **Squash merge** (recommended) - Clean history with single commit on main
- **Merge commit** (alternative) - Preserves upgrade branch history

### Rollback Plan

**If issues found before merge**:
- Abandon `upgrade-to-NET10` branch
- Create new branch from `main` to retry
- Or fix issues on upgrade branch and re-test

**If issues found after merge**:
- **Revert commit**: `git revert <commit-hash>` (safest)
- **Hard reset** (if no subsequent commits): `git reset --hard HEAD~1`
- Redeploy from `main` branch pre-upgrade state

### Git Workflow Summary

```bash
# Current state (already completed):
git checkout main
git pull origin main
git checkout -b upgrade-to-NET10

# After upgrade work completed:
git add SportsClub.SharedModels/SportsClub.SharedModels.csproj
git add SportsClub.Api/SportsClub.Api.csproj
git add SportsClub.Web/SportsClub.Web.csproj
git commit -m "Upgrade: Migrate solution to .NET 10.0 LTS

Updated all projects and packages to .NET 10.0:
- All projects: net9.0 → net10.0
- Microsoft.AspNetCore.OpenApi: 9.0.1 → 10.0.2
- Microsoft.AspNetCore.Components.WebAssembly: 9.0.1 → 10.0.2
- Microsoft.AspNetCore.Components.WebAssembly.DevServer: 9.0.1 → 10.0.2

Validation:
- Solution builds with 0 errors
- Api and Web applications start successfully
- System.Uri behavioral changes verified non-impacting
- All smoke tests passing"

git push origin upgrade-to-NET10

# Create PR via GitHub/Azure DevOps/GitLab UI
# After PR approved:
git checkout main
git merge upgrade-to-NET10 --squash
git push origin main
```

---

## Success Criteria

The .NET 10.0 upgrade is considered **successful** when all criteria below are met.

### Technical Criteria

#### Framework Migration
- ✅ All 3 projects target `net10.0` (not net9.0)
- ✅ No projects remain on net9.0
- ✅ Project files correctly specify `<TargetFramework>net10.0</TargetFramework>`

#### Package Updates
- ✅ All identified packages updated to .NET 10 versions:
  - Microsoft.AspNetCore.OpenApi: 10.0.2 (in Api project)
  - Microsoft.AspNetCore.Components.WebAssembly: 10.0.2 (in Web project)
  - Microsoft.AspNetCore.Components.WebAssembly.DevServer: 10.0.2 (in Web project)
- ✅ No packages remain on 9.0.x versions
- ✅ `dotnet restore` completes without errors
- ✅ No package dependency conflicts

#### Build Success
- ✅ `dotnet build` succeeds for entire solution
- ✅ 0 compilation errors
- ✅ 0 compilation warnings (or only acceptable warnings documented)
- ✅ All 3 projects compile successfully:
  - SportsClub.SharedModels builds
  - SportsClub.Api builds
  - SportsClub.Web builds

#### Runtime Success
- ✅ SportsClub.Api application starts without exceptions
- ✅ SportsClub.Web application loads in browser without errors
- ✅ No runtime exceptions during startup
- ✅ Applications run on .NET 10.0 runtime (verified in logs/about pages)

#### Breaking Changes Resolution
- ✅ System.Uri behavioral changes validated:
  - HttpClient base address configuration works (Web project, Program.cs:8)
  - API calls from Web to Api succeed
  - No URI-related exceptions in browser console
- ✅ No unresolved breaking changes
- ✅ All behavioral changes tested and confirmed non-impacting

### Quality Criteria

#### Code Quality
- ✅ Code quality maintained (no workarounds or technical debt introduced)
- ✅ No new compiler warnings introduced
- ✅ Project structure remains clean and organized
- ✅ No commented-out code or debug statements added

#### Security
- ✅ No new security vulnerabilities introduced
- ✅ All packages at secure versions (10.0.2 or higher)
- ✅ No known vulnerabilities in dependency tree

#### Functionality
- ✅ All application features work as expected:
  - Api endpoints respond correctly
  - Web application pages load and render
  - Api ↔ Web communication functions
  - Data serialization/deserialization works (SharedModels)
- ✅ User workflows complete successfully (smoke tests pass)
- ✅ No functional regressions identified

### Process Criteria

#### All-At-Once Strategy Followed
- ✅ All projects updated simultaneously (no incremental phases)
- ✅ No multi-targeting complexity introduced
- ✅ Single atomic upgrade operation completed
- ✅ Dependency ordering respected (SharedModels → Api, Web)

#### Source Control
- ✅ All work performed on `upgrade-to-NET10` branch
- ✅ Commit(s) include clear, complete messages
- ✅ Only intended files modified (3 .csproj files)
- ✅ No unintended changes committed
- ✅ Pull request created and reviewed (if applicable)
- ✅ Merged to `main` after full validation

#### Documentation
- ✅ Assessment document reviewed (assessment.md)
- ✅ Plan document completed (plan.md) ✅ **Complete**
- ✅ Commit messages document changes clearly
- ✅ Any issues encountered documented for future reference

#### Validation
- ✅ All testing checklist items completed
- ✅ Build validation passed
- ✅ Runtime validation passed
- ✅ Functional validation passed
- ✅ No blocking issues remain

### Sign-Off Checklist

Before considering the upgrade complete:

- [ ] **Technical Lead**: All technical criteria met
- [ ] **QA/Testing**: All validation tests passed
- [ ] **Security**: No new vulnerabilities introduced
- [ ] **DevOps**: Build and deployment process updated (if needed)
- [ ] **Documentation**: Upgrade documented in commit history
- [ ] **Stakeholders**: Informed of successful upgrade

### Definition of Done

**The upgrade is DONE when**:
1. All projects run on .NET 10.0 ✅
2. All packages updated to .NET 10 versions ✅
3. Solution builds with 0 errors ✅
4. Applications function correctly ✅
5. All tests pass (manual validation) ✅
6. No security vulnerabilities ✅
7. Code merged to `main` branch ✅
8. Team notified and documentation complete ✅

---

## Upgrade Execution Steps

### Implementation Timeline

#### Phase 0: Prerequisites (if needed)
**Estimated Time**: 5 minutes  
**Operations**:
- Verify .NET 10.0 SDK installed: `dotnet --list-sdks`

**Deliverables**: SDK verified

#### Phase 1: Atomic Upgrade
**Operations** (performed as single coordinated batch):
1. **Update all project files to net10.0**:
   - Edit `SportsClub.SharedModels\SportsClub.SharedModels.csproj`: Change TargetFramework to net10.0
   - Edit `SportsClub.Api\SportsClub.Api.csproj`: Change TargetFramework to net10.0
   - Edit `SportsClub.Web\SportsClub.Web.csproj`: Change TargetFramework to net10.0

2. **Update all package references**:
   - Edit `SportsClub.Api\SportsClub.Api.csproj`: Update Microsoft.AspNetCore.OpenApi to 10.0.2
   - Edit `SportsClub.Web\SportsClub.Web.csproj`: 
     - Update Microsoft.AspNetCore.Components.WebAssembly to 10.0.2
     - Update Microsoft.AspNetCore.Components.WebAssembly.DevServer to 10.0.2

3. **Restore dependencies**:
   - Run `dotnet restore` at solution level

4. **Build solution and fix all compilation errors**:
   - Run `dotnet build`
   - Address any compilation errors (none expected)
   - Focus on System.Uri behavioral changes if issues arise

5. **Verify**:
   - Solution builds with 0 errors
   - All 3 projects compile successfully

**Deliverables**: Solution builds with 0 errors

#### Phase 2: Validation
**Operations**:
1. **Execute smoke tests**:
   - Start SportsClub.Api and verify endpoints
   - Start SportsClub.Web and verify application loads
   - Test Api ↔ Web integration
   - Validate System.Uri usage in HttpClient (Program.cs:8)

2. **Manual validation**:
   - Verify all functional scenarios work
   - Check browser console for errors
   - Confirm no regressions

**Deliverables**: All tests pass, applications function correctly

#### Phase 3: Source Control
**Operations**:
1. **Commit changes**:
   - Stage 3 modified .csproj files
   - Create atomic commit with comprehensive message
   - Push to `upgrade-to-NET10` branch

2. **Create Pull Request**:
   - PR: `upgrade-to-NET10` → `main`
   - Include validation results in PR description

3. **Merge after approval**:
   - Squash merge to `main`
   - Tag release (optional): `v1.0.0-net10`

**Deliverables**: Changes merged to main, team notified

---

**Plan Generation Complete**: Ready for execution stage.
