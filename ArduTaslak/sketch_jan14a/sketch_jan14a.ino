void setup() {
  // put your setup code here, to run once:
Serial.begin(115200);
}

void loop() {
  
Serial.print("<1>,");//paketNo
Serial.print("<0>,");//uydu statüsü
Serial.print("<00000>,");//hata kod
Serial.print("<19/08/2021,03/42/2>,");//zaman
Serial.print("<8870>,");//basınc1
Serial.print("<9514>,");//basınc2
Serial.print("<196>,");//yükseklik1
Serial.print("<179>,");//yükseklik2
Serial.print("<3>,");//irtifa farkı
Serial.print("<13>,");//iinş hızı
Serial.print("<25.2>,");//sıcaklık
Serial.print("<11.2>,");//pil gerilimi
Serial.print("<40.5456>,");//gps1 latituda
Serial.print("<29.31564>,");//gps1 longititude
Serial.print("<725.2>,");//gps1 altitude
Serial.print("<10.96>,");//pitch
Serial.print("<0.31>,");//roll
Serial.print("<0.03>,");//yaw
Serial.println("<1412>");//takım no,
delay(1000);

}
