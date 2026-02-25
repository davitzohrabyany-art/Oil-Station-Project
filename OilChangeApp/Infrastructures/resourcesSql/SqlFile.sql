CREATE TABLE  IF NOT EXISTS `user` (
  `id` int AUTO_INCREMENT PRIMARY KEY,
  `telegram_id` bigint UNIQUE NOT NULL,
  `phone` varchar(40)
);

CREATE TABLE  IF NOT EXISTS `car` (
  `id` int  AUTO_INCREMENT PRIMARY KEY,
  `car_num` varchar(17),
  `car_name` varchar(34),
  `password` varchar(255),
  `oil_type` varchar(255)
);

CREATE TABLE  IF NOT EXISTS `user_car` (
  `user_id` int,
  `car_id` int,
   PRIMARY KEY(user_id, car_id) 
);

CREATE TABLE  IF NOT EXISTS `admin` (
  `id` int AUTO_INCREMENT PRIMARY KEY,
  `nickname` varchar(255),
  `password` varchar(255),
  `email` varchar(255)
);

CREATE TABLE  IF NOT EXISTS `service_visit` (
  `id` int PRIMARY KEY,
  `car_id` int,
  `visit_date` datetime
);

CREATE TABLE  IF NOT EXISTS `oil_change` (
  `id` int PRIMARY KEY,
  `service_id` int,
  `oil_name` varchar(255),
  `oil_liters` decimal,
  `next_change_date` datetime,
  `next_change_km` decimal,
  `oil_location` varchar(50)
);

ALTER TABLE `user_car` ADD FOREIGN KEY (`user_id`) REFERENCES `user` (`id`);

ALTER TABLE `user_car` ADD FOREIGN KEY (`car_id`) REFERENCES `car` (`id`);

ALTER TABLE `service_visit` ADD FOREIGN KEY (`car_id`) REFERENCES `car` (`id`);

ALTER TABLE `oil_change` ADD FOREIGN KEY (`service_id`) REFERENCES `service_visit` (`id`);

-- Users
INSERT INTO `user` (id, telegram_id, phone) VALUES
                                                (1, 1234567890, '+37491123456'),
                                                (2, 9876543210, '+37499111222');

-- Cars
INSERT INTO `car` (id, car_num, car_name, oil_type) VALUES
                                                        (1, '34AB123', 'Toyota Corolla', '5W-30'),
                                                        (2, '56CD456', 'BMW X5', '0W-40'),
                                                        (3, '78EF789', 'Mercedes C200', '5W-40');

-- User-Car relationships
INSERT INTO `user_car` (user_id, car_id) VALUES
                                             (1, 1),
                                             (1, 2),
                                             (2, 3);

-- Admins
INSERT INTO `admin` (id, nickname, password, email) VALUES
                                                        (1, 'superadmin', 'hashed_password_123', 'admin@example.com'),
                                                        (2, 'manager', 'hashed_password_456', 'manager@example.com');

-- Service Visits
INSERT INTO `service_visit` (id, car_id, visit_date) VALUES
                                                         (1, 1, '2025-01-15 10:30:00'),
                                                         (2, 2, '2025-02-01 14:00:00'),
                                                         (3, 3, '2025-02-10 09:00:00');

-- Oil Changes
INSERT INTO `oil_change` (id, service_id, oil_name, oil_liters, next_change_date, next_change_km, oil_location) VALUES
                                                                                                                    (1, 1, 'Castrol GTX 5W-30', 4.5, '2025-07-15 10:30:00', 15000, 'Engine'),
                                                                                                                    (2, 2, 'Mobil 1 0W-40', 6.0, '2025-08-01 14:00:00', 12000, 'Engine'),
                                                                                                                    (3, 3, 'Shell Helix 5W-40', 5.0, '2025-08-10 09:00:00', 10000, 'Engine');
