## 🏛️ Introduction

Modern systems—whether mechanical, electronic, or software‑driven—are becoming more complex, more interconnected, and more dependent on continuous performance. As they evolve, so do the methods required to understand how systems age and degrade, and how that information can be used to anticipate failures rather than reacted to. Traditional maintenance approaches relied on fixed schedules or simple fault detection, offering limited insight into how a component was deteriorating or when a failure was likely to occur.

### Prognostics and Health Management (PHM)

The field of **PHM provides a structured way to observe system behavior, interpret degradation, and make informed decisions before failures occur.** Instead of treating maintenance as a calendar‑based activity, PHM uses real data—sensor readings, operational history, and performance trends—to estimate the true health of a system and predict its remaining useful life.

### The Three Pillars of the Prognostics and Health Management Domain:

• **Pre‑processing of data**
Preparing raw measurements so meaningful indicators of system health can be extracted.

• **Data‑guided prognosis**
Using those indicators to understand degradation trends and predict future performance or time‑to‑failure.

• **Decision process**
Turning health assessments and predictions into actionable maintenance or operational decisions.

### PCRE — Prognostics, Condition‑based, Reliability, and Evaluation

PCRE a full-spectrum health-management framework

🔧 **P — Prognostics**
Forecasting future system health, degradation trends, and remaining useful life.

📊 **C — Condition‑based**
Using real‑time or periodic sensor data to assess the current state of the system rather than relying on fixed schedules.

🛡️ **R — Reliability**
Quantifying how likely a component or system is to perform without failure over time, given its condition and environment.

🧪 **E — Evaluation**
Interpreting the data, models, and predictions to support decisions—maintenance timing, risk mitigation, lifecycle planning.



A broader health‑management structure often referenced across industry aligns with these concepts:

• **Prognostics**
     → NASA, PHM Society, IEEE

• **Condition‑based**
     → DoD CBM+, ISO standards

• **Reliability**
     → IEEE Reliability Society, MIL‑HDBK‑217

• **Evaluation**
     → PHM decision support frameworks

## 🏛️ Project Overview

Utilizing Prognostic Health Management (PHM) analysis of current health telemetry to predict the future state and rRemaining Useful Life (RUL) of a system before failure occurs.

### NASA Prognostics Data Repository

This project uses the publicly available NASA Prognostics Data Repository. A collection of data sets focused exclusively on prognostic time-series data from a prior nominal state to a failed state.

**The PCRE framework serves as a Digital Twin** (the final high-fidelity software representation of the physical hardware used to evaluate prognostic health). By mapping raw electrical telemetry into a structured .NET/PostgreSQL environment, the system creates a virtual surrogate that tracks real-time health and forecasts Remaining Useful Life (RUL) without further physical intervention.

### NASA Prognostics Data Set 12. Capacitor Electrical Stress

**Experiment**

Capacitors were subjected to Accelerated Life Testing (ALT) Electrical Stress under three voltage levels, i.e. 10V, 12V, and 14V. Data Set contains Electrical Impedance Spectroscopy (EIS) data as well as Charge/Discharge Signal data.

### Acknowledgments & Data Sources

This project utilizes the publicly available **NASA Prognostics Data Repository**, a collection focused exclusively on prognostic time-series data from nominal states to failed states.

* **Data Set Reference Document**: [http://www.femto-st.fr/en/Research-departments/AS2M/Research-groups/PHM/IEEE-PHM-2012-Data-challenge.php]

* **Data Set**: [Capacitor Electrical Stress Data Set 12](https://www.nasa.gov/intelligent-systems-division/discovery-and-systems-health/pcoe/pcoe-data-set-repository/)
* **Citation**: J. Renwick, C. Kulkarni, and J. Celaya, “Capacitor Electrical Stress Data Set”, NASA Prognostics Data Repository, NASA Ames Research Center, Moffett Field, CA.
* **Publication**: J. Renwick, C. Kulkarni and J. Celaya, “Analysis of Electrolytic Capacitor Degradation under Electrical Overstress for Prognostic Studies”, in the Proceedings of the Annual Conference of the Prognostics and Health Management Society, Coronado CA, October 2015.

## 🏛️ Project Objective

Transforming 5GB of raw transient telemetry into actionable insights regarding component degradation and Remaining Useful Life (RUL). This project bridges the gap between hardware physics and business value through a high-fidelity PostgreSQL Medallion Architecture. By ensuring total data integrity—from the ingestion of the original experimental dataset through the PostgreSQL conversion process to relational persistence—the pipeline preserves the critical signal resolution required for prognostic modeling. 

By demonstrating core competencies across **five distinct industry roles**, the project showcases the full lifecycle of a Predictive Mission—from raw physics to executive decision support:

## 🎭 The 5-Role Persona Framework

### 📈 The Business Analyst (The "Value")
* **The Story:** "I identified the 14V stress level as a High-Risk Operational Profile. By quantifying the 'Liability Cliff,' I translated raw capacitor decay into a business-ready forecast for CapEx planning and risk mitigation."

* **Action:** Translated technical degradation slopes into Reliability Stories to justify maintenance budgets.

* **Focus:** Risk Mitigation and Asset Lifecycle Management.

### 🛠️ The Data Engineer (The "Pipes")

* **The Story:** "I engineered the high-performance PostgreSQL schema and the 'Atomic Fetcher' to ensure that 5GB of raw telemetry arrived cleaned, sanitized, and ready for modeling."

* **Action:** Utilized ROW_NUMBER() and PARTITION BY SQL logic to resolve data 'ghosting' and duplication during ingestion.

* **Focus:** Data Integrity and Pipeline Structural Integrity.

### 🏗️ The Systems Analyst (The "Architecture")

* **The Story:** "I designed the project’s Information Architecture to bridge the gap between hardware telemetry and software presentation, ensuring the complex data lifecycle is navigable for all stakeholders."

* **Action:** Implemented HTML Anchors and cross-linked Markdown Roadmaps for non-linear, intuitive navigation.

* **Focus:** State Management and User Experience (UX).

### 🧪 The Data Scientist (The "Oracle")

* **The Story:** "I applied 'Foreknowledge' to the dataset, utilizing rolling-window smoothing to identify the 'Knee' of the curve—the transition point from stability to imminent failure."

* **Action:** Developed a 95% EOL (End of Life) Threshold model using Linear Regression trends.

* **Focus:** Predictive Power and Statistical Modeling.

### 🌉 The Programmer Analyst (The "Bridge")

* **The Story:** "I orchestrated the full-cycle technical narrative, bridging the gap between high-fidelity PostgreSQL persistence and end-user consumption. By developing a .NET 9 Web Service alongside the Python visualization layer, I transformed raw telemetry into a production-ready diagnostic suite."

* **Action:** Harmonized the end-to-end workflow from SQL ingestion to complex Matplotlib/Seaborn visualization. Engineered a RESTful API using the Repository Pattern to expose "Health Signatures" for real-time monitoring.

* **Focus:** Full-Cycle Workflow Optimization and Technical Storytelling. Engineered a RESTful API using the Repository Pattern and Dapper to map PostgreSQL signal telemetry to JSON Health Signatures for real-time monitoring.

## 🏗️ Systems Analyst "Battle Stories"

**Roadblock: The Array Bottleneck** `UNNEST` was insufficient for "dirty" arrays containing `None` values. I engineered an **Atomic Fetcher** using Python-side `clean_mean` logic to sanitize telemetry before aggregation.

**Roadblock: The Stability Wall** Linear Regression originally failed on stable assets. I implemented **Adaptive Rolling Windows** (5–50 cycles) to visualize the "Velocity of Change" even in healthy units.

## 🤖 AI Development Narrative: The "Force Multiplier"

During development, AI was utilized as an **Architecture Consultant** and **Pair Programmer** to navigate a "blind" data hierarchy.

* Decoding Hierarchy: Iteratively refined h5py traversal logic to map the internal MATLAB struct hierarchy for Transient_Data.

* Memory Gating: Developed a .copy() and del strategy to process 5GB of telemetry within an 8.00 GB RAM footprint.

* Logic Refinement: Assisted in transitioning from SQL-side AVG() to a robust Python-side clean_mean function to sanitize NoneType artifacts within high-frequency signal vectors.

* Refactor SQL: Optimize the 04_utilities scripts for batch-inserting 400-point arrays.

* Debug Memory Leaks: Trace the garbage collection during the Atomic Fetcher routine.

* Cross-Platform Architecture: Design the interface between the Python analytics engine and the .NET Web Service."


## 🛠️ Technical Highlight:

### 🛰️ The Atomic Fetcher

To process 5GB of raw telemetry within a strict 8.00 GB RAM footprint, this project utilizes a custom "Atomic Fetcher" logic within the Analytical Framework to navigate non-linear MATLAB structures via the PostgreSQL Silver Layer. This is a dynamic, smart operation designed to traverse and navigate the non-linear, inconsistent hierarchy of the original MATLAB file structures.

**The "One-at-a-Time" Approach**
The fetcher operates **atomically** regarding memory—meaning it treats each data request as a self-contained, isolated operation.

**Execution:** By using .copy() to prevent pointer reuse and del to explicitly clear DataFrames, the fetcher ensures memory is fully released before the next "atom" of data is processed.

**Purpose:** This prevents the **"Memory Bloat"** that typically occurs when processing high-frequency signal vectors in a standard loop.

```
# Atomic Fetcher: Navigates non-linear structs and manages memory
def get_capacitor_data(cap_id, limit=None):
    """Refined Atomic fetcher: Cleans NoneTypes within arrays to prevent TypeErrors."""
    limit_clause = f"LIMIT {limit}" if limit else ""
    
    query = f"""
    SELECT serial_date, v_out
    FROM prognostics.transient_readings 
    WHERE cap_id = {cap_id} 
    ORDER BY serial_date ASC 
    {limit_clause}
    """
    
    with atomic_engine.connect() as conn:
        df = pd.read_sql(text(query), conn)
    
    if not df.empty:
        df['human_date'] = df['serial_date'].apply(matlab_to_datetime)
        
        # --- ROBUST AGGREGATION ---
        # 1. Filter out None values from inside the list
        # 2. Calculate mean only on the numeric remains
        def clean_mean(arr):
            if arr is None: return np.nan
            # List comprehension to keep only floats/ints
            clean_arr = [v for v in arr if v is not None]
            return np.mean(clean_arr) if clean_arr else np.nan

        df['avg_voltage_out'] = df['v_out'].apply(clean_mean)
        df['cycle_index'] = range(len(df))
        
        # Drop raw array to keep memory low
        df = df.drop(columns=['v_out'])
        
    return df

```
## 🛡️ Atomic Fetcher SQL Verification Layer

The following queries, located in 03_audits.sql and 04_utilities.sql, provide the "Ground Truth" verification for the 5-role mission. ensuring the high-fidelity integrity of the data platform. The .NET webservice development for three (3) PCRE.API http endpoints are verified with OpenAPI Scalar:

| Role | Verification Objective | SQL Proof of Work |
| :--- | :--- | :--- |
| **Data Engineer** | Signal DNA Check | `SELECT v_out[1:5] FROM transient_readings` |
| **Programmer Analyst** | Ingestion Performance | `EXPLAIN ANALYZE SELECT serial_date, v_out` |
|                        | API Payload Mapping   | `SELECT pg_typeof(v_out), count(*) FROM ...` |
|                        | First 100 cycles      | `GET /api/Capacitor/{id}` |
|                        | Degraded cycles       | `GET /api/Capacitor/{id}/alerts` |
|                        | Health Index 0 to 1.0 | `GET /api/Capacitor/{id}/status` |
| **Business Analyst**   | Risk Quantification   | `SELECT test_voltage_v, pg_size_pretty(SUM(...))` |

## 🗄️ Database Lifecycle (Medallion Architecture)

The project utilizes a Medallion Architecture to transform raw data into a prognostic-ready Gold Layer:

* **⚡ The Hardware Constraint**: Specifically optimized for a **8.00 GB RAM** footprint. The **Bronze-to-Silver** ingestion (`import.py`) utilizes an iterative **chunking parser** to prevent memory overflow during the MATLAB-to-Python struct extraction.

* **📊 The Ingestion Benchmark**: Successfully verified **ES12C1 cohort ingestion (77,237 rows)** with zero data loss, utilizing **PostgreSQL 16** as the persistent Silver Layer.

**Medallion Architecture Strategy:**

* **🟫 Bronze Layer**: Raw `.mat` structs. Preserving the original "Experimental Truth."

* **⬜ Silver Layer**: Relational SQL Tables. Sanitized, indexed, and query-optimized telemetry.
  
  ![alt text](/images/Prognostics_ERDiagram.jpg)

* **🟦 Gold Layer**: Feature Arrays. Health Signatures optimized for Predictive Modeling.
  
## 🌐 .NET Enterprise Backend Integration

To transition the project from a research environment to a production-ready software suite, the Silver Layer is exposed via a .NET 9 REST API. This architecture establishes the .NET Web Service and the Python Analytical Notebook as 'sibling' consumers of the same relational PostgreSQL data. This dual-consumption model proves the platform's interoperability, allowing the backend to manage data governance while the Python layer focuses on scientific research.

**Technical Implementation:**

* **Architecture:** Decoupled N-Tier architecture consisting of `PCRE.API` (Controllers), `PCRE.Data` (Repositories), and `PCRE.Models` (DTOs).
* **Data Access:** High-performance mapping utilizing **Dapper** and the **Repository Pattern** to ensure contract stability.
* **Interoperability:** Enables external consumption by Power BI, React dashboards, or mobile diagnostic tools via standard JSON.
* **The "JSON Bridge":** Engineered custom mapping to convert complex PostgreSQL numeric arrays into strongly-typed C# `List<double>` objects, resolving hardware-to-software data mismatches.
  
## 🚦 Endpoint Strategy & Scalar Verification

The following endpoints were engineered to facilitate the six prognostic plots while providing real-time fleet monitoring:

| Endpoint | Logic Type | Visualization Support |
| :--- | :--- | :--- |
| `GET /api/Capacitor/{id}` | **Atomic Ingestion** | Provides high-resolution vector data for **Plots 1 & 2** (Pulse Anatomy). |
| `GET /api/Capacitor/{id}/alerts` | **Edge Analysis** | Uses server-side SQL `unnest` and `AVG` to isolate degraded cycles for **Plots 3 & 4**. |
| `GET /api/Capacitor/{id}/status` | **Smart Metadata** | Normalizes voltage into a Health Index (0.0-1.0) and categorical status for **Plot 5**. |


JSON

```
[
  {
    "readingId": 1,
    "serialDate": 735920.5499652778,
    "vLoad": "[0.1264, 1.7260, 1.6814, ...[400 samples]]",
    "vOut": "[0.1087, 1.9517, 2.2960, ...[400 samples]]",
    "humanDate": "2014-11-17T13:11:57",
    "avgVoltage": 4.700986015027598,
    "healthIndex": 0.97937,
    "status": "Nominal"
  }
]

```

## 🏛️ Analytical Framework

The analysis is divided into two distinct phases to ensure data validation and comparative insights:

### Part I: Data Forensic & Signal Validation

Objective: Validate the integrity of the 5GB+ PostgreSQL database ingestion.

Focus: Verifying Cohort Separation (Plot #1) and Signal Fidelity (Plot #2) to ensure the "DNA" of each stress level (10V, 12V, 14V) is unique and non-aliased.

### Part II: Prognostic Modeling & Accelerated Aging

Objective: Quantify the relationship between electrical stress and physical failure.

Focus: Feature Extraction (Plot #3) and Longitudinal Health Decay (Plot #4) to identify the "Knee" of the degradation curve—the point where a component transitions from stable operation to imminent failure.

### 🗺️ Notebook Architecture & Signal Lifecycle Roadmap

| Cell # | Content Type (Navigation) | Objective |
| :--- | :--- | :--- |
| **3** | [**Infrastructure & Setup**](#setup) | Database Connection & Helper Functions |
| **4** | [**Plot #1: Multi-Voltage Baseline**](#plot1) | Part I: Data Forensic & Signal Validation |
| **5** | [**Plot #2: Pulse Anatomy**](#plot2) | Part I: High-Resolution Signal Fidelity |
| **6** | [**Plot #3: Peak Stability**](#plot3) | Part II: Longitudinal Feature Extraction |
| **7** | [**Plot #4: Health Decay Overlay**](#plot4) | Part II: 1000-Cycle Stress Analysis |
| **8** | [**Plot #5: Failure Distribution**](#plot5) | Part II: Statistical Fleet Variance |
| **9** | [**Plot #6: RUL Forecast**](#plot6) | Part II: Predictive Prognostics |

### 📉 PHM Analytic Framework: From Forensic Signals to Prognostic Forecasts

| Phase | # | Visualization | Narrative Goal (The "Why") | Professional Lens |
| :--- | :--- | :--- | :--- | :--- |
| **Forensic** | 1 | **Multi-Voltage Baseline** | Validates 10V/12V/14V cohort separation; proves experimental and schema integrity. | **Data Engineer**: Schema Validation |
| **Forensic** | 2 | **Transient Anatomy** | Identifies the high-frequency discharge initiation anchor within a single signal burst. | **Programmer Analyst**: Signal Fidelity |
| **Modeling** | 3 | **Longitudinal Stability** | Tracks "Peak Voltage" drift and baseline feature extraction over 1,000+ cycles. | **Systems Analyst**: Stability Monitoring |
| **Modeling** | 4 | **Health Decay Overlay** | Compares 14V extreme stress vs. control units to visualize accelerated dielectric failure. | **Data Scientist**: Comparative Analytics |
| **Predictive** | 5 | **Fleet Distribution** | Quantifies population-wide operational risk and asset variance using Boxplots. | **Business Analyst**: Risk Assessment |
| **Predictive** | 6 | **RUL Forecast** | Projects statistical "Time-to-Failure" and Remaining Useful Life (RUL) trajectories. | **Prognostics Lead**: Actionable Insight |

------------------------------------------------------

## 🛠️ Technical Stack & Dependencies

### Bash
pip install \
  sqlalchemy==2.0.43 \
  psycopg2-binary==2.9.11 \
  h5py==3.15.1 \
  seaborn==0.13.2 \
  adjustText==1.3.0 \
  PyQt6==6.9.1 \
  winloop==0.2.2 \
  numexpr==2.11.0 \
  Bottleneck==1.4.2 \
  jupyter==1.1.1


### System Specifications:

**Optimized for**: 8.00 GB RAM | PostgreSQL 16
**Status**: ES12C1 Ingestion Verified (77,237 Rows)

------------------------------------------------------

### Project Structure: NASA PCRE Prognostics

### 📂 Project Tree
```text
PCRE_PROJECT/
├── .vscode/                 # Editor configurations
├── dotnet/                  # [.NET 9 Web Service - Sub-module]
│   ├── PCRE.API/            # REST Controllers & OpenAPI (Scalar)
│   │   ├── bin/
│   │   ├── Controllers/     # API Endpoints
│   │   ├── obj/
│   │   ├── Properties/
│   │   ├── appsettings.json
│   │   ├── PCRE.API.csproj
│   │   └── Program.cs       # Service Entry Point
│   ├── PCRE.Data/           # Dapper Repositories & SQL Mapping
│   │   ├── bin/
│   │   ├── IDbConnectionFactory.cs
│   │   ├── IPrognosticsRepository.cs
│   │   ├── NpgsqlConnectionFactory.cs
│   │   └── PrognosticsRepository.cs # Core SQL Logic
│   ├── PCRE.Models/         # TransientReading.cs & DTOs
│   │   └── TransientReading.cs # High-Fidelity Data Model
│   └── PCRE_Service.sln     # Visual Studio Solution File
├── images/                  # Prognostic Visualization Suite
│   ├── Plot1_Baseline.png
│   ├── Plot2_SignalFidelity.png
│   ├── Plot3_VoltageStability.png
│   ├── Plot4_HealthDecay.png
│   ├── Plot5_Distribution.png
│   ├── Plot6_RULForecast.png
│   ├── Predictive_Maintenance.jpg
│   └── Prognostics_ERDiagram.jpg
├── python_scripts/          # Data Engineering & Analysis
│   ├── db_utils.py          # Database ingestion logic
│   └── health_degradation_analysis.ipynb
├── sql/                     # Relational Database Lifecycle
│   ├── 01_schema.sql        # Table Definitions
│   ├── 02_seed_metadata.sql # Cohort Setup
│   ├── 03_audits.sql        # Data Verification
│   └── 04_utilities.sql     # Maintenance Tools
├── .gitignore               # Excludes build artifacts
├── README.md                # Project documentation
└── requirements.txt         # Python environment dependencies
```

### 📂 Project Verification Scripts
The following scripts facilitate the conversion from raw telemetry to relational persistence:

* 🏗️ **[01_schema.sql](./sql/01_schema.sql)**: Defines the Information Architecture.
* 🌱 **[02_seed_metadata.sql](./sql/02_seed_metadata.sql)**: Seeds the experimental cohorts.
* 🛡️ **[03_audits.sql](./sql/03_audits.sql)**: Performs the High-Fidelity "DNA" check.
* 📜 **[04_utilities.sql](./sql/04_utilities.sql)**: Operational tools and performance tuning.
* 💻 **[db_utils.py](/python/scripts/db_utils.py)**: The Atomic Fetcher ingestion driver.
* 📓 **[analysis.ipynb](/python/scripts/health_degradation_analysis.ipynb)**: Phase I-III prognostic modeling.
* 🌐 **[PCRE.API](/dotnet/PCRE.API/)**: RESTful endpoint provider for Health Signatures.
* 🏗️ **[PCRE.Data](/dotnet/PCRE.Data/)**: Repository Layer implements the bridge between PostgreSQL and the .NET runtime.

## 📊 Experimental Results & Narrative Analysis

## Phase I: Data Forensic & Signal Validation

### Plot 1: Multi-Voltage Baseline<a id="plot1"></a>

**Technical Objective:** Verify cohort separation (10V, 12V, 14V).

**Insight:** Validated "Physics of the Baseline"; designated voltage plateaus remain stable post-ingestion, proving the integrity of the data pour.

![alt text](</images/Plot1_Baseline Verification.png>)

### Plot 2: Transient Anatomy<a id="plot2"></a>

**Technical Objective:** Isolate discharge cycles from v_out arrays.

**Insight:** Identified Discharge Initiation at Sample Index 100, proving the high-frequency sampling quality required for precision PHM.

![alt text](</images/Plot2_SignalFidelity.png>)

## Phase II: Accelerated Life Testing (ALT)

### Plot 3: Baseline Voltage Stability<a id="plot3"></a>

**Technical Objective:** Monitor drift over 500 cycles.

**Insight:** Discovered a "Positive Drift" (4.4V to 5.0V), necessitating data normalization before predictive modeling.

![alt text](</images/Plot3_BaselineVoltageStability.png>)

### Plot 4: Comparative Health Decay<a id="plot4"></a>

**Technical Objective:** Overlay stress cohorts against the 95% Failure Threshold.

**Insight:** 14V cohorts showed significant instability, breaching failure thresholds between cycles 550–900.

![alt text](</images/Plot4_HealthDecay.png>)

## Phase III: Population Statistics & Prognostics

### Plot 5: Statistical Health Distribution<a id="plot5"></a>

**Technical Objective:** Fleet-wide boxplot assessment of unit health at Cycle 500.

**Insight:** 10V Control units showed high consistency (102% health); the absence of 12V/14V units at this milestone validates the accelerated aging impact.

![alt text](</images/Plot5_FailureDistribution.png>)

### Plot 6: RUL Forecast (Unit 1)Technical Objective:<a id="plot6"></a> 

**Technical Objective:** Linear regression on smoothed health signatures.

**Insight:** Successfully calculated the "Velocity of Change" with a degradation slope of $0.003483$, establishing the baseline for predictive maintenance.

![alt text](</images/Plot6_RULForecast.png>)


# 🚀 How to Replicate this Mission

### 1. Infrastructure Setup

Initialize the PostgreSQL 16 environment using the hardened scripts in the `/sql` directory:

1. Run `01_schema.sql` to build the Information Architecture.

2. Run `02_seed_metadata.sql` to establish the experimental context.


### 2. .NET API Gateway (Backend)

Ensure you have the **.NET 9 SDK** installed:

1. Navigate to the `dotnet/PCRE.API` directory.

2. Restore dependencies and launch the service:

```
Bash

# Restore project dependencies
dotnet restore

# Launch the .NET Web API
dotnet run
```

3. **Interactive Documentation:** Navigate to `http://localhost:5161/scalar/v1` to explore the **OpenAPI/Scalar** interface and test the `/status` and `/alerts` endpoints.

### 3. Data Ingestion & Analysis (Python)
    
1. **Step 1:Ingest:** — Run `python_scripts/db_utils.py` to perform the one-time migration of raw `.mat` telementary to the PostgreSQL Silver Layer.
   
2. **Step 2:Analyze:** — Open health_degradation_analysis.ipynb. The embedded get_capacitor_data() helper serves as the Atomic Fetcher, utilizing a clean_mean() function to sanitize high-frequency signal vectors in real-time.