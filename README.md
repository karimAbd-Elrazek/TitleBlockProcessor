# Revit API Title Block Performance Test

This project benchmarks different methods of retrieving **title blocks from sheets** using the Revit API.  
It helps understand how performance can vary depending on the approach used.

## 🔍 Purpose

Compare execution time, memory usage, and efficiency of:

- `FilteredElementCollector` on each sheet
- `GetDependentElements()` per sheet
- Cached results from `GetDependentElements()`

## 🧪 Test Details

- Tested on **Autodesk Snowdon Towers architectural model**
- Includes all **55 sheets**
- Measures total time, average time per sheet, and memory usage

## 📊 Summary of Results

| Method | Avg Time/Sheet | Total Time | Notes |
|--------|----------------|------------|-------|
| FilteredElementCollector | 1293 ms | 71.1 s | Slowest |
| GetDependentElements     | 0.39 ms  | 21 ms  | Fast |
| Cached Results           | 0.14 ms  | 8 ms   | Fastest |


## ✅ Requirements

- Autodesk Revit (tested with 2020+)


