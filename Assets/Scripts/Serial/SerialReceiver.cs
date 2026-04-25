using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialReceiver : MonoBehaviour
{
    [Header("Serial Info")]
    [SerializeField] private string portName;
    [SerializeField] private int baudRate;
    [SerializeField] private SpriteRenderer testSprite;

    private SerialPort _serialPort;
    private string receivedStringMessage;
    private bool _continue;

    private Thread readThread;

    private int spawnTriggerButton;

    private void Start()
    {
        readThread = new Thread(Read);

        _serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);

        _serialPort.ReadTimeout = 500;

        _serialPort.Open();
        _continue = true;
        readThread.Start();
    }

    private void Update()
    {
        Debug.Log(receivedStringMessage);

        spawnTriggerButton = 
            receivedStringMessage != null && 
            receivedStringMessage != "" ? 
            Convert.ToInt32(receivedStringMessage) : 0;
    }

    private void Read()
    {
        while (_continue)
        {
            try
            {
                receivedStringMessage = _serialPort.ReadLine();
            }
            catch (TimeoutException) { }
            catch (ObjectDisposedException) { break; }
            catch (IOException) { break; }
        }
    }

    private void OnApplicationQuit()
    {
        _continue = false;
        _serialPort.Close();
    }

    public bool IsTriggerSpawnButton()
    {
        return spawnTriggerButton == 1;
    }
}
