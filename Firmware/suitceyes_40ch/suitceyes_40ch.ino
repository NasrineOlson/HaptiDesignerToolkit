#include <Arduino.h>
#include <SoftwareSerial.h>
#include "SPI.h"
#include "pwm_outputbuffer.h"

#define VERSION "2.0.0"

#define CMD_OK 0
#define CMD_ERR_UNKNOWN 1
#define CMD_ERR_PARAMETER 2
#define CMD_ERR_FRAMES_MISSING 3
#define CMD_OUT_OF_MEMORY 4
#define CMD_EOF 5 

// UART buffer
const byte numChars = 400;
char receivedChars[numChars];
boolean newData = false; 
Stream* newDataFromPort = &Serial1;

PWMOutputbuffer _currentOutput = PWMOutputbuffer();

const int timeout = 2000;
long _lastReceivedMessage = 0;
int slave_channel_index = 0;

SoftwareSerial slaveSerial(SCK, MOSI); // RX, TX

void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
    
  Serial.begin(9600);
  Serial1.begin(9600);
  slaveSerial.begin(9600);
  
  _currentOutput.begin();  
}

void loop() {  
  static int previous_spi_rx = HIGH;
  
  // Check from USB
  if(Serial)
    recvWithStartEndMarkers(&Serial);

  // Check from bluetooth
  recvWithStartEndMarkers(&Serial1);

  // Check from slave uart
  recvWithStartEndMarkers(&slaveSerial);
  
  if (newData == true) {    
    int ret = parse_command(newDataFromPort, receivedChars);
    _lastReceivedMessage = millis();
    send_reply(newDataFromPort, ret);   
    newData = false;
  }
 
  _currentOutput.update();
 
  // Check if the output should be turned off due to failsafe
  if(millis() > _lastReceivedMessage + timeout){
    stop_play();

    _lastReceivedMessage = millis();
  }
}

void recvWithStartEndMarkers(Stream* port) {
  static boolean recvInProgress = false;
  static byte ndx = 0;
  char startMarker = '[';
  char endMarker = ']';
  char rc;

  while (port->available() > 0 && newData == false) {
    rc = port->read();

    if (recvInProgress == true) {
      if (rc != endMarker) {
        receivedChars[ndx] = rc;
        ndx++;
        if (ndx >= numChars) {
          ndx = numChars - 1;
        }
      }
      else {
        receivedChars[ndx] = '\0'; // terminate the string
        recvInProgress = false;
        ndx = 0;
        newDataFromPort = port;
        newData = true;
      }
    }

    else if (rc == startMarker) {
      recvInProgress = true;
    }
  }
}

int parse_command(Stream* port, char* command) {
  const char delim[2] = ",";
  char* token;

  String subcommand = String(command);
  
  token = strtok(command, delim);

  if (strcmp(token, "V") == 0) { // Get version
    port->println(VERSION);
    return CMD_OK;
  } 
  else if (strcmp(token, "K") == 0) { // Keep alive    
    slaveSerial.print("[K]");
    return CMD_OK;
  } 
  else if (strcmp(token, "S") == 0) { // Slave command motors  
    char* motor;
    char* offset = strtok(NULL, delim);
    slave_channel_index = String(offset).toInt();
   
    while( (motor = strtok(NULL, delim))!=NULL) {
      if(!parse_motor(motor, slave_channel_index)) {      
        return CMD_ERR_PARAMETER;
      }
    }

    slaveSerial.print("[");
    slaveSerial.print("S,");
    slaveSerial.print(slave_channel_index + MAX_CHANNELS);
    slaveSerial.print(",");
    slaveSerial.print(removeSlaveCommand(subcommand));
    slaveSerial.print("]");
    
    return CMD_OK;
  }
  else if (strcmp(token, "M") == 0) { // Run motors  
    char* motor;
    
    while( (motor = strtok(NULL, delim))!=NULL) {
      if(!parse_motor(motor, 0)) {      
        return CMD_ERR_PARAMETER;
      }
    }          

    slaveSerial.print("[");
    slaveSerial.print("S,");
    slaveSerial.print(MAX_CHANNELS);
    slaveSerial.print(",");
    slaveSerial.print(subcommand.substring(subcommand.indexOf(",") + 1));
    slaveSerial.print("]");
    
    
    return CMD_OK;
  }

  return CMD_ERR_UNKNOWN;
}

String removeSlaveCommand(String subcommand){
  int first = subcommand.indexOf(",");
  int second = subcommand.indexOf(",", first + 1);
  return subcommand.substring(second + 1);
}

void stop_play() {
  _currentOutput.set_all_channels(0);
}

void send_reply(Stream* port, int ret) {
   if (ret == CMD_OK)
      port->print("[O]");
    else {
      port->print("[E,");
      port->print(ret);
      port->print("]");
    }
}

bool parse_motor(char* motor_string, int slave_offset){  
  String s = String(motor_string);

  int split = s.indexOf(":");
  if(split < 0)
    return false;

  String channels_string = s.substring(0, split);
  String power_string = s.substring(split + 1);  
  int power = power_string.toInt();
  if(power < 0 || power > 255)
    return false;

  if(channels_string == "all"){    
    _currentOutput.set_all_channels(power);      
  }
  else if(channels_string.indexOf("-") < 0){
    int ch = channels_string.toInt() - slave_offset;
    if(ch >=0 && ch < MAX_CHANNELS)      
      _currentOutput.set_channel(ch, power);
  }
  else 
  {
    bool first = true;
    int ch_split = channels_string.indexOf("-");
    String lower_ch = channels_string.substring(0, ch_split);
    String upper_ch = channels_string.substring(ch_split + 1);
    
    int lch = lower_ch.toInt() - slave_offset;
    int uch = upper_ch.toInt() - slave_offset;
          
    for(int t = lch; t <= uch; t++) {
       if(t >=0 && t < MAX_CHANNELS)
          _currentOutput.set_channel(t, power);      
    }       
  }   
  return true;
}
