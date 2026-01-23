SET search_path TO prognostics, public;

-- MONITOR: Live upload progress (Percent Complete)
SELECT 
    c.name, 
    COUNT(tr.reading_id) AS total_rows,
    ROUND((COUNT(tr.reading_id)::numeric / 77237) * 100, 2) || '%' AS upload_progress,
    pg_size_pretty(SUM(pg_column_size(tr.*))) AS storage_footprint
FROM capacitors c
JOIN transient_readings tr ON c.cap_id = tr.cap_id
GROUP BY c.name
ORDER BY c.name;

-- CLEANUP: Wipe telemetry while keeping table structure (Optional)
-- TRUNCATE TABLE transient_readings RESTART IDENTITY;

-- GHOST-BUSTER: Find and remove metadata orphans
-- DELETE FROM capacitors WHERE cap_id NOT IN (SELECT DISTINCT cap_id FROM transient_readings);

-- Risk Audit: Identify units that have breached the 95% EOL threshold
-- Translates raw decay into a business 'Action Required' list
SELECT 
    cap_id, 
    MAX(cycle) as last_cycle,
    MIN(health_index) as lowest_health
FROM gold_health_metrics
GROUP BY cap_id
HAVING MIN(health_index) < 0.95;

-- Ingestion Tracker: Real-time monitoring of the Silver Layer population
SELECT 
    c.cohort, 
    COUNT(t.reading_id) as samples_ingested
FROM capacitors c
JOIN transient_readings t ON c.cap_id = t.cap_id
GROUP BY c.cohort;

-- [ROLE: PROGRAMMER ANALYST]
-- FETCH PERFORMANCE: Simulates the get_capacitor_data() workload
-- Explains why the index idx_readings_cap_id is critical for Python retrieval.
EXPLAIN ANALYZE
SELECT serial_date, v_out
FROM prognostics.transient_readings 
WHERE cap_id = 1 
ORDER BY serial_date ASC;

-- [ROLE: BUSINESS ANALYST]
-- COHORT STRESS SUMMARY: Aggregates degradation signals by test voltage
SELECT 
    c.test_voltage_v,
    COUNT(t.reading_id) as sample_count,
    pg_size_pretty(SUM(pg_column_size(t.v_out))) as telemetry_volume
FROM prognostics.capacitors c
JOIN prognostics.transient_readings t ON c.cap_id = t.cap_id
GROUP BY c.test_voltage_v;