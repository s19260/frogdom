<?php
header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *");

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
    // USERNAME REGISTRATION
    if (isset($_POST['username'])) {
        $username = $conn->real_escape_string($_POST['username']);

        if(empty($username)) {
            throw new Exception("Username cannot be empty");
        }

        $stmt = $conn->prepare("INSERT INTO users (username) VALUES (?)");
        $stmt->bind_param("s", $username);
        $stmt->execute();

        if($stmt->affected_rows > 0) {
            $new_user_id = $stmt->insert_id;
            $response = [
                "status" => "success",
                "message" => "Username saved",
                "user_id" => $new_user_id
            ];
        } else {
            $response = ["status" => "error", "message" => "Username already exists"];
        }
        $stmt->close();
    }
    // LEVEL COMPLETION TIME
    elseif (isset($_POST['completion_time']) && isset($_POST['scene_name']) && isset($_POST['user_id'])) {
        $user_id = intval($_POST['user_id']);
        $completion_time = floatval($_POST['completion_time']);
        $scene_name = $conn->real_escape_string($_POST['scene_name']);

        $stmt = $conn->prepare("INSERT INTO player_times (user_id, scene_name, completion_time) VALUES (?, ?, ?)");
        $stmt->bind_param("isd", $user_id, $scene_name, $completion_time);
        $stmt->execute();

        $response = ["status" => "success", "affected_rows" => $stmt->affected_rows];
        $stmt->close();
    }
    // DEATH COUNT
    elseif (isset($_POST['death_count']) && isset($_POST['user_id'])) {
        $user_id = intval($_POST['user_id']);
        $death_count = intval($_POST['death_count']);
        $scene_name = isset($_POST['scene_name']) ? $conn->real_escape_string($_POST['scene_name']) : null;

        if ($death_count <= 0) throw new Exception("Invalid death count");

        $stmt = $conn->prepare("INSERT INTO player_deaths (user_id, death_count, scene_name) VALUES (?, ?, ?)");
        $stmt->bind_param("iis", $user_id, $death_count, $scene_name);
        $stmt->execute();

        $response = ["status" => "success", "affected_rows" => $stmt->affected_rows];
        $stmt->close();
    }
    // INPUT EVENTS
    elseif (isset($_POST['input_type']) && isset($_POST['input_value']) && isset($_POST['user_id'])) {
        $user_id = intval($_POST['user_id']);
        $input_type = $conn->real_escape_string($_POST['input_type']);
        $input_value = $conn->real_escape_string($_POST['input_value']);

        $stmt = $conn->prepare("INSERT INTO player_inputs (user_id, input_type, input_value) VALUES (?, ?, ?)");
        $stmt->bind_param("iss", $user_id, $input_type, $input_value);
        $stmt->execute();

        $response = ["status" => "success", "affected_rows" => $stmt->affected_rows];
        $stmt->close();
    }
    else {
        throw new Exception("Missing required parameters");
    }

} catch (Exception $e) {
    $response["message"] = $e->getMessage();
    error_log("PHP Error: " . $e->getMessage()); // Log errors for debugging
}

$conn->close();
echo json_encode($response);
?>
