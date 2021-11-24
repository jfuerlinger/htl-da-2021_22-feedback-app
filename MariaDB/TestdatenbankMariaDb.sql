-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server Version:               10.6.4-MariaDB - mariadb.org binary distribution
-- Server Betriebssystem:        Win64
-- HeidiSQL Version:             11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Exportiere Datenbank Struktur für feedbackapp-demo
CREATE DATABASE IF NOT EXISTS `feedbackapp-demo` /*!40100 DEFAULT CHARACTER SET latin1 COLLATE latin1_general_ci */;
USE `feedbackapp-demo`;

-- Exportiere Struktur von Tabelle feedbackapp-demo.testdata
CREATE TABLE IF NOT EXISTS `testdata` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Testtext` text COLLATE latin1_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Exportiere Daten aus Tabelle feedbackapp-demo.testdata: ~0 rows (ungefähr)
/*!40000 ALTER TABLE `testdata` DISABLE KEYS */;
REPLACE INTO `testdata` (`Id`, `Testtext`) VALUES
	(1, 'Testdaten, Login funktioniert!');
/*!40000 ALTER TABLE `testdata` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
