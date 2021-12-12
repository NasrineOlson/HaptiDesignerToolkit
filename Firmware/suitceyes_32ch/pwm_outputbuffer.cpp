#include <Arduino.h>
#include "pwm_outputbuffer.h"
#include "pca9685.h"

#define PCA9586_ADDR1 0x40
#define PCA9586_ADDR2 0x41

PCA9685Driver driver1 = PCA9685Driver(PCA9586_ADDR1, 12);
PCA9685Driver driver2 = PCA9685Driver(PCA9586_ADDR2, 6);

typedef struct {
  int pwm_channel;
  PCA9685Driver* driver;
} MapPWMChannel;

MapPWMChannel _mapChannels[] = {
  {8, &driver1},
  {9, &driver1},
  {10, &driver1},
  {11, &driver1},
  
  {12, &driver1},
  {13, &driver1},
  {14, &driver1},
  {15, &driver1},
  
  {0, &driver2},
  {1, &driver2},
  {2, &driver2},
  {3, &driver2},
  
  {4, &driver2},
  {5, &driver2},
  {6, &driver2},
  {7, &driver2},
  
  {8, &driver2},
  {9, &driver2},
  {10, &driver2},
  {11, &driver2},
  
  {12, &driver2},
  {13, &driver2},
  {14, &driver2},
  {16, &driver2},

  {0, &driver1},
  {1, &driver1},
  {2, &driver1},
  {3, &driver1},
  
  {4, &driver1},
  {5, &driver1},
  {6, &driver1},
  {7, &driver1}  
};

PWMOutputbuffer::PWMOutputbuffer() {
  set_all_channels(0);
}

void PWMOutputbuffer::begin() {
  driver1.begin();    
  driver2.begin();
  
  Wire.setClock(400000);

  set_all_channels(0);  

  update(true);
  
  driver1.setOutputEnable(true); 
  driver2.setOutputEnable(true); 
}

void PWMOutputbuffer::set_channel(uint8_t ch, uint8_t pwm) {
  if(ch < MAX_CHANNELS) 
    channels[ch] = pwm;
}

uint8_t PWMOutputbuffer::get_channel(uint8_t ch) {
  return channels[ch];
}

void PWMOutputbuffer::set_all_channels(uint8_t pwm) {
  for(int ch = 0 ; ch < MAX_CHANNELS; ch++){
    channels[ch] = pwm;
  }
}

void PWMOutputbuffer::update(bool forced = false) {
  for(int ch = 0 ; ch < MAX_CHANNELS; ch++) {
    if( (channels[ch] != last_update[ch]) || forced) {
      byte value = channels[ch];
      int mapped = map(value, 0, 255, 0, 4095);

      _mapChannels[ch].driver->setPWM(_mapChannels[ch].pwm_channel, mapped);
            
      last_update[ch] = channels[ch];
    }
  }      
}
