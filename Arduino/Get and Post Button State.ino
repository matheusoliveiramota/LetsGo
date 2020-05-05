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
const char *ssid = "Elias";  //ENTER YOUR WIFI SETTINGS
const char *password = "ediedi25";

/* Webserver parameters */
const String host = "192.168.15.254:6002";   //IP adress of local API

/* Structs */
struct Porta
{
  int porta, estadoAnterior, estadoMesa; 
  long  timer;
};

/* Variables */
int _codRestaurante = 1;
int estadoBotao;
long tempoEspera = 500;
struct Porta porta0 = {12, LOW, LOW, 0};
struct Porta porta1 = {14, LOW, LOW, 0};
struct Porta porta2 = {5, LOW, LOW, 0};
struct Porta porta3 = {4, LOW, LOW, 0};

/* Array of sensors */
const int tamanhoArray = 4;
struct Porta portas[tamanhoArray];


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
  portas[0] = porta0;
  portas[1] = porta1;
  portas[2] = porta2;
  portas[3] = porta3;

  /* Set input Pin and Get the last State of Book */
  int i;
  for(i=0;i<tamanhoArray;i++)
  {
     pinMode(portas[i].porta, INPUT);
     portas[i].estadoMesa = getEstadoMesa(portas[i].porta);
     Serial.println(portas[i].estadoMesa);
  }

  delay(200);
}

//=======================================================================
//                    Main Program Loop
//=======================================================================
void loop() {

  int i;
  for(i=0;i<tamanhoArray;i++)
  {
      estadoBotao = digitalRead(portas[i].porta);
      if (estadoBotao == HIGH && portas[i].estadoAnterior == LOW && millis() - portas[i].timer > tempoEspera) {
        if (portas[i].estadoMesa == 1) {
          int estado = postEstadoMesa(portas[i].porta,2);
          portas[i].estadoMesa = estado;
          Serial.println(portas[i].estadoMesa);
        } else {
          int estado = postEstadoMesa(portas[i].porta,1);
          portas[i].estadoMesa = estado;
          Serial.println(portas[i].estadoMesa);
        }
        portas[i].timer = millis();
      }
      
      portas[i].estadoAnterior == estadoBotao;
  }
}

int getEstadoMesa(int porta)
{
   HTTPClient http;

   http.begin("http://" + host + "/api/Placa/GetEstadoMesa/" + String(_codRestaurante) + "/" + String(porta));
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
      const int porta = doc["porta"];
      const int estadoMesa = doc["codEstadoMesa"];
      
      Serial.println(String(porta) + " - " + String(estadoMesa));
      http.end();
      return estadoMesa;
   }
   
   http.end();
   return -1;
}

int postEstadoMesa(int porta, int estadoMesa)
{
  HTTPClient http;    //Declare object of class HTTPClient

  const int capacity = JSON_OBJECT_SIZE(4);
  StaticJsonDocument<capacity> doc;

  doc["codRestaurante"] = _codRestaurante;
  doc["porta"] = porta;
  doc["codEstadoMesa"] = estadoMesa;

  String data;
  serializeJson(doc, data);

  Serial.println(data);

  http.begin("http://" + host + "/api/Placa/PostEstadoMesa"); 
  http.addHeader("Content-Type", "application/json"); 

  int httpCode = http.POST(data);   
  String payload = http.getString();    

  Serial.println(httpCode);   
  
  int response = payload.toInt();    

  http.end();  

  return response;
}