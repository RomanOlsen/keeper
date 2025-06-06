CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture',
  cover_img VARCHAR(1000) COMMENT 'User Cover Image'

) default charset utf8mb4 COMMENT '';

CREATE TABLE keeps (
id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
created_at DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
name VARCHAR(255) NOT NULL,
description VARCHAR(1000) NOT NULL,
img VARCHAR(1000) NOT NULL,
views INT NOT NULL,
creator_id VARCHAR(255) NOT NULL,
FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE

);
SELECT * FROM vaults

CREATE TABLE vaults (
  id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img VARCHAR(1000) NOT NULL,
  is_private BOOLEAN DEFAULT FALSE NOT NULL,
  creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE

);

CREATE TABLE vault_keeps(
  id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP NOT NULL,
  keep_id int NOT NULL,
  vault_id int NOT NULL,
  creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (keep_id) REFERENCES keeps(id) ON DELETE CASCADE,
    FOREIGN KEY (vault_id) REFERENCES vaults(id) ON DELETE CASCADE,
    FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE



);

DROP TABLE accounts;

    SELECT vault_keeps.*, keeps.*, accounts.* FROM vault_keeps
    INNER JOIN keeps ON keeps.id = vault_keeps.keep_id
    INNER JOIN accounts ON accounts.id = vault_keeps.creator_id
    WHERE vault_keeps.vault_id = @vaultId;

-- unsure how to count them
SELECT * ,
COUNT (keeps.id) AS kept 
FROM vault_keeps
LEFT OUTER JOIN keeps ON keeps.id = vault_keeps.keep_id
GROUP BY vault_keeps.id;
WHERE vault_keeps.id = LAST_INSERT_ID();