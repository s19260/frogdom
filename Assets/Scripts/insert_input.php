<?php
header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");
error_reporting(E_ALL);
ini_set('display_errors', 1);

$servername = "sql7.freesqldatabase.com:3306";
$username = "sql7785323";
$password = "5kb85XqgQB";
$dbname = "sql7785323";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die(json_encode(["status" => "error", "message" => "Connection failed: " . $conn->connect_error]));
}

$response = ["status" => "error", "message" => "Invalid request"];

try {
    if (isset($_POST['input_type'], $_POST['input_value'], $_POST['user_id'])) {
        $user_id = (int)$_POST['user_id'];
        $input_type = $conn->real_escape_string($_POST['input_type']);
        $input_value = $conn->real_escape_string($_POST['input_value']);

        if(empty($input_type) || empty($input_value)) {
            throw new Exception("Input type or value cannot be empty");
        }

        $stmt = $conn->prepare("INSERT INTO player_inputs (user_id, input_type, input_value) VALUES (?, ?, ?)");
        $stmt->bind_param("iss", $user_id, $input_type, $input_value);
        $stmt->execute();

        $response = ["status" => "success", "affected_rows" => $stmt->affected_rows];
        $stmt->close();
    }
    elseif (isset($_POST['username'])) {
        $username = $conn->real_escape_string($_POST['username']);

        if(empty($username)) {
            throw new Exception("Username cannot be empty");
        }

        $stmt = $conn->prepare("INSERT INTO users (username) VALUES (?)");
        $stmt->bind_param("s", $username);
        $stmt->execute();

        if($stmt->affected_rows > 0) {
            $new_user_id = $stmt->insert_id;
            $response = ["status" => "success", "message" => "Username saved", "user_id" => $new_user_id];
        } else {
            $response = ["status" => "error", "message" => "Username already exists"];
        }
        $stmt->close();
    }
    elseif (isset($_POST['completion_time'], $_POST['scene_name'], $_POST['user_id'])) {
        $user_id = (int)$_POST['user_id'];
        $completion_time = floatval($_POST['completion_time']);
        $scene_name = $conn->real_escape_string($_POST['scene_name']);

        $stmt = $conn->prepare("INSERT INTO player_times (user_id, scene_name, completion_time) VALUES (?, ?, ?)");
        $stmt->bind_param("isd", $user_id, $scene_name, $completion_time);
        $stmt->execute();

        $response = ["status" => "success", "affected_rows" => $stmt->affected_rows];
        $stmt->close();
    }
    elseif (isset($_POST['death_count'], $_POST['user_id'])) {
        $user_id = (int)$_POST['user_id'];
        $death_count = intval($_POST['death_count']);
        $scene_name = isset($_POST['scene_name']) ? $conn->real_escape_string($_POST['scene_name']) : null;

        $stmt = $conn->prepare("INSERT INTO player_deaths (user_id, death_count, scene_name) VALUES (?, ?, ?)");
        $stmt->bind_param("iis", $user_id, $death_count, $scene_name);
        $stmt->execute();

        $response = ["status" => "success", "affected_rows" => $stmt->affected_rows];
        $stmt->close();
    }
    else {
        throw new Exception("Missing required parameters");
    }

} catch (Exception $e) {
    $response["message"] = $e->getMessage();
    error_log("PHP Error: " . $e->getMessage());
}

$conn->close();
echo json_encode($response);
?>
