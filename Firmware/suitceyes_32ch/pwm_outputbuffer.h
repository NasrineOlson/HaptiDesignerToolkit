#ifndef _PWM_OUTPUTBUFFER_H
#define _PWM_OUTPUTBUFFER_H

#include <Arduino.h>
#include <Wire.h>

#define MAX_CHANNELS 32

class PWMOutputbuffer {
public:
  PWMOutputbuffer();
  void begin();
  void set_channel(uint8_t ch, uint8_t pwm);
  uint8_t get_channel(uint8_t ch);
  void set_all_channels(uint8_t pwm);
  void update(bool forced = false);  
private:  
  byte channels[MAX_CHANNELS];
  byte last_update[MAX_CHANNELS];
};

#endif
