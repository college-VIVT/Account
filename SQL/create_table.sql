CREATE TABLE table_account
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    login    VARCHAR(20) NOT NULL,
    password VARCHAR(20) NOT NULL
);

CREATE TABLE table_user
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name  VARCHAR(75) NOT NULL,
    email      VARCHAR(50) NOT NULL,
    CONSTRAINT table_user_table_account_id_fk
        FOREIGN KEY (id) REFERENCES table_account (id)
);