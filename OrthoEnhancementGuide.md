# Ortho Module Enhancement Guide (A to Z)

## 1. Data Model & Relationships

### Current Schema
| Table | PK | Key Columns | Relationship |
|-------|-----|-------------|--------------|
| **OrthoInf** | OrthoID | PatientID | Patient info (currently allows multiple per patient) |
| **OrthoDiag** | DiagID | OrthoID, PatientID | Diagnoses (many per patient) |
| **OrthoTreat** | TreatID | OrthoID, PatientID | Treatment (currently links via OrthoID) |
| **OrthoTrtDet** | DetID | OrthoID, PatientID | Steps (currently links via OrthoID) |

### Your Desired Model
```
Patient (1) ──► OrthoInf (1)        ← Patient info, ONE per patient
                    │
                    └──► OrthoDiag (many)   ← Diagnoses, Add/Edit/Delete
                              │
                              ├──► OrthoTreat (1)   ← Treatment plan, ONE per diagnosis
                              │         │
                              │         └── Add hidden after first; Edit/Delete visible
                              │
                              └──► OrthoTrtDet (many) ← Steps, always Add/Edit/Delete
```

---

## 2. Schema Change: DiagID vs OrthoID

### Recommendation: **Add DiagID** (do not rename)

**Why add instead of rename:**
- **Safer migration**: Existing data uses OrthoID. Renaming would require updating all OrthoTreat/OrthoTrtDet rows to store DiagID instead of OrthoID.
- **OrthoID currently points to OrthoInf**, not OrthoDiag. So the semantic meaning would change.
- **Cleaner**: Add `DiagID` as the new FK to OrthoDiag; keep `OrthoID` temporarily for backward compatibility, then deprecate.

### SQL Changes Required

```sql
-- 1. Add DiagID to OrthoTreat
ALTER TABLE OrthoTreat ADD DiagID INT NULL;

-- 2. Add DiagID to OrthoTrtDet  
ALTER TABLE OrthoTrtDet ADD DiagID INT NULL;

-- 3. Migrate existing data: map OrthoID (OrthoInf) to DiagID (OrthoDiag)
-- For each OrthoTreat/OrthoTrtDet, find the OrthoDiag where OrthoDiag.OrthoID = OrthoTreat.OrthoID
UPDATE t SET t.DiagID = d.DiagID
FROM OrthoTreat t
INNER JOIN OrthoDiag d ON d.OrthoID = t.OrthoID AND d.PatientID = t.PatientID;

UPDATE t SET t.DiagID = d.DiagID
FROM OrthoTrtDet t
INNER JOIN OrthoDiag d ON d.OrthoID = t.OrthoID AND d.PatientID = t.PatientID;

-- 4. Add FK (optional, after migration)
-- ALTER TABLE OrthoTreat ADD CONSTRAINT FK_OrthoTreat_OrthoDiag 
--   FOREIGN KEY (DiagID) REFERENCES OrthoDiag(DiagID);
```

### OrthoInf: One Record Per Patient

Add a unique constraint so each patient has at most one OrthoInf:

```sql
-- Unique constraint: one OrthoInf per patient
CREATE UNIQUE INDEX UX_OrthoInf_PatientID ON OrthoInf(PatientID);
```

---

## 3. Button Visibility Logic

| Table | Add | Edit | Delete | When |
|-------|-----|------|--------|------|
| **OrthoInf** | Visible only when no record exists | Visible when record exists | Visible when record exists | After add → hide Add, show Edit/Delete |
| **OrthoDiag** | Always visible | Always visible | Always visible | Three buttons always |
| **OrthoTreat** | Visible only when no treatment for selected Diag | Visible when treatment exists | Visible when treatment exists | After add → hide Add, show Edit/Delete |
| **OrthoTrtDet** | Always visible | Always visible | Always visible | Three buttons always |

---

## 4. UI Restructure: Tab-Based Layout

### Proposed Layout

**Tab 1: Patient Info (OrthoInf)**
- Full width, single tab page
- All OrthoInf controls (Complaints, Birth, Feed, etc.) in a scrollable panel
- Buttons: **Add** (when no record) OR **Edit** + **Delete** (when record exists)
- No case selector needed – one record per patient

**Tab 2: Diagnosis & Treatment (OrthoDiag + OrthoTreat + OrthoTrtDet)**
- **Top section (compact)**: OrthoDiag
  - Diag selector/combo (which diagnosis)
  - Diagnosis fields (Class, Bite, CloseType, etc.)
  - Buttons: **Add Diag**, **Edit Diag**, **Delete Diag**

- **Middle section (compact)**: OrthoTreat  
  - Treatment fields (BeginDate, OrthoType, Bracket, Fixer, etc.)
  - Buttons: **Add Treatment** (when none) OR **Edit** + **Delete** (when exists)

- **Bottom section (larger)**: OrthoTrtDet – Steps
  - Grid + Add Step form (WorkDate, WireMeasure, WireType, Notes)
  - Buttons: **Add Step**, **Edit** (grid), **Delete** (grid/navigator)
  - This section gets most space (has grid)

### Visual Sketch

```
┌─────────────────────────────────────────────────────────────┐
│  Patient: [Name] (#ID)                    [Use Classic Ortho]│
├─────────────────────────────────────────────────────────────┤
│  [Tab 1: Patient Info]  [Tab 2: Diagnosis & Treatment]      │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│  TAB 1 - Patient Info (OrthoInf)                            │
│  ┌─────────────────────────────────────────────────────┐   │
│  │ Complaints, Birth, Feed, MilkTeeth... (all fields)   │   │
│  │ [Add]  OR  [Edit] [Delete]                          │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                             │
├─────────────────────────────────────────────────────────────┤
│  TAB 2 - Diagnosis & Treatment                               │
│  ┌─ Diagnosis ─────────────────────────────────────────┐   │
│  │ Diag: [Combo]  Class/Bite/Close  [Add][Edit][Delete]  │   │
│  └─────────────────────────────────────────────────────┘   │
│  ┌─ Treatment ──────────────────────────────────────────┐   │
│  │ BeginDate, Type, Bracket, Fixer... [Add] or [E][D]   │   │
│  └─────────────────────────────────────────────────────┘   │
│  ┌─ Steps (Grid) ───────────────────────────────────────┐   │
│  │ # | WorkDate | Measure | WireType | Notes            │   │
│  │ [Add Step] [Save]  Navigator: [<<][<][>][>>][Del]    │   │
│  └─────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
```

---

## 5. Implementation Order

1. **Schema**: Add DiagID, migrate data, add unique constraint on OrthoInf(PatientID)
2. **BAL/DAL**: Add DiagID to OrthoTreat and OrthoTrtDet classes; update DATA layer
3. **UI Tab 1**: OrthoInf only – hide Add when record exists, show Edit/Delete
4. **UI Tab 2**: Restructure into Diagnosis (compact) + Treatment (compact) + Steps (grid, larger)
5. **Logic**: Wire OrthoTreat and OrthoTrtDet to selected OrthoDiag (DiagID) instead of OrthoID

---

## 6. Summary

| Question | Answer |
|----------|--------|
| Add DiagID or rename OrthoID? | **Add DiagID** to OrthoTreat and OrthoTrtDet. Safer migration. |
| OrthoInf multiple per patient? | **No.** Add unique constraint; one record per patient. |
| UI crowded? | **Two tabs**: Tab1 = OrthoInf (full). Tab2 = Diag + Treat (compact) + Steps (grid, more space). |
| Steps layout? | Keep as-is: grid + add form + navigator with delete. |
