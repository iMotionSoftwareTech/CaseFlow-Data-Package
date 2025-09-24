# CaseFlow-Data-Package
The CaseFlow data package is a class-library that acts as the data layer for the CaseFlow Apps to comunicate with the SQL Database

## 📌 Versioning Guide

We follow [Semantic Versioning (SemVer)](https://semver.org/):


### ✅ When to bump

- **PATCH (x.y.Z)**  
  - Bug fixes  
  - Small internal changes (no new features, no breaking changes)  
  - Example: `1.0.0 → 1.0.1`

- **MINOR (x.Y.z)**  
  - Add new repository methods (backward-compatible)  
  - Add new static constants / parameters (non-breaking)  
  - Improvements that don’t break existing APIs  
  - Example: `1.0.0 → 1.1.0`

- **MAJOR (X.y.z)**  
  - Breaking changes to interfaces/contracts  
  - Removing or renaming methods, classes, or parameters  
  - Big refactors that change expected usage  
  - Example: `1.0.0 → 2.0.0`

---

### 🚀 Release process

1. Merge changes into **main**.  
2. Decide the correct version bump (PATCH, MINOR, MAJOR).  
3. Create a **tag** on the latest `main` commit:  
   - With **GitHub Desktop**:  
     - Switch to `main`  
     - Right-click the commit in *History* → **Create Tag…**  
   - With **Git CLI**:  
     ```bash
     git checkout main
     git pull
     git tag v1.2.0
     git push origin v1.2.0
     ```
   - With **GitHub UI**:  
     - Go to *Releases* → **Draft a new release**  
     - Choose `main` as the target  
     - Enter a tag (e.g. `v1.2.0`) → **Publish release**  
4. Pushing the tag triggers the **release workflow**:  
   - Runs tests  
   - Packs with that version  
   - Publishes to GitHub Packages  
   - Creates a GitHub Release (marked as pre-release if version contains `-alpha`, `-beta`, or `-rc`)

---

### 🌱 Pre-release versions

Use suffixes while experimenting:  
- `0.1.0-alpha.1` → very early testing  
- `0.1.0-beta.1` → feature complete, needs feedback  
- `1.0.0-rc.1` → nearly stable release candidate  

Remove the suffix for stable:  
- `1.0.0`

# 🏷️ Tag & Release Cheat Sheet

Quick reference for cutting releases in this repo.

---

## 🔹 Create a release tag (Git CLI)

```bash
# Make sure you're on main and up to date
git checkout main
git pull

# Create a tag (replace version as needed)
git tag v0.1.0-alpha.1

# Push the tag to GitHub (triggers release workflow)
git push origin v0.1.0-alpha.1
