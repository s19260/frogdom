<?php
$servername = "127.0.0.1";
$username = "serf";
$password = "serf123";
$dbname = "frogdom_db";

$input_type = isset($_POST['input_type']) ? $_POST['input_type'] : '';
$input_value = isset($_POST['input_value']) ? $_POST['input_value'] : '';

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$stmt = $conn->prepare("INSERT INTO player_inputs (input_type, input_value) VALUES (?, ?)");
$stmt->bind_param("ss", $input_type, $input_value);

if ($stmt->execute()) {
    echo "Success";
} else {
    echo "Error: " . $stmt->error;
}

$stmt->close();
$conn->close();
?>
