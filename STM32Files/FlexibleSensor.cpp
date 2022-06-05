#include "FlexibleSensor.h"
#include <Arduino.h>

void InitFlexSensors() {
  analogReadResolution(8);
  analogWriteResolution(8);
}

float SensorsRead(void) {
  int ADCflex = analogRead(flexPin);
  float Vflex = ADCflex * VCC / 255.0;
  return R_DIV * (VCC / Vflex - 1.0);
}
int CalcAngle(float flexRes, float MinResistance, float MaxResistance) {
  int angle;
  if (flexRes < MinResistance)
    angle = 0;
  else if (flexRes > MinResistance && flexRes < MaxResistance)
    angle = 45;
  else
    angle = 90;
  return angle;
}
