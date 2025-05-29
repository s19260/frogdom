<?php
header("Content-Type: application/json");

$servername = "127.0.0.1";
$username = "serf";
$password = "serf123";
$dbname = "frogdom_db";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die(json_encode(["status" => "error", "message" => "Connection failed: " . $conn->connect_error]));
}

$response = ["status" => "error", "message" => "Invalid request"];

try {
    if (isset($_POST['death_count'])) {
        $death_count = intval($_POST['death_count']);
        $scene_name = isset($_POST['scene_name']) ? $_POST['scene_name'] : null;
        if ($death_count <= 0) throw new Exception("Invalid death count");

        $stmt = $conn->prepare("INSERT INTO player_deaths (death_count, scene_name) VALUES (?, ?)");
        $stmt->bind_param("is", $death_count, $scene_name);
        $stmt->execute();
        $response = ["status" => "success", "affected_rows" => $stmt->affected_rows];
        $stmt->close();
    }
    elseif (isset($_POST['input_type']) && isset($_POST['input_value'])) {
        $input_type = $conn->real_escape_string($_POST['input_type']);
        $input_value = $conn->real_escape_string($_POST['input_value']);

        $stmt = $conn->prepare("INSERT INTO player_inputs (input_type, input_value) VALUES (?, ?)");
        $stmt->bind_param("ss", $input_type, $input_value);
        $stmt->execute();
        $response = ["status" => "success", "affected_rows" => $stmt->affected_rows];
        $stmt->close();
    }
} catch (Exception $e) {
    $response["message"] = $e->getMessage();
}

$conn->close();
echo json_encode($response);
?>
