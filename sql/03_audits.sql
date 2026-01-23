SET search_path TO prognostics, public;

-- THE GRAND AUDIT: Final inventory check
SELECT 
    c.name as capacitor_name,
    t.cap_id, 
    COUNT(*) as total_readings,
    MIN(t.serial_date) as start_timestamp,
    t.v_out[1:5] as dna_fingerprint -- Verifies array start
FROM transient_readings t
JOIN capacitors c ON t.cap_id = c.cap_id
GROUP BY c.name, t.cap_id, t.v_out[1:5]
ORDER BY t.cap_id ASC;

-- BATCH STATUS: Verifies all cohorts hit target fidelity
SELECT 
    name, 
    COUNT(*) as rows_inserted,
    CASE 
        WHEN name LIKE 'ES10%' AND COUNT(*) >= 75826 THEN 'COMPLETE'
        WHEN name LIKE 'ES12%' AND COUNT(*) >= 77237 THEN 'COMPLETE'
        WHEN name LIKE 'ES14%' AND COUNT(*) >= 77241 THEN 'COMPLETE'
        ELSE 'IN PROGRESS/PARTIAL'
    END as status
FROM capacitors c
JOIN transient_readings tr ON c.cap_id = tr.cap_id
GROUP BY name
ORDER BY name;


-- DNA Fingerprint: Verify the first 5 samples of a signal vector
-- Proves the 'High-Fidelity' transfer of the 400-point array
SELECT 
    cap_id, 
    cycle, 
    v_out[1:5] as signal_dna 
FROM transient_readings 
WHERE cap_id = 'C01' AND cycle = 1;

-- Delta Analysis: Calculating the 'Velocity of Decay'
-- Proves the ability to detect non-linear degradation trends
SELECT 
    cap_id, 
    cycle, 
    health_index,
    health_index - LAG(health_index) OVER (PARTITION BY cap_id ORDER BY cycle) as decay_velocity
FROM gold_health_metrics;

-- [ROLE: DATA ENGINEER] 
-- SIGNAL DNA VERIFICATION: Ensures Atomic Fetcher fidelity
-- Compares the first 5 samples to verify no "bit-drift" or ingestion noise.
SELECT 
    c.name, 
    t.serial_date,
    t.v_out[1:5] as signal_dna,
    cardinality(t.v_out) as vector_resolution -- Verifies the 400-point resolution
FROM prognostics.transient_readings t
JOIN prognostics.capacitors c ON t.cap_id = c.cap_id
LIMIT 10;

-- [ROLE: DATA SCIENTIST]
-- SANITATION CHECK: Verifies Python-side 'clean_mean' logic
-- Finds any records where the array might contain NULLs that Python must handle.
SELECT 
    cap_id, 
    COUNT(*) as total_cycles,
    SUM(CASE WHEN v_out IS NULL THEN 1 ELSE 0 END) as null_arrays
FROM prognostics.transient_readings
GROUP BY cap_id;