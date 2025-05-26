-- MySQL dump 10.13  Distrib 8.0.25, for Win64 (x86_64)
--
-- Host: 182.50.133.86    Database: bnt_studenthub
-- ------------------------------------------------------
-- Server version	5.5.51-38.1-log

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
-- Table structure for table `ad_answers`
--

DROP TABLE IF EXISTS `ad_answers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ad_answers` (
  `adanswerid` int(11) NOT NULL AUTO_INCREMENT,
  `adanswer_loginid` int(11) NOT NULL,
  `adanswer_mocktestid` int(11) NOT NULL,
  `adanswer_timerid` int(11) NOT NULL,
  `adanswer_questionid` int(11) NOT NULL,
  `adanswer_answer` varchar(255) NOT NULL,
  `adanswer_open` char(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`adanswerid`)
) ENGINE=MyISAM AUTO_INCREMENT=213 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ad_mock_payments`
--

DROP TABLE IF EXISTS `ad_mock_payments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ad_mock_payments` (
  `mockpaymentid` int(11) NOT NULL AUTO_INCREMENT,
  `mock_login_id` int(11) NOT NULL,
  `mock_exmcrs_id` int(11) NOT NULL,
  `mock_mocktest_id` int(11) NOT NULL,
  `mock_payment_mode` varchar(250) NOT NULL,
  `mock_razorpayment_id` varchar(255) NOT NULL,
  `mock_razororder_id` varchar(255) NOT NULL,
  `mock_ccavenue_order_id` varchar(255) NOT NULL,
  `mock_ccavenue_tracking_id` varchar(255) NOT NULL,
  `mock_ccavenue_bank_ref_no` varchar(250) NOT NULL,
  `mock_ccavenue_order_status` varchar(25) NOT NULL,
  `mock_ccavenue_failure_message` text NOT NULL,
  `mock_ccavenue_payment_mode` varchar(250) NOT NULL,
  `mock_ccavenue_card_name` varchar(250) NOT NULL,
  `mock_ccavenue_status_code` varchar(250) NOT NULL,
  `mock_ccavenue_status_message` text NOT NULL,
  `mock_ccavenue_currency` varchar(250) NOT NULL,
  `mock_ccavenue_amount` varchar(250) NOT NULL,
  `mock_ccavenue_billing_name` varchar(250) NOT NULL,
  `mock_ccavenue_billing_state` varchar(250) NOT NULL,
  `mock_ccavenue_billing_address` tinytext NOT NULL,
  `mock_ccavenue_billing_city` varchar(250) NOT NULL,
  `mock_ccavenue_billing_zip` varchar(250) NOT NULL,
  `mock_ccavenue_billing_country` varchar(250) NOT NULL,
  `mock_ccavenue_billing_tel` varchar(250) NOT NULL,
  `mock_ccavenue_billing_email` varchar(250) NOT NULL,
  `mock_ccavenue_delivery_name` varchar(250) NOT NULL,
  `mock_ccavenue_delivery_address` varchar(250) NOT NULL,
  `mock_ccavenue_delivery_city` varchar(250) NOT NULL,
  `mock_ccavenue_delivery_state` varchar(250) NOT NULL,
  `mock_ccavenue_delivery_zip` varchar(250) NOT NULL,
  `mock_ccavenue_delivery_country` varchar(250) NOT NULL,
  `mock_ccavenue_delivery_tel` varchar(250) NOT NULL,
  `mock_ccavenue_merchant_param1` varchar(250) NOT NULL,
  `mock_ccavenue_merchant_param2` varchar(250) NOT NULL,
  `mock_ccavenue_merchant_param3` varchar(250) NOT NULL,
  `mock_ccavenue_merchant_param4` varchar(250) NOT NULL,
  `mock_ccavenue_merchant_param5` varchar(250) NOT NULL,
  `mock_ccavenue_vault` varchar(250) NOT NULL,
  `mock_ccavenue_offer_type` varchar(250) NOT NULL,
  `mock_ccavenue_offer_code` varchar(250) NOT NULL,
  `mock_ccavenue_discount_value` varchar(250) NOT NULL,
  `mock_ccavenue_mer_amount` varchar(250) NOT NULL,
  `mock_ccavenue_eci_value` varchar(250) NOT NULL,
  `mock_ccavenue_retry` varchar(250) NOT NULL,
  `mock_ccavenue_response_code` varchar(250) NOT NULL,
  `mock_ccavenue_billing_notes` varchar(250) NOT NULL,
  `mock_ccavenue_trans_date` varchar(250) NOT NULL,
  `mock_ccavenue_bin_country` varchar(250) NOT NULL,
  `mock_ccavenue_status` char(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`mockpaymentid`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ad_mock_register`
--

DROP TABLE IF EXISTS `ad_mock_register`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ad_mock_register` (
  `adloginid` int(11) NOT NULL AUTO_INCREMENT,
  `adlogin_name` varchar(255) NOT NULL,
  `adlogin_password` varchar(250) NOT NULL,
  `adlogin_emailid` varchar(250) NOT NULL,
  `adlogin_mobileno` varchar(250) NOT NULL,
  `adlogin_mobileverified` varchar(250) NOT NULL DEFAULT 'Not Verified',
  `adlogin_created_on` datetime NOT NULL,
  `adlogin_status` char(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`adloginid`)
) ENGINE=MyISAM AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ad_otp_logs`
--

DROP TABLE IF EXISTS `ad_otp_logs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ad_otp_logs` (
  `otp_id` int(11) NOT NULL AUTO_INCREMENT,
  `otp_key` int(11) NOT NULL,
  `otp_mobile_no` varchar(25) NOT NULL,
  `otp_sent_time` datetime NOT NULL,
  `otp_status` char(1) NOT NULL DEFAULT '1' COMMENT '1- Active,0 - Inactive',
  PRIMARY KEY (`otp_id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ad_timer`
--

DROP TABLE IF EXISTS `ad_timer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ad_timer` (
  `adtimerid` int(11) NOT NULL AUTO_INCREMENT,
  `adtimer_login_id` int(11) NOT NULL,
  `adtimer_mocktest_id` int(11) NOT NULL,
  `adtimer_timer` varchar(25) NOT NULL,
  `adtimer_date` date NOT NULL,
  `adtimer_acde` char(1) NOT NULL DEFAULT '0' COMMENT '0 - Start,1 - Closed, 2 - Resume , 3 - Cancel',
  PRIMARY KEY (`adtimerid`)
) ENGINE=MyISAM AUTO_INCREMENT=27 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ci_sessions`
--

DROP TABLE IF EXISTS `ci_sessions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ci_sessions` (
  `id` varchar(40) NOT NULL,
  `ip_address` varchar(45) NOT NULL,
  `timestamp` int(10) unsigned NOT NULL DEFAULT '0',
  `data` blob NOT NULL,
  PRIMARY KEY (`id`),
  KEY `ci_sessions_timestamp` (`timestamp`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary view structure for view `getallstudentslist`
--

DROP TABLE IF EXISTS `getallstudentslist`;
/*!50001 DROP VIEW IF EXISTS `getallstudentslist`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `getallstudentslist` AS SELECT 
 1 AS `ID`,
 1 AS `FullName`,
 1 AS `City`,
 1 AS `Email`,
 1 AS `Status`,
 1 AS `ProfilePic`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `his_shub_addresses`
--

DROP TABLE IF EXISTS `his_shub_addresses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_addresses` (
  `addrs_his_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `addrs_his_TransactionType` varchar(50) DEFAULT NULL,
  `addrs_his_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `addrs_Id` int(10) unsigned NOT NULL,
  `addrs_InstitutionId` int(10) unsigned NOT NULL,
  `addrs_name` varchar(30) NOT NULL,
  `addrs_Line1` varchar(100) NOT NULL,
  `addrs_Line2` varchar(50) DEFAULT NULL,
  `addrs_Line3` varchar(50) DEFAULT NULL,
  `addrs_Area` varchar(30) NOT NULL,
  `addrs_City` varchar(45) NOT NULL,
  `addrs_Pincode` varchar(10) NOT NULL,
  `addrs_State` varchar(30) DEFAULT NULL,
  `addrs_Country` varchar(30) DEFAULT NULL,
  `addrs_MapLocation` text,
  `addrs_Phone1` varchar(20) DEFAULT NULL,
  `addrs_Phone2` varchar(20) DEFAULT NULL,
  `addrs_Mobile1` varchar(20) DEFAULT NULL,
  `addrs_Mobile2` varchar(20) DEFAULT NULL,
  `addrs_ContactPerson` varchar(50) DEFAULT NULL,
  `addrs_Email1` varchar(150) DEFAULT NULL,
  `addrs_Email2` varchar(150) DEFAULT NULL,
  `addrs_Status` varchar(10) DEFAULT 'ACTIVE',
  `addrs_CreatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`addrs_his_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='history addresses of institutes, colleges etc';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_collegecourse`
--

DROP TABLE IF EXISTS `his_shub_collegecourse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_collegecourse` (
  `coll_his_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `coll_his_TransactionType` varchar(50) DEFAULT NULL,
  `coll_his_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `colcrs_Id` int(10) unsigned NOT NULL,
  `colcrs_Name` varchar(100) NOT NULL,
  `colcrs_UrlKeyword` varchar(150) DEFAULT NULL,
  `colcrs_CourseType` int(10) unsigned DEFAULT NULL,
  `colcrs_CourseLevel` int(10) unsigned DEFAULT NULL COMMENT 'After10th,AfterGrad..',
  `colcrs_DegreeType` int(10) unsigned DEFAULT NULL,
  `colcrs_Logo` varchar(150) DEFAULT NULL,
  `colcrs_CoverPic` varchar(150) DEFAULT NULL,
  `colcrs_ShortDescription` varchar(500) DEFAULT NULL,
  `colcrs_Description` text,
  `colcrs_IsTrending` varchar(10) DEFAULT NULL,
  `colcrs_IsFeatured` varchar(15) DEFAULT NULL,
  `colcrs_IsTop` varchar(15) DEFAULT NULL,
  `colcrs_EligibilityCriteria` text,
  `colcrs_Duration` varchar(30) DEFAULT NULL,
  `colcrs_ExamType` varchar(100) DEFAULT NULL,
  `colcrs_FeeDesc` varchar(500) DEFAULT NULL,
  `colcrs_FeeMinimum` varchar(30) DEFAULT NULL,
  `colcrs_FeeMaximum` varchar(30) DEFAULT NULL,
  `colcrs_AdmissionProcess` text,
  `colcrs_TopEntranceTest` text,
  `colcrs_SelectionProcess` text,
  `colcrs_LateralEntry` text,
  `colcrs_AcademicOptions` text,
  `colcrs_SalaryOfferedFreshes` varchar(30) DEFAULT NULL,
  `colcrs_JobProfile` text,
  `colcrs_JobProspects` text,
  `colcrs_Top5Recruiters` varchar(250) DEFAULT NULL,
  `colcrs_Status` varchar(10) DEFAULT 'ACTIVE',
  `colcrs_CreatedDate` datetime DEFAULT NULL,
  `colcrs_ModifiedDate` datetime DEFAULT NULL,
  `colcrs_CreatedBy` int(11) DEFAULT NULL,
  `colcrs_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`coll_his_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_collegecoursesrelation`
--

DROP TABLE IF EXISTS `his_shub_collegecoursesrelation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_collegecoursesrelation` (
  `coll_his_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `coll_his_TransactionType` varchar(50) DEFAULT NULL,
  `coll_his_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `collcrsrel_Id` int(10) unsigned NOT NULL,
  `collcrsrel_CollegeId` int(10) unsigned NOT NULL,
  `collcrsrel_CollegeCourseId` int(10) unsigned NOT NULL,
  `collcrsrel_Seats` varchar(30) DEFAULT NULL,
  `collcrsrel_FeeDetails` varchar(150) DEFAULT NULL,
  `collcrsrel_Mode` varchar(30) DEFAULT NULL,
  `collcrsrel_Duration` varchar(30) DEFAULT NULL,
  `collcrsrel_CreatedDate` datetime DEFAULT NULL,
  `collcrsrel_CreatedBy` int(11) DEFAULT NULL,
  `collcrsrel_UpdatedDate` datetime DEFAULT NULL,
  `collcrsrel_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`coll_his_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1 COMMENT='college and courses relation';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_collegefacilities`
--

DROP TABLE IF EXISTS `his_shub_collegefacilities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_collegefacilities` (
  `coll_his_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `coll_his_TransactionType` varchar(50) DEFAULT NULL,
  `coll_his_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `collfac_Id` int(10) unsigned NOT NULL,
  `collfac_CollegeId` int(10) unsigned NOT NULL,
  `collfac_FacilityId` int(10) unsigned NOT NULL,
  `collfac_CreatedDate` datetime DEFAULT NULL,
  `collfac_CreatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`coll_his_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_collegeimages`
--

DROP TABLE IF EXISTS `his_shub_collegeimages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_collegeimages` (
  `coll_his_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `coll_his_TransactionType` varchar(50) DEFAULT NULL,
  `coll_his_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `collimg_Id` int(10) unsigned NOT NULL,
  `collimg_CollegeId` int(10) unsigned NOT NULL,
  `collimg_ImageName` varchar(150) DEFAULT NULL,
  `collimg_CreatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`coll_his_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_colleges`
--

DROP TABLE IF EXISTS `his_shub_colleges`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_colleges` (
  `coll_his_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `coll_his_TransactionType` varchar(50) DEFAULT NULL,
  `coll_his_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `coll_ID` int(10) unsigned NOT NULL,
  `coll_Name` varchar(250) NOT NULL,
  `coll_UrlKeyword` varchar(250) DEFAULT NULL,
  `coll_Type` int(10) unsigned NOT NULL,
  `coll_Description` text,
  `coll_Logo` varchar(150) DEFAULT NULL,
  `coll_CoverPic` varchar(150) DEFAULT NULL,
  `coll_AddressLine1` varchar(150) NOT NULL,
  `coll_AddressLine2` varchar(150) DEFAULT NULL,
  `coll_AddressLine3` varchar(150) DEFAULT NULL,
  `coll_City` varchar(50) NOT NULL,
  `coll_State` varchar(50) NOT NULL,
  `coll_Pincode` varchar(15) NOT NULL,
  `coll_Country` varchar(45) NOT NULL,
  `coll_Location` varchar(500) DEFAULT NULL,
  `coll_Approval` varchar(50) DEFAULT NULL,
  `coll_Accreditation` varchar(50) DEFAULT NULL,
  `coll_Ownership` varchar(30) DEFAULT NULL,
  `coll_EducationStatus` varchar(30) DEFAULT NULL COMMENT 'co-ed...',
  `coll_Website` varchar(100) DEFAULT NULL,
  `coll_Phone1` varchar(20) DEFAULT NULL,
  `coll_Phone2` varchar(20) DEFAULT NULL,
  `coll_Email1` varchar(100) DEFAULT NULL,
  `coll_Email2` varchar(100) DEFAULT NULL,
  `coll_Facebook` varchar(150) DEFAULT NULL,
  `coll_Twitter` varchar(150) DEFAULT NULL,
  `coll_Youtube` varchar(150) DEFAULT NULL,
  `coll_HighestPackage` varchar(45) DEFAULT NULL,
  `coll_AveragePackage` varchar(45) DEFAULT NULL,
  `coll_LowestPackage` varchar(45) DEFAULT NULL,
  `coll_InternationalPackage` varchar(45) DEFAULT NULL,
  `coll_Recruiters` varchar(1000) DEFAULT NULL,
  `coll_EstablishedYear` varchar(10) DEFAULT NULL,
  `coll_CampusSize` varchar(45) DEFAULT NULL,
  `coll_CreatedDate` datetime DEFAULT NULL,
  `coll_Status` varchar(15) NOT NULL,
  `coll_IsTrending` varchar(15) DEFAULT NULL,
  `coll_IsFeatured` varchar(15) DEFAULT NULL,
  `coll_IsTop` varchar(15) DEFAULT NULL,
  `coll_UpdatedDate` datetime DEFAULT NULL,
  `coll_CreatedBy` int(11) DEFAULT NULL,
  `coll_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`coll_his_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_dropdowntypes`
--

DROP TABLE IF EXISTS `his_shub_dropdowntypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_dropdowntypes` (
  `type_his_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `type_his_TransactionType` varchar(15) DEFAULT NULL,
  `type_his_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `type_Id` int(10) unsigned NOT NULL,
  `type_Name` varchar(30) NOT NULL COMMENT 'COURSESTEAM/NEWS/JOBS/TRAININGMODE',
  `type_Value` varchar(100) NOT NULL,
  `type_UrlKeyword` varchar(100) DEFAULT NULL,
  `type_CreatedDate` datetime DEFAULT NULL,
  `type_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`type_his_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=64 DEFAULT CHARSET=latin1 COMMENT='all types of classifications, types for courses, news, job alerst and more.';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_examslistbytype`
--

DROP TABLE IF EXISTS `his_shub_examslistbytype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_examslistbytype` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `examlist_id` varchar(55) NOT NULL,
  `state_id` varchar(55) NOT NULL,
  `exam_type_id` varchar(55) NOT NULL,
  `exam_type` varchar(255) NOT NULL,
  `examslist_title` varchar(250) NOT NULL,
  `results_api` varchar(250) NOT NULL,
  `api_key` varchar(55) NOT NULL,
  `status` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_examtype`
--

DROP TABLE IF EXISTS `his_shub_examtype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_examtype` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `exam_type_id` varchar(55) NOT NULL,
  `state_id` varchar(55) NOT NULL,
  `exam_type_title` varchar(100) NOT NULL,
  `created_date` date NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_institutioncourses`
--

DROP TABLE IF EXISTS `his_shub_institutioncourses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_institutioncourses` (
  `inscrs_his_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `inscrs_his_TransactionType` varchar(50) DEFAULT NULL,
  `inscrs_his_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `inscrs_Id` int(10) unsigned NOT NULL,
  `inscrs_InstitutionId` int(10) unsigned NOT NULL,
  `inscrs_CourseId` int(10) unsigned NOT NULL,
  `inscrs_CourseFee` varchar(25) DEFAULT NULL,
  `inscrs_Duration` varchar(30) DEFAULT NULL,
  `inscrs_ClassRoomTraining` varchar(10) DEFAULT 'false',
  `inscrs_OnlineTraining` varchar(10) DEFAULT 'false',
  `inscrs_Proxy` varchar(10) DEFAULT 'false',
  `inscrs_CorporateTraining` varchar(10) DEFAULT 'false',
  `inscrs_Internship` varchar(10) DEFAULT 'false',
  `inscrs_CreatedDate` datetime DEFAULT NULL,
  `inscrs_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`inscrs_his_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COMMENT='courses teached by an institution';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_institutioncoursestrainingmodes`
--

DROP TABLE IF EXISTS `his_shub_institutioncoursestrainingmodes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_institutioncoursestrainingmodes` (
  `trnmd_his_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `trnmd_his_TransactionType` varchar(50) DEFAULT NULL,
  `trnmd_his_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `trnmd_Id` int(10) unsigned NOT NULL,
  `trnmd_CourseId` int(10) unsigned DEFAULT NULL,
  `trnmd_TrainingModeId` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`trnmd_his_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_institutions`
--

DROP TABLE IF EXISTS `his_shub_institutions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_institutions` (
  `ins_his_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ins_his_TransactionType` varchar(50) DEFAULT NULL,
  `ins_his_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `ins_Id` int(10) unsigned NOT NULL,
  `ins_Name` varchar(150) NOT NULL,
  `ins_Type` varchar(20) NOT NULL COMMENT 'INSTITUTION/PERSON',
  `ins_UrlKeyword` varchar(150) NOT NULL,
  `ins_About` text,
  `ins_Address` varchar(200) DEFAULT NULL,
  `ins_Address2` varchar(50) DEFAULT NULL,
  `ins_Address3` varchar(50) DEFAULT NULL,
  `ins_Area` varchar(50) NOT NULL,
  `ins_City` varchar(50) NOT NULL,
  `ins_MapLocation` varchar(45) DEFAULT NULL,
  `ins_Pincode` varchar(10) DEFAULT NULL,
  `ins_State` varchar(30) NOT NULL,
  `ins_Country` varchar(30) DEFAULT NULL,
  `ins_Phone1` varchar(20) DEFAULT NULL,
  `ins_Phone2` varchar(20) DEFAULT NULL,
  `ins_LandLine` varchar(20) DEFAULT NULL,
  `ins_Email1` varchar(50) DEFAULT NULL,
  `ins_Email2` varchar(50) DEFAULT NULL,
  `ins_Website` varchar(60) DEFAULT NULL,
  `ins_Status` varchar(15) DEFAULT NULL,
  `ins_IsTop` varchar(15) DEFAULT NULL,
  `ins_IsFeatured` varchar(10) DEFAULT 'false',
  `ins_IsTrending` varchar(10) DEFAULT 'false',
  `ins_JobAssistance` varchar(10) DEFAULT 'false',
  `ins_TrainingSatisfaction` varchar(10) DEFAULT 'false',
  `ins_JobGurarantee` varchar(10) DEFAULT 'false',
  `ins_IsVerified` varchar(10) DEFAULT 'false',
  `ins_Logo` varchar(100) DEFAULT NULL,
  `ins_Broucher` varchar(100) DEFAULT NULL,
  `ins_CreatedDate` datetime DEFAULT NULL,
  `ins_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ins_his_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_maincourses`
--

DROP TABLE IF EXISTS `his_shub_maincourses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_maincourses` (
  `course_his_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `course_his_TransactionType` varchar(50) DEFAULT NULL,
  `course_his_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `course_Id` int(10) unsigned NOT NULL,
  `course_Name` varchar(100) NOT NULL,
  `course_UrlKeyword` varchar(150) NOT NULL,
  `course_Description` text,
  `course_Syllabus` text,
  `course_Eligibility` text,
  `course_IsInTopList` varchar(10) DEFAULT 'false',
  `course_IsFeatured` varchar(10) DEFAULT 'false',
  `course_IsTrending` varchar(10) DEFAULT 'false',
  `course_Stream` int(11) unsigned DEFAULT NULL,
  `course_Logo` varchar(100) DEFAULT NULL,
  `course_Status` varchar(15) DEFAULT 'ACTIVE',
  `course_MetaTitle` varchar(150) DEFAULT NULL,
  `course_MetaDescription` varchar(250) DEFAULT NULL,
  `course_CreatedDate` datetime DEFAULT NULL,
  `course_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`course_his_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_mocktestquestions`
--

DROP TABLE IF EXISTS `his_shub_mocktestquestions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_mocktestquestions` (
  `ques_his_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ques_his_TransactionType` varchar(15) DEFAULT NULL,
  `ques_his_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `ques_Id` int(10) unsigned NOT NULL,
  `ques_MockTestId` int(10) unsigned NOT NULL,
  `ques_QuestionText` text CHARACTER SET utf8,
  `ques_QuestionImage` varchar(250) DEFAULT NULL,
  `ques_AnswerOption` varchar(5) NOT NULL,
  `ques_AnswerOption2` varchar(5) DEFAULT NULL,
  `ques_AnswerOption3` varchar(5) DEFAULT NULL,
  `ques_OptionA` text CHARACTER SET utf8,
  `ques_OptionAImage` varchar(100) DEFAULT NULL,
  `ques_OptionB` text CHARACTER SET utf8,
  `ques_OptionBImage` varchar(250) DEFAULT NULL,
  `ques_OptionC` text CHARACTER SET utf8,
  `ques_OptionCImage` varchar(250) DEFAULT NULL,
  `ques_OptionD` text CHARACTER SET utf8,
  `ques_OptionDImage` varchar(250) DEFAULT NULL,
  `ques_OptionE` text CHARACTER SET utf8,
  `ques_OptionEImage` varchar(250) DEFAULT NULL,
  `ques_Explanation` text CHARACTER SET utf8,
  `ques_ExplanationImage` varchar(250) DEFAULT NULL,
  `ques_CreatedDate` timestamp NULL DEFAULT NULL,
  `ques_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ques_his_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_mocktests`
--

DROP TABLE IF EXISTS `his_shub_mocktests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_mocktests` (
  `mocktest_his_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `mocktest_his_TransactionType` varchar(15) DEFAULT NULL,
  `mocktest_his_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `mocktest_Id` int(10) unsigned NOT NULL,
  `mocktest_CourseId` int(10) unsigned NOT NULL,
  `mocktest_Difficulty` varchar(15) DEFAULT NULL,
  `mocktest_Title` varchar(500) NOT NULL,
  `mocktest_Type` int(10) unsigned DEFAULT NULL,
  `mocktest_Subject` varchar(100) DEFAULT NULL,
  `mocktest_Chapter` varchar(100) DEFAULT NULL,
  `mocktest_Year` varchar(20) DEFAULT NULL,
  `mocktest_QuestionMaxTime` varchar(20) DEFAULT NULL,
  `mocktest_TestMaxTime` varchar(20) DEFAULT NULL,
  `mocktest_Description` varchar(1000) CHARACTER SET utf8 DEFAULT NULL,
  `mocktest_MaximumQuestions` varchar(5) DEFAULT NULL,
  `mocktest_CreatedDate` datetime DEFAULT NULL,
  `mocktest_UpdatedDate` datetime DEFAULT NULL,
  `ques_Price` varchar(50) DEFAULT NULL,
  `ques_DiscountOfferPrice` varchar(50) DEFAULT NULL,
  `ques_CouponCodeOffer` varchar(100) DEFAULT NULL,
  `ques_FreePaidTest` varchar(45) DEFAULT NULL,
  `ques_GovtPrivateExam` varchar(45) DEFAULT NULL,
  `ques_Language` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`mocktest_his_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `his_shub_states`
--

DROP TABLE IF EXISTS `his_shub_states`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `his_shub_states` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `state_id` varchar(55) NOT NULL,
  `created_date` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `rslt_CourseTypes`
--

DROP TABLE IF EXISTS `rslt_CourseTypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rslt_CourseTypes` (
  `corseTyp_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `corseTyp_ID` varchar(20) NOT NULL,
  `corseTyp_Name` varchar(100) NOT NULL,
  `corseTyp_State` varchar(45) NOT NULL,
  `corseTyp_ExamType` varchar(45) DEFAULT NULL,
  `corseTyp_University` varchar(100) DEFAULT NULL,
  `corseTyp_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `corseTyp_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`corseTyp_Sno`)
) ENGINE=MyISAM AUTO_INCREMENT=19 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `rslt_DistrictsMaster`
--

DROP TABLE IF EXISTS `rslt_DistrictsMaster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rslt_DistrictsMaster` (
  `dstrct_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `dstrct_ID` varchar(10) NOT NULL,
  `dstrct_Name` varchar(80) NOT NULL,
  `dstrct_ExamType` int(11) DEFAULT NULL,
  `dstrct_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `dstrct_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`dstrct_Sno`)
) ENGINE=MyISAM AUTO_INCREMENT=144 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `rslt_IntermediateYear2Regular`
--

DROP TABLE IF EXISTS `rslt_IntermediateYear2Regular`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rslt_IntermediateYear2Regular` (
  `inter2reg_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `inter2reg_ExamType` int(11) NOT NULL,
  `inter2reg_ExamYear` varchar(10) NOT NULL,
  `inter2reg_FullRecord` varchar(250) DEFAULT NULL,
  `inter2reg_ROLLNO` varchar(15) DEFAULT NULL,
  `inter2reg_DIST` varchar(5) DEFAULT NULL,
  `inter2reg_CNAME` varchar(50) DEFAULT NULL,
  `inter2reg_YR1PC1` varchar(5) DEFAULT NULL,
  `inter2reg_YR1MKS1` varchar(5) DEFAULT NULL,
  `inter2reg_YR1IND1` varchar(5) DEFAULT NULL,
  `inter2reg_YR1RS1` varchar(5) DEFAULT NULL,
  `inter2reg_YR1PC2` varchar(5) DEFAULT NULL,
  `inter2reg_YR1MKS2` varchar(5) DEFAULT NULL,
  `inter2reg_YR1IND2` varchar(5) DEFAULT NULL,
  `inter2reg_YR1RS2` varchar(5) DEFAULT NULL,
  `inter2reg_YR1PC3` varchar(5) DEFAULT NULL,
  `inter2reg_YR1MKS3` varchar(5) DEFAULT NULL,
  `inter2reg_YR1IND3` varchar(5) DEFAULT NULL,
  `inter2reg_YR1RS3` varchar(5) DEFAULT NULL,
  `inter2reg_YR1PC4` varchar(5) DEFAULT NULL,
  `inter2reg_YR1MKS4` varchar(5) DEFAULT NULL,
  `inter2reg_YR1IND4` varchar(5) DEFAULT NULL,
  `inter2reg_YR1RS4` varchar(5) DEFAULT NULL,
  `inter2reg_YR1PC5` varchar(5) DEFAULT NULL,
  `inter2reg_YR1MKS5` varchar(5) DEFAULT NULL,
  `inter2reg_YR1IND5` varchar(5) DEFAULT NULL,
  `inter2reg_YR1RS5` varchar(5) DEFAULT NULL,
  `inter2reg_YR1PC6` varchar(5) DEFAULT NULL,
  `inter2reg_YR1MKS6` varchar(5) DEFAULT NULL,
  `inter2reg_YR1IND6` varchar(5) DEFAULT NULL,
  `inter2reg_YR1RS6` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC1` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS1` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND1` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS1` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC2` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS2` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND2` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS2` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC3` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS3` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND3` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS3` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC4` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS4` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND4` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS4` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC5` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS5` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND5` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS5` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC6` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS6` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND6` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS6` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC7` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS7` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND7` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS7` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC8` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS8` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND8` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS8` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC9` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS9` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND9` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS9` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC10` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS10` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND10` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS10` varchar(5) DEFAULT NULL,
  `inter2reg_TOTAL` varchar(5) DEFAULT NULL,
  `inter2reg_RESULT` varchar(8) DEFAULT NULL,
  `inter2reg_ADD_FLG` varchar(2) DEFAULT NULL,
  `inter2reg_LINKNO` varchar(10) DEFAULT NULL,
  `inter2reg_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `inter2reg_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`inter2reg_Sno`),
  KEY `PI_rslt_IntermediateYear2Regular_RollNo` (`inter2reg_ROLLNO`)
) ENGINE=MyISAM AUTO_INCREMENT=1856219 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `rslt_IntermediateYear2Regular_Temp`
--

DROP TABLE IF EXISTS `rslt_IntermediateYear2Regular_Temp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rslt_IntermediateYear2Regular_Temp` (
  `inter2reg_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `inter2reg_ExamType` int(11) NOT NULL,
  `inter2reg_ExamYear` varchar(10) NOT NULL,
  `inter2reg_FullRecord` varchar(250) DEFAULT NULL,
  `inter2reg_ROLLNO` varchar(15) DEFAULT NULL,
  `inter2reg_DIST` varchar(5) DEFAULT NULL,
  `inter2reg_CNAME` varchar(50) DEFAULT NULL,
  `inter2reg_YR1PC1` varchar(5) DEFAULT NULL,
  `inter2reg_YR1MKS1` varchar(5) DEFAULT NULL,
  `inter2reg_YR1IND1` varchar(5) DEFAULT NULL,
  `inter2reg_YR1RS1` varchar(5) DEFAULT NULL,
  `inter2reg_YR1PC2` varchar(5) DEFAULT NULL,
  `inter2reg_YR1MKS2` varchar(5) DEFAULT NULL,
  `inter2reg_YR1IND2` varchar(5) DEFAULT NULL,
  `inter2reg_YR1RS2` varchar(5) DEFAULT NULL,
  `inter2reg_YR1PC3` varchar(5) DEFAULT NULL,
  `inter2reg_YR1MKS3` varchar(5) DEFAULT NULL,
  `inter2reg_YR1IND3` varchar(5) DEFAULT NULL,
  `inter2reg_YR1RS3` varchar(5) DEFAULT NULL,
  `inter2reg_YR1PC4` varchar(5) DEFAULT NULL,
  `inter2reg_YR1MKS4` varchar(5) DEFAULT NULL,
  `inter2reg_YR1IND4` varchar(5) DEFAULT NULL,
  `inter2reg_YR1RS4` varchar(5) DEFAULT NULL,
  `inter2reg_YR1PC5` varchar(5) DEFAULT NULL,
  `inter2reg_YR1MKS5` varchar(5) DEFAULT NULL,
  `inter2reg_YR1IND5` varchar(5) DEFAULT NULL,
  `inter2reg_YR1RS5` varchar(5) DEFAULT NULL,
  `inter2reg_YR1PC6` varchar(5) DEFAULT NULL,
  `inter2reg_YR1MKS6` varchar(5) DEFAULT NULL,
  `inter2reg_YR1IND6` varchar(5) DEFAULT NULL,
  `inter2reg_YR1RS6` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC1` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS1` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND1` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS1` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC2` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS2` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND2` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS2` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC3` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS3` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND3` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS3` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC4` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS4` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND4` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS4` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC5` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS5` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND5` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS5` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC6` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS6` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND6` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS6` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC7` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS7` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND7` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS7` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC8` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS8` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND8` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS8` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC9` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS9` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND9` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS9` varchar(5) DEFAULT NULL,
  `inter2reg_YR2PC10` varchar(5) DEFAULT NULL,
  `inter2reg_YR2MKS10` varchar(5) DEFAULT NULL,
  `inter2reg_YR2IND10` varchar(5) DEFAULT NULL,
  `inter2reg_YR2RS10` varchar(5) DEFAULT NULL,
  `inter2reg_TOTAL` varchar(5) DEFAULT NULL,
  `inter2reg_RESULT` varchar(8) DEFAULT NULL,
  `inter2reg_ADD_FLG` varchar(2) DEFAULT NULL,
  `inter2reg_LINKNO` varchar(10) DEFAULT NULL,
  `inter2reg_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`inter2reg_Sno`)
) ENGINE=MyISAM AUTO_INCREMENT=488761 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `rslt_IntermediateYear2Voc`
--

DROP TABLE IF EXISTS `rslt_IntermediateYear2Voc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rslt_IntermediateYear2Voc` (
  `inter2voc_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `inter2voc_ExamType` int(11) NOT NULL,
  `inter2voc_ExamYear` varchar(10) NOT NULL,
  `inter2voc_FullRecord` varchar(500) DEFAULT NULL,
  `inter2voc_ROLLNO` varchar(15) DEFAULT NULL,
  `inter2voc_DIST` varchar(5) DEFAULT NULL,
  `inter2voc_CNAME` varchar(50) DEFAULT NULL,
  `inter2voc_COURSE` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FPC1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FMKS1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FIND1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FRES1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FPC2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FMKS2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FIND2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FRES2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TPC1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TMKS1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TIND1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TRES1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TPC2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TMKS2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TIND2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TRES2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TPC3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TMKS3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TIND3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TRES3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PPC1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PMKS1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PIND1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PRES1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PPC2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PMKS2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PIND2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PRES2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PPC3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PMKS3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PIND3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PRES3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PPC4` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PMKS4` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PIND4` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PRES4` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FPC1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FMKS1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FIND1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FRES1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FPC2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FMKS2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FIND2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FRES2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TPC1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TMKS1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TIND1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TRES1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TPC2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TMKS2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TIND2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TRES2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TPC3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TMKS3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TIND3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TRES3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PPC1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PMKS1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PIND1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PRES1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PPC2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PMKS2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PIND2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PRES2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PPC3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PMKS3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PIND3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PRES3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PPC4` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PMKS4` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PIND4` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PRES4` varchar(5) DEFAULT NULL,
  `inter2voc_TOTAL` varchar(10) DEFAULT NULL,
  `inter2voc_RESULT` varchar(10) DEFAULT NULL,
  `inter2voc_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `inter2voc_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`inter2voc_Sno`)
) ENGINE=MyISAM AUTO_INCREMENT=394439 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `rslt_IntermediateYear2Voc_Temp`
--

DROP TABLE IF EXISTS `rslt_IntermediateYear2Voc_Temp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rslt_IntermediateYear2Voc_Temp` (
  `inter2voc_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `inter2voc_ExamType` int(11) NOT NULL,
  `inter2voc_ExamYear` varchar(10) NOT NULL,
  `inter2voc_FullRecord` varchar(500) DEFAULT NULL,
  `inter2voc_ROLLNO` varchar(15) DEFAULT NULL,
  `inter2voc_DIST` varchar(5) DEFAULT NULL,
  `inter2voc_CNAME` varchar(50) DEFAULT NULL,
  `inter2voc_COURSE` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FPC1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FMKS1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FIND1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FRES1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FPC2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FMKS2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FIND2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1FRES2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TPC1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TMKS1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TIND1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TRES1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TPC2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TMKS2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TIND2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TRES2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TPC3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TMKS3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TIND3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1TRES3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PPC1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PMKS1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PIND1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PRES1` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PPC2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PMKS2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PIND2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PRES2` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PPC3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PMKS3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PIND3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PRES3` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PPC4` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PMKS4` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PIND4` varchar(5) DEFAULT NULL,
  `inter2voc_YR1PRES4` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FPC1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FMKS1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FIND1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FRES1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FPC2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FMKS2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FIND2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2FRES2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TPC1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TMKS1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TIND1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TRES1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TPC2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TMKS2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TIND2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TRES2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TPC3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TMKS3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TIND3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2TRES3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PPC1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PMKS1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PIND1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PRES1` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PPC2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PMKS2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PIND2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PRES2` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PPC3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PMKS3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PIND3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PRES3` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PPC4` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PMKS4` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PIND4` varchar(5) DEFAULT NULL,
  `inter2voc_YR2PRES4` varchar(5) DEFAULT NULL,
  `inter2voc_TOTAL` varchar(10) DEFAULT NULL,
  `inter2voc_RESULT` varchar(10) DEFAULT NULL,
  `inter2voc_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`inter2voc_Sno`)
) ENGINE=MyISAM AUTO_INCREMENT=39137 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `rslt_SubjectsMaster`
--

DROP TABLE IF EXISTS `rslt_SubjectsMaster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rslt_SubjectsMaster` (
  `sbjct_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `sbjct_ID` varchar(10) NOT NULL,
  `sbjct_Name` varchar(100) NOT NULL,
  `sbjct_CourseID` int(11) DEFAULT NULL,
  `sbjct_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `sbjct_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`sbjct_Sno`)
) ENGINE=MyISAM AUTO_INCREMENT=20924 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `rslt_UploadedResults`
--

DROP TABLE IF EXISTS `rslt_UploadedResults`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rslt_UploadedResults` (
  `upld_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `upld_ExamType` int(11) NOT NULL,
  `upld_ExamYear` varchar(20) NOT NULL,
  `upld_ExamMonth` varchar(25) DEFAULT NULL,
  `upld_ResultReleaseDate` varchar(20) DEFAULT NULL,
  `upld_FileName` varchar(150) DEFAULT NULL,
  `upld_Status` varchar(30) DEFAULT 'NEW',
  `upld_CreatedDate` datetime DEFAULT NULL,
  `upld_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`upld_Sno`)
) ENGINE=MyISAM AUTO_INCREMENT=37 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `rslt_WebApiDetails`
--

DROP TABLE IF EXISTS `rslt_WebApiDetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rslt_WebApiDetails` (
  `webapi_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `webapi_Title` varchar(100) NOT NULL,
  `webapi_Description` varchar(1000) DEFAULT NULL,
  `webapi_Url` varchar(250) DEFAULT NULL,
  `webapi_ExamType` int(11) DEFAULT NULL,
  `webapi_CreatedDate` datetime DEFAULT NULL,
  `webapi_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`webapi_Sno`)
) ENGINE=MyISAM AUTO_INCREMENT=18 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `rst_CoursesMaster`
--

DROP TABLE IF EXISTS `rst_CoursesMaster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rst_CoursesMaster` (
  `corse_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `corse_ID` varchar(15) NOT NULL,
  `corse_Name` varchar(100) NOT NULL,
  `corse_CourseType` int(11) DEFAULT NULL,
  `corse_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `corse_ModifiedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`corse_Sno`)
) ENGINE=MyISAM AUTO_INCREMENT=917 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_addresses`
--

DROP TABLE IF EXISTS `shub_addresses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_addresses` (
  `addrs_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `addrs_InstitutionId` int(10) unsigned NOT NULL,
  `addrs_name` varchar(30) NOT NULL,
  `addrs_Line1` varchar(100) NOT NULL,
  `addrs_Line2` varchar(50) DEFAULT NULL,
  `addrs_Line3` varchar(50) DEFAULT NULL,
  `addrs_Area` varchar(30) NOT NULL,
  `addrs_City` varchar(45) NOT NULL,
  `addrs_Pincode` varchar(10) NOT NULL,
  `addrs_State` varchar(30) DEFAULT NULL,
  `addrs_Country` varchar(30) DEFAULT NULL,
  `addrs_MapLocation` text,
  `addrs_Phone1` varchar(20) DEFAULT NULL,
  `addrs_Phone2` varchar(20) DEFAULT NULL,
  `addrs_Mobile1` varchar(20) DEFAULT NULL,
  `addrs_Mobile2` varchar(20) DEFAULT NULL,
  `addrs_ContactPerson` varchar(50) DEFAULT NULL,
  `addrs_Email1` varchar(150) DEFAULT NULL,
  `addrs_Email2` varchar(150) DEFAULT NULL,
  `addrs_Status` varchar(10) DEFAULT 'ACTIVE',
  `addrs_CreatedDate` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`addrs_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='addresses of institutes, colleges etc';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_apply`
--

DROP TABLE IF EXISTS `shub_apply`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_apply` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `from_id` varchar(50) NOT NULL,
  `from_type` varchar(50) NOT NULL,
  `apply_name` varchar(200) NOT NULL,
  `apply_email` varchar(60) NOT NULL,
  `apply_mobileno` varchar(15) NOT NULL,
  `apply_comments` text NOT NULL,
  `created_datetime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_batchschedule`
--

DROP TABLE IF EXISTS `shub_batchschedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_batchschedule` (
  `btch_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `btch_CourseId` varchar(10) NOT NULL,
  `btch_InstructorId1` varchar(10) DEFAULT NULL,
  `btch_InstructorId2` varchar(10) DEFAULT NULL,
  `btch_StartDate` varchar(20) DEFAULT NULL,
  `btch_Timing` varchar(20) DEFAULT NULL,
  `btch_ClassDuration` varchar(20) DEFAULT NULL,
  `btch_ClassMode` varchar(20) DEFAULT NULL,
  `btch_MaxStrength` varchar(10) DEFAULT NULL,
  `btch_Discount` float DEFAULT '0',
  `btch_VideoLink` varchar(250) DEFAULT NULL,
  `btch_PayementLink` varchar(250) DEFAULT NULL,
  `btch_VirtualClassRoomLink` varchar(350) DEFAULT NULL,
  `btch_CourseDuration` varchar(30) DEFAULT NULL,
  `btch_Notes` varchar(3000) DEFAULT NULL,
  `btch_Status` varchar(15) DEFAULT NULL,
  `btch_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `btch_UpdatedDate` datetime DEFAULT NULL,
  `btch_CreatedBy` varchar(20) DEFAULT NULL,
  `btch_UpdatedBy` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`btch_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_batchstudentrelation`
--

DROP TABLE IF EXISTS `shub_batchstudentrelation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_batchstudentrelation` (
  `btchstd_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `btchstd_BatchId` int(11) NOT NULL,
  `btchstd_StudentId` int(11) NOT NULL,
  `btchstd_Status` varchar(20) DEFAULT NULL,
  `btchstd_Payment` varchar(20) DEFAULT NULL,
  `btchstd_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `btchstd_UpdatedDate` datetime DEFAULT NULL,
  `btchstd_CreatedBy` int(11) DEFAULT NULL,
  `btchstd_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`btchstd_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_chapters`
--

DROP TABLE IF EXISTS `shub_chapters`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_chapters` (
  `chptr_Id` int(11) NOT NULL AUTO_INCREMENT,
  `chptr_SectionId` int(11) DEFAULT NULL,
  `chptr_ChapterTitle` varchar(150) DEFAULT NULL,
  `chptr_SortId` int(11) DEFAULT NULL,
  `chptr_Description` varchar(500) DEFAULT NULL,
  `chptr_VideoLink1` varchar(500) DEFAULT NULL,
  `chptr_Video1Duration` varchar(15) DEFAULT NULL,
  `chptr_VideoLink2` varchar(500) DEFAULT NULL,
  `chptr_Video2Duration` varchar(15) DEFAULT NULL,
  `chptr_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `chptr_UpdatedDate` datetime DEFAULT NULL,
  `chptr_CreatedBy` int(11) DEFAULT NULL,
  `chptr_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`chptr_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_collegecourse`
--

DROP TABLE IF EXISTS `shub_collegecourse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_collegecourse` (
  `colcrs_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `colcrs_Name` varchar(100) NOT NULL,
  `colcrs_UrlKeyword` varchar(150) DEFAULT NULL,
  `colcrs_CourseType` int(10) unsigned DEFAULT NULL,
  `colcrs_CourseLevel` int(10) unsigned DEFAULT NULL COMMENT 'After10th,AfterGrad..',
  `colcrs_DegreeType` int(10) unsigned DEFAULT NULL,
  `colcrs_Logo` varchar(150) DEFAULT NULL,
  `colcrs_CoverPic` varchar(150) DEFAULT NULL,
  `colcrs_ShortDescription` varchar(500) DEFAULT NULL,
  `colcrs_Description` text,
  `colcrs_IsTrending` varchar(10) DEFAULT NULL,
  `colcrs_IsFeatured` varchar(15) DEFAULT NULL,
  `colcrs_IsTop` varchar(15) DEFAULT NULL,
  `colcrs_EligibilityCriteria` text,
  `colcrs_Duration` varchar(30) DEFAULT NULL,
  `colcrs_ExamType` varchar(100) DEFAULT NULL,
  `colcrs_FeeDesc` varchar(500) DEFAULT NULL,
  `colcrs_FeeMinimum` varchar(30) DEFAULT NULL,
  `colcrs_FeeMaximum` varchar(30) DEFAULT NULL,
  `colcrs_AdmissionProcess` text,
  `colcrs_TopEntranceTest` text,
  `colcrs_SelectionProcess` text,
  `colcrs_LateralEntry` text,
  `colcrs_AcademicOptions` text,
  `colcrs_SalaryOfferedFreshes` varchar(30) DEFAULT NULL,
  `colcrs_JobProfile` text,
  `colcrs_JobProspects` text,
  `colcrs_Top5Recruiters` varchar(250) DEFAULT NULL,
  `colcrs_Status` varchar(10) DEFAULT 'ACTIVE',
  `colcrs_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `colcrs_ModifiedDate` datetime DEFAULT NULL,
  `colcrs_CreatedBy` int(11) DEFAULT NULL,
  `colcrs_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`colcrs_Id`),
  KEY `FK_CollCourse_CourseLevel_idx` (`colcrs_CourseLevel`),
  KEY `FK_CollCourse_DegreeType_idx` (`colcrs_DegreeType`),
  KEY `FK_CollCourse_CourseType` (`colcrs_CourseType`)
) ENGINE=InnoDB AUTO_INCREMENT=1593 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_collegecoursesrelation`
--

DROP TABLE IF EXISTS `shub_collegecoursesrelation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_collegecoursesrelation` (
  `collcrsrel_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `collcrsrel_CollegeId` int(10) unsigned NOT NULL,
  `collcrsrel_CollegeCourseId` int(10) unsigned NOT NULL,
  `collcrsrel_Seats` varchar(30) DEFAULT NULL,
  `collcrsrel_FeeDetails` varchar(150) DEFAULT NULL,
  `collcrsrel_Mode` varchar(30) DEFAULT NULL,
  `collcrsrel_Duration` varchar(30) DEFAULT NULL,
  `collcrsrel_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `collcrsrel_CreatedBy` int(11) DEFAULT NULL,
  `collcrsrel_UpdatedDate` datetime DEFAULT NULL,
  `collcrsrel_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`collcrsrel_Id`),
  KEY `FK_CollCrsRel_CollId_idx` (`collcrsrel_CollegeId`),
  KEY `FK_CollCrsRel_CollCrsId_idx` (`collcrsrel_CollegeCourseId`)
) ENGINE=InnoDB AUTO_INCREMENT=4104 DEFAULT CHARSET=utf8 COMMENT='college and courses relation';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_collegefacilities`
--

DROP TABLE IF EXISTS `shub_collegefacilities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_collegefacilities` (
  `collfac_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `collfac_CollegeId` int(10) unsigned NOT NULL,
  `collfac_FacilityId` int(10) unsigned NOT NULL,
  `collfac_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `collfac_CreatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`collfac_Id`),
  KEY `FK_CollegeFacility_CollegeId_idx` (`collfac_CollegeId`),
  KEY `FK_CollegeFacility_FacilityId_idx` (`collfac_FacilityId`)
) ENGINE=InnoDB AUTO_INCREMENT=2478 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_collegeimages`
--

DROP TABLE IF EXISTS `shub_collegeimages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_collegeimages` (
  `collimg_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `collimg_CollegeId` int(10) unsigned NOT NULL,
  `collimg_ImageName` varchar(150) DEFAULT NULL,
  `collimg_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`collimg_Id`),
  KEY `FK_CollegeImages_CollegeId_idx` (`collimg_CollegeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_colleges`
--

DROP TABLE IF EXISTS `shub_colleges`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_colleges` (
  `coll_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `coll_Name` varchar(250) NOT NULL,
  `coll_UrlKeyword` varchar(250) DEFAULT NULL,
  `coll_Type` int(10) unsigned NOT NULL,
  `coll_Description` text,
  `coll_Logo` varchar(150) DEFAULT NULL,
  `coll_CoverPic` varchar(150) DEFAULT NULL,
  `coll_AddressLine1` varchar(150) NOT NULL,
  `coll_AddressLine2` varchar(150) DEFAULT NULL,
  `coll_AddressLine3` varchar(150) DEFAULT NULL,
  `coll_City` varchar(50) NOT NULL,
  `coll_State` varchar(50) NOT NULL,
  `coll_Pincode` varchar(15) NOT NULL,
  `coll_Country` varchar(45) NOT NULL,
  `coll_Location` varchar(500) DEFAULT NULL,
  `coll_Approval` varchar(50) DEFAULT NULL,
  `coll_Accreditation` varchar(50) DEFAULT NULL,
  `coll_Ownership` varchar(30) DEFAULT NULL,
  `coll_EducationStatus` varchar(30) DEFAULT NULL COMMENT 'co-ed...',
  `coll_Website` varchar(100) DEFAULT NULL,
  `coll_Phone1` varchar(20) DEFAULT NULL,
  `coll_Phone2` varchar(20) DEFAULT NULL,
  `coll_Email1` varchar(100) DEFAULT NULL,
  `coll_Email2` varchar(100) DEFAULT NULL,
  `coll_Facebook` varchar(150) DEFAULT NULL,
  `coll_Twitter` varchar(150) DEFAULT NULL,
  `coll_Youtube` varchar(150) DEFAULT NULL,
  `coll_HighestPackage` varchar(45) DEFAULT NULL,
  `coll_AveragePackage` varchar(45) DEFAULT NULL,
  `coll_LowestPackage` varchar(45) DEFAULT NULL,
  `coll_InternationalPackage` varchar(45) DEFAULT NULL,
  `coll_Recruiters` varchar(1000) DEFAULT NULL,
  `coll_EstablishedYear` varchar(10) DEFAULT NULL,
  `coll_CampusSize` varchar(45) DEFAULT NULL,
  `coll_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `coll_Status` varchar(15) NOT NULL,
  `coll_IsTrending` varchar(15) DEFAULT NULL,
  `coll_IsFeatured` varchar(15) DEFAULT NULL,
  `coll_IsTop` varchar(15) DEFAULT NULL,
  `coll_UpdatedDate` datetime DEFAULT NULL,
  `coll_CreatedBy` int(11) DEFAULT NULL,
  `coll_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`coll_ID`),
  KEY `shub_colleges_CollegeType_idx` (`coll_Type`)
) ENGINE=InnoDB AUTO_INCREMENT=200 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_colreview`
--

DROP TABLE IF EXISTS `shub_colreview`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_colreview` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `rev_col_id` varchar(15) NOT NULL,
  `rev_col_name` varchar(250) NOT NULL,
  `rev_cou_name` varchar(250) NOT NULL,
  `rev_name` varchar(150) NOT NULL,
  `rev_email` varchar(60) NOT NULL,
  `rev_phone` varchar(15) NOT NULL,
  `rev_batch` varchar(10) NOT NULL,
  `rev_referby` varchar(150) NOT NULL,
  `rev_referemail` varchar(60) NOT NULL,
  `rev_comments` text NOT NULL,
  `rev_rating_money` varchar(5) NOT NULL DEFAULT '0',
  `rev_rating_crowd` varchar(5) NOT NULL DEFAULT '0',
  `rev_rating_placements` varchar(5) NOT NULL DEFAULT '0',
  `rev_rating_infrastructure` varchar(5) NOT NULL DEFAULT '0',
  `rev_rating_curiculam` varchar(5) NOT NULL DEFAULT '0',
  `rev_recommand` varchar(5) NOT NULL,
  `rev_image` varchar(150) NOT NULL,
  `create_datetime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=80 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_coursereviews`
--

DROP TABLE IF EXISTS `shub_coursereviews`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_coursereviews` (
  `crsrvw_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `crsrvw_CourseId` int(11) NOT NULL,
  `crsrvw_StudentId` int(11) NOT NULL,
  `crsrvw_BatchId` int(11) DEFAULT NULL,
  `crsrvw_Rating` int(11) NOT NULL,
  `crsrvw_Comment` varchar(2500) DEFAULT NULL,
  `crsrvw_Status` varchar(30) DEFAULT NULL,
  `crsrvw_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `crsrvw_UpdatedDate` datetime DEFAULT NULL,
  `crsrvw_CreatedBy` varchar(20) DEFAULT NULL,
  `crsrvw_UpdatedBy` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`crsrvw_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_courses`
--

DROP TABLE IF EXISTS `shub_courses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_courses` (
  `course_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `course_Name` varchar(30) NOT NULL,
  `course_UrlName` varchar(20) DEFAULT NULL,
  `course_Fee` float DEFAULT NULL,
  `course_ShortDescription` varchar(250) DEFAULT NULL,
  `course_Description` varchar(2000) DEFAULT NULL,
  `course_Status` varchar(10) DEFAULT 'ACTIVE',
  `course_Featured` varchar(5) DEFAULT 'NO',
  `course_Duration` varchar(30) DEFAULT NULL,
  `course_Logo` varchar(500) DEFAULT NULL,
  `course_CoverPicture` varchar(500) DEFAULT NULL,
  `course_Curriculum` text,
  `course_DemoLink` varchar(500) DEFAULT NULL,
  `course_DemoLink2` varchar(500) DEFAULT NULL,
  `course_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `course_UpdatedDate` datetime DEFAULT NULL,
  `course_CreatedBy` int(11) DEFAULT NULL,
  `course_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`course_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_dropdowntypes`
--

DROP TABLE IF EXISTS `shub_dropdowntypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_dropdowntypes` (
  `type_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `type_Name` varchar(30) NOT NULL COMMENT 'COURSESTEAM/NEWS/JOBS/TRAININGMODE',
  `type_Value` varchar(100) NOT NULL,
  `type_UrlKeyword` varchar(100) DEFAULT NULL,
  `type_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `type_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`type_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=156 DEFAULT CHARSET=latin1 COMMENT='all types of classifications, types for courses, news, job a';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_dropdowntypes_ref`
--

DROP TABLE IF EXISTS `shub_dropdowntypes_ref`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_dropdowntypes_ref` (
  `drpref_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `drpref_Text` varchar(25) NOT NULL,
  `drpref_Value` varchar(25) NOT NULL,
  `drpref_Status` bit(1) DEFAULT b'1',
  `drpref_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`drpref_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8 COMMENT='main reference values for Dropdown type groups for Group Typ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_enquiries`
--

DROP TABLE IF EXISTS `shub_enquiries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_enquiries` (
  `enq_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `enq_Name` varchar(50) DEFAULT NULL,
  `enq_Email` varchar(50) DEFAULT NULL,
  `enq_PhoneNumber` varchar(20) DEFAULT NULL,
  `enq_Subject` varchar(250) DEFAULT NULL,
  `enq_Message` varchar(2500) DEFAULT NULL,
  `enq_Status` varchar(30) DEFAULT 'NEW',
  `enq_Type` varchar(50) DEFAULT NULL,
  `enq_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`enq_Sno`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_examcourses`
--

DROP TABLE IF EXISTS `shub_examcourses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_examcourses` (
  `exmcrs_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `exmcrs_Name` varchar(150) NOT NULL,
  `exmcrs_UrlKeyword` varchar(150) DEFAULT NULL,
  `exmcrs_Description` text,
  `exmcrs_ExamType` int(10) unsigned NOT NULL,
  `exmcrs_ExamStream` int(10) unsigned NOT NULL,
  `exmcrs_ExamLevel` int(10) unsigned NOT NULL,
  `exmcrs_ExamMode` int(10) unsigned NOT NULL,
  `exmcrs_Registration` text,
  `exmcrs_Eligibility` text,
  `exmcrs_PatternSyllabus` text,
  `exmcrs_PreparationTips` text,
  `exmcrs_AdmitCard` text,
  `exmcrs_Results` text,
  `exmcrs_ImportantDates` text,
  `exmcrs_ApplicationStartDate` date DEFAULT NULL,
  `exmcrs_ApplicationEndDate` date DEFAULT NULL,
  `exmcrs_ExamDate` date DEFAULT NULL,
  `exmcrs_Counselling` text,
  `exmcrs_RelatedArticles` text,
  `exmcrs_Logo` varchar(250) DEFAULT NULL,
  `exmcrs_Status` varchar(20) DEFAULT 'ACTIVE',
  `exmcrs_PackageValidity` varchar(30) DEFAULT NULL,
  `exmcrs_Price` varchar(45) DEFAULT NULL,
  `exmcrs_OfferPrice` varchar(45) DEFAULT NULL,
  `exmcrs_FreeTestsCount` varchar(45) DEFAULT NULL,
  `exmcrs_PaidTestsCount` varchar(45) DEFAULT NULL,
  `exmcrs_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `exmcrs_UpdatedDate` datetime DEFAULT NULL,
  `exmcrs_CreatedBy` int(11) DEFAULT NULL,
  `exmcrs_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`exmcrs_Id`),
  KEY `FK_ExamType_idx` (`exmcrs_ExamType`),
  KEY `FK_ExamStream_idx` (`exmcrs_ExamStream`),
  KEY `FK_ExamLevel_idx` (`exmcrs_ExamLevel`),
  KEY `FK_ExamMode_idx` (`exmcrs_ExamMode`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_exceptions`
--

DROP TABLE IF EXISTS `shub_exceptions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_exceptions` (
  `ex_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `ex_name` varchar(500) DEFAULT NULL,
  `ex_location` varchar(500) DEFAULT NULL,
  `ex_other` varchar(500) DEFAULT NULL,
  `ex_createddate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ex_Sno`)
) ENGINE=InnoDB AUTO_INCREMENT=206 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_insreview`
--

DROP TABLE IF EXISTS `shub_insreview`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_insreview` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `rev_ins_id` varchar(10) NOT NULL,
  `rev_ins_name` varchar(250) NOT NULL,
  `rev_cou_name` varchar(250) NOT NULL,
  `rev_name` varchar(200) NOT NULL,
  `rev_email` varchar(60) NOT NULL,
  `rev_phone` varchar(15) NOT NULL,
  `rev_referby` varchar(150) NOT NULL,
  `rev_referemail` varchar(60) NOT NULL,
  `rev_comments` text NOT NULL,
  `rev_rating` varchar(5) NOT NULL DEFAULT '0',
  `rev_recommand` varchar(10) NOT NULL,
  `rev_image` varchar(150) NOT NULL,
  `create_datetime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=123 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_institutioncourses`
--

DROP TABLE IF EXISTS `shub_institutioncourses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_institutioncourses` (
  `inscrs_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `inscrs_InstitutionId` int(10) unsigned NOT NULL,
  `inscrs_CourseId` int(10) unsigned NOT NULL,
  `inscrs_CourseFee` varchar(25) DEFAULT NULL,
  `inscrs_Duration` varchar(30) DEFAULT NULL,
  `inscrs_ClassRoomTraining` varchar(10) DEFAULT 'false',
  `inscrs_OnlineTraining` varchar(10) DEFAULT 'false',
  `inscrs_Proxy` varchar(10) DEFAULT 'false',
  `inscrs_CorporateTraining` varchar(10) DEFAULT 'false',
  `inscrs_Internship` varchar(10) DEFAULT 'false',
  `inscrs_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `inscrs_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`inscrs_Id`),
  KEY `InstitutionIdForCourse_idx` (`inscrs_InstitutionId`),
  KEY `CourseIdForCourse_idx` (`inscrs_CourseId`),
  CONSTRAINT `CourseIdForCourse` FOREIGN KEY (`inscrs_CourseId`) REFERENCES `shub_maincourses` (`course_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `InstitutionIdForCourse` FOREIGN KEY (`inscrs_InstitutionId`) REFERENCES `shub_institutions` (`ins_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10679 DEFAULT CHARSET=latin1 COMMENT='courses teached by an institution';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_institutioncoursestrainingmodes`
--

DROP TABLE IF EXISTS `shub_institutioncoursestrainingmodes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_institutioncoursestrainingmodes` (
  `trnmd_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `trnmd_CourseId` int(10) unsigned DEFAULT NULL,
  `trnmd_TrainingModeId` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`trnmd_Id`),
  KEY `FK_shub_institutioncoursestrainingmodes_TrainingMode_idx` (`trnmd_TrainingModeId`),
  KEY `FK_shub_institutioncoursestrainingmodes_Course_idx` (`trnmd_CourseId`),
  CONSTRAINT `FK_shub_institutioncoursestrainingmodes_Course` FOREIGN KEY (`trnmd_CourseId`) REFERENCES `shub_institutioncourses` (`inscrs_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_shub_institutioncoursestrainingmodes_TrainingMode` FOREIGN KEY (`trnmd_TrainingModeId`) REFERENCES `shub_dropdowntypes` (`type_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=11034 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_institutions`
--

DROP TABLE IF EXISTS `shub_institutions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_institutions` (
  `ins_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ins_Name` varchar(150) NOT NULL,
  `ins_Type` varchar(20) NOT NULL COMMENT 'INSTITUTION/PERSON',
  `ins_UrlKeyword` varchar(150) NOT NULL,
  `ins_About` text,
  `ins_Address` varchar(200) DEFAULT NULL,
  `ins_Address2` varchar(50) DEFAULT NULL,
  `ins_Address3` varchar(50) DEFAULT NULL,
  `ins_Area` varchar(50) NOT NULL,
  `ins_City` varchar(50) NOT NULL,
  `ins_MapLocation` varchar(45) DEFAULT NULL,
  `ins_Pincode` varchar(10) DEFAULT NULL,
  `ins_State` varchar(30) NOT NULL,
  `ins_Country` varchar(30) DEFAULT NULL,
  `ins_Phone1` varchar(20) DEFAULT NULL,
  `ins_Phone2` varchar(20) DEFAULT NULL,
  `ins_LandLine` varchar(20) DEFAULT NULL,
  `ins_Email1` varchar(50) DEFAULT NULL,
  `ins_Email2` varchar(50) DEFAULT NULL,
  `ins_Website` varchar(60) DEFAULT NULL,
  `ins_Status` varchar(15) DEFAULT NULL,
  `ins_IsTop` varchar(15) DEFAULT NULL,
  `ins_IsFeatured` varchar(10) DEFAULT 'false',
  `ins_IsTrending` varchar(10) DEFAULT 'false',
  `ins_JobAssistance` varchar(10) DEFAULT 'false',
  `ins_TrainingSatisfaction` varchar(10) DEFAULT 'false',
  `ins_JobGurarantee` varchar(10) DEFAULT 'false',
  `ins_IsVerified` varchar(10) DEFAULT 'false',
  `ins_Logo` varchar(100) DEFAULT NULL,
  `ins_Broucher` varchar(100) DEFAULT NULL,
  `ins_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `ins_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ins_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=854 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_instructors`
--

DROP TABLE IF EXISTS `shub_instructors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_instructors` (
  `instr_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `instr_FullName` varchar(50) NOT NULL,
  `instr_Age` varchar(10) DEFAULT NULL,
  `instr_Gender` varchar(10) DEFAULT NULL,
  `instr_Education` varchar(50) DEFAULT NULL,
  `instr_WorkExperience` varchar(250) DEFAULT NULL,
  `instr_CurrentWorkPosition` varchar(100) DEFAULT NULL,
  `instr_Description` varchar(500) DEFAULT NULL,
  `instr_Address` varchar(250) DEFAULT NULL,
  `instr_City` varchar(30) DEFAULT NULL,
  `instr_State` varchar(30) DEFAULT NULL,
  `instr_Country` varchar(30) DEFAULT NULL,
  `instr_Dob` date DEFAULT NULL,
  `instr_Pincode` varchar(15) DEFAULT NULL,
  `instr_Email` varchar(100) NOT NULL,
  `instr_Password` varchar(1000) NOT NULL,
  `instr_PasswordStatus` varchar(15) DEFAULT NULL,
  `instr_Email2` varchar(100) DEFAULT NULL,
  `instr_Phone1` varchar(15) DEFAULT NULL,
  `instr_Phone2` varchar(15) DEFAULT NULL,
  `instr_Status` varchar(15) DEFAULT NULL,
  `instr_Featured` varchar(10) DEFAULT NULL,
  `instr_Website` varchar(50) DEFAULT NULL,
  `instr_Photo` varchar(250) DEFAULT NULL,
  `instr_CoverPhoto` varchar(250) DEFAULT NULL,
  `instr_FacebookLink` varchar(250) DEFAULT NULL,
  `instr_TwitterLink` varchar(250) DEFAULT NULL,
  `instr_LinkedinLink` varchar(250) DEFAULT NULL,
  `instr_VerificationCode` varchar(100) DEFAULT NULL,
  `instr_UnsuccessfulLogins` int(11) DEFAULT NULL,
  `instr_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `instr_UpdatedDate` datetime DEFAULT NULL,
  `instr_CreatedBy` varchar(10) DEFAULT NULL,
  `instr_UpdatedBy` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`instr_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_jobalerts`
--

DROP TABLE IF EXISTS `shub_jobalerts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_jobalerts` (
  `job_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `job_Type` int(10) unsigned NOT NULL,
  `job_Title` varchar(200) NOT NULL,
  `job_Details` text,
  `job_JD` text,
  `job_Company` varchar(50) DEFAULT NULL,
  `job_CompanyLogo` varchar(100) DEFAULT NULL,
  `job_Address` varchar(100) DEFAULT NULL,
  `job_City` varchar(30) DEFAULT NULL,
  `job_Area` varchar(30) DEFAULT NULL,
  `job_State` varchar(30) DEFAULT NULL,
  `job_Country` varchar(30) DEFAULT NULL,
  `job_Pincode` varchar(10) DEFAULT NULL,
  `job_Date` varchar(20) DEFAULT NULL,
  `job_Time` varchar(15) DEFAULT NULL,
  `job_Status` varchar(15) DEFAULT 'ACTIVE',
  `job_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `job_UpdatedDate` datetime DEFAULT NULL,
  `job_Website` varchar(45) DEFAULT NULL,
  `job_Domain` varchar(45) DEFAULT NULL,
  `job_Designation` varchar(45) DEFAULT NULL,
  `job_EligibilityCriteria` text,
  `job_CtcAnnum` varchar(45) DEFAULT NULL,
  `job_BondPeriod` varchar(45) DEFAULT NULL,
  `job_SkillsRequired` text,
  `job_SelectionProcess` text,
  `job_AdditionalInformation` text,
  `job_ContactInfo` text,
  `job_LastDateToApply` date DEFAULT NULL,
  PRIMARY KEY (`job_Id`),
  KEY `JobAlertType_idx` (`job_Type`),
  CONSTRAINT `JobAlertType` FOREIGN KEY (`job_Type`) REFERENCES `shub_dropdowntypes` (`type_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_maincourses`
--

DROP TABLE IF EXISTS `shub_maincourses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_maincourses` (
  `course_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `course_Name` varchar(100) NOT NULL,
  `course_UrlKeyword` varchar(150) NOT NULL,
  `course_Description` text,
  `course_Syllabus` text,
  `course_Eligibility` text,
  `course_IsInTopList` varchar(10) DEFAULT 'false',
  `course_IsFeatured` varchar(10) DEFAULT 'false',
  `course_IsTrending` varchar(10) DEFAULT 'false',
  `course_Stream` int(11) unsigned DEFAULT NULL,
  `course_Logo` varchar(100) DEFAULT NULL,
  `course_Status` varchar(15) DEFAULT 'ACTIVE',
  `course_MetaTitle` varchar(150) DEFAULT NULL,
  `course_MetaDescription` varchar(250) DEFAULT NULL,
  `course_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `course_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`course_Id`),
  KEY `CourseStream_idx` (`course_Stream`),
  CONSTRAINT `CourseStream` FOREIGN KEY (`course_Stream`) REFERENCES `shub_dropdowntypes` (`type_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1621 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_mocktestanswers`
--

DROP TABLE IF EXISTS `shub_mocktestanswers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_mocktestanswers` (
  `ans_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ans_MockTestId` int(11) NOT NULL,
  `ans_QuestionId` int(11) NOT NULL,
  `ans_Answer` varchar(5) NOT NULL,
  `ans_TimeTotal` varchar(15) DEFAULT '0',
  `ans_TimeAnswered` varchar(15) DEFAULT '0',
  `ans_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `ans_CreatedBy` int(11) DEFAULT '0',
  PRIMARY KEY (`ans_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_mocktestquestions`
--

DROP TABLE IF EXISTS `shub_mocktestquestions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_mocktestquestions` (
  `ques_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ques_MockTestId` int(10) unsigned NOT NULL,
  `ques_QuestionText` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `ques_QuestionImage` varchar(250) DEFAULT NULL,
  `ques_AnswerOption` varchar(5) NOT NULL,
  `ques_AnswerOption2` varchar(5) DEFAULT NULL,
  `ques_AnswerOption3` varchar(5) DEFAULT NULL,
  `ques_OptionA` text CHARACTER SET utf8,
  `ques_OptionAImage` varchar(100) DEFAULT NULL,
  `ques_OptionB` text CHARACTER SET utf8,
  `ques_OptionBImage` varchar(250) DEFAULT NULL,
  `ques_OptionC` text CHARACTER SET utf8,
  `ques_OptionCImage` varchar(250) DEFAULT NULL,
  `ques_OptionD` text CHARACTER SET utf8,
  `ques_OptionDImage` varchar(250) DEFAULT NULL,
  `ques_OptionE` text CHARACTER SET utf8,
  `ques_OptionEImage` varchar(250) DEFAULT NULL,
  `ques_Explanation` text CHARACTER SET utf8,
  `ques_ExplanationImage` varchar(250) DEFAULT NULL,
  `ques_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `ques_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ques_Id`),
  KEY `MockTestRelation_idx` (`ques_MockTestId`),
  CONSTRAINT `MockTestRelation` FOREIGN KEY (`ques_MockTestId`) REFERENCES `shub_mocktests` (`mocktest_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=702 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_mocktests`
--

DROP TABLE IF EXISTS `shub_mocktests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_mocktests` (
  `mocktest_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `mocktest_CourseId` int(10) unsigned NOT NULL,
  `mocktest_Difficulty` varchar(15) DEFAULT NULL COMMENT 'EASY/MEDIUM/HARD',
  `mocktest_Title` varchar(500) NOT NULL,
  `mocktest_Type` int(10) unsigned DEFAULT NULL,
  `mocktest_Subject` varchar(100) DEFAULT NULL,
  `mocktest_Chapter` varchar(100) DEFAULT NULL,
  `mocktest_Year` varchar(20) DEFAULT NULL,
  `mocktest_QuestionMaxTime` varchar(20) DEFAULT NULL,
  `mocktest_TestMaxTime` varchar(20) DEFAULT NULL,
  `mocktest_Description` varchar(1000) CHARACTER SET utf8 DEFAULT NULL,
  `mocktest_MaximumQuestions` varchar(5) DEFAULT NULL,
  `mocktest_Price` double NOT NULL,
  `mocktest_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `mocktest_UpdatedDate` datetime DEFAULT NULL,
  `ques_Price` varchar(50) DEFAULT NULL,
  `ques_DiscountOfferPrice` varchar(50) DEFAULT NULL,
  `ques_CouponCodeOffer` varchar(100) DEFAULT NULL,
  `ques_FreePaidTest` varchar(45) DEFAULT NULL,
  `ques_GovtPrivateExam` varchar(45) DEFAULT NULL,
  `ques_Language` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`mocktest_Id`),
  KEY `MockTestCourse_idx` (`mocktest_CourseId`),
  KEY `FK_MockTestType_idx` (`mocktest_Type`),
  CONSTRAINT `FK_MockTestCourse` FOREIGN KEY (`mocktest_CourseId`) REFERENCES `shub_examcourses` (`exmcrs_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_MockTestType` FOREIGN KEY (`mocktest_Type`) REFERENCES `shub_dropdowntypes` (`type_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=59 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_mockteststats`
--

DROP TABLE IF EXISTS `shub_mockteststats`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_mockteststats` (
  `stat_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `stat_MockTestId` int(10) unsigned NOT NULL,
  `stat_StudentId` int(10) unsigned NOT NULL,
  `stat_CourseId` int(10) unsigned NOT NULL,
  `stat_Status` varchar(20) DEFAULT 'NEW',
  `stat_TotalAnswered` int(11) DEFAULT '0',
  `stat_TotalCorrect` int(11) DEFAULT '0',
  `stat_TotalQuestions` int(11) DEFAULT '0',
  `stat_Percent` int(11) DEFAULT '0',
  `stat_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `stat_CreatedBy` int(11) DEFAULT '0',
  PRIMARY KEY (`stat_Id`),
  KEY `FK_shub_mockteststats_MockTestId_idx` (`stat_MockTestId`),
  KEY `FK_shub_mockteststats_CourseId_idx` (`stat_CourseId`),
  KEY `FK_shub_mockteststats_StudentId_idx` (`stat_StudentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_newsalerts`
--

DROP TABLE IF EXISTS `shub_newsalerts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_newsalerts` (
  `news_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `news_TypeId` int(10) unsigned NOT NULL COMMENT 'id from type. EXAM/RESULTS/SCHOLARSHIPS/EXAMDATES/NOTIFICATIONS',
  `news_Title` varchar(200) NOT NULL,
  `news_UrlKeyword` varchar(220) DEFAULT NULL,
  `news_Description` text,
  `news_CoverImage` varchar(150) DEFAULT NULL,
  `news_CourseId` int(10) unsigned DEFAULT NULL,
  `news_InstitutionId` int(10) unsigned DEFAULT NULL,
  `news_Keywords` varchar(200) DEFAULT NULL,
  `news_Status` varchar(15) DEFAULT 'ACTIVE',
  `news_ExpiryDate` date DEFAULT NULL,
  `news_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `news_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`news_Id`),
  KEY `NewsType_idx` (`news_TypeId`),
  KEY `FK_shub_newsalerts_InsituteId_idx` (`news_InstitutionId`),
  KEY `FK_shub_newsalerts_CouresId_idx` (`news_CourseId`),
  CONSTRAINT `FK_shub_newsalerts_CouresId` FOREIGN KEY (`news_CourseId`) REFERENCES `shub_maincourses` (`course_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_shub_newsalerts_InsituteId` FOREIGN KEY (`news_InstitutionId`) REFERENCES `shub_institutions` (`ins_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `NewsType` FOREIGN KEY (`news_TypeId`) REFERENCES `shub_dropdowntypes` (`type_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_pages`
--

DROP TABLE IF EXISTS `shub_pages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_pages` (
  `page_Id` int(11) NOT NULL AUTO_INCREMENT,
  `page_name` varchar(45) DEFAULT NULL,
  `page_heading` varchar(100) DEFAULT NULL,
  `page_content` longtext,
  `page_metatitle` varchar(100) DEFAULT NULL,
  `page_metadescription` varchar(250) DEFAULT NULL,
  `page_updatedby` int(11) DEFAULT NULL,
  `page_updateddate` datetime DEFAULT NULL,
  PRIMARY KEY (`page_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_placements`
--

DROP TABLE IF EXISTS `shub_placements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_placements` (
  `plcmnt_Id` int(11) NOT NULL AUTO_INCREMENT,
  `plcmnt_StudentName` varchar(45) DEFAULT NULL,
  `plcmnt_Gender` varchar(10) DEFAULT NULL,
  `plcmnt_CompanyName` varchar(45) DEFAULT NULL,
  `plcmnt_Photo` varchar(100) DEFAULT NULL,
  `plcmnt_JobPosition` varchar(45) DEFAULT NULL,
  `plcmnt_City` varchar(30) DEFAULT NULL,
  `plcmnt_CompanyLogo` varchar(100) DEFAULT NULL,
  `plcmnt_CreatedBy` int(11) DEFAULT NULL,
  `plcmnt_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `plcmnt_UpdatedBy` int(11) DEFAULT NULL,
  `plcmnt_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`plcmnt_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_questionpapers`
--

DROP TABLE IF EXISTS `shub_questionpapers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_questionpapers` (
  `qpaper_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `qpaper_CourseId` int(10) unsigned NOT NULL,
  `qpaper_Title` varchar(100) DEFAULT NULL,
  `qpaper_Year` varchar(10) DEFAULT NULL,
  `qpaper_Month` varchar(20) DEFAULT NULL,
  `qpaper_FileName` varchar(150) DEFAULT NULL,
  `qpaper_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`qpaper_Id`),
  KEY `CourseQuestionPaper_idx` (`qpaper_CourseId`),
  CONSTRAINT `CourseQuestionPaper` FOREIGN KEY (`qpaper_CourseId`) REFERENCES `shub_maincourses` (`course_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COMMENT='previous quesion papers';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_sectionmaterials`
--

DROP TABLE IF EXISTS `shub_sectionmaterials`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_sectionmaterials` (
  `material_Id` int(11) NOT NULL AUTO_INCREMENT,
  `material_SectionId` int(11) DEFAULT NULL,
  `material_FileName` varchar(500) DEFAULT NULL,
  `material_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`material_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_sectionscourse`
--

DROP TABLE IF EXISTS `shub_sectionscourse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_sectionscourse` (
  `section_Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `section_CourseId` int(11) unsigned NOT NULL,
  `section_Title` varchar(150) DEFAULT NULL,
  `section_Description` varchar(250) DEFAULT NULL,
  `section_SortId` int(11) DEFAULT NULL,
  `section_UpdatedBy` int(11) DEFAULT NULL,
  `section_CreatedBy` int(11) DEFAULT NULL,
  `section_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `section_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`section_Id`),
  KEY `FK_shub_sectionscourse_CourseId_idx` (`section_CourseId`),
  CONSTRAINT `FK_shub_sectionscourse_CourseId` FOREIGN KEY (`section_CourseId`) REFERENCES `shub_maincourses` (`course_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_sectiontestquestions`
--

DROP TABLE IF EXISTS `shub_sectiontestquestions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_sectiontestquestions` (
  `sectestqn_Id` int(10) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `sectestqn_TestId` int(11) DEFAULT NULL,
  `sectestqn_Question` varchar(1000) DEFAULT NULL,
  `sectestqn_OptionA` varchar(500) DEFAULT NULL,
  `sectestqn_OptionB` varchar(500) DEFAULT NULL,
  `sectestqn_OptionC` varchar(500) DEFAULT NULL,
  `sectestqn_OptionD` varchar(500) DEFAULT NULL,
  `sectestqn_Answer` varchar(5) DEFAULT NULL,
  `sectestqn_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `sectestqn_UpdatedDate` datetime DEFAULT NULL,
  `sectestqn_CreatedBy` int(11) DEFAULT NULL,
  `sectestqn_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`sectestqn_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_sectiontests`
--

DROP TABLE IF EXISTS `shub_sectiontests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_sectiontests` (
  `sectiontest_Id` int(11) NOT NULL AUTO_INCREMENT,
  `sectiontest_SectionId` int(11) DEFAULT NULL,
  `sectiontest_Difficulty` varchar(15) DEFAULT NULL,
  `sectiontest_CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `sectiontest_UpdatedDate` datetime DEFAULT NULL,
  `sectiontest_CreatedBy` int(11) DEFAULT NULL,
  `sectiontest_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`sectiontest_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_settings`
--

DROP TABLE IF EXISTS `shub_settings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_settings` (
  `set_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `set_Name` varchar(50) DEFAULT NULL,
  `set_Value` varchar(1000) DEFAULT NULL,
  `set_CreatedBy` int(11) DEFAULT NULL,
  `set_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `set_UpdatedBy` int(11) DEFAULT NULL,
  `set_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`set_Sno`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_students`
--

DROP TABLE IF EXISTS `shub_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_students` (
  `std_Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `std_FullName` varchar(50) NOT NULL,
  `std_Gender` varchar(10) DEFAULT NULL,
  `std_Age` varchar(10) DEFAULT NULL,
  `std_Education` varchar(50) DEFAULT NULL COMMENT 'GRADUATE/POST-GRADUATE',
  `std_WhoReferredUs` varchar(45) DEFAULT NULL,
  `std_WorkExperience` varchar(250) DEFAULT NULL,
  `std_CurrentWorkPosition` varchar(100) DEFAULT NULL,
  `std_Description` varchar(500) DEFAULT NULL,
  `std_Address` varchar(250) DEFAULT NULL,
  `std_City` varchar(30) DEFAULT NULL,
  `std_State` varchar(30) DEFAULT NULL,
  `std_Country` varchar(30) DEFAULT NULL,
  `std_Dob` date DEFAULT NULL,
  `std_Pincode` varchar(15) DEFAULT NULL,
  `std_Email` varchar(100) NOT NULL,
  `std_Password` varchar(1000) NOT NULL,
  `std_PasswordStatus` varchar(15) DEFAULT NULL COMMENT 'ACTIVE / INACTIVE',
  `std_Email2` varchar(100) DEFAULT NULL,
  `std_Phone1` varchar(15) DEFAULT NULL,
  `std_Phone2` varchar(15) DEFAULT NULL,
  `std_Status` varchar(15) DEFAULT NULL COMMENT 'NEW / ACTIVE / INACTIVE',
  `std_ProfilePic` varchar(100) DEFAULT NULL,
  `std_VerificationCode` varchar(150) DEFAULT NULL,
  `std_UnsuccessfulLogins` int(11) DEFAULT NULL,
  `std_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `std_UpdatedDate` datetime DEFAULT NULL,
  `std_CreatedBy` int(11) DEFAULT NULL,
  `std_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`std_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_users`
--

DROP TABLE IF EXISTS `shub_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_users` (
  `user_Id` int(11) NOT NULL AUTO_INCREMENT,
  `user_FullName` varchar(45) DEFAULT NULL,
  `user_Email` varchar(50) DEFAULT NULL,
  `user_Password` varchar(500) DEFAULT NULL,
  `user_AccessLevel` varchar(30) DEFAULT NULL,
  `user_Status` varchar(30) DEFAULT NULL,
  `user_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `user_UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`user_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `shub_vouchers`
--

DROP TABLE IF EXISTS `shub_vouchers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shub_vouchers` (
  `vchr_Sno` int(11) NOT NULL AUTO_INCREMENT,
  `vchr_Name` varchar(45) DEFAULT NULL,
  `vchr_VoucherCode` varchar(50) DEFAULT NULL,
  `vchr_StartDate` date DEFAULT NULL,
  `vchr_EndDate` date DEFAULT NULL,
  `vchr_Notes` varchar(1000) DEFAULT NULL,
  `vchr_Image` varchar(100) DEFAULT NULL,
  `vchr_Status` varchar(15) DEFAULT NULL,
  `vchr_CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `vchr_CreatedBy` int(11) DEFAULT NULL,
  `vchr_UpdatedDate` datetime DEFAULT NULL,
  `vchr_UpdatedBy` int(11) DEFAULT NULL,
  PRIMARY KEY (`vchr_Sno`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary view structure for view `vw_mocktestslist`
--

DROP TABLE IF EXISTS `vw_mocktestslist`;
/*!50001 DROP VIEW IF EXISTS `vw_mocktestslist`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_mocktestslist` AS SELECT 
 1 AS `CourseId`,
 1 AS `Name`,
 1 AS `Status`,
 1 AS `Stream`,
 1 AS `TotalTests`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vw_tutorialcourseslist`
--

DROP TABLE IF EXISTS `vw_tutorialcourseslist`;
/*!50001 DROP VIEW IF EXISTS `vw_tutorialcourseslist`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_tutorialcourseslist` AS SELECT 
 1 AS `CourseId`,
 1 AS `Name`,
 1 AS `Status`,
 1 AS `Stream`*/;
SET character_set_client = @saved_cs_client;

--
-- Dumping routines for database 'bnt_studenthub'
--
/*!50003 DROP PROCEDURE IF EXISTS `AdminLogin` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `AdminLogin`(IN OperateMode VARCHAR(30), Email VARCHAR(100), Password VARCHAR(1000), VerificationCode VARCHAR(50), UserID VARCHAR(30), OUT Result VARCHAR(50))
BEGIN
    
    IF (OperateMode = "LoginAdmin") THEN  
	BEGIN
        
        IF((SELECT COUNT(*) FROM shub_users WHERE user_Email = email) = 1) THEN
		BEGIN
			DECLARE check_count INT; 
			SET result = "FAILED";	
			
			SELECT COUNT(*) INTO check_count 
            FROM shub_users 
            WHERE user_Email = Email AND user_Password = Password AND user_Status = 'ACTIVE';
			
			-- LOGIN SUCCESSFUL
			IF(check_count = 1)	THEN
			BEGIN	
				SET result = "SUCCESS";	
			END;
			
			-- USER EXISTS BUT WRONG PASSWORD
			ELSE
			BEGIN
				IF(SELECT COUNT(*) FROM shub_users WHERE user_Email = Email AND user_Password != Password) = 1	THEN
				BEGIN	
					SET result = "INCORRECT PASSWORD";	
					
					-- KEEPING COUNT OF UNSUCESSLOGINS
					-- UPDATE shub_students
					-- SET
						-- std_UnsuccessfulLogins = std_UnsuccessfulLogins + 1,
						-- std_UpdatedDate = NOW()
					-- WHERE std_email = Email;

					
					-- MAKING USER INACTIVE
					-- IF(SELECT std_UnsuccessfulLogins FROM shub_students WHERE std_email = Email) = 5	THEN
					-- BEGIN	
						-- SET result = "USER BLOCKED - 5 UNSUCCESSFUL LOGINS - CONTACT ADMINISTRATOR";	
						
						-- UPDATE shub_students
						-- SET
							-- std_UnsuccessfulLogins = 0,
							-- std_Status = 'BLOCKED',
							-- std_UpdatedDate = NOW()
						-- WHERE std_Email = Email;
					-- END;
					-- END IF;
					
				END;
				END IF;
			END;
			END IF;
		END;
		
		-- USER DO NOT EXIST
		ELSE
		BEGIN
			IF((SELECT COUNT(*) FROM shub_users WHERE user_Email = Email AND (user_Status = 'BLOCKED' OR user_Status = 'INACTIVE')) = 1) THEN
			BEGIN
				SET result = "USER INACTIVE/BLOCKED - CONTACT ADMINISTRATOR";	
            END;
            
            ELSE
            BEGIN
				SET result = "USER DO NOT EXIST";
            END;
            END IF;
        
		END;
		END IF;

		SELECT result;
        
	END;
	END IF;	
    
    IF(OperateMode = "UpdatePassword") THEN
    BEGIN
		UPDATE shub_students
		SET
		std_Password = PasswordUser,
		std_UpdatedDate = NOW()
		WHERE std_ID = Sno;
        
        SELECT 'UPDATED' INTO Result;
    END;
    END IF;
    
    
    IF (OperateMode = "VerifyEmail") THEN 
	BEGIN
		IF ((SELECT COUNT(*) FROM shub_students WHERE std_VerificationCode = VerificationCode AND std_Id = Email AND std_Status = 'NEW') > 0 ) THEN
		BEGIN
			UPDATE shub_students
			SET
				std_Status = 'ACTIVE',
				std_PasswordStatus = 'ACTIVE',
				std_UnsuccessfulLogins = 0,
				std_UpdatedDate = NOW()
			WHERE std_VerificationCode = VerificationCode AND std_Id = Email AND std_Status = 'NEW';
			
			SELECT 'EMAIL VERIFIED SUCCESSFULLY' INTO Result;
		END;
		
		ELSE
		BEGIN
			SELECT 'INVALID VERIFICATION' INTO Result;
		END;
		END IF;            
		
	END;
	END IF;	
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AdminLogin2` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `AdminLogin2`(IN OperateMode VARCHAR(30), Email VARCHAR(100), Password VARCHAR(1000), VerificationCode VARCHAR(50), UserID VARCHAR(30), OUT Result VARCHAR(50))
BEGIN
    
    IF (OperateMode = "LoginAdmin") THEN  
	BEGIN
        
        IF((SELECT COUNT(*) FROM shub_users WHERE user_Email = email) = 1) THEN
		BEGIN
			DECLARE check_count INT; 
			SET result = "FAILED";	
			
			SELECT COUNT(*) INTO check_count 
            FROM shub_users 
            WHERE user_Email = Email AND user_Password = Password AND user_Status = 'ACTIVE';
			
			-- LOGIN SUCCESSFUL
			IF(check_count = 1)	THEN
			BEGIN	
				SET result = "SUCCESS";	
			END;
			
			-- USER EXISTS BUT WRONG PASSWORD
			ELSE
			BEGIN
				IF(SELECT COUNT(*) FROM shub_users WHERE user_Email = Email AND user_Password != Password) = 1	THEN
				BEGIN	
					SET result = "INCORRECT PASSWORD";	
					
					-- KEEPING COUNT OF UNSUCESSLOGINS
					-- UPDATE shub_students
					-- SET
						-- std_UnsuccessfulLogins = std_UnsuccessfulLogins + 1,
						-- std_UpdatedDate = NOW()
					-- WHERE std_email = Email;

					
					-- MAKING USER INACTIVE
					-- IF(SELECT std_UnsuccessfulLogins FROM shub_students WHERE std_email = Email) = 5	THEN
					-- BEGIN	
						-- SET result = "USER BLOCKED - 5 UNSUCCESSFUL LOGINS - CONTACT ADMINISTRATOR";	
						
						-- UPDATE shub_students
						-- SET
							-- std_UnsuccessfulLogins = 0,
							-- std_Status = 'BLOCKED',
							-- std_UpdatedDate = NOW()
						-- WHERE std_Email = Email;
					-- END;
					-- END IF;
					
				END;
				END IF;
			END;
			END IF;
		END;
		
		-- USER DO NOT EXIST
		ELSE
		BEGIN
			IF((SELECT COUNT(*) FROM shub_users WHERE user_Email = Email AND (user_Status = 'BLOCKED' OR user_Status = 'INACTIVE')) = 1) THEN
			BEGIN
				SET result = "USER INACTIVE/BLOCKED - CONTACT ADMINISTRATOR";	
            END;
            
            ELSE
            BEGIN
				SET result = "USER DO NOT EXIST";
            END;
            END IF;
        
		END;
		END IF;

		SELECT result;
        
	END;
	END IF;	
    
    IF(OperateMode = "UpdatePassword") THEN
    BEGIN
		UPDATE shub_students
		SET
		std_Password = PasswordUser,
		std_UpdatedDate = NOW()
		WHERE std_ID = Sno;
        
        SELECT 'UPDATED' INTO Result;
    END;
    END IF;
    
    
    IF (OperateMode = "VerifyEmail") THEN 
	BEGIN
		IF ((SELECT COUNT(*) FROM shub_students WHERE std_VerificationCode = VerificationCode AND std_Id = Email AND std_Status = 'NEW') > 0 ) THEN
		BEGIN
			UPDATE shub_students
			SET
				std_Status = 'ACTIVE',
				std_PasswordStatus = 'ACTIVE',
				std_UnsuccessfulLogins = 0,
				std_UpdatedDate = NOW()
			WHERE std_VerificationCode = VerificationCode AND std_Id = Email AND std_Status = 'NEW';
			
			SELECT 'EMAIL VERIFIED SUCCESSFULLY' INTO Result;
		END;
		
		ELSE
		BEGIN
			SELECT 'INVALID VERIFICATION' INTO Result;
		END;
		END IF;            
		
	END;
	END IF;	
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `BatchesData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `BatchesData`(IN OperateMode VARCHAR(30), Sno VARCHAR(30), UserId VARCHAR(30), CourseId VARCHAR(10), InstructorId1  VARCHAR(10), InstructorId2  VARCHAR(10), StartDate VARCHAR(20), Timing VARCHAR(20), ClassDuration VARCHAR(20), ClassMode VARCHAR(20), MaxStrength VARCHAR(10), Discount VARCHAR(20), VideoLink VARCHAR(250), PayementLink VARCHAR(250), VirtualClassRoomLink VARCHAR(350), CourseDuration VARCHAR(30), Notes VARCHAR(3000), Status VARCHAR(20), OUT Result VARCHAR(50))
BEGIN
	
    IF(OperateMode = "AddBatch") THEN
    BEGIN
		
		INSERT INTO shub_batchschedule
		(btch_CourseId, btch_InstructorId1, btch_InstructorId2, btch_StartDate, btch_Timing, btch_ClassDuration, 
		btch_ClassMode, btch_MaxStrength, btch_Discount, btch_VideoLink, btch_PayementLink, btch_VirtualClassRoomLink,
		btch_CourseDuration, btch_Notes, btch_Status, btch_CreatedBy)
		VALUES
		(CourseId, InstructorId1, InstructorId2, StartDate, Timing, ClassDuration, 
		ClassMode, MaxStrength, Discount, VideoLink, PayementLink, VirtualClassRoomLink,
		CourseDuration, Notes, Status, UserId);
		
		/* SELECT 'ADDED' INTO Result;*/
		SELECT LAST_INSERT_ID() INTO Result;
		
    END;
    END IF;
    
    
    IF(OperateMode = "UpdateBatch") THEN
    BEGIN
		UPDATE shub_batchschedule
		SET
        btch_CourseId = CourseId, 
        btch_InstructorId1 = InstructorId1, 
        btch_InstructorId2 = InstructorId2, 
        btch_StartDate = StartDate,
        btch_Timing = Timing, 
        btch_ClassDuration = ClassDuration, 
		btch_ClassMode = ClassMode, 
        btch_MaxStrength = MaxStrength,
        btch_Discount = Discount, 
        btch_VideoLink = VideoLink, 
        btch_PayementLink = PayementLink, 
        btch_VirtualClassRoomLink = VirtualClassRoomLink,
		btch_CourseDuration = CourseDuration, 
        btch_Notes = Notes, 
        btch_Status = Status,
        btch_UpdatedBy = UserId,
        btch_UpdatedDate = NOW()
		WHERE btch_Id = Sno;
        
        SELECT 'UPDATED' INTO Result;
    END;
    END IF;
        
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `BatchesStudentsRelationData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `BatchesStudentsRelationData`(IN OperateMode VARCHAR(30), Sno VARCHAR(30), UserId VARCHAR(30), BatchId INT, StudentId INT, Status VARCHAR(20), Payment VARCHAR(20), OUT Result VARCHAR(50))
BEGIN
	
    IF(OperateMode = "AddBatchStudent") THEN
    BEGIN
		
        IF((SELECT COUNT(*) FROM shub_batchstudentrelation WHERE btchstd_BatchId = BatchId AND btchstd_StudentId = StudentId) = 0) THEN
        BEGIN
			INSERT INTO shub_batchstudentrelation
			(btchstd_BatchId, btchstd_StudentId, btchstd_Status, btchstd_Payment, btchstd_CreatedBy)
			VALUES
			(BatchId, StudentId, 'NEW', 'PENDING', UserId);
			
			/* SELECT 'ADDED' INTO Result;*/
			SELECT LAST_INSERT_ID() INTO Result;
        END;
        
        ELSE
        BEGIN
			SELECT 'ALREADY REGISTERED TO BATCH' INTO Result;
        END;
        END IF;
		
    END;
    END IF;
    
    
    IF(OperateMode = "UpdateBatchStudent") THEN
    BEGIN
		UPDATE shub_batchstudentrelation
		SET
        -- btchstd_BatchId = BatchId, 
        -- btchstd_StudentId = StudentId, 
        btchstd_Status = Status,
        btchstd_Payment = Payment, 
        btchstd_UpdatedBy = UserId,
        btchstd_UpdatedDate = NOW()
		WHERE btchstd_Id = Sno AND btchstd_BatchId = BatchId AND btchstd_StudentId = StudentId;
        
        SELECT 'UPDATED' INTO Result;
    END;
    END IF;
    
    
    IF(OperateMode = "InactivateBatchStudent") THEN
    BEGIN
		UPDATE shub_batchstudentrelation
		SET 
			btchstd_Status = 'INACTIVE',
			btchstd_UpdatedBy = UserId,
			btchstd_UpdatedDate = NOW()
		WHERE btchstd_BatchId = BatchId AND btchstd_StudentId = StudentId;
        
        SELECT 'STUDENT INACTIVATED' INTO Result;
    END;
    END IF;
    
    
    IF(OperateMode = "ActivateBatchStudent") THEN
    BEGIN
		UPDATE shub_batchstudentrelation
		SET 
			btchstd_Status = 'ACTIVE',
			btchstd_UpdatedBy = UserId,
			btchstd_UpdatedDate = NOW()
		WHERE btchstd_BatchId = BatchId AND btchstd_StudentId = StudentId;
        
        SELECT 'STUDENT ACTIVATED' INTO Result;
    END;
    END IF;
        
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `ChaptersData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `ChaptersData`(IN OperateMode VARCHAR(20), Sno VARCHAR(10), User VARCHAR(10), SectionId VARCHAR(10), ChapterTitle VARCHAR(150), SortId VARCHAR(10), Description VARCHAR(500), VideoLink1 VARCHAR(500),   Video1Duration VARCHAR(15), VideoLink2 VARCHAR(500), Video2Duration VARCHAR(15), OUT Result VARCHAR(50))
BEGIN

	IF(OperateMode = "AddChapter") THEN
    BEGIN
    
		INSERT INTO shub_chapters
		(
			chptr_SectionId, chptr_ChapterTitle, chptr_SortId, chptr_Description, chptr_VideoLink1, chptr_Video1Duration,
            chptr_VideoLink2, chptr_Video2Duration, chptr_CreatedBy
		)
		VALUES
		(
			SectionId, ChapterTitle, SortId, Description, VideoLink1, Video1Duration, 
            VideoLink2, Video2Duration, User
		);
		
		SELECT LAST_INSERT_ID() INTO Result;
		
    END;
    END IF;
    
    
    IF(OperateMode = "UpdateChapter") THEN
    BEGIN
		UPDATE shub_chapters SET
			chptr_SectionId = SectionId, 
            chptr_ChapterTitle = ChapterTitle, 
            chptr_SortId = SortId, 
            chptr_Description = Description, 
            chptr_VideoLink1 = VideoLink1, 
            chptr_Video1Duration = Video1Duration,
            chptr_VideoLink2 = VideoLink2,
            chptr_Video2Duration = Video2Duration,
            chptr_UpdatedDate = NOW(),
            chptr_UpdatedBy = Sno
		WHERE chptr_Id = Sno;
        
        SELECT 'UPDATED' INTO Result;
        
    END;
    END IF;
    
    
    IF(OperateMode = "DeleteChapter") THEN
    BEGIN
		DELETE FROM shub_chapters
		WHERE chptr_Id = Sno;
        
        SELECT 'DELETED' INTO Result;
        
    END;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CourseReviewData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `CourseReviewData`(IN OperateMode VARCHAR(30), Sno VARCHAR(30), UserId VARCHAR(30), CourseId INT, StudentId INT, BatchId INT, Rating INT, Comment VARCHAR(2500), Status VARCHAR(30), OUT Result VARCHAR(50))
BEGIN

    IF(OperateMode = "AddCourseReview") THEN
    BEGIN
		
		INSERT INTO shub_coursereviews
		(crsrvw_CourseId, crsrvw_StudentId, crsrvw_BatchId, crsrvw_Rating, 
        crsrvw_Comment, crsrvw_Status, crsrvw_CreatedBy)
		VALUES
		(CourseId, StudentId, BatchId, Rating, 
        Comment, 'NEW',  UserId);
		
		/* SELECT 'ADDED' INTO Result;*/
		SELECT LAST_INSERT_ID() INTO Result;
		
    END;
    END IF;
    
    
    IF(OperateMode = "InactiveCourseReview") THEN
    BEGIN
		UPDATE shub_coursereviews
		SET
        -- crsrvw_CourseId = CourseId, 
        -- crsrvw_StudentId = StudentId, 
        -- crsrvw_BatchId = BatchId, 
        -- crsrvw_Rating = Rating, 
        -- crsrvw_Comment = Comment, 
        crsrvw_Status = 'INACTIVE', 
        crsrvw_UpdatedBy = UserId,
        crsrvw_UpdatedDate = NOW()
		WHERE crsrvw_Id = Sno;
        
        SELECT 'UPDATED' INTO Result;
    END;
    END IF;
    
    
    IF(OperateMode = "ActiveCourseReview") THEN
    BEGIN
		UPDATE shub_coursereviews
		SET
        -- crsrvw_CourseId = CourseId, 
        -- crsrvw_StudentId = StudentId, 
        -- crsrvw_BatchId = BatchId, 
        -- crsrvw_Rating = Rating, 
        -- crsrvw_Comment = Comment, 
        crsrvw_Status = 'ACTIVE', 
        crsrvw_UpdatedBy = UserId,
        crsrvw_UpdatedDate = NOW()
		WHERE crsrvw_Id = Sno;
        
        SELECT 'UPDATED' INTO Result;
    END;
    END IF;
        
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CoursesData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `CoursesData`(IN OperateMode VARCHAR(30), Sno VARCHAR(30), UserId VARCHAR(30), Name VARCHAR(30), UrlName VARCHAR(20), Fee VARCHAR(30), ShortDescription VARCHAR(250), Description VARCHAR(1000), Status VARCHAR(10), Featured VARCHAR(5), Duration VARCHAR(30), Logo VARCHAR(500), CoverPicture VARCHAR(500), Curriculum TEXT, DemoLink VARCHAR(500), DemoLink2 VARCHAR(500), OUT Result VARCHAR(50))
BEGIN
	
    IF(OperateMode = "AddCourse") THEN
    BEGIN
		IF((SELECT COUNT(course_Name) FROM shub_courses WHERE course_Name = Name) = 0) THEN
        BEGIN
			INSERT INTO shub_courses
			(course_Name, course_UrlName, course_Fee, course_ShortDescription, course_Description, course_Status, 
            course_Featured, course_Duration, course_Logo, course_CoverPicture, course_Curriculum,
            course_DemoLink, course_DemoLink2, course_CreatedBy)
			VALUES
			(Name, UrlName, Fee, ShortDescription, Description, Status, 
            Featured, Duration, Logo, CoverPicture, Curriculum,
            DemoLink, DemoLink2, UserId);
			
			/* SELECT 'ADDED' INTO Result;*/
            SELECT LAST_INSERT_ID() INTO Result;
        END;
        
        ELSE
        BEGIN
			SELECT 'COURSE ALREADY EXISTS' INTO Result;
        END;
        END IF;
		
    END;
    END IF;
    
    
    IF(OperateMode = "UpdateCourse") THEN
    BEGIN
		UPDATE shub_courses
		SET
        course_Name = Name, 
        course_UrlName = UrlName,
        course_Fee = Fee, 
        course_ShortDescription = ShortDescription, 
        course_Description = Description, 
        course_Status = Status, 
		course_Featured = Featured, 
        course_Duration = Duration, 
        course_Logo = Logo, 
        course_CoverPicture = CoverPicture, 
        course_Curriculum = Curriculum,
		course_DemoLink = DemoLink, 
        course_DemoLink2 = DemoLink2, 
        course_UpdatedBy = UserId,
        course_UpdatedDate = NOW()
		WHERE course_Id = Sno;
        
        SELECT 'UPDATED' INTO Result;
    END;
    END IF;
        
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `EnquiryData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `EnquiryData`(IN OperateMode VARCHAR(20), Name VARCHAR(50), Email VARCHAR(50), Phone VARCHAR(20), Subject VARCHAR(100), Message VARCHAR(2000), OUT Result VARCHAR(20))
BEGIN

	IF (OperateMode = "AddEnquiry") THEN 
	BEGIN
		
        INSERT INTO shub_enquiries
        (
			enq_Name, enq_Email, enq_PhoneNumber, enq_Subject, enq_Message
        )
        VALUES
        (
			Name, Email, Phone, Subject, Message
        );
        
        
        SELECT LAST_INSERT_ID() INTO Result;
    END;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAdminData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `GetAdminData`(IN OperateMode VARCHAR(30), IN KeyValue VARCHAR(50))
BEGIN
	
    IF(OperateMode = "ALLSTUDENTS") THEN
    BEGIN
		SELECT * FROM getallstudentslist;
    END;
    END IF;
    
    
    IF(OperateMode = "EDITSTUDENT") THEN
    BEGIN
		SELECT 
			std_FullName AS FullName,
			std_Gender AS Gender,
			std_Age AS Age,
			std_Education AS Education,
			std_WhoReferredUs AS WhoReferredUs,
			std_WorkExperience AS WorkExperience,
			std_CurrentWorkPosition AS CurrentWorkPosition,
			std_Description AS Description,
			std_Address AS Address,
			std_City AS City,
			std_State AS State,
			std_Country AS Country,
			DATE_FORMAT(std_Dob, '%d-%m-%Y') AS Dob,
			std_Pincode AS Pincode,
			std_Email AS Email,
			-- std_Password AS Password,
			-- std_PasswordStatus AS PasswordStatus,
			std_Email2 AS Email2,
			std_Phone1 AS Phone1,
			std_Phone2 AS Phone2,
			std_Status AS Status,
			std_ProfilePic AS ProfilePic
			-- std_VerificationCode AS VerificationCode,
			-- std_UnsuccessfulLogins AS UnsuccessfulLogins,
			-- std_CreatedDate AS CreatedDate,
			-- std_UpdatedDate AS UpdatedDate,
			-- std_CreatedBy AS CreatedBy,
			-- std_UpdatedBy AS UpdatedBy

		FROM shub_students WHERE std_Id = KeyValue;
    END;    
    END IF;
    
    
    IF (OperateMode = "VERIFYEMAIL") THEN 
	BEGIN
		SELECT std_Id AS ID, std_Email AS Email, std_VerificationCode AS VerificationCode
		FROM shub_students
        WHERE std_Id = KeyValue AND std_Status = 'NEW';
	END;
	END IF;	
    
    
    IF (OperateMode = "ADMINLOGINDATA") THEN 
	BEGIN
		SELECT user_Id AS ID, user_Email AS Email, user_FullName AS FullName
		FROM shub_users
        WHERE user_Email = KeyValue;
	END;
	END IF;	
    
    
    
    IF (OperateMode = "INSTRUCTORLOGINDATA") THEN 
	BEGIN
		SELECT std_Id AS ID, std_Email AS Email, std_VerificationCode AS VerificationCode
		FROM shub_students
        WHERE std_Id = KeyValue AND std_Status = 'NEW';
	END;
	END IF;	
    
    
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetBatchesData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `GetBatchesData`(IN OperateMode VARCHAR(30), IN KeyValue VARCHAR(50))
BEGIN
	
    IF(OperateMode = "ALLBATCHES") THEN
    BEGIN
		SELECT 
			btch_Id AS ID, course_Name AS Course, course_Fee AS Fee, 
            DATE_FORMAT(btch_StartDate, '%Y-%m-%d') AS 'Date', btch_Timing AS Timings, 
            btch_ClassDuration AS 'Class Duration', btch_Discount AS Discount, instr_FullName AS Instructor
        FROM shub_batchschedule AS BTCH
			LEFT JOIN shub_courses AS CRS ON BTCH.btch_CourseId = CRS.course_Id 
            LEFT JOIN shub_instructors AS INS ON BTCH.btch_InstructorId1 = INS.instr_Id
		ORDER BY btch_Id DESC;
    END;
    END IF;
    
    
    IF(OperateMode = "EDITBATCH") THEN
    BEGIN
		SELECT 
			btch_CourseId AS CourseId,
			btch_InstructorId1 AS InstructorId1,
			btch_InstructorId2 AS InstructorId2,
			DATE_FORMAT(btch_StartDate, '%Y-%m-%d') AS StartDate,
			btch_Timing AS Timing,
			btch_ClassDuration AS ClassDuration,
			btch_ClassMode AS ClassMode,
            btch_MaxStrength AS MaxStrength,
			btch_Discount AS Discount,
			btch_VideoLink AS VideoLink,
			btch_PayementLink AS PaymentLink,
			btch_VirtualClassRoomLink AS VirtualClassRoomLink,
			btch_CourseDuration AS CourseDuration,
            btch_Notes AS Notes,
			btch_Status AS Status
		FROM shub_batchschedule WHERE btch_Id = KeyValue;
    END;    
    END IF;
    
    
    IF(OperateMode = "ALLINSTRUCTORS") THEN
    BEGIN
		SELECT 
			instr_FullName AS Name, Instr_Id AS InstructorId
        FROM shub_instructors ORDER BY instr_FullName ASC;
	END;
    END IF;
    
    
    IF(OperateMode = "ALLCOURSES") THEN
    BEGIN
		SELECT 
			course_Name AS Name, course_Id AS CourseId
        FROM shub_courses ORDER BY course_Name ASC;
	END;
    END IF;
    
    
    IF(OperateMode = "BATCHSTUDENTSADMIN") THEN
    BEGIN
		SELECT btchstd_BatchId AS BatchId,
			std_FullName AS Name, std_Id AS StudentId, btchstd_Payment AS Payment, std_Email AS Email,
            std_Phone1 AS Phone, btchstd_Status AS BatchStatus
        FROM shub_batchstudentrelation AS BTCH
			INNER JOIN shub_students AS STD ON BTCH.btchstd_BatchId = KeyValue AND BTCH.btchstd_StudentId = STD.std_Id
        ORDER BY std_FullName ASC;
	END;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetChaptersData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `GetChaptersData`(IN OperateMode VARCHAR(30), IN KeyValue VARCHAR(50))
BEGIN
	
	IF(OperateMode = "ALLCHAPTERS") THEN
	BEGIN
		SELECT 
			chptr_ChapterTitle AS ChapterTitle, chptr_SortId AS SortId, chptr_Description AS Description, 
            chptr_VideoLink1 AS VideoLink1, chptr_VideoLink2 AS VideoLink2,
            chptr_Document1 AS Document1, chptr_Document2 AS Document2
		FROM shub_chapters
        WHERE chptr_CourseId = KeyValue;
	END;
	END IF;
    
    
    IF(OperateMode = "CHAPTERDETAILS") THEN
    BEGIN
		SELECT 
			chptr_ChapterTitle AS ChapterTitle, 
            chptr_SortId AS SortId, 
            chptr_Description AS Description, 
            chptr_VideoLink1 AS VideoLink1, 
            chptr_VideoLink2 AS VideoLink2,
            chptr_Video1Duration AS VideoDuration1, 
            chptr_Video2Duration AS VideoDuration2
		FROM shub_chapters 
        WHERE chptr_Id = KeyValue;
    END;    
    END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetCoursesData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `GetCoursesData`(IN OperateMode VARCHAR(30), IN KeyValue VARCHAR(50))
BEGIN
	
	IF(OperateMode = "ALLCOURSES") THEN
	BEGIN
		SELECT 
			course_Id AS ID, course_Name AS Name, course_Fee AS Fee, course_Duration AS Duration,
			course_Status AS Status, course_Logo AS Logo, course_DemoLink AS DemoLink
		FROM shub_courses;
	END;
	END IF;
    
    
    IF(OperateMode = "EDITCOURSE") THEN
    BEGIN
		SELECT 
			course_Name AS Name,
			course_UrlName AS UrlName,
			course_Fee AS Fee,
			course_ShortDescription AS ShortDescription,
			course_Description AS Description,
			course_Status AS Status,
			course_Featured AS Featured,
			course_Duration AS Duration,
			course_Logo AS Logo,
			course_CoverPicture AS CoverPicture,
			course_Curriculum AS Curriculum,
			course_DemoLink AS DemoLink,
			course_DemoLink2 AS DemoLink2
		FROM shub_courses WHERE course_Id = KeyValue;
    END;    
    END IF;


	IF(OperateMode = "SECTIONCOURSENAME") THEN
	BEGIN
		/*SELECT 
			course_Name AS CourseName
		FROM shub_courses
		WHERE course_Id = (SELECT Section_CourseId FROM shub_sectionscourse WHERE section_Id = KeyValue
				GROUP BY Section_CourseId);*/

		SELECT 
			course_Name AS CourseName
		FROM shub_maincourses
		WHERE course_Id = KeyValue;
	END;
	END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetDashboardStats` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `GetDashboardStats`()
BEGIN
	SELECT 'Colleges' AS Field, COUNT(1) AS Count FROM shub_colleges 
    UNION
    SELECT 'CollegeStates' AS Field, COUNT(T.coll_state) AS Count FROM (SELECT coll_state FROM shub_colleges GROUP BY coll_state) AS T
    UNION
    SELECT 'CollegeCities' AS Field, COUNT(T.coll_city) AS Count FROM (SELECT coll_city FROM shub_colleges GROUP BY coll_city) AS T
    UNION
    SELECT 'CollegeCourses' AS Field, COUNT(1) AS Count FROM shub_collegecourse
    UNION
    SELECT 'CollegeCourseStreams' AS Field, COUNT(1) AS Count FROM shub_dropdowntypes WHERE type_Name = 'COLLEGECOURSETYPE'
    UNION
    SELECT 'CollegeCourseLevels' AS Field, COUNT(1) AS Count FROM shub_dropdowntypes WHERE type_Name = 'COLLEGECOURSELEVEL'
    UNION
    SELECT 'CollegeCourseDegrees' AS Field, COUNT(1) AS Count FROM shub_dropdowntypes WHERE type_Name = 'DEGREETYPE'
    UNION
    SELECT 'Institutions' AS Field, COUNT(1) AS Count FROM shub_institutions
    UNION
    SELECT 'InstitutionTrainingMode' AS Field, COUNT(1) AS Count FROM shub_dropdowntypes WHERE type_Name = 'TRAININGMODE'
    UNION
    SELECT 'InstitutionCities' AS Field, COUNT(T.ins_city) AS Count FROM (SELECT ins_city FROM shub_institutions GROUP BY ins_city) AS T
    UNION
    SELECT 'InstitutionCourses' AS Field, COUNT(1) AS Count FROM shub_maincourses
    UNION
    SELECT 'ExamCourses' AS Field, COUNT(1) AS Count FROM shub_examcourses
    UNION
    SELECT 'ExamStreams' AS Field, COUNT(1) AS Count FROM shub_dropdowntypes WHERE type_Name = 'EXAMSTREAM'
    UNION
    SELECT 'ExamLevels' AS Field, COUNT(1) AS Count FROM shub_dropdowntypes WHERE type_Name = 'EXAMLEVEL'
    UNION
    SELECT 'MockTests' AS Field, COUNT(1) AS Count FROM shub_mocktests
    UNION
    SELECT 'MockTestsFree' AS Field, COUNT(1) AS Count FROM shub_mocktests WHERE ques_FreePaidTest = 'FREE'
    UNION
    SELECT 'MockTestsPaid' AS Field, COUNT(1) AS Count FROM shub_mocktests WHERE ques_FreePaidTest = 'PAID'
    UNION
    SELECT 'MockTestsGovernment' AS Field, COUNT(1) AS Count FROM shub_mocktests WHERE ques_GovtPrivateExam = 'GOVERNMENT'
    UNION
    SELECT 'MockTestsPrivate' AS Field, COUNT(1) AS Count FROM shub_mocktests WHERE ques_GovtPrivateExam = 'PRIVATE'
    UNION
    SELECT 'JobAlerts' AS Field, COUNT(1) AS Count FROM shub_jobalerts
    UNION
    SELECT 'JobCompanies' AS Field, COUNT(T.job_Company) AS Count FROM (SELECT job_Company FROM shub_jobalerts GROUP BY job_Company) AS T
    UNION
    SELECT 'JobCities' AS Field, COUNT(T.job_City) AS Count FROM (SELECT job_City FROM shub_jobalerts GROUP BY job_City) AS T
    UNION
    SELECT 'NewsAlerts' AS Field, COUNT(1) AS Count FROM shub_newsalerts
    UNION
    SELECT 'NewsCategories' AS Field, COUNT(1) AS Count FROM shub_dropdowntypes WHERE type_Name = 'NEWS';
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetEnquiriesData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `GetEnquiriesData`(IN OperateMode VARCHAR(20), KeyValue VARCHAR(20))
BEGIN
	
    IF(OperateMode = "AllEnquiries") THEN
    BEGIN
		SELECT enq_Name AS Name, enq_Email AS Email, enq_PhoneNumber AS Phone, enq_Message AS Message,
			DATE_FORMAT(enq_CreatedDate, '%d-%m-%Y') AS 'Date'
        FROM shub_enquiries ORDER BY enq_CreatedDate DESC;
    END;
    END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetFrontData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `GetFrontData`(IN OperateMode VARCHAR(30), IN KeyValue VARCHAR(50))
BEGIN
	
    IF(OperateMode = "PAGES") THEN
    BEGIN
		SELECT page_Id AS ID, page_heading AS Heading, page_content AS Content, 
			page_metatitle  AS MetaTitle, page_metadescription AS MetaDescription
		FROM shub_pages
        WHERE page_name = KeyValue;
    END;
    END IF;
    
	IF(OperateMode = "HOME") THEN
    BEGIN
    
		SELECT set_Value AS Banner
        FROM shub_settings
        WHERE set_Name IN ('Banner1', 'Banner2', 'Banner3', 'Banner4', 'Banner5') 
			AND set_Value IS NOT NULL AND set_Value != '';
            
		SELECT 
			course_Id AS Id, course_Name AS Name, course_Fee AS Fee, course_Status AS Status, 
			course_Featured AS Featured, course_Duration AS Duration, course_Logo AS Logo,
			course_DemoLink AS Demo1, course_DemoLink2 AS Demo2
		FROM shub_courses
        ORDER BY course_Name LIMIT 0, 8;
        
        SELECT 
			plcmnt_StudentName AS Name, plcmnt_Gender AS Gender, plcmnt_CompanyName AS Company,
			plcmnt_Photo AS Photo, plcmnt_JobPosition AS Job, plcmnt_City AS City
		FROM bnt_studenthub.shub_placements
		ORDER BY plcmnt_CreatedDate DESC LIMIT 0,10;
        
    END;
    END IF;

    IF(OperateMode = "COURSEDATA") THEN
    BEGIN
		SELECT course_Id AS ID, course_Name AS Name, course_UrlName AS UrlName, course_Fee AS Fee,
			course_ShortDescription AS ShortDescription, course_Description AS Description, 
            course_Featured AS Featured, course_Duration AS Duration, course_Logo AS Logo,
            course_CoverPicture AS CoverPicture, course_Curriculum AS Curriculum, course_DemoLink AS DemoLink,
            course_DemoLink2 AS DemoLink2
		FROM shub_courses
        WHERE course_UrlName = KeyValue;
    END;
    END IF;

    IF(OperateMode = "ALLCOURSES") THEN
    BEGIN
		SELECT 
			course_Id AS Id, course_Name AS Name, course_Fee AS Fee, course_Status AS Status, 
			course_Featured AS Featured, course_Duration AS Duration, course_Logo AS Logo,
			course_DemoLink AS Demo1, course_DemoLink2 AS Demo2
		FROM shub_courses
        ORDER BY course_Name;
    END;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetPlacementsData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `GetPlacementsData`(IN OperateMode VARCHAR(30), IN KeyValue VARCHAR(50))
BEGIN
	
    IF(OperateMode = "ALLPLACEMENTS") THEN
    BEGIN
		SELECT 
			plcmnt_Id AS ID, plcmnt_StudentName AS StudentName, plcmnt_Gender AS Gender, 
            plcmnt_CompanyName AS CompanyName, plcmnt_Photo AS Photo, plcmnt_JobPosition AS JobPosition,
            plcmnt_City AS City, plcmnt_CompanyLogo AS CompanyLogo
        FROM shub_placements;
    END;
    END IF;
    
    
    IF(OperateMode = "EDITPLACEMENT") THEN
    BEGIN
        SELECT 
			plcmnt_Id AS ID, plcmnt_StudentName AS StudentName, plcmnt_Gender AS Gender, 
            plcmnt_CompanyName AS CompanyName, plcmnt_Photo AS Photo, plcmnt_JobPosition AS JobPosition,
            plcmnt_City AS City, plcmnt_CompanyLogo AS CompanyLogo
        FROM shub_placements
        WHERE plcmnt_Id = KeyValue;
    END;
    END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetReviewsData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `GetReviewsData`(IN OperateMode VARCHAR(30), IN KeyValue VARCHAR(50))
BEGIN
	
    IF(OperateMode = "ALLREVIEWS") THEN
    BEGIN
		SELECT COU.course_Name AS Course, DATE_FORMAT(BTH.btch_StartDate, '%d-%m-%Y') AS BatchDate, 
			STD.std_FullName AS Student, INS.instr_FullName AS Instructor, REW.crsrvw_Comment AS Comment, 
			REW.crsrvw_Rating AS Rating, crsrvw_Status AS Status, crsrvw_Id AS ID
		FROM shub_coursereviews AS REW
			LEFT JOIN shub_courses AS COU ON REW.crsrvw_CourseId = COU.course_Id
			LEFT JOIN shub_students AS STD ON REW.crsrvw_StudentId = STD.std_Id
			LEFT JOIN shub_batchschedule AS BTH ON REW.crsrvw_BatchId = BTH.btch_Id
			LEFT JOIN shub_instructors AS INS ON BTH.btch_InstructorId1 = INS.instr_Id
		ORDER BY REW.crsrvw_CreatedDate DESC LIMIT 1, 100;
    END;
    END IF;
    
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetSectionData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `GetSectionData`(IN OperateMode VARCHAR(30), IN KeyValue VARCHAR(50))
BEGIN
	
	IF(OperateMode = "SECTIONSLIST") THEN
	BEGIN
		SELECT
			section_Id AS Id, section_Title AS Title, section_SortId AS SortId
		FROM shub_sectionscourse
		WHERE section_CourseId = KeyValue
		ORDER BY section_SortId ASC;

		SELECT 
			chptr_Id AS ChapterId, chptr_SectionId AS SectionId, chptr_ChapterTitle AS Title, 
			chptr_Video1Duration AS Video1Duration, chptr_SortId AS SortId, 
			chptr_VideoLink1 AS VideoLink1
		FROM shub_chapters 
		WHERE chptr_SectionId IN (SELECT section_Id FROM shub_sectionscourse WHERE section_CourseId = KeyValue)
		ORDER BY chptr_SortId;
	END;
	END IF;
    
    
    IF(OperateMode = "SECTIONDETAILS") THEN
    BEGIN
		SELECT course_Name AS CourseName
		FROM shub_maincourses WHERE course_Id = (
			SELECT section_CourseId FROM shub_sectionscourse WHERE section_Id = KeyValue GROUP BY section_CourseId);

		SELECT 
			section_Id AS Id, section_CourseId AS CourseId, section_Title AS Title, 
			section_Description AS Description, section_SortId AS SortId
		FROM shub_sectionscourse WHERE section_Id = KeyValue;

		SELECT 
			chptr_Id AS ChapterId, chptr_SectionId AS SectionId, chptr_ChapterTitle AS Title, 
			chptr_Video1Duration AS Video1Duration, chptr_SortId AS SortId, 
			chptr_VideoLink1 AS VideoLink1
		FROM shub_chapters 
		WHERE chptr_SectionId = KeyValue;

		SELECT
			sectiontest_Id AS TestId, sectiontest_Difficulty AS Difficulty, '0' AS TotalQuestions
		FROM shub_sectiontests
		WHERE sectiontest_SectionId = KeyValue;

		SELECT
			material_Id AS MaterialId, material_SectionId AS SectionId, material_FileName AS FileName
		FROM shub_sectionmaterials
		WHERE material_SectionId = KeyValue;
    END;    
    END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetTestQuestions` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `GetTestQuestions`(IN OperateMode VARCHAR(30), IN KeyValue VARCHAR(50))
BEGIN
	
	IF(OperateMode = "ALLQUESTIONS") THEN
	BEGIN
		SELECT 
			sectiontest_Difficulty AS Difficulty
		FROM shub_sectiontests WHERE sectiontest_Id = KeyValue;
		
		SELECT 
			@a:=@a+1 Sno, 
			sectestqn_Id AS QuestionId, sectestqn_Question AS Question, sectestqn_OptionA AS OptionA, 
            sectestqn_OptionB AS OptionB, sectestqn_OptionC AS OptionC,
            sectestqn_OptionD AS OptionD, sectestqn_Answer AS Answer
		FROM shub_sectiontestquestions, (SELECT @a:= 0) AS a
        WHERE sectestqn_TestId = KeyValue;
	END;
	END IF;
    
    
    IF(OperateMode = "EDITQUESTION") THEN
    BEGIN
		SELECT 
			chptrtest_ChapterId AS ChapterId, 
            chptrtest_Question AS Question, 
            chptrtest_OptionA AS OptionA, 
            chptrtest_OptionB AS OptionB, 
            chptrtest_OptionC AS OptionC, 
            chptrtest_OptionD AS OptionD, 
            chptrtest_Answer AS Answer
		FROM shub_chaptertests 
        WHERE chptrtest_Id = KeyValue;
    END;    
    END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `PagesData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `PagesData`(IN OperateMode VARCHAR(30), Sno VARCHAR(30), UserId VARCHAR(30), Name VARCHAR(45), Heading VARCHAR(100), Content LONGTEXT, MetaTitle VARCHAR(100), MetaDescription VARCHAR(250), OUT Result VARCHAR(50))
BEGIN

    IF(OperateMode = "UpdatePage") THEN
    BEGIN 
		
        UPDATE shub_pages SET 
			page_heading = Heading,
            page_content = Content,
            page_metatitle = MetaTitle,
            page_metadescription = MetaDescription,
            page_updatedby = UserId,
            page_updateddate = NOW()
		WHERE page_name = Name;
    END;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `PlacementsData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `PlacementsData`(IN OperateMode VARCHAR(30), Sno VARCHAR(30), UserId VARCHAR(30), StudentName VARCHAR(45), Gender VARCHAR(10), CompanyName VARCHAR(45), Photo VARCHAR(100), JobPosition VARCHAR(45), City VARCHAR(30), CompanyLogo VARCHAR(100), OUT Result VARCHAR(50))
BEGIN
	
     IF(OperateMode = "AddPlacement") THEN
     BEGIN
		IF((SELECT COUNT(plcmnt_StudentName) FROM shub_placements WHERE plcmnt_StudentName = StudentName) = 0) THEN
        BEGIN
			
            INSERT INTO shub_placements
            (
				plcmnt_StudentName, plcmnt_Gender, plcmnt_CompanyName, plcmnt_Photo, 
                plcmnt_JobPosition, plcmnt_City, plcmnt_CompanyLogo, plcmnt_CreatedBy
            )
            VALUES
            (
				StudentName, Gender, CompanyName, Photo,
                JobPosition, City, CompanyLogo, UserId
            );
            
            /* SELECT 'ADDED' INTO Result;*/
            SELECT LAST_INSERT_ID() INTO Result;
        END;
        
        ELSE
        BEGIN
			SELECT 'STUDENT NAME ALREADY EXISTS' INTO Result;
        END;
        END IF;
        
	 END;
     END IF;
     
     
     IF(OperateMode = "UpdatePlacement") THEN
     BEGIN
		UPDATE shub_placements SET
			plcmnt_StudentName = StudentName,
            plcmnt_Gender = Gender,
            plcmnt_CompanyName = CompanyName,
            plcmnt_Photo = Photo,
            plcmnt_JobPosition = JobPosition,
            plcmnt_City = City,
            plcmnt_CompanyLogo = CompanyLogo,
            plcmnt_UpdatedBy = UserId,
            plcmnt_UpdatedDate = NOW()
        WHERE plcmnt_Id = Sno;
     END;
     END IF;
     
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SectionMaterialData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `SectionMaterialData`(IN OperateMode VARCHAR(30), Sno VARCHAR(10), User VARCHAR(10), SectionId VARCHAR(10), FileName VARCHAR(500), OUT Result VARCHAR(50))
BEGIN

	IF(OperateMode = "AddSectionMaterial") THEN
    BEGIN
    
		INSERT INTO shub_sectionmaterials
		(
			material_SectionId, material_FileName
		)
		VALUES
		(
			SectionId, FileName
		);
		
		SELECT LAST_INSERT_ID() INTO Result;
		
    END;
    END IF;
    
    
    IF(OperateMode = "DeleteSectionMaterial") THEN
    BEGIN
		DELETE FROM shub_sectionmaterials
        WHERE material_Id = Sno;
        
        SELECT 'DELETED' INTO Result;
        
    END;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SectionsData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `SectionsData`(IN OperateMode VARCHAR(20), Sno VARCHAR(10), User VARCHAR(10), CourseId VARCHAR(10), Title VARCHAR(150), Description VARCHAR(250), SortId INT, OUT Result VARCHAR(50))
BEGIN

	IF(OperateMode = "AddSection") THEN
    BEGIN
    
		INSERT INTO shub_sectionscourse
		(
			section_CourseId, section_Title, section_Description, section_SortId, section_CreatedBy
		)
		VALUES
		(
			CourseId, Title, Description, SortId, User
		);
		
		SELECT LAST_INSERT_ID() INTO Result;
		
    END;
    END IF;
    
    
    IF(OperateMode = "UpdateSection") THEN
    BEGIN
		UPDATE shub_sectionscourse SET
			section_Title = Title,
            section_Description = Description,
            section_SortId = SortId,
            section_UpdatedBy = User,
            section_UpdatedDate = NOW()
		WHERE section_Id = Sno;
        
        SELECT 'UPDATED' INTO Result;
        
    END;
    END IF;
    
    
    IF(OperateMode = "DeleteSection") THEN
    BEGIN
		DELETE FROM shub_sectionscourse
		WHERE section_Id = Sno;
        
        SELECT 'DELETED' INTO Result;
        
    END;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SectionTestQuestionsData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `SectionTestQuestionsData`(IN OperateMode VARCHAR(20), Sno VARCHAR(10), User VARCHAR(10), TestId VARCHAR(10), Question VARCHAR(1000), OptionA VARCHAR(500), OptionB VARCHAR(500), OptionC VARCHAR(500),  OptionD VARCHAR(500), Answer VARCHAR(5),  OUT Result VARCHAR(50))
BEGIN

	IF(OperateMode = "AddTestQuestion") THEN
    BEGIN
    
		INSERT INTO shub_sectiontestquestions
		(
			sectestqn_TestId, sectestqn_Question, sectestqn_OptionA, sectestqn_OptionB, sectestqn_OptionC, 
			sectestqn_OptionD, sectestqn_Answer, sectestqn_CreatedBy
		)
		VALUES
		(
			TestId, Question, OptionA, OptionB, OptionC, 
			OptionD, Answer, User
		);
		
		SELECT LAST_INSERT_ID() INTO Result;
		
    END;
    END IF;
    
    
    IF(OperateMode = "UpdateTestQuestion") THEN
    BEGIN
		UPDATE shub_sectiontestquestions SET
			sectestqn_TestId = TestId, 
            sectestqn_Question = Question, 
            sectestqn_OptionA = OptionA, 
            sectestqn_OptionB = OptionB, 
            sectestqn_OptionC = OptionC, 
			sectestqn_OptionD = OptionD, 
            sectestqn_Answer = Answer,
            sectestqn_UpdatedBy = User,
            sectestqn_UpdatedDate = NOW()
		WHERE sectestqn_Id = Sno;
        
        SELECT 'UPDATED' INTO Result;
        
    END;
    END IF;
    
    
    IF(OperateMode = "DeleteTestQuestion") THEN
    BEGIN
		DELETE FROM shub_sectiontestquestions
		WHERE sectestqn_Id = Sno;
        
        SELECT 'DELETED' INTO Result;
        
    END;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SectionTestsData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `SectionTestsData`(IN OperateMode VARCHAR(20), Sno VARCHAR(10), User VARCHAR(10), SectionId VARCHAR(10), Difficulty VARCHAR(500), OUT Result VARCHAR(50))
BEGIN

	IF(OperateMode = "AddSectionTest") THEN
    BEGIN
    
		INSERT INTO shub_sectiontests
		(
			sectiontest_SectionId, sectiontest_Difficulty, sectiontest_CreatedBy
		)
		VALUES
		(
			SectionId, Difficulty, User
		);
		
		SELECT LAST_INSERT_ID() INTO Result;
		
    END;
    END IF;
    
    
    IF(OperateMode = "UpdateSectionTest") THEN
    BEGIN
		UPDATE shub_sectiontests
        SET sectiontest_Difficulty = Difficulty,
			sectiontest_UpdatedBy = User,
            sectiontest_UpdatedDate = NOW()
        WHERE sectiontest_Id = Sno;
        
        SELECT 'UPDATED' INTO Result;
        
    END;
    END IF;
    
    
    IF(OperateMode = "DeleteSectionTest") THEN
    BEGIN
		DELETE FROM shub_sectiontests
        WHERE sectiontest_Id = Sno;
        
        SELECT 'DELETED' INTO Result;
        
    END;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateResults` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_UNSIGNED_SUBTRACTION' */ ;
DELIMITER ;;
CREATE DEFINER=`shub_bnt_main`@`%` PROCEDURE `UpdateResults`(IN ExamType VARCHAR(30), IN ExamYear VARCHAR(10))
BEGIN
	
    IF(ExamType = "1" OR ExamType = "3" OR ExamType = "7" OR ExamType = "9") THEN /* Telangana & AP Intermediate Year 1 & 2 Regular */
    BEGIN
		DELETE FROM `bnt_studenthub`.`rslt_IntermediateYear2Regular` 
			WHERE inter2reg_ExamType = ExamType AND inter2reg_ExamYear = ExamYear;

		/* ADDING NEW RECORDS */

		INSERT INTO `bnt_studenthub`.`rslt_IntermediateYear2Regular`
		(inter2reg_ExamType,
        inter2reg_ExamYear,
        `inter2reg_FullRecord`,
		`inter2reg_ROLLNO`,
        inter2reg_CNAME,
		`inter2reg_CreatedDate`)
		SELECT 
			T1.inter2reg_ExamType,
			T1.inter2reg_ExamYear,
			T1.inter2reg_FullRecord,
			T1.inter2reg_ROLLNO,
            substring(T1.inter2reg_FullRecord, 13, 30),
			NOW()
		FROM rslt_IntermediateYear2Regular_Temp AS T1;

    END;
    END IF;
    
    IF(ExamType = "2" OR ExamType = "4" OR ExamType = "8" OR ExamType = "10") THEN /* Telangana & AP Intermediate Year 1 & 2 Vocational */
    BEGIN
		DELETE FROM `bnt_studenthub`.`rslt_IntermediateYear2Voc` 
			WHERE inter2voc_ExamType = ExamType AND inter2voc_ExamYear = ExamYear;
        
		/* ADDING NEW RECORDS */

		INSERT INTO `bnt_studenthub`.`rslt_IntermediateYear2Voc`
		(inter2voc_ExamType,
        inter2voc_ExamYear,
        `inter2voc_FullRecord`,
		`inter2voc_ROLLNO`,
        inter2reg_CNAME,
		`inter2voc_CreatedDate`)
		SELECT 
			T1.inter2voc_ExamType,
			T1.inter2voc_ExamYear,
			T1.inter2voc_FullRecord,
			T1.inter2voc_ROLLNO,
            substring(T1.inter2voc_FullRecord, 13, 30),
			NOW()
		FROM rslt_IntermediateYear2Voc_Temp AS T1;

    END;
    END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `getallstudentslist`
--

/*!50001 DROP VIEW IF EXISTS `getallstudentslist`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`shub_bnt_main`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `getallstudentslist` AS select `shub_students`.`std_Id` AS `ID`,`shub_students`.`std_FullName` AS `FullName`,`shub_students`.`std_City` AS `City`,`shub_students`.`std_Email` AS `Email`,`shub_students`.`std_Status` AS `Status`,`shub_students`.`std_ProfilePic` AS `ProfilePic` from `shub_students` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_mocktestslist`
--

/*!50001 DROP VIEW IF EXISTS `vw_mocktestslist`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`shub_bnt_main`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_mocktestslist` AS select `main`.`exmcrs_Id` AS `CourseId`,`main`.`exmcrs_Name` AS `Name`,`main`.`exmcrs_Status` AS `Status`,`strm`.`type_Value` AS `Stream`,count(`mk`.`mocktest_CourseId`) AS `TotalTests` from ((`shub_mocktests` `mk` join `shub_examcourses` `main` on((`mk`.`mocktest_CourseId` = `main`.`exmcrs_Id`))) join `shub_dropdowntypes` `strm` on((`main`.`exmcrs_ExamStream` = `strm`.`type_Id`))) group by `main`.`exmcrs_Id`,`main`.`exmcrs_Name`,`main`.`exmcrs_Status`,`strm`.`type_Value` order by `main`.`exmcrs_Name`,`strm`.`type_Value` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vw_tutorialcourseslist`
--

/*!50001 DROP VIEW IF EXISTS `vw_tutorialcourseslist`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`shub_bnt_main`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_tutorialcourseslist` AS select `main`.`course_Id` AS `CourseId`,`main`.`course_Name` AS `Name`,`main`.`course_Status` AS `Status`,`strm`.`type_Value` AS `Stream` from (`shub_maincourses` `main` join `shub_dropdowntypes` `strm` on((`main`.`course_Stream` = `strm`.`type_Id`))) where `main`.`course_Id` in (select `shub_sectionscourse`.`section_CourseId` AS `CourseId` from `shub_sectionscourse` group by `shub_sectionscourse`.`section_CourseId`) order by `main`.`course_Name`,`strm`.`type_Value` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-04-17 14:28:28
