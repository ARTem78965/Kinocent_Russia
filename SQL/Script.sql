create database CenterCinema;
use CenterCinema;

show tables;

--
-- База данных: `CinemaCenter`
--

DELIMITER $$
--
-- Процедуры
--
CREATE PROCEDURE `avg_reiting` ()  NO SQL
select id as "Номер", nazv as "Название фильма", dlitelnost as "Длительность", date_start as "Начало", date_finish as "Конец", company_prodakshn as "Продакшн", reiting as "Рейтинг" from films where reiting > 2.5;

CREATE DEFINER=`root`@`localhost` PROCEDURE `bilets_S` ()  NO SQL
SELECT 
bilets.id AS "Номер", bilets.datetime AS "Время", films.nazv AS "Фильм", bilets.zal AS "Зал", mesta.id AS "Место" , bilets.price AS "Цена"
FROM bilets
INNER JOIN seans ON bilets.seans = seans.film
INNER JOIN films ON seans.film = films.id 
INNER JOIN mesta on bilets.mesta = mesta.id;

CREATE DEFINER=`root`@`localhost` PROCEDURE `dviz_bilets_S` ()  NO SQL
SELECT dviz_bilets.id AS "Номер", dviz_bilets.number AS "Номер билета", dviz_bilets.date AS "Дата", operacia.name AS "Операция", users.fio AS "Пользователь" FROM dviz_bilets INNER JOIN operacia ON dviz_bilets.operacia = operacia.id INNER JOIN users ON dviz_bilets.users = users.id;

CREATE DEFINER=`root`@`localhost` PROCEDURE `films_S` ()  NO SQL
select id as "Номер", nazv as "Название фильма", dlitelnost as "Длительность", date_start as "Начало", date_finish as "Конец", company_prodakshn as "Продакшн", reiting as "Рейтинг" from films;

CREATE DEFINER=`root`@`localhost` PROCEDURE `mesta_S` ()  NO SQL
SELECT id AS "Номер места" FROM mesta;

CREATE DEFINER=`root`@`localhost` PROCEDURE `operacia_S` ()  NO SQL
SELECT id AS "Номер операции", name AS "Тип операции" FROM operacia;

CREATE DEFINER=`root`@`localhost` PROCEDURE `role_S` ()  NO SQL
SELECT id AS "Номер", name AS "Привелегии" FROM role;

CREATE DEFINER=`root`@`localhost` PROCEDURE `seans_S` ()  NO SQL
SELECT seans.id AS "Номер", seans.zal AS "Зал", seans.date AS "Дата", seans.time AS "Время", films.nazv AS "Фильм" FROM seans  INNER JOIN films ON seans.film = films.id;

CREATE DEFINER=`root`@`localhost` PROCEDURE `users_S` ()  NO SQL
SELECT users.id AS "Номер", users.fio AS "ФИО", users.phone AS "Телефон", users.login AS "Логин", users.password AS "Пароль", role.name AS "Привелегии" FROM users INNER JOIN role ON users.role = role.id;

CREATE DEFINER=`root`@`localhost` PROCEDURE `zals_S` ()  NO SQL
SELECT id AS "Номер зала", kolvo_mest AS "Кол-во мест" FROM zals;

-- --------------------------------------------------------

--
-- Структура таблицы `bilets`
--

CREATE TABLE `bilets` (
  `id` int NOT NULL,
  `datetime` datetime NOT NULL,
  `seans` int NOT NULL,
  `zal` int NOT NULL,
  `mesta` int NOT NULL,
  `price` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Структура таблицы `dviz_bilets`
--

CREATE TABLE `dviz_bilets` (
  `id` int NOT NULL,
  `number` int NOT NULL,
  `date` datetime NOT NULL,
  `operacia` int NOT NULL,
  `users` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Структура таблицы `films`
--

CREATE TABLE `films` (
  `id` int NOT NULL,
  `nazv` varchar(255) NOT NULL,
  `dlitelnost` char(3) NOT NULL,
  `date_start` datetime NOT NULL,
  `date_finish` datetime NOT NULL,
  `company_prodakshn` varchar(255) NOT NULL,
  `reiting` char(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Структура таблицы `mesta`
--

CREATE TABLE `mesta` (
  `id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `mesta`
--

INSERT INTO `mesta` (`id`) VALUES
(1),
(2),
(3),
(4),
(5),
(6),
(7),
(8),
(9),
(10),
(11),
(12),
(13),
(14),
(15),
(16),
(17),
(18),
(19),
(20),
(21),
(22),
(23),
(24),
(25),
(26),
(27),
(28),
(29),
(30),
(31),
(32),
(33),
(34),
(35),
(36),
(37),
(38),
(39),
(40),
(41),
(42),
(43),
(44),
(45),
(46),
(47),
(48),
(49),
(50),
(51),
(52),
(53),
(54),
(55),
(56),
(57),
(58),
(59),
(60),
(61),
(62),
(63),
(64),
(65),
(66),
(67),
(68),
(69),
(70),
(71),
(72),
(73),
(74),
(75),
(76),
(77),
(78),
(79),
(80),
(81),
(82),
(83),
(84),
(85),
(86),
(87),
(88),
(89),
(90),
(91),
(92),
(93),
(94),
(95),
(96),
(97),
(98),
(99),
(100);

-- --------------------------------------------------------

--
-- Структура таблицы `operacia`
--

CREATE TABLE `operacia` (
  `id` int NOT NULL,
  `name` varchar(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `operacia`
--

INSERT INTO `operacia` (`id`, `name`) VALUES
(1, 'Наличные'),
(2, 'Безналичные');

-- --------------------------------------------------------

--
-- Структура таблицы `role`
--

CREATE TABLE `role` (
  `id` char(3) NOT NULL,
  `name` varchar(13) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `role`
--

INSERT INTO `role` (`id`, `name`) VALUES
('101', 'Администратор'),
('102', 'Сотрудник'),
('103', 'Клиент');

-- --------------------------------------------------------

--
-- Структура таблицы `seans`
--

CREATE TABLE `seans` (
  `id` int NOT NULL,
  `zal` int NOT NULL,
  `date` date NOT NULL,
  `time` time NOT NULL,
  `film` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Структура таблицы `users`
--

CREATE TABLE `users` (
  `id` int NOT NULL,
  `fio` varchar(255) NOT NULL,
  `phone` char(12) NOT NULL,
  `login` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `role` char(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Структура таблицы `zals`
--

CREATE TABLE `zals` (
  `id` int NOT NULL,
  `kolvo_mest` char(3) DEFAULT '100'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `zals`
--

INSERT INTO `zals` (`id`, `kolvo_mest`) VALUES
(1, '100'),
(2, '100'),
(3, '100');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `bilets`
--
ALTER TABLE `bilets`
  ADD PRIMARY KEY (`id`),
  ADD KEY `seans` (`seans`),
  ADD KEY `zal` (`zal`),
  ADD KEY `mesta` (`mesta`);

--
-- Индексы таблицы `dviz_bilets`
--
ALTER TABLE `dviz_bilets`
  ADD PRIMARY KEY (`id`),
  ADD KEY `number` (`number`),
  ADD KEY `operacia` (`operacia`),
  ADD KEY `users` (`users`);

--
-- Индексы таблицы `films`
--
ALTER TABLE `films`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `mesta`
--
ALTER TABLE `mesta`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `operacia`
--
ALTER TABLE `operacia`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `seans`
--
ALTER TABLE `seans`
  ADD PRIMARY KEY (`id`),
  ADD KEY `zal` (`zal`),
  ADD KEY `film` (`film`);

--
-- Индексы таблицы `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD KEY `role` (`role`);

--
-- Индексы таблицы `zals`
--
ALTER TABLE `zals`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `bilets`
--
ALTER TABLE `bilets`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `dviz_bilets`
--
ALTER TABLE `dviz_bilets`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `films`
--
ALTER TABLE `films`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `seans`
--
ALTER TABLE `seans`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `users`
--
ALTER TABLE `users`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `zals`
--
ALTER TABLE `zals`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `bilets`
--
ALTER TABLE `bilets`
  ADD CONSTRAINT `bilets_ibfk_1` FOREIGN KEY (`seans`) REFERENCES `seans` (`id`),
  ADD CONSTRAINT `bilets_ibfk_2` FOREIGN KEY (`mesta`) REFERENCES `mesta` (`id`);

--
-- Ограничения внешнего ключа таблицы `dviz_bilets`
--
ALTER TABLE `dviz_bilets`
  ADD CONSTRAINT `dviz_bilets_ibfk_1` FOREIGN KEY (`number`) REFERENCES `bilets` (`id`),
  ADD CONSTRAINT `dviz_bilets_ibfk_2` FOREIGN KEY (`operacia`) REFERENCES `operacia` (`id`),
  ADD CONSTRAINT `dviz_bilets_ibfk_3` FOREIGN KEY (`users`) REFERENCES `users` (`id`);

--
-- Ограничения внешнего ключа таблицы `seans`
--
ALTER TABLE `seans`
  ADD CONSTRAINT `seans_ibfk_1` FOREIGN KEY (`zal`) REFERENCES `zals` (`id`),
  ADD CONSTRAINT `seans_ibfk_2` FOREIGN KEY (`film`) REFERENCES `films` (`id`);

--
-- Ограничения внешнего ключа таблицы `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`role`) REFERENCES `role` (`id`);
COMMIT;

call avg_reiting();

call role_S();
call users_S();
call bilets_S();
call seans_S();
call zals_S();
call mesta_S();
call dviz_bilets_S();
call operacia_S();
call films_S();

select * from films;
select * from seans;
select * from bilets;
select * from dviz_bilets;
select* from zals;
select * from operacia;
select * from reiting;

create table reiting(
id int primary key auto_increment not null,
film varchar(255) not null,
reiting float not null
);

alter table reiting modify column comment varchar(255) null;

CREATE PROCEDURE `reiting_S` ()  NO SQL
select id as "Номер", film as "Название фильма", reiting as "Рейтинг", comment as "Отзыв" from reiting;

insert into reiting(film, reiting) values("Пираты", 4);
insert into reiting(film, reiting) values("Пираты", 5);

select count(id), sum(reiting) from reiting where film = 'Пираты';
select (sum(reiting)/count(id)) as "Средний рейтинг" from reiting where film = 'Пираты';

alter table films modify column reiting float null;

CREATE PROCEDURE `reiting_S` ()  NO SQL
select id as "Номер", film as "Название фильма", reiting as "Рейтинг", comment as "Отзыв" from reiting;

call reiting_S();