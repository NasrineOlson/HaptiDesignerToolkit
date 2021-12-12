#include <Arduino.h>
#include "pwm_outputbuffer.h"
#include "pca9685.h"

#define PCA9586_ADDR1 0x40
#define PCA9586_ADDR2 0x41
#define PCA9586_ADDR3 0x42

PCA9685Driver driver1 = PCA9685Driver(PCA9586_ADDR1, 12);
PCA9685Driver driver2 = PCA9685Driver(PCA9586_ADDR2, 6);
PCA9685Driver driver3 = PCA9685Driver(PCA9586_ADDR3, 4);

typedef struct {
  int pwm_channel;
  PCA9685Driver* driver;
} MapPWMChannel;

MapPWMChannel _mapChannels[] = {
  // Ch 0
  {8, &driver1},
  {9, &driver1},
  {10, &driver1},
  {11, &driver1},

  // Ch 4
  {12, &driver1},
  {13, &driver1},
  {14, &driver1},
  {15, &driver1},

  // Ch 8
  {0, &driver2},
  {1, &driver2},
  {2, &driver2},
  {3, &driver2},

  // Ch 12
  {4, &driver2},
  {5, &driver2},
  {6, &driver2},
  {7, &driver2},

  // Ch 16
  {0, &driver3},
  {1, &driver3},
  {2, &driver3},
  {3, &driver3},

  // Ch 20
  {4, &driver3},
  {5, &driver3},
  {6, &driver3},
  {7, &driver3},
  
  // Ch 24
  {8, &driver2},
  {9, &driver2},
  {10, &driver2},
  {11, &driver2},

  // Ch 28
  {12, &driver2},
  {13, &driver2},
  {14, &driver2},
  {15, &driver2},

  // Ch 32
  {0, &driver1},
  {1, &driver1},
  {2, &driver1},
  {3, &driver1},

  // Ch 36
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
  driver3.begin();
  
  Wire.setClock(400000);

  set_all_channels(0);  

  update(true);
  
  driver1.setOutputEnable(true); 
  driver2.setOutputEnable(true); 
  driver3.setOutputEnable(true); 
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
