<?php
$pdo = new PDO("sqlite::memory:");
$pdo->exec("VACUUM");
echo "ok";