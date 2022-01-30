using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class inputProcessor : MonoBehaviour
{

    static SerialPort serialPort;

    public int potion;
    //Note Max 16 char limit.....
    public string LCD_line1;
    public string LCD_line2;

    public int encoder = 0;
    public bool dial_press = false;

    public int dial = -1;

    //This a byte plz respect it
    //LEDs will only process 0 to 9 values anyways
    public byte timer;

    public byte game_state = 0;

    private int RotteryE_counter = 0;
    private int RotteryE_offset = 0;

    public string lastMSG = "";


    public bool simon = false;

    float pollUpdate = 0.00f;

    // Start is called before the first frame update
    void Start()
    {
        serialPort = new SerialPort();
        serialPort.PortName = "COM3";
        serialPort.BaudRate = 9600;
        serialPort.Open();

        serialPort.WriteTimeout = 500;
        serialPort.ReadTimeout = 500;

        serialPort.DiscardOutBuffer();

    }
    public int getPotion()
    {
        return potion;
    }
    public bool open = false;

    // Update is called once per frame
    void Update()
    {
        open = serialPort.IsOpen;
        
        if (Time.time - pollUpdate > 0.25f)
        {
            

            string dataPacket = serialPort.ReadLine();

            if(!(dataPacket.Contains("START") && dataPacket.Contains("END"))) return;

            dataPacket = dataPacket.Remove(dataPacket.IndexOf("END"));
            dataPacket = dataPacket.Remove(0,dataPacket.IndexOf("START"));

            var split = dataPacket.Split(' ');

            if(split[1] == "0")
            {
                dial_press = true;
            }

            int.TryParse(split[2],out encoder);

            int.TryParse(split[3],out dial);

            if(split[7] == "1")
            {
                simon = true;
            } else
            {
                simon = false;
            }

            int.TryParse(split[8],out potion);

            lastMSG = dataPacket;
            pollUpdate = Time.time;

            sendPoll();
        }

        serialPort.DiscardInBuffer();
    }


    public int getRottery_counter()
    {
        return RotteryE_counter - RotteryE_offset;
    }

    public void resetRottery_counter()
    {
        RotteryE_offset = RotteryE_counter;
    }

    

    public void sendPoll()
    {

        byte[] pollBuffer0 = new byte[18];
        byte[] pollBuffer1 = new byte[18];
        byte[] pollBuffer2 = new byte[18];
        
        //LCD_line1.ToCharArray().CopyTo(pollBuffer,0);
        
        LCD_line1 =  LCD_line1.PadRight(16,' ');
        LCD_line2 =  LCD_line2.PadRight(16,' ');

        for (int i = 0; i < 16; i++)
        {
            pollBuffer0[i] = (byte)LCD_line1.ToCharArray()[i];
        }

        for (int i = 0; i < 16; i++)
        {
            pollBuffer1[i] = (byte)LCD_line2.ToCharArray()[i];
        }


        byte[] pollb2 = new byte[2];
        //Package 1
        serialPort.Write(pollBuffer0,0,16);
        pollb2[0] = 0;
        serialPort.Write(pollb2,0,2);

        //Packages 2
        serialPort.Write(pollBuffer1,0,16);
        pollb2[0] = 1;
        serialPort.Write(pollb2,0,2);

        //Package 3
        pollBuffer2[0] = timer;
        pollBuffer2[1] = game_state;

        serialPort.Write(pollBuffer2,0,16);
        
        pollb2[0] = 2;
        serialPort.Write(pollb2,0,2);





    }



}
