void setup() {
  // put your setup code here, to run once:

Serial.begin(115200);
}
int paketNo = 0 ;
int basinc1 = 100;
int basinc2 = 50 ;
String uyduStatu = "1";
int pitch = 15;
int roll = 74;
int yaw = 94 ;


void loop() {
  
Serial.print("<");Serial.print(paketNo);Serial.print(">,"); //paketNo
Serial.print("<");Serial.print(uyduStatu);Serial.print(">,"); //uydu statüsü
Serial.print("<01010>,");//hata kod
Serial.print("<19/08/2021,03/42/2>,");//zaman
Serial.print("<");Serial.print(basinc1);Serial.print(">,");//basınc1
Serial.print("<");Serial.print(basinc2);Serial.print(">,"); //basınc2
Serial.print("<196>,");//yükseklik1
Serial.print("<179>,");//yükseklik2
Serial.print("<3>,");//irtifa farkı
Serial.print("<13>,");//iinş hızı
Serial.print("<25.2>,");//sıcaklık
Serial.print("<11.2>,");//pil gerilimi
// 40.99227422261599, 39.77443049509968
Serial.print("<40.99227422261599>,");//gps1 latituda
Serial.print("<39.77443049509968>,");//gps1 longititude
Serial.print("<725.2>,");//gps1 altitude
Serial.print("<");Serial.print(pitch);Serial.print(">,");//pitch
Serial.print("<");Serial.print(roll);Serial.print(">,");//roll
Serial.print("<");Serial.print(yaw);Serial.print(">,");//yaw
Serial.print("<1412>,");//takım no
Serial.println("<3>");//tasiyici inis hizi
delay(1000);
basinc1++;
pitch++;
roll++;
yaw++;
if(basinc1%10 == 0){
  uyduStatu = "1?";
}else{
   uyduStatu = "2";
}
basinc2 = basinc2 + 50;
paketNo++;
}
