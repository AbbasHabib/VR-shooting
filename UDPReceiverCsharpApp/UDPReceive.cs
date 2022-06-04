using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;

public class UDPReceive : MonoBehaviour
{
    private Thread receiveThread;

    private UdpClient client;

    [SerializeField]
    private int port = 8051;

    public static ConcurrentQueue<String> actionQueue { get; private set; }

    public void Start()
    {
        init();
    }

    private void init()
    { 
        actionQueue =  new ConcurrentQueue<String>();
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        IPEndPoint anyIP;
        byte[] data;
        client = new UdpClient(port);
        try
        {
            anyIP = new IPEndPoint(IPAddress.Any, 0);
            data = client.Receive(ref anyIP);
            string text = Encoding.UTF8.GetString(data);
            actionQueue.Enqueue(text);
        }
        catch (Exception err)
        {
            print(err.ToString());
            return;
        }
        while (true)
        {
            data = client.Receive(ref anyIP);
            string text = Encoding.UTF8.GetString(data);
            actionQueue.Enqueue(text);
        }
    }
}
