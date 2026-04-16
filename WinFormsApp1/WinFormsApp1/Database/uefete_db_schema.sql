CREATE DATABASE IF NOT EXISTS `uefete_db`
CHARACTER SET utf8mb4
COLLATE utf8mb4_spanish_ci;

USE `uefete_db`;

SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS `historial`;
DROP TABLE IF EXISTS `personajes`;
DROP TABLE IF EXISTS `usuarios`;
SET FOREIGN_KEY_CHECKS = 1;

CREATE TABLE IF NOT EXISTS `usuarios` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `nombre` VARCHAR(50) NOT NULL,
    `email` VARCHAR(120) NOT NULL,
    `password` VARCHAR(255) NOT NULL,
    `fecha_registro` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    UNIQUE KEY `uk_usuarios_nombre` (`nombre`),
    UNIQUE KEY `uk_usuarios_email` (`email`)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS `personajes` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `nombre` VARCHAR(50) NOT NULL,
    `fuerza` INT NOT NULL,
    `defensa` INT NOT NULL,
    `velocidad` INT NOT NULL,
    `resistencia` INT NOT NULL,
    `tecnica` INT NOT NULL,
    `es_predefinido` TINYINT(1) NOT NULL DEFAULT 0,
    `id_propietario` INT NULL,
    PRIMARY KEY (`id`),
    KEY `idx_personajes_propietario` (`id_propietario`),
    CONSTRAINT `fk_personajes_usuarios`
        FOREIGN KEY (`id_propietario`) REFERENCES `usuarios` (`id`)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT `chk_personajes_fuerza` CHECK (`fuerza` BETWEEN 0 AND 100),
    CONSTRAINT `chk_personajes_defensa` CHECK (`defensa` BETWEEN 0 AND 100),
    CONSTRAINT `chk_personajes_velocidad` CHECK (`velocidad` BETWEEN 0 AND 100),
    CONSTRAINT `chk_personajes_resistencia` CHECK (`resistencia` BETWEEN 0 AND 100),
    CONSTRAINT `chk_personajes_tecnica` CHECK (`tecnica` BETWEEN 0 AND 100)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS `historial` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `id_usuario` INT NOT NULL,
    `id_personaje` INT NULL,
    `nombre_personaje` VARCHAR(50) NOT NULL,
    `rival` VARCHAR(50) NOT NULL,
    `resultado` VARCHAR(20) NOT NULL,
    `fecha` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `detalle` TEXT NULL,
    PRIMARY KEY (`id`),
    KEY `idx_historial_usuario` (`id_usuario`),
    KEY `idx_historial_personaje` (`id_personaje`),
    CONSTRAINT `fk_historial_usuarios`
        FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id`)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT `fk_historial_personajes`
        FOREIGN KEY (`id_personaje`) REFERENCES `personajes` (`id`)
        ON DELETE SET NULL
        ON UPDATE CASCADE
) ENGINE=InnoDB;

INSERT INTO `personajes`
    (`nombre`, `fuerza`, `defensa`, `velocidad`, `resistencia`, `tecnica`, `es_predefinido`, `id_propietario`)
SELECT *
FROM (
    SELECT 'Titan' AS nombre, 65 AS fuerza, 55 AS defensa, 45 AS velocidad, 55 AS resistencia, 40 AS tecnica, 1 AS es_predefinido, NULL AS id_propietario
    UNION ALL
    SELECT 'Sombra', 45, 40, 70, 45, 60, 1, NULL
    UNION ALL
    SELECT 'Gladius', 55, 60, 50, 45, 50, 1, NULL
) AS nuevos
WHERE NOT EXISTS (
    SELECT 1
    FROM `personajes` p
    WHERE p.`nombre` = nuevos.`nombre` AND p.`es_predefinido` = 1
);
