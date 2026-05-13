using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialReceiver : MonoBehaviour
{
    public static SerialReceiver Instance { get; private set; }

    [Header("Serial Info")]
    [SerializeField] private string portName;
    [SerializeField] private int baudRate;
    [SerializeField] private SpriteRenderer testSprite;

    private SerialPort _serialPort;
    private string receivedStringMessage;
    private bool _continue;

    private Thread readThread;

    private int spawnInput;
    private int predatorInput;
    private int preyInput;
    private int randomInput;

    private enum PortIndex
    {
        Spawn,
        Prey,
        Predator,
        Random
    }

    private void Awake()
    {
        Instance = this;   
    }

    private void Start()
    {
        _continue = false;

        readThread = new Thread(Read);

        _serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);

        if (_serialPort.IsOpen)
        {
            _serialPort.Close();
        }

        _serialPort.ReadTimeout = 500;

        _serialPort.Open();
        _continue = true;
        readThread.Start();
    }

    private void Update()
    {
        if (string.IsNullOrEmpty(receivedStringMessage)) return;

        string[] decomposeMessage = receivedStringMessage.Split(',');

        if (decomposeMessage.Length < 4) return;
        
        spawnInput = Convert.ToInt32(decomposeMessage[(int)PortIndex.Spawn]);
        preyInput = Convert.ToInt32(decomposeMessage[(int)PortIndex.Prey]);
        predatorInput = Convert.ToInt32(decomposeMessage[(int)PortIndex.Predator]);
        randomInput = Convert.ToInt32(decomposeMessage[(int)PortIndex.Random]);
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

    private void OnDisable()
    {
        _continue = false;
        if (_serialPort != null && _serialPort.IsOpen)
        {
            _serialPort.Close();
        }
    }

    public bool IsSpawnInputHeld()
    {
        return spawnInput == 1;
    }

    public bool IsRandomInputPressed()
    {
        return randomInput == 1;
    }

    public bool IsPreyInputPressed()
    {
        return preyInput == 1;
    }

    public bool IsPredatorInputPressed()
    {
        return predatorInput == 1;
    }
}
