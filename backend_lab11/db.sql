CREATE TABLE authors (
    id SERIAL PRIMARY KEY,
    name VARCHAR(200) NOT NULL
);

CREATE TABLE publishers (
    id SERIAL PRIMARY KEY,
    name VARCHAR(200) NOT NULL
);

CREATE TABLE books (
    id SERIAL PRIMARY KEY,
    title VARCHAR(250) NOT NULL,
    year INTEGER NOT NULL,
    author_id INTEGER NOT NULL,
    publisher_id INTEGER NOT NULL,
    FOREIGN KEY (author_id) REFERENCES authors(id) ON DELETE CASCADE,
    FOREIGN KEY (publisher_id) REFERENCES publishers(id) ON DELETE CASCADE
);

INSERT INTO authors (name) VALUES
('Лев Толстой'),
('Фёдор Достоевский'),
('Александр Пушкин'),
('Михаил Булгаков'),
('Иван Тургенев');

INSERT INTO publishers (name) VALUES
('Эксмо'),
('АСТ'),
('Азбука'),
('Питер'),
('Просвещение');

INSERT INTO books (title, year, author_id, publisher_id) VALUES
('Война и мир', 1869, 1, 1),
('Анна Каренина', 1877, 1, 2),
('Преступление и наказание', 1866, 2, 2),
('Идиот', 1869, 2, 3),
('Евгений Онегин', 1833, 3, 1),
('Мастер и Маргарита', 1967, 4, 4),
('Отцы и дети', 1862, 5, 5);