# Revit API Title Block Performance Test

This project benchmarks different methods of retrieving **title blocks from sheets** using the Revit API.  
It helps understand how performance can vary depending on the approach used.

## 🔍 Purpose

Compare execution time, memory usage, and efficiency of:

- `FilteredElementCollector` on each sheet
- `GetDependentElements()` per sheet
- Cached results from `GetDependentElements()` ---> Retrieve data once and reuse it during the session.

## 🧪 Test Details

- Tested on **Autodesk Snowdon Towers architectural model**
- Includes all **55 sheets**
- Measures total time, average time per sheet, and memory usage

## 📊 Summary of Results

| Method | Avg Time/Sheet | Total Time |
|--------|----------------|------------|
| FilteredElementCollector | 1293 ms | 71.1 s | 
| GetDependentElements     | 0.39 ms  | 21 ms  | 
| Cached Results           | 0.14 ms  | 8 ms   | 


## ✅ Requirements

- Autodesk Revit ( 2020 or above)


