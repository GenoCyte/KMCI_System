-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 08, 2025 at 03:24 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `kmci_database`
--

-- --------------------------------------------------------

--
-- Table structure for table `budget_allocation`
--

CREATE TABLE `budget_allocation` (
  `id` int(11) NOT NULL,
  `project_code` varchar(100) NOT NULL,
  `quotation_id` int(11) NOT NULL,
  `bid_price` int(11) NOT NULL,
  `total_cost` int(11) NOT NULL,
  `status` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `budget_allocation`
--

INSERT INTO `budget_allocation` (`id`, `project_code`, `quotation_id`, `bid_price`, `total_cost`, `status`) VALUES
(2, 'PROJ25-00007', 33, 311, 270, 'Pending'),
(6, 'PROJ25-00008', 35, 649, 564, 'Approved'),
(7, 'PROJ25-00007', 38, 693, 603, 'Approved');

-- --------------------------------------------------------

--
-- Table structure for table `budget_category`
--

CREATE TABLE `budget_category` (
  `id` int(11) NOT NULL,
  `project_code` varchar(100) NOT NULL,
  `allocation_id` int(11) NOT NULL,
  `category_name` varchar(100) NOT NULL,
  `category_budget` int(11) NOT NULL,
  `category_expenses` int(11) NOT NULL,
  `category_remaining` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `budget_category`
--

INSERT INTO `budget_category` (`id`, `project_code`, `allocation_id`, `category_name`, `category_budget`, `category_expenses`, `category_remaining`) VALUES
(23, 'PROJ25-00008', 0, 'Procurement', 564, 564, 0),
(24, 'PROJ25-00008', 0, 'Delivery', 100, 0, 100),
(25, 'PROJ25-00008', 0, 'Packaging', 200, 0, 200),
(26, 'PROJ25-00007', 0, 'Purchasing', 603, 0, 603),
(27, 'PROJ25-00007', 0, 'Delivery', 200, 0, 200);

-- --------------------------------------------------------

--
-- Table structure for table `budget_transaction`
--

CREATE TABLE `budget_transaction` (
  `id` int(11) NOT NULL,
  `project_code` varchar(100) NOT NULL,
  `transaction_description` text NOT NULL,
  `transaction_category` varchar(100) NOT NULL,
  `transaction_amount` int(11) NOT NULL,
  `transaction_date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `budget_transaction`
--

INSERT INTO `budget_transaction` (`id`, `project_code`, `transaction_description`, `transaction_category`, `transaction_amount`, `transaction_date`) VALUES
(10, 'PROJ25-00008', 'Procurement 1', 'Procurement', 564, '2025-11-05');

-- --------------------------------------------------------

--
-- Table structure for table `company_address`
--

CREATE TABLE `company_address` (
  `id` int(11) NOT NULL,
  `company_id` int(11) NOT NULL,
  `house_num` text NOT NULL,
  `street` text NOT NULL,
  `subdivision` text NOT NULL,
  `barangay` text NOT NULL,
  `city` text NOT NULL,
  `province` text NOT NULL,
  `region` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `company_address`
--

INSERT INTO `company_address` (`id`, `company_id`, `house_num`, `street`, `subdivision`, `barangay`, `city`, `province`, `region`) VALUES
(1, 1, '123', 'Kalaban Street', 'Camellya', 'Sto tomas', 'Cebu City', 'Cebu', 'Region VI'),
(9, 8, '123', 'Kalayaan', 'N/A', 'Sto Cristo', 'Davao City', 'Davao', 'VIII'),
(10, 9, '421', 'Gomez', 'N/A', 'Grand Locale', 'Bagiuo City', 'Bagiuo', 'VII'),
(11, 1, '231', 'Syempre', 'N/A', 'kayaan', 'Bocaue', 'Bulacan', 'III'),
(12, 10, '232', 'Kalabaw', 'N/A', 'Halaya', 'Sta maria', 'Bulacan', 'III'),
(13, 11, '112', 'Tondo', 'N?A', 'Tondo', 'Manila', 'Manila', 'NCR'),
(14, 11, '223', 'Galbana', 'N/A', 'Poblacion 1', 'San Jose Del Monte', 'Bulacan', 'III');

-- --------------------------------------------------------

--
-- Table structure for table `company_list`
--

CREATE TABLE `company_list` (
  `id` int(11) NOT NULL,
  `company_name` text NOT NULL,
  `tin` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `company_list`
--

INSERT INTO `company_list` (`id`, `company_name`, `tin`) VALUES
(1, 'kalapati', '2147483647'),
(8, 'John\'s Computer Parts', '2147483647'),
(9, 'Hiemdal\'s Kitchen', '2147483647'),
(10, 'Jeffry Washing', '2147483647'),
(11, 'Team Liquid', '123-213-213-123');

-- --------------------------------------------------------

--
-- Table structure for table `company_role`
--

CREATE TABLE `company_role` (
  `id` int(11) NOT NULL,
  `company_id` int(11) NOT NULL,
  `role` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `company_role`
--

INSERT INTO `company_role` (`id`, `company_id`, `role`) VALUES
(1, 1, 'Client'),
(2, 1, 'Internal'),
(3, 8, 'Supplier'),
(4, 8, 'Client'),
(5, 9, 'Client'),
(6, 10, 'Client'),
(7, 11, 'Supplier'),
(8, 11, 'Client');

-- --------------------------------------------------------

--
-- Table structure for table `customer`
--

CREATE TABLE `customer` (
  `customer_id` int(11) NOT NULL,
  `name` text NOT NULL,
  `contact_person` text NOT NULL,
  `contact_number` text NOT NULL,
  `address` text NOT NULL,
  `email` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `customer`
--

INSERT INTO `customer` (`customer_id`, `name`, `contact_person`, `contact_number`, `address`, `email`) VALUES
(18, 'Kingsland Company', 'Rachel Andy', '09283746584', 'Quiapo Street', 'kingsland@gmail,com'),
(19, 'Hiems Congloromate', 'King Arthur', '09827364532', 'North Caloocan', 'hiems@gmail.com'),
(20, 'Dali Grocery', 'Mang Tanod', '09827361823', 'towerville', 'dali@gmail.com'),
(21, 'Tissue Enterprise', 'Halt Gomez', '0983728372324', 'Bangong Bayan', 'tissue@gmail,com'),
(22, 'Kalasao Corp', 'Japs Perez', '09832937232', 'Japanese', 'kalasao@gmail.com'),
(23, 'fdsfds', 'dsfdsfsdf', 'fdsfsd', 'dsfdsfds', 'sdfdsf'),
(24, 'Kalapati Corporation', 'Kael Lopez', '098283728232', 'Sapang Palay', 'Kapalapit@gmail.com'),
(25, 'Rebz', 'sdasdasd', 'dasd', 'asda', 'asdasd'),
(26, 'BSU', 'Ako', '09282738232', 'Sarmiento', 'bsu@gmail.com');

-- --------------------------------------------------------

--
-- Table structure for table `product_list`
--

CREATE TABLE `product_list` (
  `id` int(11) NOT NULL,
  `sku_upc` varchar(100) NOT NULL,
  `prod_name` text NOT NULL,
  `brand` varchar(100) NOT NULL,
  `main_category` varchar(100) NOT NULL,
  `additional_category` varchar(100) NOT NULL,
  `sub_category` varchar(100) NOT NULL,
  `description` text NOT NULL,
  `pref_vendor` varchar(100) NOT NULL,
  `uom` varchar(100) NOT NULL,
  `base_price` decimal(10,2) NOT NULL,
  `incoming` int(11) NOT NULL,
  `outgoing` int(11) NOT NULL,
  `current` int(11) NOT NULL,
  `status` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `product_list`
--

INSERT INTO `product_list` (`id`, `sku_upc`, `prod_name`, `brand`, `main_category`, `additional_category`, `sub_category`, `description`, `pref_vendor`, `uom`, `base_price`, `incoming`, `outgoing`, `current`, `status`) VALUES
(1, 'PAP-000001', 'Hard Copy Paper (Substance 20) - A4', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy20', 'P-Lim Trading Incorporated', 'REAM', 163.00, 0, 0, 0, 'Active'),
(2, 'PAP-000001', 'Hard Copy Paper (Substance 20) - A4', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy20', 'Advance Paper Corp.', 'REAM', 141.00, 0, 0, 0, 'Active'),
(3, 'PAP-000002', 'Hard Copy Paper (Substance 20) - Legal (8.5x13)', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy20', 'P-Lim Trading Incorporated', 'REAM', 186.00, 0, 0, 0, 'Active'),
(4, 'PAP-000002', 'Hard Copy Paper (Substance 20) - Legal (8.5x13)', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy20', 'Advance Paper Corp.', 'REAM', 160.70, 0, 0, 0, 'Active'),
(5, 'PAP-000003', 'Hard Copy Paper (Substance 20) - Letter', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy20', 'P-Lim Trading Incorporated', 'REAM', 157.00, 0, 0, 0, 'Active'),
(6, 'PAP-000003', 'Hard Copy Paper (Substance 20) - Letter', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy20', 'Advance Paper Corp.', 'REAM', 136.00, 0, 0, 0, 'Active'),
(7, 'PAP-000004', 'Hard Copy Paper (Substance 24) - A4', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy24', 'P-Lim Trading Incorporated', 'REAM', 187.00, 0, 0, 0, 'Active'),
(8, 'PAP-000004', 'Hard Copy Paper (Substance 24) - A4', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy24', 'Advance Paper Corp.', 'REAM', 160.70, 0, 0, 0, 'Active'),
(9, 'PAP-000005', 'Hard Copy Paper (Substance 24) - Legal (8.5x13)', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy24', 'P-Lim Trading Incorporated', 'REAM', 213.00, 0, 0, 0, 'Active'),
(10, 'PAP-000005', 'Hard Copy Paper (Substance 24) - Legal (8.5x13)', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy24', 'Advance Paper Corp.', 'REAM', 183.20, 0, 0, 0, 'Active'),
(11, 'PAP-000006', 'Hard Copy Paper (Substance 24) - Letter', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy24', 'P-Lim Trading Incorporated', 'REAM', 180.00, 0, 0, 0, 'Active'),
(12, 'PAP-000006', 'Hard Copy Paper (Substance 24) - Letter', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy24', 'Advance Paper Corp.', 'REAM', 155.00, 0, 0, 0, 'Active'),
(13, 'STG-000001', 'ADVANCE BALIKBAYAN BOX (20X20X20 IN) - BROWN', 'Advance', 'School and Office Supplies', 'Storage Solutions', 'Balikbayan Boxes', 'Box - Balikbayan', 'P-Lim Trading Incorporated', 'PC', 103.00, 0, 0, 0, 'Active'),
(14, 'STG-000002', 'ADVANCE BALIKBAYAN BOX (20X20X20 IN) - WHITE', 'Advance', 'School and Office Supplies', 'Storage Solutions', 'Balikbayan Boxes', 'Box - Balikbayan', 'P-Lim Trading Incorporated', 'PC', 160.00, 0, 0, 0, 'Active'),
(15, 'STG-000003', 'ADVANCE STORE-ALL (10.25X12.5X15.75)', 'Advance', 'School and Office Supplies', 'Storage Solutions', 'Storage Boxes', 'Box - Storage', 'P-Lim Trading Incorporated', 'PC', 95.00, 0, 0, 0, 'Active'),
(16, 'PAP-000007', 'A-Plus Copy Paper (Substance 20) - A4', 'A-Plus', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy20', 'Advance Paper Corp.', 'ream', 132.70, 0, 0, 0, 'Active'),
(17, 'PAP-000008', 'A-Plus Copy Paper (Substance 20) - Legal', 'A-Plus', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy20', 'Advance Paper Corp.', 'ream', 151.30, 0, 0, 0, 'Active'),
(18, 'PAP-000009', 'A-Plus Copy Paper (Substance 20) - Letter', 'A-Plus', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy20', 'Advance Paper Corp.', 'ream', 128.00, 0, 0, 0, 'Active'),
(19, 'PAP-000010', 'A-Plus Copy Paper (Substance 24) - A4', 'A-Plus', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy24', 'Advance Paper Corp.', 'ream', 151.40, 0, 0, 0, 'Active'),
(20, 'PAP-000011', 'A-Plus Copy Paper (Substance 24) - Legal', 'A-Plus', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy24', 'Advance Paper Corp.', 'ream', 172.50, 0, 0, 0, 'Active'),
(21, 'PAP-000012', 'A-Plus Copy Paper (Substance 24) - Letter', 'A-Plus', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy24', 'Advance Paper Corp.', 'ream', 146.00, 0, 0, 0, 'Active'),
(22, 'NTB-000001', 'Advance A+ Neon Notes - Composition Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Composition Notebooks', 'Notebook - Composition Notebook', 'Advance Paper Corp.', 'Book', 15.75, 0, 0, 0, 'Active'),
(23, 'NTB-000002', 'Advance Adventure Time - Composition Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Composition Notebooks', 'Notebook - Composition Notebook', 'Advance Paper Corp.', 'Book', 16.25, 0, 0, 0, 'Active'),
(24, 'NTB-000003', 'Advance Batman - Composition Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Composition Notebooks', 'Notebook - Composition Notebook', 'Advance Paper Corp.', 'Book', 16.25, 0, 0, 0, 'Active'),
(25, 'NTB-000004', 'Advance Color Blast - Composition Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Composition Notebooks', 'Notebook - Composition Notebook', 'Advance Paper Corp.', 'Book', 15.75, 0, 0, 0, 'Active'),
(26, 'NTB-000005', 'Advance Color Coding - Composition Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Composition Notebooks', 'Notebook - Composition Notebook', 'Advance Paper Corp.', 'Book', 15.75, 0, 0, 0, 'Active'),
(27, 'NTB-000006', 'Advance Funny Faces - Composition Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Composition Notebooks', 'Notebook - Composition Notebook', 'Advance Paper Corp.', 'Book', 15.75, 0, 0, 0, 'Active'),
(28, 'NTB-000007', 'Advance Neon Calypso - Composition Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Composition Notebooks', 'Notebook - Composition Notebook', 'Advance Paper Corp.', 'Book', 15.75, 0, 0, 0, 'Active'),
(29, 'NTB-000008', 'Advance Superman - Composition Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Composition Notebooks', 'Notebook - Composition Notebook', 'Advance Paper Corp.', 'Book', 16.25, 0, 0, 0, 'Active'),
(30, 'NTB-000009', 'Advance We Bare Bears - Composition Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Composition Notebooks', 'Notebook - Composition Notebook', 'Advance Paper Corp.', 'Book', 16.25, 0, 0, 0, 'Active'),
(31, 'NTB-000010', 'Easywrite K-12 - Composition Notebook', 'Easywrite', 'School and Office Supplies', 'Notebooks', 'Composition Notebooks', 'Notebook - Composition Notebook', 'Advance Paper Corp.', 'Book', 21.75, 0, 0, 0, 'Active'),
(32, 'NTB-000011', 'Easywrite Premium Notes - Composition Notebook', 'Easywrite', 'School and Office Supplies', 'Notebooks', 'Composition Notebooks', 'Notebook - Composition Notebook', 'Advance Paper Corp.', 'Book', 17.50, 0, 0, 0, 'Active'),
(33, 'NTB-000012', 'Advance A+ Neon Notes - Writing Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Writing Notebooks', 'Notebooks - Writing Notebooks', 'Advance Paper Corp.', 'Book', 15.75, 0, 0, 0, 'Active'),
(34, 'NTB-000013', 'Advance Adventure Time - Writing Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Writing Notebooks', 'Notebooks - Writing Notebooks', 'Advance Paper Corp.', 'Book', 16.25, 0, 0, 0, 'Active'),
(35, 'NTB-000014', 'Advance Batman - Writing Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Writing Notebooks', 'Notebooks - Writing Notebooks', 'Advance Paper Corp.', 'Book', 16.25, 0, 0, 0, 'Active'),
(36, 'NTB-000015', 'Advance Color Blast - Writing Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Writing Notebooks', 'Notebooks - Writing Notebooks', 'Advance Paper Corp.', 'Book', 15.75, 0, 0, 0, 'Active'),
(37, 'NTB-000016', 'Advance Color Coding - Writing Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Writing Notebooks', 'Notebooks - Writing Notebooks', 'Advance Paper Corp.', 'Book', 15.75, 0, 0, 0, 'Active'),
(38, 'NTB-000017', 'Advance Funny Faces - Writing Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Writing Notebooks', 'Notebooks - Writing Notebooks', 'Advance Paper Corp.', 'Book', 15.75, 0, 0, 0, 'Active'),
(39, 'NTB-000018', 'Advance Neon Calypso - Writing Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Writing Notebooks', 'Notebooks - Writing Notebooks', 'Advance Paper Corp.', 'Book', 15.75, 0, 0, 0, 'Active'),
(40, 'NTB-000019', 'Advance Superman - Writing Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Writing Notebooks', 'Notebooks - Writing Notebooks', 'Advance Paper Corp.', 'Book', 16.25, 0, 0, 0, 'Active'),
(41, 'NTB-000020', 'Advance We Bare Bears - Writing Notebook', 'Advance', 'School and Office Supplies', 'Notebooks', 'Writing Notebooks', 'Notebooks - Writing Notebooks', 'Advance Paper Corp.', 'Book', 16.25, 0, 0, 0, 'Active'),
(42, 'NTB-000021', 'Easywrite K-12 - Writing Notebook', 'Easywrite', 'School and Office Supplies', 'Notebooks', 'Writing Notebooks', 'Notebooks - Writing Notebooks', 'Advance Paper Corp.', 'Book', 21.75, 0, 0, 0, 'Active'),
(43, 'NTB-000022', 'Easywrite Premium Notes - Writing Notebook', 'Easywrite', 'School and Office Supplies', 'Notebooks', 'Writing Notebooks', 'Notebooks - Writing Notebooks', 'Advance Paper Corp.', 'Book', 17.50, 0, 0, 0, 'Active'),
(44, 'HSS-000001', 'Bioderm Family Germacidal Soap 60g - White (Clean white)', 'Bioderm', 'School and Office Supplies', 'Health and Safety Supplies', 'Sanitation Products', 'Soap - Sanitation Products', 'International Pharmaceuticals Inc.', 'PC', 14.27, 0, 0, 0, 'Active'),
(45, 'HSS-000002', 'Bioderm Family Germacidal Soap 60g - Blue (Coolness)', 'Bioderm', 'School and Office Supplies', 'Health and Safety Supplies', 'Sanitation Products', 'Soap - Sanitation Products', 'International Pharmaceuticals Inc.', 'PC', 15.03, 0, 0, 0, 'Active'),
(46, 'HSS-000003', 'Bioderm Family Germacidal Soap 60g - Green (Freshen)', 'Bioderm', 'School and Office Supplies', 'Health and Safety Supplies', 'Sanitation Products', 'Soap - Sanitation Products', 'International Pharmaceuticals Inc.', 'PC', 14.73, 0, 0, 0, 'Active'),
(47, 'HSS-000004', 'Bioderm Family Germacidal Soap 60g - Pink (Bloom)', 'Bioderm', 'School and Office Supplies', 'Health and Safety Supplies', 'Sanitation Products', 'Soap - Sanitation Products', 'International Pharmaceuticals Inc.', 'PC', 14.73, 0, 0, 0, 'Active'),
(48, 'HSS-000005', 'Bioderm Family Germacidal Soap 60g - Dark Blue (Intense Coolness)', 'Bioderm', 'School and Office Supplies', 'Health and Safety Supplies', 'Sanitation Products', 'Soap - Sanitation Products', 'International Pharmaceuticals Inc.', 'PC', 15.78, 0, 0, 0, 'Active'),
(49, 'HSS-000006', 'SECA 803 Electronic Flat Scale', 'Seca', 'School and Office Supplies', 'Health and Safety Supplies', 'Measuring Equipment', 'Flat Scale - Measuring Equipment', 'Wellness Pro', 'unit', 8370.00, 0, 0, 0, 'Active'),
(50, 'PKG-000001', 'Sacks Plain White 18x18', 'Generic', 'Agriculture', 'Packaging Supplies', 'Sacks', 'Sack - Large Storing Bad', 'Sako Factory Inc.', 'PC', 6.20, 0, 0, 0, 'Active'),
(51, 'OFC-000001', 'Stamp Pad No. 1 (Clear)', 'HBW', 'School and Office Supplies', 'Office Consumables', 'Stamp Pad', 'Stamp - Marking Tool', 'Officemaster and Paper Sales', 'PC', 35.00, 0, 0, 0, 'Active'),
(52, 'WRI-000001', 'Refill Ink 30ml (Black) - 12pcs/Box', 'Pilot', 'School and Office Supplies', 'Writing Instruments', 'Ink Refills', 'Ink - Ink Refill', 'Officemaster and Paper Sales', 'Box', 750.00, 0, 0, 0, 'Active'),
(53, 'WRI-000002', 'Super Color Marker Broad - 12pcs/Box', 'Pilot', 'School and Office Supplies', 'Writing Instruments', 'Markers', 'Marker - Writing Tool', 'Officemaster and Paper Sales', 'Box', 348.00, 0, 0, 0, 'Active'),
(54, 'PWR-000001', 'Energizer Max AAA Battery (4pcs/pack)', 'Energizer', 'Electronics and Electrical Supplies', 'Power Source', 'Batteries', 'Batteries - Battery', 'Officemaster and Paper Sales', 'pack', 64.00, 0, 0, 0, 'Active'),
(55, 'CLN-000001', 'Albatross with Holder 50g', 'Albatross', 'School and Office Supplies', 'Cleaning and Maintenance', 'Air Fresheners', 'Albatross - Air Freshener', 'Officemaster and Paper Sales', 'set', 57.00, 0, 0, 0, 'Active'),
(56, 'CLN-000002', 'Albatross with Holder 100g', 'Albatross', 'School and Office Supplies', 'Cleaning and Maintenance', 'Air Fresheners', 'Albatross - Air Freshener', 'Officemaster and Paper Sales', 'set', 87.00, 0, 0, 0, 'Active'),
(57, 'CUT-000001', 'Cutter Big Heavy Duty', 'Generic', 'School and Office Supplies', 'Cutting Tools', 'Cutters', 'Cutter - Cutting Tool', 'Officemaster and Paper Sales', 'PC', 140.00, 0, 0, 0, 'Active'),
(58, 'WRI-000003', 'Flextok Retractable Ballpen 0.5mm - Black', 'Flextok', 'School and Office Supplies', 'Writing Instruments', 'Ballpens', 'Ballpen - Writing Tool', 'Officemaster and Paper Sales', 'Box', 65.00, 0, 0, 0, 'Active'),
(59, 'HSS-000007', 'Cotton Hand Gloves', 'Generic', 'School and Office Supplies', 'Health and Safety Supplies', 'Protective Gloves', 'Gloves - Protective Wear', 'Officemaster and Paper Sales', 'Pair', 30.00, 0, 0, 0, 'Active'),
(60, 'WRI-000004', 'Faber Castle Highlighter - Green', 'Faber Castle', 'School and Office Supplies', 'Writing Instruments', 'Highlighters', 'Highlighter - Writing Tool/Marking Tool', 'Officemaster and Paper Sales', 'PC', 28.00, 0, 0, 0, 'Active'),
(61, 'CLN-000003', 'Glass Window Cleaner', 'Lave', 'School and Office Supplies', 'Cleaning and Maintenance', 'Cleaning Solutions', 'Glass Cleaner - Cleaning Solution', 'Officemaster and Paper Sales', 'PC', 54.00, 0, 0, 0, 'Active'),
(62, 'PAP-000013', 'Groundwood Paper Short', 'Pilot', 'School and Office Supplies', 'Paper Products', 'Groundwood Paper', 'Paper - Groundwood Paper', 'Officemaster and Paper Sales', 'Ream', 175.00, 0, 0, 0, 'Active'),
(63, 'CLN-000004', 'Lysol Disinfectant Spray 170g - Crisp Linen Scent', 'Lysol', 'School and Office Supplies', 'Cleaning and Maintenance', 'Disinfectants', 'Disinfectant Spray - Disinfectants', 'Officemaster and Paper Sales', 'Can', 300.00, 0, 0, 0, 'Active'),
(64, 'CLN-000005', 'Mop Head', 'Generic', 'School and Office Supplies', 'Cleaning and Maintenance', 'Mops', 'Mop - Cleaning Tool', 'Officemaster and Paper Sales', 'PC', 144.00, 0, 0, 0, 'Active'),
(65, 'CLN-000006', 'Mop Squeezer', 'Generic', 'School and Office Supplies', 'Cleaning and Maintenance', 'Mop Accessories', 'Mop Squeezer - Cleaning Tool', 'Officemaster and Paper Sales', 'PC', 550.00, 0, 0, 0, 'Active'),
(66, 'CLN-000007', 'Muriatic Acid', 'Gleam', 'School and Office Supplies', 'Cleaning and Maintenance', 'Cleaning Solutions', 'Acid - Cleaning Solutions', 'Officemaster and Paper Sales', 'Bottle', 102.00, 0, 0, 0, 'Active'),
(67, 'DSK-000001', 'Office Tray Metal - 3 Layer', 'Generic', 'School and Office Supplies', 'Desk Accessories', 'Document Trays', 'Metal Tray - Document Organizer', 'Officemaster and Paper Sales', 'PC', 139.00, 0, 0, 0, 'Active'),
(68, 'WRI-000005', 'Pilot Permanent Broad Marker - Black', 'Pilot', 'School and Office Supplies', 'Writing Instruments', 'Markers', 'Marker - Marking Tool', 'Officemaster and Paper Sales', 'Box', 425.00, 0, 0, 0, 'Active'),
(69, 'WRI-000006', 'Pilot Permanent Marker Refill Ink - Black', 'Pilot', 'School and Office Supplies', 'Writing Instruments', 'Ink Refills', 'Ink - Ink Refill', 'Officemaster and Paper Sales', 'Box', 92.00, 0, 0, 0, 'Active'),
(70, 'CLN-000008', 'Round rags', 'Generic', 'School and Office Supplies', 'Cleaning and Maintenance', 'Cleaning Cloths & Rags', 'Rags - Cleaning Tool', 'Officemaster and Paper Sales', 'Kg', 75.00, 0, 0, 0, 'Active'),
(71, 'CUT-000002', 'Joy Scissors 8.5 inch (12pcs/box) - High Quality', 'Joy', 'School and Office Supplies', 'Cutting Tools', 'Scissors', 'Scissors - Cutting Tool', 'Officemaster and Paper Sales', 'Box', 161.00, 0, 0, 0, 'Active'),
(72, 'BND-000001', 'Tiger Head Staple Wire #35', 'Tiger Head', 'School and Office Supplies', 'Binding Supplies', 'Staple Wires', 'Staple Wire - Staple Wire Refill', 'Officemaster and Paper Sales', 'Box', 37.00, 0, 0, 0, 'Active'),
(73, 'BND-000002', 'Max Stapler with remover', 'Max', 'School and Office Supplies', 'Binding Supplies', 'Staplers', 'Stapler - Fastening Tool', 'Officemaster and Paper Sales', 'PC', 421.00, 0, 0, 0, 'Active'),
(74, 'PAP-000014', 'Joy Sticky Note 1.5 x 2 inch', 'Joy', 'School and Office Supplies', 'Paper Products', 'Sticky Notes', 'Sticky Note - Organizing Tool', 'Officemaster and Paper Sales', 'Pad', 35.00, 0, 0, 0, 'Active'),
(75, 'ADT-000001', 'Tape Dispenser 2 inch Metal', 'Generic', 'School and Office Supplies', 'Adhesives and Tapes', 'Tape Dispensers', 'Tape Dispenser', 'Officemaster and Paper Sales', 'PC', 130.00, 0, 0, 0, 'Active'),
(76, 'ADT-000002', 'Tape Dispenser 1 inch', 'HBW', 'School and Office Supplies', 'Adhesives and Tapes', 'Tape Dispensers', 'Tape Dispenser', 'Officemaster and Paper Sales', 'PC', 90.00, 0, 0, 0, 'Active'),
(77, 'CLN-000009', 'Toilet Pump', 'Generic', 'School and Office Supplies', 'Cleaning and Maintenance', 'Sanitation Products', 'Pump - Sanitation Products', 'Officemaster and Paper Sales', 'PC', 114.00, 0, 0, 0, 'Active'),
(78, 'CLN-000010', 'Zonrox', 'Zonrox', 'School and Office Supplies', 'Cleaning and Maintenance', 'Sanitation Products', 'Zonrox - Cleaning Solution', 'Officemaster and Paper Sales', 'Gallon', 169.00, 0, 0, 0, 'Active'),
(79, 'CLN-000011', 'Cobweb remover broom', 'Generic', 'School and Office Supplies', 'Cleaning and Maintenance', 'Cleaning Tools', 'Broom - Cleaning Tool', 'Officemaster and Paper Sales', 'PC', 129.00, 0, 0, 0, 'Active'),
(80, 'PAP-000001', 'Hard Copy Paper (Substance 20) - A4', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy20', 'Officemaster and Paper Sales', 'REAM', 141.00, 0, 0, 0, 'Active'),
(81, 'PAP-000002', 'Hard Copy Paper (Substance 20) - Legal (8.5x13)', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy20', 'Officemaster and Paper Sales', 'REAM', 161.00, 0, 0, 0, 'Active'),
(82, 'PAP-000003', 'Hard Copy Paper (Substance 20) - Letter', 'Advance', 'School and Office Supplies', 'Paper Products', 'Copy Paper', 'Paper - Copy20', 'Officemaster and Paper Sales', 'REAM', 131.00, 0, 0, 0, 'Active'),
(83, 'CLN-000012', 'Hygienix 70% Solution with Moisturizer - Ethyl Alcohol', 'Hygienix', 'School and Office Supplies', 'Cleaning and Maintenance', 'Sanitation Products', 'Alcohol - Cleaning Solution', 'Splash Corporation', 'Gallon', 611.00, 0, 0, 0, 'Active'),
(84, 'HSS-000008', 'Immunpro Sodium Ascorbate plus Zinc Non-acidic (500mg) 100 tablets/box', 'Immunpro', 'School and Office Supplies', 'Health and Safety Supplies', 'Vitamins & Supplements', 'Multi Vitamins - Supplements', 'Mercury Drug', 'Box', 825.00, 0, 0, 0, 'Active'),
(85, 'HWR-000001', 'Screw Driver', 'CAT', '', '', 'Tools', 'Yellow Screw Driver', '', 'Pcs', 0.00, 0, 0, 0, 'Active');

-- --------------------------------------------------------

--
-- Table structure for table `project_list`
--

CREATE TABLE `project_list` (
  `id` int(11) NOT NULL,
  `project_code` varchar(100) NOT NULL,
  `company_id` int(11) NOT NULL,
  `company_name` text NOT NULL,
  `address_id` int(11) NOT NULL,
  `proponent_id` int(11) NOT NULL,
  `tin` varchar(15) NOT NULL,
  `description` text NOT NULL,
  `budget_allocation` decimal(10,0) NOT NULL,
  `status` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `project_list`
--

INSERT INTO `project_list` (`id`, `project_code`, `company_id`, `company_name`, `address_id`, `proponent_id`, `tin`, `description`, `budget_allocation`, `status`) VALUES
(8, 'PROJ25-00004', 1, 'kalapati', 0, 0, '0', 'School Supplies', 30000, ''),
(9, 'PROJ25-00005', 8, 'John\'s Computer Parts', 0, 0, '0', 'Computer parts Supplies', 2323, ''),
(10, 'PROJ25-00006', 9, 'Hiemdal\'s Kitchen', 0, 0, '0', 'Kitchen tools', 312323, ''),
(12, 'PROJ25-00007', 11, 'Team Liquid', 13, 13, '123-213-213-123', 'Gaming Chair', 803, ''),
(13, 'PROJ25-00008', 11, 'Team Liquid', 14, 13, '123-213-213-123', 'Gaming Headset', 864, 'Under Negotiation'),
(14, 'PROJ25-00009', 10, 'Jeffry Washing', 0, 0, '2147483647', 'Kitchen Tools', 0, 'Under Negotiation');

-- --------------------------------------------------------

--
-- Table structure for table `proponents`
--

CREATE TABLE `proponents` (
  `id` int(11) NOT NULL,
  `company_id` int(11) NOT NULL,
  `proponent_name` varchar(100) NOT NULL,
  `proponent_email` text NOT NULL,
  `proponent_number` varchar(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `proponents`
--

INSERT INTO `proponents` (`id`, `company_id`, `proponent_name`, `proponent_email`, `proponent_number`) VALUES
(1, 1, 'Grace Period', 'grace@gmail.com', '09827364512'),
(9, 8, 'Christian Bale', 'bale@gmail.com', '09283746273'),
(10, 9, 'Steven Universe', 'steven@gmail.com', '0983748293'),
(11, 1, 'Hanna Marie', 'marie@gmail.com', '09283726374'),
(12, 10, 'Kamihate Josuke', 'kamihate@gmail.com', '0982736451'),
(13, 11, 'Grab Kalasa', 'grab@gmail.com', '09827364512'),
(14, 11, 'Celebi Hams', 'Hams@gmail.com', '09287382832');

-- --------------------------------------------------------

--
-- Table structure for table `purchase_order`
--

CREATE TABLE `purchase_order` (
  `id` int(11) NOT NULL,
  `project_code` varchar(100) NOT NULL,
  `vendor_id` int(11) NOT NULL,
  `quotation_id` int(11) NOT NULL,
  `po_date` date DEFAULT NULL,
  `po_name` varchar(100) NOT NULL,
  `vendor_name` varchar(100) NOT NULL,
  `quantity` int(11) NOT NULL,
  `grand_total` decimal(10,2) NOT NULL,
  `remarks` text NOT NULL,
  `status` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `purchase_order`
--

INSERT INTO `purchase_order` (`id`, `project_code`, `vendor_id`, `quotation_id`, `po_date`, `po_name`, `vendor_name`, `quantity`, `grand_total`, `remarks`, `status`) VALUES
(32, 'PROJ25-00007', 2, 38, '2025-11-08', 'PO-001', 'Advance Paper Corp.', 3, 503.36, '', 'Pending');

-- --------------------------------------------------------

--
-- Table structure for table `purchase_order_items`
--

CREATE TABLE `purchase_order_items` (
  `id` int(11) NOT NULL,
  `po_id` int(11) NOT NULL,
  `sku_upc` varchar(100) NOT NULL,
  `prod_name` varchar(100) NOT NULL,
  `brand` varchar(100) NOT NULL,
  `quantity` int(11) NOT NULL,
  `unit_price` decimal(10,2) NOT NULL,
  `sub_total` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `purchase_order_items`
--

INSERT INTO `purchase_order_items` (`id`, `po_id`, `sku_upc`, `prod_name`, `brand`, `quantity`, `unit_price`, `sub_total`) VALUES
(45, 32, 'PAP-000001', 'Hard Copy Paper (Substance 20) - A4', 'Advance', 1, 162.15, 162.15),
(46, 32, 'PAP-000002', 'Hard Copy Paper (Substance 20) - Legal (8.5x13)', 'Advance', 1, 184.81, 184.81),
(47, 32, 'PAP-000003', 'Hard Copy Paper (Substance 20) - Letter', 'Advance', 1, 156.40, 156.40);

-- --------------------------------------------------------

--
-- Table structure for table `purchase_request`
--

CREATE TABLE `purchase_request` (
  `id` int(11) NOT NULL,
  `project_code` varchar(100) NOT NULL,
  `vendor_id` int(11) NOT NULL,
  `quotation_id` int(11) NOT NULL,
  `pr_name` varchar(100) NOT NULL,
  `vendor_name` varchar(100) NOT NULL,
  `quantity` int(11) NOT NULL,
  `grand_total` decimal(10,2) NOT NULL,
  `status` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `purchase_request`
--

INSERT INTO `purchase_request` (`id`, `project_code`, `vendor_id`, `quotation_id`, `pr_name`, `vendor_name`, `quantity`, `grand_total`, `status`) VALUES
(2, '', 8, 37, '', 'CLN-000004', 1, 345.00, 'Pending'),
(4, '', 9, 37, '', 'CLN-000012', 1, 702.65, 'Pending'),
(16, 'PROJ25-00007', 2, 38, 'PR-001', 'Advance Paper Corp.', 3, 503.36, 'Approved'),
(17, 'PROJ25-00007', 8, 38, 'PR-002', 'Officemaster and Paper Sales', 2, 189.75, 'Approved');

-- --------------------------------------------------------

--
-- Table structure for table `purchase_request_items`
--

CREATE TABLE `purchase_request_items` (
  `id` int(11) NOT NULL,
  `pr_id` int(11) NOT NULL,
  `sku_upc` varchar(100) NOT NULL,
  `prod_name` varchar(100) NOT NULL,
  `brand` varchar(100) NOT NULL,
  `quantity` int(11) NOT NULL,
  `unit_price` decimal(10,2) NOT NULL,
  `sub_total` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `purchase_request_items`
--

INSERT INTO `purchase_request_items` (`id`, `pr_id`, `sku_upc`, `prod_name`, `brand`, `quantity`, `unit_price`, `sub_total`) VALUES
(10, 16, 'PAP-000001', 'Hard Copy Paper (Substance 20) - A4', 'Advance', 1, 162.15, 162.15),
(11, 16, 'PAP-000002', 'Hard Copy Paper (Substance 20) - Legal (8.5x13)', 'Advance', 1, 184.81, 184.81),
(12, 16, 'PAP-000003', 'Hard Copy Paper (Substance 20) - Letter', 'Advance', 1, 156.40, 156.40),
(13, 17, 'CLN-000008', 'Round rags', 'Generic', 1, 86.25, 86.25),
(14, 17, 'ADT-000002', 'Tape Dispenser 1 inch', 'HBW', 1, 103.50, 103.50);

-- --------------------------------------------------------

--
-- Table structure for table `quotation`
--

CREATE TABLE `quotation` (
  `quotation_id` int(11) NOT NULL,
  `quotation_name` varchar(100) NOT NULL,
  `project_code` varchar(100) NOT NULL,
  `company_id` int(11) NOT NULL,
  `address_id` int(11) NOT NULL,
  `proponent_id` int(11) NOT NULL,
  `quotation_date` date NOT NULL,
  `validity_period` date NOT NULL,
  `payment` varchar(100) NOT NULL,
  `delivery_time` varchar(100) NOT NULL,
  `total_cost` decimal(10,2) NOT NULL,
  `bid_price` decimal(10,2) NOT NULL,
  `bid_percentage` int(11) NOT NULL,
  `status` varchar(50) NOT NULL,
  `remarks` text NOT NULL,
  `requested_by` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `quotation`
--

INSERT INTO `quotation` (`quotation_id`, `quotation_name`, `project_code`, `company_id`, `address_id`, `proponent_id`, `quotation_date`, `validity_period`, `payment`, `delivery_time`, `total_cost`, `bid_price`, `bid_percentage`, `status`, `remarks`, `requested_by`) VALUES
(37, 'RFQ-003', 'PROJ25-00008', 11, 14, 13, '2025-11-05', '2025-11-12', '2', '3', 911.00, 1047.65, 15, 'Approved', '', ''),
(38, 'RFQ-004', 'PROJ25-00007', 11, 13, 13, '2025-11-07', '2025-11-14', '12', '1', 602.70, 693.11, 15, 'Approved', '', ''),
(39, 'RFQ-005', 'PROJ25-00008', 11, 14, 13, '2025-11-08', '2025-11-15', '12', '2', 9000.00, 10350.00, 15, 'Pending', 'gfhgfhg', '');

-- --------------------------------------------------------

--
-- Table structure for table `quotation_items`
--

CREATE TABLE `quotation_items` (
  `item_id` int(11) NOT NULL,
  `quotation_id` int(11) NOT NULL,
  `sku_upc` varchar(100) NOT NULL,
  `pref_vendor` varchar(100) NOT NULL,
  `quantity` int(11) NOT NULL,
  `unit_price` decimal(10,2) NOT NULL,
  `sub_total` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `quotation_items`
--

INSERT INTO `quotation_items` (`item_id`, `quotation_id`, `sku_upc`, `pref_vendor`, `quantity`, `unit_price`, `sub_total`) VALUES
(109, 37, 'CLN-000012', 'Splash Corporation', 1, 702.65, 702.65),
(110, 37, 'CLN-000004', 'Officemaster and Paper Sales', 1, 345.00, 345.00),
(111, 38, 'PAP-000002', 'Advance Paper Corp.', 1, 184.81, 184.81),
(112, 38, 'PAP-000001', 'Advance Paper Corp.', 1, 162.15, 162.15),
(113, 38, 'PAP-000003', 'Advance Paper Corp.', 1, 156.40, 156.40),
(114, 38, 'CLN-000008', 'Officemaster and Paper Sales', 1, 86.25, 86.25),
(115, 38, 'ADT-000002', 'Officemaster and Paper Sales', 1, 103.50, 103.50),
(116, 39, 'ADT-000002', 'Officemaster and Paper Sales', 100, 103.50, 10350.00);

-- --------------------------------------------------------

--
-- Table structure for table `vendor_list`
--

CREATE TABLE `vendor_list` (
  `id` int(11) NOT NULL,
  `vendor_name` text NOT NULL,
  `vendor_address` text NOT NULL,
  `vendor_city` varchar(100) NOT NULL,
  `vendor_state` varchar(100) NOT NULL,
  `vendor_zip` text NOT NULL,
  `vendor_phone` varchar(100) NOT NULL,
  `vendor_email` text NOT NULL,
  `vendor_person` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `vendor_list`
--

INSERT INTO `vendor_list` (`id`, `vendor_name`, `vendor_address`, `vendor_city`, `vendor_state`, `vendor_zip`, `vendor_phone`, `vendor_email`, `vendor_person`) VALUES
(1, 'P-Lim Trading Incorporated', '127 Jose Bautista St. Barangay 80', 'Caloocan City', 'NCR', '1403', '0918-985-7057', 'plimtrading@gmail.com', 'Arjay'),
(2, 'Advance Paper Corp.', '47 Rodriguez Drive Jordan Valley Village', 'Caloocan City', 'NCR', '1401', '0999-392-2059', 'christysanjuan@gmail.com', 'Ms. Christy San Juan'),
(3, 'Goldwings Stationery Products Inc.', '2081 F Benitez Zone 079 Barangay 722', 'Malate', 'NCR', '1004', '(632) 8523-2871', 'sales@goldwings.com.ph', 'Joy'),
(4, 'Margarrett Enterprise Incorporated', 'Pulo Diezmo Road Brgy. Pulo', 'Cabuyao Laguna', 'Region 4A', '4025', '09283746374', 'margarrettenterprise@gmail.com', 'Ms. Alya'),
(5, 'International Pharmaceuticals Inc.', 'B.Suico', 'Mandaue City Cebu', 'Region 7', '6014', '09557513594', 'jgdelacruz@ipi.ph', 'Mr. Joco Dela Cruz'),
(6, 'Wellness Pro Inc.', '56 San Rafael St.', 'Pasig City', 'NCR', '1603', '09272997245', 'tyrone_tadifa@wellnessproinc.com', 'Mr. Tyrone Tadifa'),
(7, 'Sako Factory Inc.', 'Jolly Industrial Park C17 Plaridel Bypass Rd', 'Plaridel Bulacan', 'NCR', '3004', '09451343622', 'dinnobeltran@yahoo.com', 'Ms. Maira Fetalvero'),
(8, 'Officemaster and Paper Sales', '491 Yuchengco St Binondo', 'Manila City', 'NCR', '1000', '09086575000', 'officemaster@gmail.com', 'Ms. Joan'),
(9, 'Splash Corporation', '', '', '', '', '', '', ''),
(10, 'Mercury Drug', '', '', '', '', '', '', '');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `budget_allocation`
--
ALTER TABLE `budget_allocation`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `budget_category`
--
ALTER TABLE `budget_category`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `budget_transaction`
--
ALTER TABLE `budget_transaction`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `company_address`
--
ALTER TABLE `company_address`
  ADD PRIMARY KEY (`id`),
  ADD KEY `company_id` (`company_id`);

--
-- Indexes for table `company_list`
--
ALTER TABLE `company_list`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `company_role`
--
ALTER TABLE `company_role`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`customer_id`);

--
-- Indexes for table `product_list`
--
ALTER TABLE `product_list`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `project_list`
--
ALTER TABLE `project_list`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `proponents`
--
ALTER TABLE `proponents`
  ADD PRIMARY KEY (`id`),
  ADD KEY `company_id` (`company_id`);

--
-- Indexes for table `purchase_order`
--
ALTER TABLE `purchase_order`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `purchase_order_items`
--
ALTER TABLE `purchase_order_items`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `purchase_request`
--
ALTER TABLE `purchase_request`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `purchase_request_items`
--
ALTER TABLE `purchase_request_items`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `quotation`
--
ALTER TABLE `quotation`
  ADD PRIMARY KEY (`quotation_id`),
  ADD KEY `customer_id` (`company_id`);

--
-- Indexes for table `quotation_items`
--
ALTER TABLE `quotation_items`
  ADD PRIMARY KEY (`item_id`),
  ADD KEY `product_id` (`sku_upc`),
  ADD KEY `quotation_items_ibfk_1` (`quotation_id`);

--
-- Indexes for table `vendor_list`
--
ALTER TABLE `vendor_list`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `budget_allocation`
--
ALTER TABLE `budget_allocation`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `budget_category`
--
ALTER TABLE `budget_category`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT for table `budget_transaction`
--
ALTER TABLE `budget_transaction`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `company_address`
--
ALTER TABLE `company_address`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `company_list`
--
ALTER TABLE `company_list`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `company_role`
--
ALTER TABLE `company_role`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `customer`
--
ALTER TABLE `customer`
  MODIFY `customer_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT for table `product_list`
--
ALTER TABLE `product_list`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=86;

--
-- AUTO_INCREMENT for table `project_list`
--
ALTER TABLE `project_list`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `proponents`
--
ALTER TABLE `proponents`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `purchase_order`
--
ALTER TABLE `purchase_order`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=33;

--
-- AUTO_INCREMENT for table `purchase_order_items`
--
ALTER TABLE `purchase_order_items`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=48;

--
-- AUTO_INCREMENT for table `purchase_request`
--
ALTER TABLE `purchase_request`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `purchase_request_items`
--
ALTER TABLE `purchase_request_items`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `quotation`
--
ALTER TABLE `quotation`
  MODIFY `quotation_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=40;

--
-- AUTO_INCREMENT for table `quotation_items`
--
ALTER TABLE `quotation_items`
  MODIFY `item_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=117;

--
-- AUTO_INCREMENT for table `vendor_list`
--
ALTER TABLE `vendor_list`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `company_address`
--
ALTER TABLE `company_address`
  ADD CONSTRAINT `company_address_ibfk_1` FOREIGN KEY (`company_id`) REFERENCES `company_list` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `proponents`
--
ALTER TABLE `proponents`
  ADD CONSTRAINT `proponents_ibfk_1` FOREIGN KEY (`company_id`) REFERENCES `company_list` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `quotation_items`
--
ALTER TABLE `quotation_items`
  ADD CONSTRAINT `quotation_items_ibfk_1` FOREIGN KEY (`quotation_id`) REFERENCES `quotation` (`quotation_id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
