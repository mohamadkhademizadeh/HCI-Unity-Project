# VR/AR Gaze vs. Joystick — Unity Usability Study

Compare **gaze (dwell)** vs **joystick (reticle + button)** for target selection in VR/AR. Includes:
- Unity prototype (scripts)
- Turnkey user study plan (SUS, NASA‑TLX)
- Data schema + analysis notebook (paired t‑tests, effect sizes)
- Report template for your write‑up

> Devices: Meta Quest (OpenXR) by default. Open an issue for Vive/Index/HoloLens/AR support.

## Repo Structure
```
.
├─ UnityProject/
│  └─ Assets/
│     └─ Scripts/           # C# scripts for gaze and joystick techniques
├─ analysis/
│  ├─ analysis.ipynb        # Stats, charts
│  └─ requirements.txt
├─ data/
│  └─ data_template.csv     # Column schema for logs
├─ docs/
│  ├─ study_plan.md         # Procedure, measures, analysis plan
│  └─ report_template.md    # Write‑up skeleton
└─ .github/workflows/
   └─ lint.yml              # CI for notebook lint/exec smoke test
```

## Quick Start
1. **Unity:** Create a 3D (URP optional) project using **OpenXR + XR Interaction Toolkit**.
2. Copy `UnityProject/Assets/Scripts` into your project. Add targets to a grid, attach components as described in comments.
3. Build & run on headset. Logs are saved to `Application.persistentDataPath/data.csv` on device.
4. Pull logs to `data/` and rename to `your_study_data.csv` (keep header format).
5. **Python:** Create a venv and install requirements:
   ```bash
   python -m venv .venv && source .venv/bin/activate
   pip install -r analysis/requirements.txt
   ```
6. Open `analysis/analysis.ipynb`, update the CSV path, and run all cells.
7. Fill out `docs/report_template.md` with your results and export to PDF.

## Study Design
- Within‑subjects; counterbalanced order (AB/BA)
- N = 10–16
- DVs: time (ms), error rate, SUS (0–100), NASA‑TLX (6 subscales)
- Stats: paired t‑tests + Cohen’s d

## Unity Input
- **Gaze:** dwell selection (0.6–0.8s typical)
- **Joystick:** primary2DAxis to move reticle; trigger/A to select

## License
MIT — see `LICENSE`.

## Citation
See `CITATION.cff`.
