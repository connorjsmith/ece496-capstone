#include <WiFi.h>

const char* ssid     = "Swaminarayan";
const char* password = "master123";
const char* host = "pillbox-capstone.azurewebsites.net";
const char* gethost = "httpbin.org";

void setup()
{
    Serial.begin(115200);
    delay(10);
    pinMode(13, OUTPUT); //Declares LED output
    
    // WiFi Setup
    Serial.print("Connecting to ");
    Serial.println(ssid);
    WiFi.begin(ssid, password);

    while (WiFi.status() != WL_CONNECTED) {
        delay(500);
        Serial.print(".");
    }

    Serial.println("");
    Serial.println("WiFi connected");
    Serial.println("IP address: ");
    Serial.println(WiFi.localIP());
}

void loop()
{
    digitalWrite(13, HIGH);
    delay(500);
    digitalWrite(13, LOW);
    delay(500);
    digitalWrite(13, HIGH);
    delay(500);
    digitalWrite(13, LOW);
    delay(3500);
    
    Serial.print("connecting to ");
    Serial.println(host);

    // Use WiFiClient class to create TCP connections
    WiFiClient client;
    const int httpPort = 80;
    if (!client.connect(host, httpPort)) {
        Serial.println("connection failed");
        return;
    }

    /* POST TO HEARTBEAT */
    String url = "/api/Heartbeat";
    Serial.print("Requesting URL: ");
    Serial.println(url);
    client.println(String("POST ") + url + " HTTP/1.1");
    client.println("Host: " + String(host));
    client.println("Content-Type: application/x-www-form-urlencoded");

    client.print("Content-length: ");
    client.print(String("deviceId=1").length());
    client.println("");
    client.println("");
    client.print(String("deviceId=1"));
    
    unsigned long timeout = millis();
    while (client.available() == 0) {
        if (millis() - timeout > 5000) {
            Serial.println(">>> Client Timeout !");
            client.stop();
            return;
        }
    }

    // Read all the lines of the reply from server and print them to Serial
    while(client.available()) {
        String line = client.readStringUntil('\r');
        Serial.print(line);
    }

    Serial.println();
    Serial.println("closing connection");

    delay(5000);
    Serial.print("connecting to ");
    Serial.println(gethost);

    // Use WiFiClient class to create TCP connections
    client.stop();
    if (!client.connect(gethost, httpPort)) {
        Serial.println("connection failed");
        return;
    }

    digitalWrite(13, HIGH);
    delay(500);
    digitalWrite(13, LOW);
    delay(500);
    digitalWrite(13, HIGH);
    delay(500);
    digitalWrite(13, LOW);
    delay(3500);
    
     /* GET FROM SERVER */
    // We now create a URI for the request
    String geturl = "/get";
    //url += streamId;
    //url += "?private_key=";
    //url += privateKey;
    //url += "&value=";
    //url += value;

    Serial.print("Requesting URL: ");
    Serial.println(geturl);

    // This will send the request to the server
    client.print(String("GET ") + geturl + " HTTP/1.1\r\n" +
                 "Host: " + gethost + "\r\n" +
                 "Connection: close\r\n\r\n");
    timeout = millis();
    while (client.available() == 0) {
        if (millis() - timeout > 5000) {
            Serial.println(">>> Client Timeout !");
            client.stop();
            return;
        }
    }

    // Read all the lines of the reply from server and print them to Serial
    while(client.available()) {
        String line = client.readStringUntil('\r');
        Serial.print(line);
    }

    Serial.println();
    Serial.println("closing connection");
}
