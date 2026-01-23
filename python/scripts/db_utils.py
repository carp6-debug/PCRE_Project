# =============================================================================
"""
### 🏛️ ROLE: THE DATA ENGINEER (The "Pipes")
### ⚙️ FEATURE: Medallion Ingestion Engine (run_batch_import)

### ⚔️ BATTLE STORY: The Inconsistent Hierarchy
MATLAB structures are notoriously non-linear. This module acts as a "Smart Vehicle," 
navigating nested structs to extract 400-point signal vectors while managing a 
strict 8GB RAM footprint through explicit memory gating (.copy() and del).

### 🏛️ MEDALLION TRANSITION: 
[BRONZE: Raw MATLAB] -> [SILVER: Relational PostgreSQL]
"""
# =============================================================================

import h5py
import pandas as pd
import numpy as np
from sqlalchemy import create_engine, text

# 1. Connection (8GB Optimized)
engine = create_engine('postgresql+psycopg2://postgres:Lr591T66@localhost:5432/PCRE')

def run_batch_import(file_info):
    for file_path, group_name in file_info:
        print(f"\n🚀 STARTING BATCH: {group_name}")
        
        # Explicitly using 'with' to ensure file closure after each batch
        with h5py.File(file_path, 'r') as f:
            print(f"📂 Internal Groups Found: {list(f.keys())}") 
            base_path = f"{group_name}/Transient_Data"
            
            if group_name not in f:
                print(f"❌ ERROR: Group {group_name} not found. Available: {list(f.keys())}")
                continue
            
            # FORCE NEW MEMORY OBJECT: .copy() prevents pointer reuse from previous files
            master_timestamps = np.array(f[f"{base_path}/Serial_Date"]).flatten().copy()
            num_readings = len(master_timestamps)
            print(f"📊 Found {num_readings} unique timestamps for {group_name}.")

            available_keys = list(f[base_path].keys())
            
            for i in range(1, 9):
                cap_name = f"{group_name}C{i}"
                if cap_name not in available_keys:
                    print(f"  ⏩ Skipping {cap_name}: Not in file.")
                    continue

                with engine.connect() as conn:
                    res = conn.execute(
                        text("SELECT cap_id FROM prognostics.capacitors WHERE name = :n"),
                        {"n": cap_name}
                    )
                    cap_id = res.scalar()

                if cap_id is None:
                    print(f"  ⚠️ Warning: {cap_name} metadata missing. Skipping.")
                    continue

                print(f"  📦 Processing {cap_name} (ID: {cap_id})...")
                
                # FORCE NEW MEMORY OBJECTS for voltage arrays
                v_load_raw = np.array(f[f"{base_path}/{cap_name}/VL"]).copy()
                v_out_raw = np.array(f[f"{base_path}/{cap_name}/VO"]).copy()
                
                # Orientation Logic
                if v_load_raw.shape[0] == 400 and v_load_raw.shape[1] != 400:
                    v_load = v_load_raw.T
                    v_out = v_out_raw.T
                else:
                    v_load = v_load_raw
                    v_out = v_out_raw

                # Surgical Alignment with isolated variables
                min_len = min(len(master_timestamps), v_load.shape[0])
                final_ts = master_timestamps[:min_len]
                final_vl = v_load[:min_len]
                final_vo = v_out[:min_len]

                # Create DataFrame with native Python floats (better for SQL arrays)
                df = pd.DataFrame({
                    'cap_id': [int(cap_id)] * min_len,
                    'serial_date': [float(d) for d in final_ts],
                    'v_load': [[float(x) for x in row] for row in final_vl], 
                    'v_out': [[float(x) for x in row] for row in final_vo]
                })

                print(f"    💾 Writing {len(df)} rows to Database...")
                df.to_sql('transient_readings', engine, schema='prognostics', 
                          if_exists='append', index=False, method='multi', chunksize=250)
                
                print(f"  ✅ {cap_name} saved.")
                del df # Prevent RAM bloat

if __name__ == "__main__":
    # Ensure these paths are exact as per your File Explorer check
    files_to_process = [
        (r'D:\Documents\Projects\PCRE_Project\data\ES12_v2.mat', 'ES12'),
        (r'D:\Documents\Projects\PCRE_Project\data\ES14_v2.mat', 'ES14')
    ]
    run_batch_import(files_to_process)
    print("\n🎉 MISSION COMPLETE: Database re-seeded with unique stress-test data.")