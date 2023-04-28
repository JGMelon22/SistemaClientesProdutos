-- Table clients
CREATE TABLE clients
(
   id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
   name VARCHAR(50) NOT NULL,
   last_name VARCHAR(50) NOT NULL,
   email VARCHAR(50) NOT NULL,
   active NUMBER NOT NULL -- To use boolean FROM ASP NET
);

-- Table products
CREATE TABLE products
(
   id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
   name VARCHAR(100) NOT NULL,
   value FLOAT NOT NULL,
   active NUMBER NOT NULL -- To use boolean FROM ASP NET
);
