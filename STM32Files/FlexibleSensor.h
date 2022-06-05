
#ifndef FlexibleSensor_H
#define FlexibleSensor_H

#define flexPin PA2
#define MAX_POT_VAL 255

#define maxFlexBend 100
#define maxFlexUnBend 25.9

#define R_DIV 8120.0           // resistor used to create a voltage divider
#define flatResistance 30000.0 // resistance when flat
#define bendResistance 80000.0 // resistance at 90 deg

#define VCC 5

float SensorsRead(void);
int CalcAngle(float flexRes, float MinResistance, float MaxResistance);
void InitFlexSensors();
#endif