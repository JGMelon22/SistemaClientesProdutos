-- Table clients
CREATE TABLE clients
(
    id        INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name      VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email     VARCHAR(50) NOT NULL,
    active    NUMBER      NOT NULL -- To use boolean FROM ASP NET
);

-- Table products
CREATE TABLE products
(
    id     INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name   VARCHAR(100) NOT NULL,
    value  FLOAT        NOT NULL,
    active NUMBER       NOT NULL -- To use boolean FROM ASP NET
);

-- Table clients_products
CREATE TABLE CLIENTS_PRODUCTS
(
    id_client INT,
    id_product INT,
    CONSTRAINT fk_client FOREIGN KEY(id_client)
        REFERENCES clients(id),
    CONSTRAINT fk_product FOREIGN KEY(id_product)
        REFERENCES products(id)
);