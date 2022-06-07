using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;

public class UDPReceive : MonoBehaviour
{
    private Thread receiveThread = null;

    private UdpClient client;

    private int port = 8051;
    bool alive = false;

    public static ConcurrentQueue<String> actionQueue { get; private set; }

    public void OnApplicationQuit()
    {
        try
        {
            receiveThread.Abort();
        }
        catch
        {
            print("thread cannot be aborted");

        }
        print("thread aborted");
    }
    private void Update()
    {
        if (!alive)
        {
            try
            {
                if (receiveThread != null)
                    receiveThread.Abort();
            }
            catch (Exception e)
            {
                print(e);
                alive = false;
            }
            try
            {
                actionQueue = new ConcurrentQueue<String>();
                receiveThread = new Thread(new ThreadStart(ReceiveData));
                receiveThread.IsBackground = true;
                receiveThread.Start();
                alive = receiveThread.IsAlive;
            }
            catch (Exception e)
            {
                alive = false;
                print(e);
            }
        }

    }

    private void ReceiveData()
    {
        try
        {
            IPEndPoint anyIP;
            byte[] data;
            client = new UdpClient(port);
            while (true)
            {
                try
                {
                    anyIP = new IPEndPoint(IPAddress.Any, 0);
                    data = client.Receive(ref anyIP);
                    string text = Encoding.UTF8.GetString(data);
                    actionQueue.Enqueue(text);
                }
                catch (Exception err)
                {
                    Debug.LogError(err.ToString());
                    alive = false;
                    return;
                }
            }
        }
        catch (Exception err)
        {
            Debug.LogError(err.ToString());
            alive = false;
            return;
        }
    }
}
