SET search_path TO prognostics, public;

-- Standardized Stress Cohort Seeding
INSERT INTO capacitors (name, test_voltage_v) VALUES 
('ES10C1', 10), ('ES10C2', 10), ('ES10C3', 10), ('ES10C4', 10),
('ES10C5', 10), ('ES10C6', 10), ('ES10C7', 10), ('ES10C8', 10),
('ES12C1', 12), ('ES12C2', 12), ('ES12C3', 12), ('ES12C4', 12),
('ES12C5', 12), ('ES12C6', 12), ('ES12C7', 12), ('ES12C8', 12),
('ES14C1', 14), ('ES14C2', 14), ('ES14C3', 14), ('ES14C4', 14),
('ES14C5', 14), ('ES14C6', 14), ('ES14C7', 14), ('ES14C8', 14)
ON CONFLICT (name) DO NOTHING;




