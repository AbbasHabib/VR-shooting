#include "MPU_DRIVERS/MPU_FULL.h"
#define M_PI 3.14159265358979323846

#define BAUD_RATE 38400

MPU6050 mpu;

// MPU control/status vars
bool dmpReady = false; // set true if DMP init was successful
uint8_t devStatus; // return status after each device operation (0 = success, !0
                   // = error)
uint16_t packetSize;    // expected DMP packet size (default is 42 bytes)
uint16_t fifoCount;     // count of all bytes currently in FIFO
uint8_t fifoBuffer[64]; // FIFO storage buffer

Quaternion q;   // [w, x, y, z]         quaternion container
VectorInt16 aa; // [x, y, z]            accel sensor measurements
VectorInt16
    aaReal; // [x, y, z]            gravity-free accel sensor measurements
VectorInt16
    aaWorld; // [x, y, z]            world-frame accel sensor measurements
VectorFloat gravity; // [x, y, z]            gravity vector
float euler[3];      // [psi, theta, phi]    Euler angle container
float
    ypr[3]; // [yaw, pitch, roll]   yaw/pitch/roll container and gravity vector

void SendQuaternion();

void setup() {
  Wire.begin();
  Wire.setClock(400000);

  Serial.begin(BAUD_RATE);

  while (!Serial) {
  };
  mpu.initialize();
  devStatus = mpu.dmpInitialize();
  mpu.setXGyroOffset(54);
  mpu.setYGyroOffset(-21);
  mpu.setZGyroOffset(5);

  if (devStatus == 0) {
    mpu.setDMPEnabled(true);
    // set our DMP Ready flag so the main loop() function knows it's okay to use
    // it
    dmpReady = true;
    // get expected DMP packet size for later comparison
    packetSize = mpu.dmpGetFIFOPacketSize();
  } else {
    // Error
    Serial.println("Error!");
  }
}

void loop() {
  int mpuIntStatus = mpu.getIntStatus();
  fifoCount = mpu.getFIFOCount();

  if ((mpuIntStatus & 0x10) || fifoCount == 1024) { // check if overflow
    mpu.resetFIFO();
  } else if (mpuIntStatus & 0x02) {
    while (fifoCount < packetSize)
      fifoCount = mpu.getFIFOCount();

    mpu.getFIFOBytes(fifoBuffer, packetSize);
    fifoCount -= packetSize;

    SendQuaternion();
  }
}

void SendQuaternion() {
  mpu.dmpGetQuaternion(&q, fifoBuffer);
  Serial.print("r/");
  Serial.print(q.w, 4);
  Serial.print("/");
  Serial.print(q.x, 4);
  Serial.print("/");
  Serial.print(q.y, 4);
  Serial.print("/");
  Serial.print(q.z, 4);
  Serial.print("#");
}

void SendEuler() {
  // display Euler angles in degrees
  mpu.dmpGetQuaternion(&q, fifoBuffer);
  mpu.dmpGetEuler(euler, &q);
  Serial.print(euler[0] * 180 / M_PI);
  Serial.print("/");
  Serial.print(euler[1] * 180 / M_PI);
  Serial.print("/");
  Serial.println(euler[2] * 180 / M_PI);
}

void SendYawPitchRoll() {
  // display Euler angles in degrees
  mpu.dmpGetQuaternion(&q, fifoBuffer);
  mpu.dmpGetGravity(&gravity, &q);
  mpu.dmpGetYawPitchRoll(ypr, &q, &gravity);
  Serial.print(ypr[0] * 180 / M_PI);
  Serial.print("/");
  Serial.print(ypr[1] * 180 / M_PI);
  Serial.print("/");
  Serial.println(ypr[2] * 180 / M_PI);
}

void SendRealAccel() {
  // display real acceleration, adjusted to remove gravity
  mpu.dmpGetQuaternion(&q, fifoBuffer);
  mpu.dmpGetAccel(&aa, fifoBuffer);
  mpu.dmpGetGravity(&gravity, &q);
  mpu.dmpGetLinearAccel(&aaReal, &aa, &gravity);
  Serial.print("a/");
  Serial.print(aaReal.x);
  Serial.print("/");
  Serial.print(aaReal.y);
  Serial.print("/");
  Serial.println(aaReal.z);
}

void SendWorldAccel() {
  // display initial world-frame acceleration, adjusted to remove gravity
  // and rotated based on known orientation from quaternion
  mpu.dmpGetQuaternion(&q, fifoBuffer);
  mpu.dmpGetAccel(&aa, fifoBuffer);
  mpu.dmpGetGravity(&gravity, &q);
  mpu.dmpGetLinearAccel(&aaReal, &aa, &gravity);
  mpu.dmpGetLinearAccelInWorld(&aaWorld, &aaReal, &q);
  Serial.print("a/");
  Serial.print(aaWorld.x);
  Serial.print("/");
  Serial.print(aaWorld.y);
  Serial.print("/");
  Serial.println(aaWorld.z);
}