import bluetooth

name = "stm32car"
addr = "98:D3:91:FD:AC:CB"
pin = 1991
port = 1

s = bluetooth.BluetoothSocket(bluetooth.RFCOMM)
s.connect((addr,port))
s.send(chr(65)+chr(76)+chr(67)+chr(68))