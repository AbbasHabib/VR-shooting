#include <ESP8266WiFi.h>
#include <WiFiUdp.h>

#define WIFI_SSID "net1"
#define WIFI_PASS "yoyoyoyo"
#define RECEIVER_IP "255.255.255.255"

#define LISTENING_UDP_PORT  8051
#define SENDING_TO_PORT     8051

WiFiUDP UDP;

char buff [81];

//IPAddress local_IP(192, 168, 137, 46);
//IPAddress gateway(192, 168, 1, 1);
//IPAddress subnet(255, 255, 255, 0);
//IPAddress primaryDNS(8, 8, 8, 8);   //optional
//IPAddress secondaryDNS(8, 8, 4, 4); //optional


//IPAddress REMOTE_IP = NULL;
//uint16_t  REMOTE_PORT = NULL;

char packet_buffer[255];
  
void setup() {
  Serial.begin(38400);
//  if (!WiFi.config(local_IP, gateway, subnet, primaryDNS, secondaryDNS)) {
//    Serial.println("STA Failed to configure");
//  }
  Serial.println();
  WiFi.begin(WIFI_SSID, WIFI_PASS);
  
  // Connecting to WiFi...
  Serial.print("Connecting to ");
  Serial.print(WIFI_SSID);
  // Loop continuously while WiFi is not connected
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(100);
    Serial.print(".");
  }
  
  // Connected to WiFi
  Serial.println();
  Serial.print("Connected! IP address: ");
  Serial.println(WiFi.localIP());
 
  // Begin listening to UDP port
  UDP.begin(LISTENING_UDP_PORT);
  Serial.print("Listening on UDP port ");
  Serial.println(LISTENING_UDP_PORT);
  buff[80] = '\0';
}

void sendPacket(char* msg){
  UDP.beginPacket(RECEIVER_IP, SENDING_TO_PORT);
  UDP.write(msg);
  UDP.endPacket();
}

void loop() {
  static int indx = 0;
  static char c= '\0';
  while (Serial.available() > 0) {
    c = Serial.read();
    if(c== '\n' || c == '\r'){
      indx = 0;
      break;
    }
    if (indx < 80) {
      buff[indx++] = c; // save data in the next index in the array buff
      if (buff[indx-1] == '#') { //check for the end of the word
        buff[indx-1] = '\0';
        sendPacket(buff);
        indx = 0;
      }
    }
    else
      indx = 0;
  }
  
}