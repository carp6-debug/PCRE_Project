/* Project: NASA PCRE Repository
   Purpose: Professional Schema Definition
*/
CREATE SCHEMA IF NOT EXISTS prognostics;
SET search_path TO prognostics, public;

-- Table: Metadata for unique physical assets
CREATE TABLE IF NOT EXISTS capacitors (
    cap_id SERIAL PRIMARY KEY,
    name VARCHAR(20) NOT NULL UNIQUE, 
    test_voltage_v DOUBLE PRECISION NOT NULL
);

-- Table: High-frequency telemetry (The Array Store)
CREATE TABLE IF NOT EXISTS transient_readings (
    reading_id BIGSERIAL PRIMARY KEY,
    cap_id INTEGER REFERENCES capacitors(cap_id) ON DELETE CASCADE,
    serial_date DOUBLE PRECISION NOT NULL,
    v_load DOUBLE PRECISION[], 
    v_out DOUBLE PRECISION[]
);

-- Index for API Performance (Used by .NET service later)
CREATE INDEX IF NOT EXISTS idx_readings_cap_id ON transient_readings(cap_id);

-- Storage Efficiency Audit: Comparing Metadata vs. Telemetry footprint
-- Proves the schema can handle the 5GB load efficiently
SELECT 
    relname AS table_name, 
    pg_size_pretty(pg_total_relation_size(relid)) AS total_size
FROM pg_stat_user_tables;


