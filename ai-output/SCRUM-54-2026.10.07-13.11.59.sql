-- SQL simple migration: adds an optional raw_material_source column to raw_materials
ALTER TABLE raw_materials
ADD COLUMN raw_material_source VARCHAR(255) NULL ;