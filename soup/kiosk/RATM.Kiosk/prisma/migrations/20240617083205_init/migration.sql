-- CreateTable
CREATE TABLE `Configs` (
    `key` VARCHAR(191) NOT NULL,
    `value` VARCHAR(255) NOT NULL,
    `desc` VARCHAR(191) NULL,

    PRIMARY KEY (`key`)
) DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
