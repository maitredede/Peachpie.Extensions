<?php
$pdo = new PDO("sqlite::memory:");
$pdo->exec("CREATE TABLE data (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, value VARCHAR(100) NULL);");
echo "ok";