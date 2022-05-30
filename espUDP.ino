#include <ESP8266WiFi.h>
#include <WiFiUdp.h>

#define WIFI_SSID "net1"
#define WIFI_PASS "yoyoyoyo"
#define UDP_PORT 4210

WiFiUDP UDP;

IPAddress local_IP(192, 168, 137, 46);
IPAddress gateway(192, 168, 1, 1);
IPAddress subnet(255, 255, 255, 0);
IPAddress primaryDNS(8, 8, 8, 8);   //optional
IPAddress secondaryDNS(8, 8, 4, 4); //optional


IPAddress REMOTE_IP = NULL;
uint16_t  REMOTE_PORT = NULL;

char packet_buffer[255];
  
void setup() {
  Serial.begin(115200);
  if (!WiFi.config(local_IP, gateway, subnet, primaryDNS, secondaryDNS)) {
    Serial.println("STA Failed to configure");
  }
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
  UDP.begin(UDP_PORT);
  Serial.print("Listening on UDP port ");
  Serial.println(UDP_PORT);
  wait_for_connection_msg();
}

void wait_for_connection_msg(){
  int packetSize = UDP.parsePacket();
  if (packetSize) {
    Serial.print("Received packet! Size: ");
    Serial.println(packetSize); 
    int len = UDP.read(packet_buffer, 255);
    if (len > 0)
      packet_buffer[len] = '\0';
    Serial.print("Packet received: ");
    Serial.println(packet_buffer);
    REMOTE_IP = UDP.remoteIP();
    REMOTE_PORT = UDP.remotePort();
  }
}

void send_packet(char* msg){
  UDP.beginPacket(REMOTE_IP, REMOTE_PORT);
  UDP.write(msg);
  UDP.endPacket();
}

void loop() {
    if(!REMOTE_IP || !REMOTE_PORT)
      wait_for_connection_msg();
    send_packet("sensors data 544, 5456, 84488 ,64 ,548 , 20");
    delay(10);
}
