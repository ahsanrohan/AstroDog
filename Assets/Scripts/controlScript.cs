using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class controlScript : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        string dataPacket = serialPort.ReadLine();
        Debug.Log(dataPacket);

    }
}
