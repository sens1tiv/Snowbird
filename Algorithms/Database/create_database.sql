CREATE DATABASE snowbird CHARACTER SET utf8;

USE snowbird;

    -- Users
CREATE TABLE `snowbird`.`users`( 
    `id` INT(9) NOT NULL AUTO_INCREMENT, 
    `email` VARCHAR(40), 
    `username` VARCHAR(20) NOT NULL, 
    `password` VARCHAR(256) NOT NULL, 
    `created_at` DATETIME NOT NULL, 
    PRIMARY KEY (`id`)
) ENGINE = INNODB;

ALTER TABLE users AUTO_INCREMENT=100000000; -- Making the IDs auto increment start with the lowest 9 digit number

    -- Wallets
CREATE TABLE `snowbird`.`wallets`( 
    `id` INT NOT NULL AUTO_INCREMENT, 
    `user_id` INT NOT NULL, 
    `type` INT NOT NULL DEFAULT '0', 
    `amount` INT NOT NULL DEFAULT '0', 
    `currency` VARCHAR(3) NOT NULL, 
    `account_name` VARCHAR(40), 
    `account_number` INT, 
    `description` VARCHAR(20), 
    `created_at` DATETIME NOT NULL, 
    PRIMARY KEY (`id`)
) ENGINE = INNODB;

ALTER TABLE wallets AUTO_INCREMENT=100000000; -- Making the IDs auto increment start with the lowest 9 digit number

    -- Transactions
CREATE TABLE `snowbird`.`transactions`(
    `id` INT(9) NOT NULL AUTO_INCREMENT,
    `wallet_id` INT(9) NOT NULL,
    `type` INT NOT NULL,
    `amount` INT NOT NULL,
    `fromWalletId` INT(9) NULL,
    `toWalletId` INT(9) NULL,
    `description` VARCHAR(20) NULL,
    PRIMARY KEY(`id`)
) ENGINE = INNODB;

ALTER TABLE transactions AUTO_INCREMENT=100000000; -- Making the IDs auto increment start with the lowest 9 digit number

    -- Currencies
CREATE TABLE `snowbird`.`currencies`(
    `from` VARCHAR(3) NOT NULL,
    `to` VARCHAR(3) NOT NULL,
    `multiplier` FLOAT NOT NULL,
    `updated` DATETIME NOT NULL
) ENGINE = INNODB;