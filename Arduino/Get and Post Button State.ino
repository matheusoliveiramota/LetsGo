/* 
   POST JSON Data when button is pressed
   Author: Matheus Mota
*/

#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <ArduinoJson.h>
#include <ESP8266WebServer.h>
#include <ESP8266HTTPClient.h>

/* Set Wifi credentials. */
const char *ssid = "AndroidAP17";  //ENTER YOUR WIFI SETTINGS
const char *password = "12345678";

/* Webserver parameters */
const String host = "192.168.43.157:7300";   //IP adress of local API

/* Structs */
struct PinButton
{
  int id, previousState, stateBook; 
  long  timer;
};

/* Variables */
String _token = "ABCDEFG123@#";
int stateButton;
long debounce = 500;
struct PinButton pinButton0 = {4, LOW, LOW, 0};
struct PinButton pinButton1 = {5, LOW, LOW, 0};

/* Array of sensors */
const int arrayLength = 2;
struct PinButton buttons[arrayLength];


//=======================================================================
//                    Setup
//=======================================================================

void setup() {
  delay(1000);
  Serial.begin(115200);
  WiFi.mode(WIFI_OFF);        //Prevents reconnection issue (taking too long to connect)
  delay(1000);
  WiFi.mode(WIFI_STA);        //This line hides the viewing of ESP as wifi hotspot

  WiFi.begin(ssid, password);     //Connect to your WiFi router
  Serial.println("");

  Serial.print("Connecting");
  // Wait for connection
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  //If connection successful show IP address in serial monitor
  Serial.println("");
  Serial.print("Connected to ");
  Serial.println(ssid);
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP());  //IP address assigned to your ESP

  /* Adding sensors into array */
  buttons[0] = pinButton0;
  buttons[1] = pinButton1;

  /* Set input Pin and Get the last State of Book */
  int i;
  for(i=0;i<arrayLength;i++)
  {
     pinMode(buttons[i].id, INPUT);
     buttons[i].stateBook = getBookState(buttons[i].id);
     Serial.println(buttons[i].stateBook);
  }

  delay(200);
}

//=======================================================================
//                    Main Program Loop
//=======================================================================
void loop() {

  int i;
  for(i=0;i<arrayLength;i++)
  {
      stateButton = digitalRead(buttons[i].id);
      if (stateButton == HIGH && buttons[i].previousState == LOW && millis() - buttons[i].timer > debounce) {
        if (buttons[i].stateBook == 1) {
          int state = postBookState(buttons[i].id,0);
          buttons[i].stateBook = state;
          Serial.println(buttons[i].stateBook);
        } else {
          int state = postBookState(buttons[i].id,1);
          buttons[i].stateBook = state;
          Serial.println(buttons[i].stateBook);
        }
        buttons[i].timer = millis();
      }
      
      buttons[i].previousState == stateButton;
  }
}

int getBookState(int idBook)
{
   HTTPClient http;

   http.begin("http://" + host + "/api/Sensor/" + String(idBook));
   int httpCode = http.GET();
   
   String payload = http.getString();
   Serial.println(payload);

   const int capacity = JSON_OBJECT_SIZE(10);
   StaticJsonDocument<capacity> doc;

   DeserializationError err = deserializeJson(doc, payload);
   
   if (err) 
   {
      Serial.print(F("deserializeJson() failed with code "));
      Serial.println(err.c_str());
   }
   else
   {
      const int id = doc["idPinButton"];
      const int stateBook = doc["stateBook"];

      Serial.println(String(id) + " - " + String(stateBook));
      http.end();
      return stateBook;
   }
   
   http.end();
   return -1;
}

int postBookState(int pinButtonId, int isAvailable)
{
  HTTPClient http;    //Declare object of class HTTPClient

  const int capacity = JSON_OBJECT_SIZE(4);
  StaticJsonDocument<capacity> doc;

  doc["token"] = _token;
  doc["idPinButton"] = pinButtonId;
  doc["isAvailable"] = isAvailable;

  String data;
  serializeJson(doc, data);

  Serial.println(data);

  http.begin("http://" + host + "/api/Sensor/UpdateState");              //Specify request destination
  http.addHeader("Content-Type", "application/json");    //Specify content-type header

  int httpCode = http.POST(data);   //Send the request
  String payload = http.getString();    //Get the response payload

  Serial.println(httpCode);   //Print HTTP return code
  
  int response = payload.toInt();    //Print request response payload

  http.end();  //Close connection

  return response;
}
