<?php
$pdo = new PDO("sqlite::memory:");
$pdo->exec("CREATE TABLE data (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, value VARCHAR(100) NULL);");
$pdo->exec("INSERT INTO data (value) VALUES ('test')");
$id = $pdo->lastInsertId();
if($id === 1)
{
    echo "ok";
}
else
{
    echo "err : \$id === 1 is false";
}