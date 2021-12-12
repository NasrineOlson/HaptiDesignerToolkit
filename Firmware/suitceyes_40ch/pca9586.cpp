#include "pca9685.h"
#include <Wire.h>

PCA9685Driver::PCA9685Driver(const uint8_t addr, uint8_t oe_pin) {
  _i2caddr = addr;
  _oe_pin = oe_pin;
  _i2c = &Wire;
  
  pinMode(oe_pin, OUTPUT);  
  digitalWrite(oe_pin, HIGH);
}

void PCA9685Driver::begin() {
  _i2c->begin();
  reset();
  
  // set the default internal frequency
  setOscillatorFrequency(FREQUENCY_OSCILLATOR);
  
  setPWMFreq(1600);   
}

void PCA9685Driver::reset() {
  write8(PCA9685_MODE1, MODE1_RESTART);
  delay(10);
}

void PCA9685Driver::setOutputEnable(bool state) {
    digitalWrite(_oe_pin, state ? LOW : HIGH);      
}

void PCA9685Driver::setOscillatorFrequency(uint32_t freq) {
  _oscillator_freq = freq;
}

void PCA9685Driver::setPWMFreq(float freq) {
  // Range output modulation frequency is dependant on oscillator
  if (freq < 1)
    freq = 1;
  if (freq > 3500)
    freq = 3500; // Datasheet limit is 3052=50MHz/(4*4096)

  float prescaleval = ((_oscillator_freq / (freq * 4096.0)) + 0.5) - 1;
  if (prescaleval < PCA9685_PRESCALE_MIN)
    prescaleval = PCA9685_PRESCALE_MIN;
  if (prescaleval > PCA9685_PRESCALE_MAX)
    prescaleval = PCA9685_PRESCALE_MAX;
  uint8_t prescale = (uint8_t)prescaleval;

  uint8_t oldmode = read8(PCA9685_MODE1);
  uint8_t newmode = (oldmode & ~MODE1_RESTART) | MODE1_SLEEP; // sleep
  write8(PCA9685_MODE1, newmode);                             // go to sleep
  write8(PCA9685_PRESCALE, prescale); // set the prescaler
  write8(PCA9685_MODE1, oldmode);
  delay(5);
  // This sets the MODE1 register to turn on auto increment.
  write8(PCA9685_MODE1, oldmode | MODE1_RESTART | MODE1_AI);
}

void PCA9685Driver::setPWM(uint8_t num, uint16_t duty_cycle) {
  uint16_t on = 0;
  uint16_t off = 0;
  
  // Clamp value between 0 and 4095 inclusive.
  duty_cycle = min(duty_cycle, (uint16_t)4095);  

  if(duty_cycle == 0){
    off = 4096;
    on = 0;    
  }
  else if(duty_cycle == 4095){
    off = 0;
    on = 4096;    
  }
  else {
    off = duty_cycle;
    on = 0;
  }
  
  _i2c->beginTransmission(_i2caddr);
  _i2c->write(PCA9685_LED0_ON_L + 4 * num);
  _i2c->write(on);
  _i2c->write(on >> 8);
  _i2c->write(off);
  _i2c->write(off >> 8);
  _i2c->endTransmission();
}

/******************* Low level I2C interface */
uint8_t PCA9685Driver::read8(uint8_t addr) {
  _i2c->beginTransmission(_i2caddr);
  _i2c->write(addr);
  _i2c->endTransmission();

  _i2c->requestFrom((uint8_t)_i2caddr, (uint8_t)1);
  return _i2c->read();
}

void PCA9685Driver::write8(uint8_t addr, uint8_t d) {
  _i2c->beginTransmission(_i2caddr);
  _i2c->write(addr);
  _i2c->write(d);
  _i2c->endTransmission();
}
