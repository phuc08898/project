-- CreateTable
CREATE TABLE `AdverUnit` (
    `id` VARCHAR(191) NOT NULL,
    `name` VARCHAR(191) NOT NULL,
    `expiredBy` DATETIME(3) NOT NULL,
    `isActived` BOOLEAN NOT NULL DEFAULT true,
    `isDeleted` BOOLEAN NOT NULL DEFAULT false,

    PRIMARY KEY (`id`)
) DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- CreateTable
CREATE TABLE `Advertises` (
    `id` VARCHAR(191) NOT NULL,
    `name` VARCHAR(191) NOT NULL,
    `type` VARCHAR(191) NOT NULL,
    `pathUrl` VARCHAR(191) NOT NULL,
    `expiredBy` DATETIME(3) NOT NULL,
    `duration` INTEGER NOT NULL DEFAULT 0,
    `isActived` BOOLEAN NOT NULL DEFAULT true,
    `advUnitId` VARCHAR(191) NULL,

    PRIMARY KEY (`id`)
) DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- AddForeignKey
ALTER TABLE `Advertises` ADD CONSTRAINT `Advertises_advUnitId_fkey` FOREIGN KEY (`advUnitId`) REFERENCES `AdverUnit`(`id`) ON DELETE SET NULL ON UPDATE CASCADE;
