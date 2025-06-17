<?php
header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");
error_reporting(E_ALL);
ini_set('display_errors', 1);

$servername = "sql308.infinityfree.com";
$username = "if0_39254170";
$password = "xqwtRlVg64N2";
$dbname = "if0_39254170_frogdom";

// Connect to MySQL
$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) {
    http_response_code(500);
    echo json_encode(["status" => "error", "message" => "Connection failed: " . $conn->connect_error]);
    exit;
}

// Always initialize response
$response = ["status" => "error", "message" => "Invalid request"];

try {
    // Register user input
    if (isset($_POST['input_type'], $_POST['input_value'], $_POST['user_id'])) {
        $user_id = (int)$_POST['user_id'];
        $input_type = trim($_POST['input_type']);
        $input_value = trim($_POST['input_value']);

        if ($input_type === "" || $input_value === "") {
            throw new Exception("Input type or value cannot be empty");
        }

        $stmt = $conn->prepare("INSERT INTO player_inputs (user_id, input_type, input_value) VALUES (?, ?, ?)");
        $stmt->bind_param("iss", $user_id, $input_type, $input_value);
        $stmt->execute();
        $response = ["status" => "success", "affected_rows" => $stmt->affected_rows];
        $stmt->close();
    }
    // Register username
    elseif (isset($_POST['username'])) {
        $username = trim($_POST['username']);
        if ($username === "") {
            throw new Exception("Username cannot be empty");
        }

        $stmt = $conn->prepare("INSERT INTO users (username) VALUES (?)");
        $stmt->bind_param("s", $username);
        $stmt->execute();

        if ($stmt->affected_rows > 0) {
            $response = ["status" => "success", "message" => "Username saved", "user_id" => $stmt->insert_id];
        } else {
            $response = ["status" => "error", "message" => "Username already exists"];
        }
        $stmt->close();
    }
    // Register completion time
    elseif (isset($_POST['completion_time'], $_POST['scene_name'], $_POST['user_id'])) {
        $user_id = (int)$_POST['user_id'];
        $completion_time = floatval($_POST['completion_time']);
        $scene_name = trim($_POST['scene_name']);

        $stmt = $conn->prepare("INSERT INTO player_times (user_id, scene_name, completion_time) VALUES (?, ?, ?)");
        $stmt->bind_param("isd", $user_id, $scene_name, $completion_time);
        $stmt->execute();
        $response = ["status" => "success", "affected_rows" => $stmt->affected_rows];
        $stmt->close();
    }
    // Register death count
    elseif (isset($_POST['death_count'], $_POST['user_id'])) {
        $user_id = (int)$_POST['user_id'];
        $death_count = intval($_POST['death_count']);
        $scene_name = isset($_POST['scene_name']) ? trim($_POST['scene_name']) : null;

        $stmt = $conn->prepare("INSERT INTO player_deaths (user_id, death_count, scene_name) VALUES (?, ?, ?)");
        $stmt->bind_param("iis", $user_id, $death_count, $scene_name);
        $stmt->execute();
        $response = ["status" => "success", "affected_rows" => $stmt->affected_rows];
        $stmt->close();
    }
    // No valid parameters
    else {
        throw new Exception("Missing required parameters");
    }
} catch (Exception $e) {
    $response = ["status" => "error", "message" => $e->getMessage()];
}

$conn->close();
echo json_encode($response);
?>
