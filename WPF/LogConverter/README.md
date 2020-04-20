#  

## Log Converter

### Solution Overview

A desktop utility to convert specialized log files (.txt) int pre-formatted multi-sheet, data grouped, pivot-ready Excel reports.

#### Object Model

- Log (1)
  - Summary (*)
    - Study (*)
      - Event (*)
- Data/ Details (*)

#### Report Structure

- Summary
  - Optional Tab
  - Standard Header
  - Optional Table of Studies + events
- Data/ Details
  - Standard Data Functionality - Sort, Search
  - Data Grouping/ Collapsible by Parent/ Child relationship

#### Dependencies

- Closed XML
    Excel report builder
