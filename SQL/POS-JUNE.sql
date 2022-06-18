-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 05, 2022 at 05:37 AM
-- Server version: 10.4.13-MariaDB
-- PHP Version: 7.4.7

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pos`
--
CREATE DATABASE IF NOT EXISTS `pos` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `pos`;

-- --------------------------------------------------------

--
-- Table structure for table `admin_masterlist`
--

DROP TABLE IF EXISTS `admin_masterlist`;
CREATE TABLE IF NOT EXISTS `admin_masterlist` (
  `masterlist_id` int(11) NOT NULL AUTO_INCREMENT,
  `masterlist_username` varchar(255) NOT NULL,
  `masterlist_password` varchar(255) NOT NULL,
  `client_ipadd` varchar(50) NOT NULL,
  `client_guid` varchar(255) NOT NULL,
  `client_product_key` varchar(255) NOT NULL,
  `user_id` varchar(11) NOT NULL,
  `active` int(2) NOT NULL,
  `created_at` text NOT NULL,
  `client_store_id` int(11) NOT NULL,
  PRIMARY KEY (`masterlist_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `admin_outlets`
--

DROP TABLE IF EXISTS `admin_outlets`;
CREATE TABLE IF NOT EXISTS `admin_outlets` (
  `loc_store_id` int(11) NOT NULL AUTO_INCREMENT,
  `store_id` int(11) NOT NULL,
  `brand_name` varchar(255) NOT NULL,
  `store_name` varchar(255) NOT NULL,
  `user_guid` varchar(255) NOT NULL,
  `location_name` varchar(50) NOT NULL,
  `postal_code` varchar(50) NOT NULL,
  `address` varchar(255) NOT NULL,
  `Barangay` varchar(255) NOT NULL,
  `municipality` varchar(255) NOT NULL,
  `municipality_name` varchar(255) NOT NULL,
  `province` varchar(255) NOT NULL,
  `province_name` varchar(255) NOT NULL,
  `tin_no` varchar(255) NOT NULL,
  `tel_no` varchar(255) NOT NULL,
  `active` int(2) NOT NULL,
  `created_at` text NOT NULL,
  `MIN` varchar(255) NOT NULL,
  `MSN` varchar(255) NOT NULL,
  `PTUN` varchar(255) NOT NULL,
  PRIMARY KEY (`loc_store_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_admin_category`
--

DROP TABLE IF EXISTS `loc_admin_category`;
CREATE TABLE IF NOT EXISTS `loc_admin_category` (
  `category_id` int(11) NOT NULL AUTO_INCREMENT,
  `category_name` varchar(50) NOT NULL,
  `brand_name` varchar(255) NOT NULL,
  `updated_at` text NOT NULL,
  `origin` varchar(50) NOT NULL,
  `status` int(2) NOT NULL,
  PRIMARY KEY (`category_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_admin_products`
--

DROP TABLE IF EXISTS `loc_admin_products`;
CREATE TABLE IF NOT EXISTS `loc_admin_products` (
  `product_id` int(11) NOT NULL AUTO_INCREMENT,
  `product_sku` varchar(50) NOT NULL,
  `product_name` varchar(50) NOT NULL,
  `formula_id` varchar(255) NOT NULL,
  `product_barcode` varchar(255) NOT NULL,
  `product_category` varchar(255) NOT NULL,
  `product_price` int(255) NOT NULL,
  `product_desc` varchar(255) NOT NULL,
  `product_image` longtext NOT NULL,
  `product_status` varchar(2) NOT NULL,
  `origin` varchar(50) NOT NULL,
  `date_modified` text NOT NULL,
  `guid` varchar(255) NOT NULL,
  `store_id` int(11) NOT NULL,
  `crew_id` varchar(50) NOT NULL,
  `synced` varchar(50) NOT NULL,
  `server_product_id` int(11) NOT NULL,
  `server_inventory_id` int(11) NOT NULL,
  `price_change` int(11) NOT NULL,
  `addontype` text NOT NULL,
  `half_batch` int(11) NOT NULL,
  `partners` text NOT NULL,
  `arrangement` text NOT NULL,
  PRIMARY KEY (`product_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_audit_trail`
--

DROP TABLE IF EXISTS `loc_audit_trail`;
CREATE TABLE IF NOT EXISTS `loc_audit_trail` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `created_at` text NOT NULL,
  `group_name` text NOT NULL,
  `severity` text NOT NULL,
  `crew_id` text NOT NULL,
  `description` text NOT NULL,
  `info` text NOT NULL,
  `store_id` text NOT NULL,
  `synced` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_cash_breakdown`
--

DROP TABLE IF EXISTS `loc_cash_breakdown`;
CREATE TABLE IF NOT EXISTS `loc_cash_breakdown` (
  `cb_id` int(11) NOT NULL AUTO_INCREMENT,
  `1000` int(11) NOT NULL,
  `500` int(11) NOT NULL,
  `200` int(11) NOT NULL,
  `100` int(11) NOT NULL,
  `50` int(11) NOT NULL,
  `20` int(11) NOT NULL,
  `10` int(11) NOT NULL,
  `5` int(11) NOT NULL,
  `1` int(11) NOT NULL,
  `.25` int(11) NOT NULL,
  `.05` int(11) NOT NULL,
  `created_at` text NOT NULL,
  `crew_id` text NOT NULL,
  `status` text NOT NULL,
  `zreading` text NOT NULL,
  `synced` text NOT NULL,
  PRIMARY KEY (`cb_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_coupon_data`
--

DROP TABLE IF EXISTS `loc_coupon_data`;
CREATE TABLE IF NOT EXISTS `loc_coupon_data` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `transaction_number` text NOT NULL,
  `coupon_name` text NOT NULL,
  `coupon_desc` text NOT NULL,
  `coupon_type` text NOT NULL,
  `coupon_line` text NOT NULL,
  `coupon_total` text NOT NULL,
  `gc_value` double NOT NULL,
  `zreading` text NOT NULL,
  `status` text NOT NULL,
  `synced` text NOT NULL DEFAULT 'Unsynced',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_customer_info`
--

DROP TABLE IF EXISTS `loc_customer_info`;
CREATE TABLE IF NOT EXISTS `loc_customer_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `transaction_number` text NOT NULL,
  `cust_name` text NOT NULL,
  `cust_tin` text NOT NULL,
  `cust_address` text NOT NULL,
  `cust_business` text NOT NULL,
  `crew_id` text NOT NULL,
  `store_id` text NOT NULL,
  `created_at` text NOT NULL,
  `active` text NOT NULL,
  `synced` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_daily_transaction`
--

DROP TABLE IF EXISTS `loc_daily_transaction`;
CREATE TABLE IF NOT EXISTS `loc_daily_transaction` (
  `transaction_id` int(11) NOT NULL AUTO_INCREMENT,
  `transaction_number` varchar(255) NOT NULL,
  `grosssales` decimal(11,2) NOT NULL,
  `totaldiscount` decimal(11,2) NOT NULL,
  `amounttendered` decimal(11,2) NOT NULL,
  `change` decimal(11,2) NOT NULL,
  `amountdue` decimal(11,2) NOT NULL,
  `vatablesales` decimal(11,2) NOT NULL,
  `vatexemptsales` decimal(11,2) NOT NULL,
  `zeroratedsales` decimal(11,2) NOT NULL,
  `vatpercentage` decimal(11,2) NOT NULL,
  `lessvat` decimal(11,2) NOT NULL,
  `transaction_type` varchar(50) NOT NULL,
  `discount_type` varchar(50) NOT NULL,
  `totaldiscountedamount` decimal(11,2) NOT NULL,
  `si_number` int(10) NOT NULL,
  `crew_id` varchar(20) NOT NULL,
  `guid` varchar(50) NOT NULL,
  `active` varchar(2) NOT NULL,
  `store_id` varchar(11) NOT NULL,
  `created_at` text NOT NULL,
  `shift` varchar(255) NOT NULL,
  `zreading` text NOT NULL,
  `synced` varchar(255) NOT NULL,
  PRIMARY KEY (`transaction_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_daily_transaction_details`
--

DROP TABLE IF EXISTS `loc_daily_transaction_details`;
CREATE TABLE IF NOT EXISTS `loc_daily_transaction_details` (
  `details_id` int(11) NOT NULL AUTO_INCREMENT,
  `product_id` int(11) NOT NULL,
  `product_sku` varchar(255) NOT NULL,
  `product_name` varchar(255) NOT NULL,
  `quantity` int(20) NOT NULL,
  `price` decimal(11,2) NOT NULL,
  `total` decimal(11,2) NOT NULL,
  `crew_id` varchar(50) NOT NULL,
  `transaction_number` varchar(255) NOT NULL,
  `active` int(2) NOT NULL,
  `created_at` text NOT NULL,
  `guid` varchar(255) NOT NULL,
  `store_id` varchar(50) NOT NULL,
  `total_cost_of_goods` decimal(11,2) NOT NULL,
  `product_category` varchar(255) NOT NULL,
  `zreading` text NOT NULL,
  `transaction_type` text NOT NULL,
  `upgraded` int(11) NOT NULL,
  `addontype` text NOT NULL,
  `seniordisc` double NOT NULL,
  `seniorqty` double NOT NULL,
  `pwddisc` double NOT NULL,
  `pwdqty` double NOT NULL,
  `athletedisc` double NOT NULL,
  `athleteqty` double NOT NULL,
  `spdisc` double NOT NULL,
  `spqty` double NOT NULL,
  `synced` varchar(255) NOT NULL,
  PRIMARY KEY (`details_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_deposit`
--

DROP TABLE IF EXISTS `loc_deposit`;
CREATE TABLE IF NOT EXISTS `loc_deposit` (
  `dep_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `crew_id` varchar(50) NOT NULL,
  `transaction_number` varchar(255) NOT NULL,
  `amount` decimal(11,2) NOT NULL,
  `bank` varchar(255) NOT NULL,
  `transaction_date` varchar(255) NOT NULL,
  `store_id` varchar(11) NOT NULL,
  `guid` varchar(255) NOT NULL,
  `created_at` text NOT NULL,
  `synced` varchar(50) NOT NULL,
  PRIMARY KEY (`dep_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_expense_details`
--

DROP TABLE IF EXISTS `loc_expense_details`;
CREATE TABLE IF NOT EXISTS `loc_expense_details` (
  `expense_id` int(11) NOT NULL AUTO_INCREMENT,
  `expense_number` varchar(255) NOT NULL,
  `expense_type` varchar(50) NOT NULL,
  `item_info` varchar(255) NOT NULL,
  `quantity` int(11) NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `attachment` text NOT NULL,
  `created_at` text NOT NULL,
  `crew_id` varchar(50) NOT NULL,
  `guid` varchar(255) NOT NULL,
  `store_id` int(20) NOT NULL,
  `active` int(2) NOT NULL,
  `zreading` text NOT NULL,
  `synced` varchar(255) NOT NULL,
  PRIMARY KEY (`expense_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_expense_list`
--

DROP TABLE IF EXISTS `loc_expense_list`;
CREATE TABLE IF NOT EXISTS `loc_expense_list` (
  `expense_id` int(11) NOT NULL AUTO_INCREMENT,
  `crew_id` varchar(50) NOT NULL,
  `expense_number` varchar(255) NOT NULL,
  `total_amount` decimal(11,2) NOT NULL,
  `paid_amount` decimal(11,2) NOT NULL,
  `unpaid_amount` decimal(11,2) NOT NULL,
  `store_id` varchar(255) NOT NULL,
  `guid` varchar(255) NOT NULL,
  `created_at` text NOT NULL,
  `active` int(2) NOT NULL,
  `zreading` text NOT NULL,
  `synced` varchar(255) NOT NULL,
  PRIMARY KEY (`expense_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_e_journal`
--

DROP TABLE IF EXISTS `loc_e_journal`;
CREATE TABLE IF NOT EXISTS `loc_e_journal` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `totallines` int(11) NOT NULL,
  `content` longtext NOT NULL,
  `crew_id` text NOT NULL,
  `store_id` text NOT NULL,
  `created_at` text NOT NULL,
  `active` text NOT NULL,
  `zreading` text NOT NULL,
  `synced` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_fm_stock`
--

DROP TABLE IF EXISTS `loc_fm_stock`;
CREATE TABLE IF NOT EXISTS `loc_fm_stock` (
  `fm_id` int(11) NOT NULL AUTO_INCREMENT,
  `formula_id` varchar(255) NOT NULL,
  `stock_primary` decimal(11,2) NOT NULL,
  `stock_secondary` decimal(11,2) NOT NULL,
  `crew_id` varchar(255) NOT NULL,
  `store_id` varchar(11) NOT NULL,
  `guid` varchar(255) NOT NULL,
  `created_at` text NOT NULL,
  `status` int(2) NOT NULL,
  PRIMARY KEY (`fm_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_hold_inventory`
--

DROP TABLE IF EXISTS `loc_hold_inventory`;
CREATE TABLE IF NOT EXISTS `loc_hold_inventory` (
  `hold_id` int(255) NOT NULL AUTO_INCREMENT,
  `sr_total` int(255) NOT NULL,
  `f_id` int(255) NOT NULL,
  `qty` int(255) NOT NULL,
  `id` int(255) NOT NULL,
  `nm` varchar(255) NOT NULL,
  `org_serve` int(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `cog` decimal(11,2) NOT NULL,
  `ocog` decimal(11,2) NOT NULL,
  `prd.addid` int(11) NOT NULL,
  `origin` text NOT NULL,
  PRIMARY KEY (`hold_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_inbox_messages`
--

DROP TABLE IF EXISTS `loc_inbox_messages`;
CREATE TABLE IF NOT EXISTS `loc_inbox_messages` (
  `inbox_id` int(11) NOT NULL AUTO_INCREMENT,
  `crew_id` int(11) NOT NULL,
  `message` varchar(255) NOT NULL,
  `type` varchar(20) NOT NULL,
  `created_at` text NOT NULL,
  `origin` varchar(20) NOT NULL,
  `active` int(2) NOT NULL,
  PRIMARY KEY (`inbox_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_inv_temp_data`
--

DROP TABLE IF EXISTS `loc_inv_temp_data`;
CREATE TABLE IF NOT EXISTS `loc_inv_temp_data` (
  `inventory_id` int(11) NOT NULL AUTO_INCREMENT,
  `store_id` varchar(11) NOT NULL,
  `formula_id` int(11) NOT NULL,
  `product_ingredients` varchar(255) NOT NULL,
  `sku` varchar(255) NOT NULL,
  `stock_primary` decimal(11,2) NOT NULL,
  `stock_secondary` decimal(11,2) NOT NULL,
  `stock_no_of_servings` decimal(11,2) NOT NULL,
  `stock_status` int(11) NOT NULL,
  `critical_limit` int(11) NOT NULL,
  `guid` varchar(255) NOT NULL,
  `created_at` text NOT NULL,
  PRIMARY KEY (`inventory_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_message`
--

DROP TABLE IF EXISTS `loc_message`;
CREATE TABLE IF NOT EXISTS `loc_message` (
  `message_id` int(11) NOT NULL AUTO_INCREMENT,
  `server_message_id` int(11) NOT NULL,
  `from` text NOT NULL,
  `subject` text NOT NULL,
  `content` text NOT NULL,
  `guid` text NOT NULL,
  `store_id` text NOT NULL,
  `active` int(11) NOT NULL,
  `created_at` text NOT NULL,
  `origin` text NOT NULL,
  `seen` int(11) NOT NULL,
  PRIMARY KEY (`message_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_partners_transaction`
--

DROP TABLE IF EXISTS `loc_partners_transaction`;
CREATE TABLE IF NOT EXISTS `loc_partners_transaction` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `arrid` int(11) NOT NULL,
  `bankname` varchar(255) NOT NULL,
  `date_modified` text NOT NULL,
  `crew_id` varchar(55) NOT NULL,
  `store_id` varchar(55) NOT NULL,
  `guid` varchar(255) NOT NULL,
  `active` int(2) NOT NULL,
  `synced` varchar(55) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_pending_orders`
--

DROP TABLE IF EXISTS `loc_pending_orders`;
CREATE TABLE IF NOT EXISTS `loc_pending_orders` (
  `order_id` int(11) NOT NULL AUTO_INCREMENT,
  `crew_id` varchar(50) NOT NULL,
  `customer_name` varchar(50) NOT NULL,
  `product_name` varchar(50) NOT NULL,
  `product_quantity` int(50) NOT NULL,
  `product_price` int(50) NOT NULL,
  `product_total` int(50) NOT NULL,
  `product_id` int(11) NOT NULL,
  `product_sku` varchar(255) NOT NULL,
  `product_category` varchar(255) NOT NULL,
  `product_addon_id` int(11) NOT NULL,
  `created_at` text NOT NULL,
  `guid` varchar(50) NOT NULL,
  `active` int(11) NOT NULL,
  `increment` varchar(11) NOT NULL,
  `ColumnSumID` text NOT NULL,
  `ColumnInvID` int(11) NOT NULL,
  `Upgrade` int(11) NOT NULL,
  `Origin` text NOT NULL,
  `addontype` text NOT NULL,
  PRIMARY KEY (`order_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_pos_inventory`
--

DROP TABLE IF EXISTS `loc_pos_inventory`;
CREATE TABLE IF NOT EXISTS `loc_pos_inventory` (
  `inventory_id` int(11) NOT NULL AUTO_INCREMENT,
  `store_id` varchar(11) NOT NULL,
  `formula_id` int(11) NOT NULL,
  `product_ingredients` varchar(255) NOT NULL,
  `sku` varchar(255) NOT NULL,
  `stock_primary` double NOT NULL,
  `stock_secondary` double NOT NULL,
  `stock_no_of_servings` double NOT NULL,
  `stock_status` int(11) NOT NULL,
  `critical_limit` int(11) NOT NULL,
  `guid` varchar(255) NOT NULL,
  `date_modified` text NOT NULL,
  `crew_id` varchar(50) NOT NULL,
  `synced` varchar(255) NOT NULL,
  `server_date_modified` text NOT NULL,
  `server_inventory_id` int(11) NOT NULL,
  `main_inventory_id` int(11) NOT NULL,
  `origin` text NOT NULL,
  `zreading` text NOT NULL,
  `show_stockin` int(11) NOT NULL,
  PRIMARY KEY (`inventory_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_price_request_change`
--

DROP TABLE IF EXISTS `loc_price_request_change`;
CREATE TABLE IF NOT EXISTS `loc_price_request_change` (
  `request_id` int(11) NOT NULL AUTO_INCREMENT,
  `store_name` text NOT NULL,
  `server_product_id` text NOT NULL,
  `request_price` text NOT NULL,
  `created_at` text NOT NULL,
  `active` text NOT NULL,
  `store_id` text NOT NULL,
  `crew_id` text NOT NULL,
  `guid` text NOT NULL,
  `synced` text NOT NULL,
  PRIMARY KEY (`request_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_product_formula`
--

DROP TABLE IF EXISTS `loc_product_formula`;
CREATE TABLE IF NOT EXISTS `loc_product_formula` (
  `formula_id` int(11) NOT NULL AUTO_INCREMENT,
  `product_ingredients` varchar(255) NOT NULL,
  `primary_unit` varchar(50) NOT NULL,
  `primary_value` varchar(50) NOT NULL,
  `secondary_unit` varchar(50) NOT NULL,
  `secondary_value` varchar(50) NOT NULL,
  `serving_unit` varchar(50) NOT NULL,
  `serving_value` varchar(50) NOT NULL,
  `no_servings` varchar(250) NOT NULL,
  `status` int(2) NOT NULL,
  `date_modified` text NOT NULL,
  `unit_cost` decimal(11,2) NOT NULL,
  `store_id` varchar(50) NOT NULL,
  `guid` varchar(255) NOT NULL,
  `crew_id` varchar(50) NOT NULL,
  `origin` varchar(255) NOT NULL,
  `server_formula_id` int(11) NOT NULL,
  `server_date_modified` text NOT NULL,
  PRIMARY KEY (`formula_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_receipt`
--

DROP TABLE IF EXISTS `loc_receipt`;
CREATE TABLE IF NOT EXISTS `loc_receipt` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `type` text DEFAULT NULL,
  `description` text NOT NULL,
  `created_by` text NOT NULL,
  `created_at` text NOT NULL,
  `updated_by` text DEFAULT NULL,
  `updated_at` text DEFAULT NULL,
  `status` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_refund_return_details`
--

DROP TABLE IF EXISTS `loc_refund_return_details`;
CREATE TABLE IF NOT EXISTS `loc_refund_return_details` (
  `refret_id` int(11) NOT NULL AUTO_INCREMENT,
  `transaction_number` varchar(255) NOT NULL,
  `crew_id` varchar(50) NOT NULL,
  `reason` text NOT NULL,
  `total` decimal(11,2) NOT NULL,
  `guid` varchar(255) NOT NULL,
  `store_id` int(11) NOT NULL,
  `created_at` text NOT NULL,
  `zreading` text NOT NULL,
  `synced` varchar(255) NOT NULL,
  PRIMARY KEY (`refret_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_script_runner`
--

DROP TABLE IF EXISTS `loc_script_runner`;
CREATE TABLE IF NOT EXISTS `loc_script_runner` (
  `script_id` int(11) NOT NULL AUTO_INCREMENT,
  `script_command` text NOT NULL,
  `created_at` text NOT NULL,
  `active` text NOT NULL,
  PRIMARY KEY (`script_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_send_bug_report`
--

DROP TABLE IF EXISTS `loc_send_bug_report`;
CREATE TABLE IF NOT EXISTS `loc_send_bug_report` (
  `bug_id` int(11) NOT NULL AUTO_INCREMENT,
  `bug_desc` text NOT NULL,
  `crew_id` text NOT NULL,
  `guid` text NOT NULL,
  `store_id` text NOT NULL,
  `date_created` text NOT NULL,
  `synced` text NOT NULL DEFAULT 'Unsynced',
  PRIMARY KEY (`bug_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_senior_details`
--

DROP TABLE IF EXISTS `loc_senior_details`;
CREATE TABLE IF NOT EXISTS `loc_senior_details` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `transaction_number` text NOT NULL,
  `senior_id` text NOT NULL,
  `senior_name` text NOT NULL,
  `active` text NOT NULL,
  `crew_id` text NOT NULL,
  `store_id` text NOT NULL,
  `guid` text NOT NULL,
  `date_created` text NOT NULL,
  `totalguest` text NOT NULL,
  `totalid` text NOT NULL,
  `phone_number` text NOT NULL,
  `synced` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_settings`
--

DROP TABLE IF EXISTS `loc_settings`;
CREATE TABLE IF NOT EXISTS `loc_settings` (
  `settings_id` int(11) NOT NULL AUTO_INCREMENT,
  `C_Server` varchar(255) NOT NULL,
  `C_Username` varchar(255) NOT NULL,
  `C_Password` varchar(255) NOT NULL,
  `C_Database` varchar(255) NOT NULL,
  `C_Port` varchar(255) NOT NULL,
  `A_Export_Path` text NOT NULL,
  `A_Tax` text NOT NULL,
  `A_SIFormat` text NOT NULL,
  `A_SIBeg` text NOT NULL DEFAULT 0,
  `A_Terminal_No` text NOT NULL,
  `A_ZeroRated` text NOT NULL,
  `Dev_Company_Name` text NOT NULL,
  `Dev_Address` text NOT NULL,
  `Dev_Tin` text NOT NULL,
  `Dev_Accr_No` text NOT NULL,
  `Dev_Accr_Date_Issued` text NOT NULL,
  `Dev_Accr_Valid_Until` text NOT NULL,
  `Dev_PTU_No` text NOT NULL,
  `Dev_PTU_Date_Issued` text NOT NULL,
  `Dev_PTU_Valid_Until` text NOT NULL,
  `S_Zreading` text NOT NULL,
  `S_BackupInterval` text NOT NULL,
  `S_BackupDate` text NOT NULL,
  `S_Batter` text NOT NULL,
  `S_Brownie_Mix` text NOT NULL,
  `S_Upgrade_Price_Add` text NOT NULL,
  `S_Waffle_Bag` text NOT NULL,
  `S_Packets` text NOT NULL,
  `S_Update_Version` text NOT NULL,
  `P_Footer_Info` text NOT NULL,
  `S_logo` longtext NOT NULL,
  `S_Layout` text NOT NULL,
  `printreceipt` text NOT NULL,
  `reprintreceipt` text NOT NULL,
  `printxzread` text NOT NULL,
  `printreturns` text NOT NULL,
  `printsalesreport` text NOT NULL,
  `S_DateModified` text NOT NULL,
  `autoresetinv` text NOT NULL,
  `printcount` int(11) NOT NULL DEFAULT 2,
  `S_Old_Grand_Total` double NOT NULL,
  `S_SI_No` int(11) NOT NULL,
  `S_Trn_No` int(11) NOT NULL,
  `S_ZeroRated_Tax` text NOT NULL,
  PRIMARY KEY (`settings_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_stockadjustment_cat`
--

DROP TABLE IF EXISTS `loc_stockadjustment_cat`;
CREATE TABLE IF NOT EXISTS `loc_stockadjustment_cat` (
  `adj_id` int(11) NOT NULL AUTO_INCREMENT,
  `adj_type` text NOT NULL,
  `created_at` text NOT NULL,
  `active` text NOT NULL,
  PRIMARY KEY (`adj_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_system_logs`
--

DROP TABLE IF EXISTS `loc_system_logs`;
CREATE TABLE IF NOT EXISTS `loc_system_logs` (
  `log_id` int(11) NOT NULL AUTO_INCREMENT,
  `crew_id` varchar(50) NOT NULL,
  `log_type` varchar(255) NOT NULL,
  `log_description` text NOT NULL,
  `log_date_time` text NOT NULL,
  `log_store` varchar(20) NOT NULL,
  `guid` varchar(255) NOT NULL,
  `loc_systemlog_id` varchar(255) NOT NULL,
  `zreading` varchar(255) NOT NULL,
  `synced` varchar(255) NOT NULL,
  PRIMARY KEY (`log_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_transaction_mode_details`
--

DROP TABLE IF EXISTS `loc_transaction_mode_details`;
CREATE TABLE IF NOT EXISTS `loc_transaction_mode_details` (
  `mode_id` int(11) NOT NULL AUTO_INCREMENT,
  `transaction_type` varchar(255) NOT NULL,
  `transaction_number` varchar(255) NOT NULL,
  `fullname` varchar(255) NOT NULL,
  `reference` varchar(255) NOT NULL,
  `markup` varchar(255) NOT NULL,
  `created_at` text NOT NULL,
  `status` tinyint(4) NOT NULL,
  `store_id` varchar(255) NOT NULL,
  `guid` varchar(255) NOT NULL,
  `synced` varchar(50) NOT NULL,
  PRIMARY KEY (`mode_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_transfer_data`
--

DROP TABLE IF EXISTS `loc_transfer_data`;
CREATE TABLE IF NOT EXISTS `loc_transfer_data` (
  `transfer_id` int(11) NOT NULL AUTO_INCREMENT,
  `transfer_cat` text NOT NULL,
  `crew_id` text NOT NULL,
  `created_at` text NOT NULL,
  `created_by` text NOT NULL,
  `updated_at` text NOT NULL,
  `active` int(11) NOT NULL,
  PRIMARY KEY (`transfer_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_users`
--

DROP TABLE IF EXISTS `loc_users`;
CREATE TABLE IF NOT EXISTS `loc_users` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_level` varchar(100) NOT NULL,
  `full_name` varchar(255) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `contact_number` varchar(20) NOT NULL,
  `email` varchar(255) NOT NULL,
  `position` varchar(100) NOT NULL,
  `gender` varchar(20) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `active` varchar(2) NOT NULL,
  `guid` varchar(50) NOT NULL,
  `store_id` varchar(11) NOT NULL,
  `uniq_id` varchar(50) NOT NULL,
  `synced` varchar(255) NOT NULL,
  `user_code` text NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_xml_ref`
--

DROP TABLE IF EXISTS `loc_xml_ref`;
CREATE TABLE IF NOT EXISTS `loc_xml_ref` (
  `xml_id` int(11) NOT NULL AUTO_INCREMENT,
  `xml_name` text NOT NULL,
  `zreading` text NOT NULL,
  `created_by` text NOT NULL,
  `created_at` text NOT NULL,
  `status` text NOT NULL,
  PRIMARY KEY (`xml_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loc_zread_inventory`
--

DROP TABLE IF EXISTS `loc_zread_inventory`;
CREATE TABLE IF NOT EXISTS `loc_zread_inventory` (
  `zreadinv_id` int(11) NOT NULL AUTO_INCREMENT,
  `inventory_id` int(11) NOT NULL,
  `store_id` varchar(11) NOT NULL,
  `formula_id` int(11) NOT NULL,
  `product_ingredients` varchar(255) NOT NULL,
  `sku` varchar(255) NOT NULL,
  `stock_primary` double NOT NULL,
  `stock_secondary` double NOT NULL,
  `stock_no_of_servings` double NOT NULL,
  `stock_status` int(11) NOT NULL,
  `critical_limit` int(11) NOT NULL,
  `guid` varchar(255) NOT NULL,
  `created_at` text NOT NULL,
  `crew_id` varchar(50) NOT NULL,
  `synced` varchar(255) NOT NULL,
  `server_date_modified` text NOT NULL,
  `server_inventory_id` int(11) NOT NULL,
  `zreading` text NOT NULL,
  PRIMARY KEY (`zreadinv_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `loc_zread_table`
--

DROP TABLE IF EXISTS `loc_zread_table`;
CREATE TABLE IF NOT EXISTS `loc_zread_table` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ZXBegSiNo` text NOT NULL,
  `ZXEndSINo` text NOT NULL,
  `ZXBegTransNo` text NOT NULL,
  `ZXEndTransNo` text NOT NULL,
  `ZXBegBalance` text NOT NULL,
  `ZXCashTotal` double NOT NULL,
  `ZXGross` double NOT NULL,
  `ZXLessVat` double NOT NULL,
  `ZXLessVatDiplomat` double NOT NULL,
  `ZXLessVatOthers` double NOT NULL,
  `ZXAdditionalVat` double NOT NULL,
  `ZXVatAmount` double NOT NULL,
  `ZXLocalGovTax` double NOT NULL,
  `ZXVatableSales` double NOT NULL,
  `ZXZeroRatedSales` double NOT NULL,
  `ZXDailySales` double NOT NULL,
  `ZXNetSales` double NOT NULL,
  `ZXCashlessTotal` double NOT NULL,
  `ZXGcash` double NOT NULL,
  `ZXPaymaya` double NOT NULL,
  `ZXGrabFood` double NOT NULL,
  `ZXFoodPanda` double NOT NULL,
  `ZXShopeePay` double NOT NULL,
  `ZXCashlessOthers` double NOT NULL,
  `ZXRepExpense` double NOT NULL,
  `ZXCreditCard` double NOT NULL,
  `ZXDebitCard` double NOT NULL,
  `ZXMiscCheques` double NOT NULL,
  `ZXGiftCard` double NOT NULL,
  `ZXGiftCardSum` double NOT NULL,
  `ZXAR` double NOT NULL,
  `ZXTotalExpenses` double NOT NULL,
  `ZXCardOthers` double NOT NULL,
  `ZXDeposits` double NOT NULL,
  `ZXCashInDrawer` double NOT NULL,
  `ZXTotalDiscounts` double NOT NULL,
  `ZXSeniorCitizen` double NOT NULL,
  `ZXPWD` double NOT NULL,
  `ZXAthlete` double NOT NULL,
  `ZXSingleParent` double NOT NULL,
  `ZXItemVoidEC` double NOT NULL,
  `ZXTransactionVoid` double NOT NULL,
  `ZXTransactionCancel` double NOT NULL,
  `ZXTakeOutCharge` double NOT NULL,
  `ZXDeliveryCharge` double NOT NULL,
  `ZXReturnsExchange` double NOT NULL,
  `ZXReturnsRefund` double NOT NULL,
  `ZXTotalQTYSold` double NOT NULL,
  `ZXTotalTransactionCount` double NOT NULL,
  `ZXTotalGuess` double NOT NULL,
  `ZXCurrentTotalSales` double NOT NULL,
  `ZXOldGrandTotalSales` double NOT NULL,
  `ZXNewGrandtotalSales` double NOT NULL,
  `ZXSimplyPerfect` double NOT NULL,
  `ZXPerfectCombination` double NOT NULL,
  `ZXSavoury` double NOT NULL,
  `ZXCombo` double NOT NULL,
  `ZXFamousBlends` double NOT NULL,
  `ZXAddOns` double NOT NULL,
  `ZXThousandQty` int(11) NOT NULL,
  `ZXFiveHundredQty` int(11) NOT NULL,
  `ZXTwoHundredQty` int(11) NOT NULL,
  `ZXOneHundredQty` int(11) NOT NULL,
  `ZXFiftyQty` int(11) NOT NULL,
  `ZXTwentyQty` int(11) NOT NULL,
  `ZXTenQty` int(11) NOT NULL,
  `ZXFiveQty` int(11) NOT NULL,
  `ZXOneQty` int(11) NOT NULL,
  `ZXPointTwentyFiveQty` int(11) NOT NULL,
  `ZXPointFiveQty` int(11) NOT NULL,
  `ZXThousandTotal` double NOT NULL,
  `ZXFiveHundredTotal` double NOT NULL,
  `ZXTwoHundredTotal` double NOT NULL,
  `ZXOneHundredTotal` double NOT NULL,
  `ZXFiftyTotal` double NOT NULL,
  `ZXTwentyTotal` double NOT NULL,
  `ZXTenTotal` double NOT NULL,
  `ZXFiveTotal` double NOT NULL,
  `ZXOneTotal` double NOT NULL,
  `ZXPointTwentyFiveTotal` double NOT NULL,
  `ZXPointFiveTotal` double NOT NULL,
  `ZXdate` text NOT NULL,
  `created_by` text NOT NULL,
  `status` text NOT NULL,
  `ZXVatExemptSales` double NOT NULL,
  `ZXPremium` double NOT NULL,
  `ZXReprintCount` int(11) NOT NULL,
  `ZXLessDiscVE` double NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tbcountertable`
--

DROP TABLE IF EXISTS `tbcountertable`;
CREATE TABLE IF NOT EXISTS `tbcountertable` (
  `counter_id` int(11) NOT NULL AUTO_INCREMENT,
  `counter_value` text DEFAULT NULL,
  `date_created` text DEFAULT NULL,
  PRIMARY KEY (`counter_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tbcoupon`
--

DROP TABLE IF EXISTS `tbcoupon`;
CREATE TABLE IF NOT EXISTS `tbcoupon` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Couponname_` text NOT NULL,
  `Desc_` text NOT NULL,
  `Discountvalue_` text NOT NULL,
  `Referencevalue_` text NOT NULL,
  `Type` text NOT NULL,
  `Bundlebase_` text NOT NULL,
  `BBValue_` text NOT NULL,
  `Bundlepromo_` text NOT NULL,
  `BPValue_` text NOT NULL,
  `Effectivedate` text NOT NULL,
  `Expirydate` text NOT NULL,
  `active` text NOT NULL,
  `store_id` text NOT NULL,
  `crew_id` text NOT NULL,
  `guid` text NOT NULL,
  `origin` text NOT NULL,
  `synced` text NOT NULL,
  `date_created` text NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `triggers_loc_admin_products`
--

DROP TABLE IF EXISTS `triggers_loc_admin_products`;
CREATE TABLE IF NOT EXISTS `triggers_loc_admin_products` (
  `product_id` int(11) NOT NULL AUTO_INCREMENT,
  `product_sku` varchar(50) NOT NULL,
  `product_name` varchar(50) NOT NULL,
  `formula_id` varchar(255) NOT NULL,
  `product_barcode` varchar(255) NOT NULL,
  `product_category` varchar(255) NOT NULL,
  `product_price` int(255) NOT NULL,
  `product_desc` varchar(255) NOT NULL,
  `product_image` longtext NOT NULL,
  `product_status` varchar(2) NOT NULL,
  `origin` varchar(50) NOT NULL,
  `date_modified` text NOT NULL,
  `guid` varchar(255) NOT NULL,
  `ip_address` varchar(20) NOT NULL,
  `store_id` int(11) NOT NULL,
  `crew_id` varchar(50) NOT NULL,
  `synced` varchar(50) NOT NULL,
  PRIMARY KEY (`product_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Triggers `triggers_loc_admin_products`
--
DROP TRIGGER IF EXISTS `Copy_To_Loc_admin_products`;
DELIMITER $$
CREATE TRIGGER `Copy_To_Loc_admin_products` AFTER INSERT ON `triggers_loc_admin_products` FOR EACH ROW INSERT INTO loc_admin_products(`product_sku`, `product_name`, `formula_id`, `product_barcode`, `product_category`, `product_price`, `product_desc`, `product_image`, `product_status`, `origin`, `date_modified`, `guid`, `ip_address`, `store_id`, `crew_id`, `synced`)
SELECT `product_sku`, `product_name`, `formula_id`, `product_barcode`, `product_category`, `product_price`, `product_desc`, `product_image`, `product_status`, `origin`, `date_modified`, `guid`, `ip_address`, `store_id`, `crew_id`, `synced`
  FROM triggers_loc_admin_products
 WHERE NOT EXISTS(SELECT `product_sku`, `product_name`, `formula_id`, `product_barcode`, `product_category`, `product_price`, `product_desc`, `product_image`, `product_status`, `origin`, `date_modified`, `guid`, `ip_address`, `store_id`, `crew_id`, `synced`
                    FROM loc_admin_products
                   WHERE loc_admin_products.product_sku = triggers_loc_admin_products.product_sku )
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `triggers_loc_users`
--

DROP TABLE IF EXISTS `triggers_loc_users`;
CREATE TABLE IF NOT EXISTS `triggers_loc_users` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_level` varchar(100) NOT NULL,
  `full_name` varchar(255) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `contact_number` varchar(20) NOT NULL,
  `email` varchar(255) NOT NULL,
  `position` varchar(100) NOT NULL,
  `gender` varchar(20) NOT NULL,
  `created_at` text NOT NULL,
  `updated_at` text NOT NULL,
  `active` varchar(2) NOT NULL,
  `guid` varchar(50) NOT NULL,
  `store_id` varchar(11) NOT NULL,
  `uniq_id` varchar(50) NOT NULL,
  `synced` varchar(255) NOT NULL,
  `user_code` text NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Triggers `triggers_loc_users`
--
DROP TRIGGER IF EXISTS `Copy_To_Loc_Users`;
DELIMITER $$
CREATE TRIGGER `Copy_To_Loc_Users` AFTER INSERT ON `triggers_loc_users` FOR EACH ROW INSERT INTO loc_users(`user_level`, `full_name`, `username`, `password`, `contact_number`, `email`, `position`, `gender`, `created_at`, `updated_at`, `active`, `guid`, `store_id`, `uniq_id`, `user_code`) SELECT `user_level`, `full_name`, `username`, `password`, `contact_number`, `email`, `position`, `gender`, `created_at`, `updated_at`, `active`, `guid`, `store_id`, `uniq_id`, `user_code` FROM Triggers_loc_users WHERE NOT EXISTS(SELECT `user_level`, `full_name`, `username`, `password`, `contact_number`, `email`, `position`, `gender`, `created_at`, `updated_at`, `active`, `guid`, `store_id`, `uniq_id`, `user_code` FROM loc_users WHERE loc_users.uniq_id = Triggers_loc_users.uniq_id )
$$
DELIMITER ;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
