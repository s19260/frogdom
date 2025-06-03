<?php
header("Content-Type: application/json");
header("Access-Control-Allow-Origin: *"); // Add CORS header for Unity

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
    // ADD THIS NEW CONDITION FOR USERNAME
    if (isset($_POST['username'])) {
        $username = $conn->real_escape_string($_POST['username']);

        if(empty($username)) {
            throw new Exception("Username cannot be empty");
        }

        $stmt = $conn->prepare("INSERT INTO users (username) VALUES (?)");
        $stmt->bind_param("s", $username);
        $stmt->execute();

        if($stmt->affected_rows > 0) {
            $response = ["status" => "success", "message" => "Username saved"];
        } else {
            $response = ["status" => "error", "message" => "Username already exists"];
        }
        $stmt->close();
    }
    // Existing conditions remain unchanged...
    elseif (isset($_POST['completion_time']) && isset($_POST['scene_name'])) {
        // ... existing code ...
    }
    // ... other existing conditions ...

} catch (Exception $e) {
    $response["message"] = $e->getMessage();
}

$conn->close();
echo json_encode($response);
?>
