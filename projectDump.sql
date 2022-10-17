-- MySQL dump 10.13  Distrib 8.0.30, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: project
-- ------------------------------------------------------
-- Server version	8.0.29

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `hour`
--

DROP TABLE IF EXISTS `hour`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hour` (
  `id_hour` int NOT NULL AUTO_INCREMENT,
  `id_project` smallint NOT NULL,
  `id_person` smallint NOT NULL,
  `work_hour` int DEFAULT NULL,
  PRIMARY KEY (`id_hour`),
  KEY `id_project` (`id_project`),
  KEY `id_person` (`id_person`),
  CONSTRAINT `hour_ibfk_1` FOREIGN KEY (`id_project`) REFERENCES `project` (`id_project`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `hour_ibfk_2` FOREIGN KEY (`id_person`) REFERENCES `person` (`id_person`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hour`
--

LOCK TABLES `hour` WRITE;
/*!40000 ALTER TABLE `hour` DISABLE KEYS */;
INSERT INTO `hour` VALUES (1,109,802,300),(2,109,202,200),(3,109,203,200),(4,109,204,100),(6,109,206,400),(7,102,802,300),(8,102,203,400),(10,104,202,300),(12,104,205,400),(14,140,806,350),(18,120,805,400),(20,120,809,150),(21,140,808,150),(22,120,808,400);
/*!40000 ALTER TABLE `hour` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `person`
--

DROP TABLE IF EXISTS `person`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `person` (
  `id_person` smallint NOT NULL,
  `firstname` char(15) NOT NULL,
  `lastname` char(15) NOT NULL,
  `city` varchar(20) DEFAULT NULL,
  `birth_year` smallint DEFAULT NULL,
  `salary` double DEFAULT NULL,
  `password` varchar(255) NOT NULL,
  PRIMARY KEY (`id_person`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `person`
--

LOCK TABLES `person` WRITE;
/*!40000 ALTER TABLE `person` DISABLE KEYS */;
INSERT INTO `person` VALUES (202,'Joe','Smith','TURKU',1988,40,'pass01'),(203,'Liisa','River','HELSINKI',1991,39,'pass02'),(204,'Ann','Jones','TURKU',1979,44,''),(205,'Lisa','Simpson','HELSINKI',1985,36,''),(206,'Matt','Daniels','TAMPERE',1991,34,''),(503,'Teuvo','Testi','TAMPERE',1980,40,''),(506,'Taavi','Testi','TURKU',1960,45,''),(570,'James','Bond','LONDON',1960,40,'pass007'),(602,'Tiina','Testi','HELSINKI',1970,40,''),(802,'Tim','Morrison','HELSINKI',1970,44,''),(805,'Aku','Ankka','HELSINKI',1960,35,''),(806,'Pelle','Peloton','PORI',1970,40,'PASS806'),(808,'James','Smith','LONDON',1960,40,'$2a$11$32BfpcKSpt77ehTayE/cwO/MizXXDIsZEckqhc74VaBHFcRPgLYPa'),(809,'Roope','Ankka','Ankkalinna',1960,35,'$2a$11$C.3bjOiQ67nft/pxlhHi0eY8qBNscEjftqrxH9HqH1YZ1Pf4DyvrC');
/*!40000 ALTER TABLE `person` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `project`
--

DROP TABLE IF EXISTS `project`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `project` (
  `id_project` smallint NOT NULL,
  `pname` varchar(20) NOT NULL,
  `place` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id_project`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `project`
--

LOCK TABLES `project` WRITE;
/*!40000 ALTER TABLE `project` DISABLE KEYS */;
INSERT INTO `project` VALUES (102,'Billing','HELSINKI'),(103,'Store','HELSINKI'),(104,'Selling','TURKU'),(105,'Customers','KUOPIO'),(106,'Statistics','OULU'),(109,'Bookkeeping','HELSINKI'),(120,'ITC','LAPPEENRANTA'),(140,'Purchase','TAMPERE');
/*!40000 ALTER TABLE `project` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-10-17 14:31:18
