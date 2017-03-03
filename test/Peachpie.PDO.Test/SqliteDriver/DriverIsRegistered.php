<?php
$drivers = PDO::getAvailableDrivers();
if(in_array($drivers, "sqlite"))
{
    echo "ok";
}
else
{
    echo "ERR: driver not registered";
}