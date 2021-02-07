using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class inputProsessor : MonoBehaviour
{
    // Start is called before the first frame update
    static SerialPort serialPort;

    //DATA PACKAGE ORDER
    //LEFT_VRX 0 to 1023
    //LEFT_VRY 0 to 1023


    ///0 to 1 floating point Center at 0.5
    public float x_joystick = 0;
    ///0 to 1 floating point Center at 0.5
    public float y_joystick = 0;

    //Push Down Buttons
    public bool joystrick_held;
    public bool RotteryE_held;

    public byte gender = 0;
    //1 for Byte
    //2 for Tera Byte

    private int RotteryE_counter = 0;
    private int RotteryE_offset = 0;

    byte mode = 0;
    public byte angle = 9;

    float pollUpdate;

    void Start()
    {
        serialPort = new SerialPort();
        serialPort.PortName = "COM7";
        serialPort.BaudRate = 19200;
        serialPort.Open();

        pollUpdate = Time.time;

    }

    void Update()
    {

        string dataPacket = serialPort.ReadLine();
        var split = dataPacket.Split(' ');

        if (split.Length != 5) return;

        x_joystick = (2.0f * int.Parse(split[0]) / 1023.0f) - 1;
        y_joystick = (2.0f * int.Parse(split[1]) / 1023.0f) - 1;

        RotteryE_counter = int.Parse(split[2]);

        joystrick_held = !(int.Parse(split[3]) != 0);

        RotteryE_held = !(int.Parse(split[4]) != 0);

        gender = (byte)int.Parse(split[5]);

        if (Time.time - pollUpdate > 1)
        {
            sendPoll();
            pollUpdate = Time.time;
        }
    }

    public int getRottery_counter()
    {
        return RotteryE_counter - RotteryE_offset;
    }

    public void resetRottery_counter()
    {
        RotteryE_offset = RotteryE_counter;
    }

    public void setCannonMode()
    {
        mode = 1;
    }

    public void setCraneMode()
    {
        mode = 2;
    }

    public void setGenderMode()
    {
        mode = 3;
    }

    public void setStandByMode()
    {
        mode = 0;
    }
    public void sendPoll()
    {
        byte[] pollBuffer = new byte[10];
        pollBuffer[0] = mode;
        pollBuffer[1] = angle;
        serialPort.Write(pollBuffer, 0, 10);
    }

}
