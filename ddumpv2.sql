CREATE DATABASE  IF NOT EXISTS `cheeseandmore` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `cheeseandmore`;
-- MySQL dump 10.13  Distrib 8.0.40, for Win64 (x86_64)
--
-- Host: localhost    Database: cheeseandmore
-- ------------------------------------------------------
-- Server version	8.0.40

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
-- Table structure for table `carrito`
--

DROP TABLE IF EXISTS `carrito`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `carrito` (
  `id` int NOT NULL AUTO_INCREMENT,
  `id_usuario` int DEFAULT NULL,
  `id_producto` int DEFAULT NULL,
  `cantidad` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_usuario` (`id_usuario`),
  KEY `id_producto` (`id_producto`),
  CONSTRAINT `carrito_ibfk_1` FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `carrito_ibfk_2` FOREIGN KEY (`id_producto`) REFERENCES `productos` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carrito`
--

LOCK TABLES `carrito` WRITE;
/*!40000 ALTER TABLE `carrito` DISABLE KEYS */;
/*!40000 ALTER TABLE `carrito` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `catalogo`
--

DROP TABLE IF EXISTS `catalogo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `catalogo` (
  `id` int NOT NULL AUTO_INCREMENT,
  `categorias` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `catalogo`
--

LOCK TABLES `catalogo` WRITE;
/*!40000 ALTER TABLE `catalogo` DISABLE KEYS */;
INSERT INTO `catalogo` VALUES (1,'Tablas de queso'),(2,'Brochetas'),(3,'Canapés');
/*!40000 ALTER TABLE `catalogo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `detalleseventosv2`
--

DROP TABLE IF EXISTS `detalleseventosv2`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `detalleseventosv2` (
  `id` int NOT NULL AUTO_INCREMENT,
  `descripcion` varchar(100) NOT NULL,
  `id_evento` int DEFAULT NULL,
  `nombre` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_evento` (`id_evento`),
  CONSTRAINT `detalleseventosv2_ibfk_1` FOREIGN KEY (`id_evento`) REFERENCES `eventos` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detalleseventosv2`
--

LOCK TABLES `detalleseventosv2` WRITE;
/*!40000 ALTER TABLE `detalleseventosv2` DISABLE KEYS */;
INSERT INTO `detalleseventosv2` VALUES (1,'Evento donde se ofrecen brochetas variadas, incluyendo opciones vegetarianas y de carne.',1,'Brochetas '),(2,'Evento con mesas llenas de diferentes tipos de quesos, ideales para degustación.',2,'Quesos'),(3,'Evento con una seleccion de diferentes tipos de embutidos y quesos servidos en vasos.',3,'Embutidos y quesos');
/*!40000 ALTER TABLE `detalleseventosv2` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `eventos`
--

DROP TABLE IF EXISTS `eventos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `eventos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(60) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `eventos`
--

LOCK TABLES `eventos` WRITE;
/*!40000 ALTER TABLE `eventos` DISABLE KEYS */;
INSERT INTO `eventos` VALUES (1,'Mesas de brochetas'),(2,'Mesas de queso'),(3,'Vaso charcuteria');
/*!40000 ALTER TABLE `eventos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productos`
--

DROP TABLE IF EXISTS `productos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `precio` decimal(10,2) NOT NULL,
  `id_categoria` int NOT NULL,
  `stock` int NOT NULL,
  `descripcion` text NOT NULL,
  `preciopromocion` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_categoria` (`id_categoria`),
  CONSTRAINT `productos_ibfk_1` FOREIGN KEY (`id_categoria`) REFERENCES `catalogo` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos`
--

LOCK TABLES `productos` WRITE;
/*!40000 ALTER TABLE `productos` DISABLE KEYS */;
INSERT INTO `productos` VALUES (16,'Tabla de Queso Gourmet Extra Grande',2350.00,1,12,'aTabla de quesos para 6 personas. Disfruta de una selección de quesos franceses, italianos y mexicanos. La tabla incluye: Fromager d’affinois con trufa francés, 120gr;',NULL),(17,'Tabla de quesos grande',959.99,1,16,'Tabla de quesos para 6 personas. Disfruta de una selección de quesos franceses, italianos y mexicanos. La tabla incluye: Fromager d’affinois con trufa francés, 120gr;',NULL),(18,'Tabla de quesos y carnes frías',859.99,1,14,'Tabla de quesos para 6 personas. Disfruta de una selección de quesos franceses, italianos y mexicanos. La tabla incluye: Fromager d’affinois con trufa francés, 120gr;',NULL),(19,'Pinchos morunos',79.99,2,16,'Tabla de quesos para 6 personas. Disfruta de una selección de quesos franceses, italianos y mexicanos. La tabla incluye: Fromager d’affinois con trufa francés, 120gr;',NULL),(20,'Brochetas de cerdo caramelizado con piña',77.00,2,2,'Tabla de quesos para 6 personas. Disfruta de una selección de quesos franceses, italianos y mexicanos. La tabla incluye: Fromager d’affinois con trufa francés, 120gr;',NULL),(22,'Brochetas de nabo a la parrilla',59.99,2,16,'Tabla de quesos para 6 personas. Disfruta de una selección de quesos franceses, italianos y mexicanos. La tabla incluye: Fromager d’affinois con trufa francés, 120gr;',NULL),(23,'Hojaldres y otras masas',89.99,3,21,'Tabla de quesos para 6 personas. Disfruta de una selección de quesos franceses, italianos y mexicanos. La tabla incluye: Fromager d’affinois con trufa francés, 120gr;',NULL),(25,'Canapés de crema de aguacate y bacalao',119.99,3,18,'Tabla de quesos para 6 personas. Disfruta de una selección de quesos franceses, italianos y mexicanos. La tabla incluye: Fromager d’affinois con trufa francés, 120gr;',NULL),(26,'Canapé de salmón',89.99,3,4,'Tabla de quesos para 6 personas. Disfruta de una selección de quesos franceses, italianos y mexicanos. La tabla incluye: Fromager d’affinois con trufa francés, 120gr;',NULL);
/*!40000 ALTER TABLE `productos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `recomendaciones`
--

DROP TABLE IF EXISTS `recomendaciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recomendaciones` (
  `id` int NOT NULL AUTO_INCREMENT,
  `estrellas` int NOT NULL,
  `descripcion` varchar(60) NOT NULL,
  `id_usuarios` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `recomendaciones_ibfk_1` (`id_usuarios`),
  CONSTRAINT `recomendaciones_ibfk_1` FOREIGN KEY (`id_usuarios`) REFERENCES `usuarios` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recomendaciones`
--

LOCK TABLES `recomendaciones` WRITE;
/*!40000 ALTER TABLE `recomendaciones` DISABLE KEYS */;
INSERT INTO `recomendaciones` VALUES (11,5,'xdxdxdxdxddxdxdxddxxdxddxddxdddddddddddddddddd',5),(13,4,'sss',4),(14,5,'Que bien',3),(15,3,'JEje',2);
/*!40000 ALTER TABLE `recomendaciones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre_usuario` varchar(50) NOT NULL,
  `contrasena` varchar(255) NOT NULL,
  `correo` varchar(100) NOT NULL,
  `rol` int NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `correo` (`correo`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'admin','admin123','admin@queseria.com',1),(2,'cliente1','cliente123','cliente1@dominio.com',2),(3,'cliente2','cliente456','cliente2@dominio.com',2),(4,'Emmanuel Rocha','6BD44836198DFDB9142343BBB627CFF3C74F5B5AB2CD0255EC1A7CB10C3AAF27611E13E7A0756B22A3524BF112A30631778AFEFB520FF35CA92DABF251FE92D6','emma@gmail.com',1),(5,'123','B2FD667EDB44F69C26646EB08F42A7C6F0D475A110BD000433EDEFEE46AB2C3DACC6D21DB3B9A8C74B5B347C9DE47529DB4061C2BAE4D1E5C58C7DD356A2DCDC','123@gmail.com',2);
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'cheeseandmore'
--

--
-- Dumping routines for database 'cheeseandmore'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-03-14 11:58:41
