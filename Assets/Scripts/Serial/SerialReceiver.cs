using UnityEngine;
using System.IO.Ports;

public class SerialReceiver : MonoBehaviour
{
    [Header("Serial Info")]
    [SerializeField] private string portName;
    [SerializeField] private int baudRate;
}
