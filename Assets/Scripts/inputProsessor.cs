using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class inputProsessor : MonoBehaviour
{
    // Start is called before the first frame update
    static SerialPort serialPort;
    void Start()
    {
        serialPort = new SerialPort();
        serialPort.PortName = "COM7";
        serialPort.BaudRate = 9600;
        serialPort.Open();
    }

    //DATA PACKAGE ORDER
    //LEFT_VRX 0 to 1023
    //LEFT_VRY 0 to 1023


    ///0 to 1 floating point Center at 0.5
    public float x_joystick = 0;
    ///0 to 1 floating point Center at 0.5
    public float y_joystick = 0;

    void Update()
    {
        string dataPacket = serialPort.ReadLine();

        var split = dataPacket.Split(' ');

        x_joystick = (2.0f * int.Parse(split[0]) / 1023.0f)-1;
        y_joystick = (2.0f * int.Parse(split[1]) / 1023.0f)-1;



    }


}
