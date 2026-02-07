# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v10.0.

## Table of Contents

- [Executive Summary](#executive-Summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
- [Aggregate NuGet packages details](#aggregate-nuget-packages-details)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Projects Relationship Graph](#projects-relationship-graph)
- [Project Details](#project-details)

  - [SportsClub.Api\SportsClub.Api.csproj](#sportsclubapisportsclubapicsproj)
  - [SportsClub.SharedModels\SportsClub.SharedModels.csproj](#sportsclubsharedmodelssportsclubsharedmodelscsproj)
  - [SportsClub.Web\SportsClub.Web.csproj](#sportsclubwebsportsclubwebcsproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 3 | All require upgrade |
| Total NuGet Packages | 3 | All packages need upgrade |
| Total Code Files | 16 |  |
| Total Code Files with Incidents | 4 |  |
| Total Lines of Code | 666 |  |
| Total Number of Issues | 9 |  |
| Estimated LOC to modify | 3+ | at least 0.5% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| [SportsClub.Api\SportsClub.Api.csproj](#sportsclubapisportsclubapicsproj) | net9.0 | 🟢 Low | 1 | 0 |  | AspNetCore, Sdk Style = True |
| [SportsClub.SharedModels\SportsClub.SharedModels.csproj](#sportsclubsharedmodelssportsclubsharedmodelscsproj) | net9.0 | 🟢 Low | 0 | 0 |  | ClassLibrary, Sdk Style = True |
| [SportsClub.Web\SportsClub.Web.csproj](#sportsclubwebsportsclubwebcsproj) | net9.0 | 🟢 Low | 2 | 3 | 3+ | AspNetCore, Sdk Style = True |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 0 | 0.0% |
| ⚠️ Incompatible | 0 | 0.0% |
| 🔄 Upgrade Recommended | 3 | 100.0% |
| ***Total NuGet Packages*** | ***3*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 3 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 1624 |  |
| ***Total APIs Analyzed*** | ***1627*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| Microsoft.AspNetCore.Components.WebAssembly | 9.0.1 | 10.0.2 | [SportsClub.Web.csproj](#sportsclubwebsportsclubwebcsproj) | NuGet package upgrade is recommended |
| Microsoft.AspNetCore.Components.WebAssembly.DevServer | 9.0.1 | 10.0.2 | [SportsClub.Web.csproj](#sportsclubwebsportsclubwebcsproj) | NuGet package upgrade is recommended |
| Microsoft.AspNetCore.OpenApi | 9.0.1 | 10.0.2 | [SportsClub.Api.csproj](#sportsclubapisportsclubapicsproj) | NuGet package upgrade is recommended |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |
| T:System.Uri | 2 | 66.7% | Behavioral Change |
| M:System.Uri.#ctor(System.String) | 1 | 33.3% | Behavioral Change |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>📦&nbsp;SportsClub.Web.csproj</b><br/><small>net9.0</small>"]
    P2["<b>📦&nbsp;SportsClub.SharedModels.csproj</b><br/><small>net9.0</small>"]
    P3["<b>📦&nbsp;SportsClub.Api.csproj</b><br/><small>net9.0</small>"]
    P1 --> P2
    P3 --> P2
    click P1 "#sportsclubwebsportsclubwebcsproj"
    click P2 "#sportsclubsharedmodelssportsclubsharedmodelscsproj"
    click P3 "#sportsclubapisportsclubapicsproj"

```

## Project Details

<a id="sportsclubapisportsclubapicsproj"></a>
### SportsClub.Api\SportsClub.Api.csproj

#### Project Info

- **Current Target Framework:** net9.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** AspNetCore
- **Dependencies**: 1
- **Dependants**: 0
- **Number of Files**: 13
- **Number of Files with Incidents**: 1
- **Lines of Code**: 568
- **Estimated LOC to modify**: 0+ (at least 0.0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["SportsClub.Api.csproj"]
        MAIN["<b>📦&nbsp;SportsClub.Api.csproj</b><br/><small>net9.0</small>"]
        click MAIN "#sportsclubapisportsclubapicsproj"
    end
    subgraph downstream["Dependencies (1"]
        P2["<b>📦&nbsp;SportsClub.SharedModels.csproj</b><br/><small>net9.0</small>"]
        click P2 "#sportsclubsharedmodelssportsclubsharedmodelscsproj"
    end
    MAIN --> P2

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 318 |  |
| ***Total APIs Analyzed*** | ***318*** |  |

<a id="sportsclubsharedmodelssportsclubsharedmodelscsproj"></a>
### SportsClub.SharedModels\SportsClub.SharedModels.csproj

#### Project Info

- **Current Target Framework:** net9.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 0
- **Dependants**: 2
- **Number of Files**: 2
- **Number of Files with Incidents**: 1
- **Lines of Code**: 47
- **Estimated LOC to modify**: 0+ (at least 0.0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (2)"]
        P1["<b>📦&nbsp;SportsClub.Web.csproj</b><br/><small>net9.0</small>"]
        P3["<b>📦&nbsp;SportsClub.Api.csproj</b><br/><small>net9.0</small>"]
        click P1 "#sportsclubwebsportsclubwebcsproj"
        click P3 "#sportsclubapisportsclubapicsproj"
    end
    subgraph current["SportsClub.SharedModels.csproj"]
        MAIN["<b>📦&nbsp;SportsClub.SharedModels.csproj</b><br/><small>net9.0</small>"]
        click MAIN "#sportsclubsharedmodelssportsclubsharedmodelscsproj"
    end
    P1 --> MAIN
    P3 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 110 |  |
| ***Total APIs Analyzed*** | ***110*** |  |

<a id="sportsclubwebsportsclubwebcsproj"></a>
### SportsClub.Web\SportsClub.Web.csproj

#### Project Info

- **Current Target Framework:** net9.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** AspNetCore
- **Dependencies**: 1
- **Dependants**: 0
- **Number of Files**: 22
- **Number of Files with Incidents**: 2
- **Lines of Code**: 51
- **Estimated LOC to modify**: 3+ (at least 5.9% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["SportsClub.Web.csproj"]
        MAIN["<b>📦&nbsp;SportsClub.Web.csproj</b><br/><small>net9.0</small>"]
        click MAIN "#sportsclubwebsportsclubwebcsproj"
    end
    subgraph downstream["Dependencies (1"]
        P2["<b>📦&nbsp;SportsClub.SharedModels.csproj</b><br/><small>net9.0</small>"]
        click P2 "#sportsclubsharedmodelssportsclubsharedmodelscsproj"
    end
    MAIN --> P2

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 3 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 1196 |  |
| ***Total APIs Analyzed*** | ***1199*** |  |

