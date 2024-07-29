/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Threading;

/**
 * This class allows a Unity program to continually check for messages from a
 * serial device.
 *
 * It creates a Thread that communicates with the serial port and continually
 * polls the messages on the wire.
 * That Thread puts all the messages inside a Queue, and this SerialController
 * class polls that queue by means of invoking SerialThread.GetSerialMessage().
 *
 * The serial device must send its messages separated by a newline character.
 * Neither the SerialController nor the SerialThread perform any validation
 * on the integrity of the message. It's up to the one that makes sense of the
 * data.
 */
public class SerialController : MonoBehaviour
{/*
    // public GameObject rotatorXY;
    public Material mat1;
    public Material mat2;
    public GameObject refObj;
    Vector3 tempPos = new Vector3(0, 0, 0);
    Quaternion tempRot = Quaternion.Euler(0, 0, 0);
    //float speed = 500;
    float[] posQueue = new float[6];
    public float rotX = 0;
    public float rotY = 0;
    public float rotZ = 0;
    public float localPos = 0;
    public bool laserOn;
    public bool connected;

    *//*public float XRot
    {
        get => rotX;
        set
        {
            rotX = value;
        }
    }

    public float YRot
    {
        get => rotY;
        set
        {
            rotY = value;
        }
    }*//*

    [Tooltip("Port name with which the SerialPort object will be created.")]
    public string portName = "COM3";

    [Tooltip("Baud rate that the serial device is using to transmit data.")]
    public int baudRate = 9600;

    [Tooltip("Reference to an scene object that will receive the events of connection, " +
             "disconnection and the messages from the serial device.")]
    public GameObject messageListener;

    [Tooltip("After an error in the serial communication, or an unsuccessful " +
             "connect, how many milliseconds we should wait.")]
    public int reconnectionDelay = 100;

    [Tooltip("Maximum number of unread data messages in the queue. " +
             "New messages will be discarded.")]
    public int maxUnreadMessages = 1;

    // Constants used to mark the start and end of a connection. There is no
    // way you can generate clashing messages from your serial device, as I
    // compare the references of these strings, no their contents. So if you
    // send these same strings from the serial device, upon reconstruction they
    // will have different reference ids.
    public const string SERIAL_DEVICE_CONNECTED = "__Connected__";
    public const string SERIAL_DEVICE_DISCONNECTED = "__Disconnected__";

    // Internal reference to the Thread and the object that runs in it.
    protected Thread thread;
    protected SerialThreadLines serialThread;


    // ------------------------------------------------------------------------
    // Invoked whenever the SerialController gameobject is activated.
    // It creates a new thread that tries to connect to the serial device
    // and start reading from it.
    // ------------------------------------------------------------------------
    void OnEnable()
    {
        serialThread = new SerialThreadLines(portName,
                                             baudRate,
                                             reconnectionDelay,
                                             maxUnreadMessages);
        thread = new Thread(new ThreadStart(serialThread.RunForever));
        thread.Start();
    }

    // ------------------------------------------------------------------------
    // Invoked whenever the SerialController gameobject is deactivated.
    // It stops and destroys the thread that was reading from the serial device.
    // ------------------------------------------------------------------------
    void OnDisable()
    {
        // If there is a user-defined tear-down function, execute it before
        // closing the underlying COM port.
        if (userDefinedTearDownFunction != null)
            userDefinedTearDownFunction();

        // The serialThread reference should never be null at this point,
        // unless an Exception happened in the OnEnable(), in which case I've
        // no idea what face Unity will make.
        if (serialThread != null)
        {
            serialThread.RequestStop();
            serialThread = null;
        }

        // This reference shouldn't be null at this point anyway.
        if (thread != null)
        {
            thread.Join();
            thread = null;
        }
    }

    public float[] MovingAverage(float[] data, int size)
    {
        float[] filter = new float[data.Length];
        for (int i = size / 2; i < data.Length - size / 2; i++)
        {
            float mean = 0;
            for (var j = -size / 2; j < size / 2; j++)
                mean += data[i + j];

            filter[i] = mean / size;
        }
        return filter;
    }

    // ------------------------------------------------------------------------
    // Polls messages from the queue that the SerialThread object keeps. Once a
    // message has been polled it is removed from the queue. There are some
    // special messages that mark the start/end of the communication with the
    // device.
    // ------------------------------------------------------------------------
    void Update()
    {
        // If the user prefers to poll the messages instead of receiving them
        // via SendMessage, then the message listener should be null.
        if (messageListener == null)
            return;

        // Read the next message from the queue
        string message = (string)serialThread.ReadMessage();
        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SERIAL_DEVICE_CONNECTED))
        {
            messageListener.SendMessage("OnConnectionEvent", true);
            connected = true;
        }
        else if (ReferenceEquals(message, SERIAL_DEVICE_DISCONNECTED))
        {
            messageListener.SendMessage("OnConnectionEvent", false);
            connected = false;
        }
        else
        {
            messageListener.SendMessage("OnMessageArrived", message);
        }

        //Debug.Log("Message arrived: " + message);
        string phrase = message;
        string[] text = phrase.Split(' ');
        float[] words = new float[5];
        float words2;

        for (int x = 0; x < text.Length; x++)
        {
            //words[x] = float.Parse(text[x]);
            float.TryParse(text[x],out words2);
            words[x] = words2;
        }

        for (int i = 0; i < posQueue.Length; i++)
        {
            if (i != 5)
            {
                posQueue[i] = posQueue[i + 1];
            }
            else
            {
                posQueue[5] = words[3];
            }
        }
        //posQueue = MovingAverage(posQueue, posQueue.Length);

            rotX = words[1];
            rotY = words[2];

        localPos = words[3];
        if (words[0] == 1)
        {
            laserOn = true;
        }
        else
        {
            laserOn = false;
        }
        rotZ = words[4];
        //laser on/off toggle
       *//* if (words[0] == 1)
        {
            MeshRenderer meshRenderer = rotatorXY.GetComponent<MeshRenderer>();
            Material oldMaterial = meshRenderer.material;
            meshRenderer.material = mat1;
        }
        else
        {
            MeshRenderer meshRenderer = rotatorXY.GetComponent<MeshRenderer>();
            Material oldMaterial = meshRenderer.material;
            meshRenderer.material = mat2;
        }

        //refObj.transform.SetPositionAndRotation(0);
        Quaternion newRot = Quaternion.Euler(words[1], 0, words[2]);
        //rotatorXY.transform.Rotate(words[1], words[2], 0); //needs proper scaling to not spaz tf out
        var step = speed * Time.deltaTime;
        rotatorXY.transform.rotation = newRot;
        //rotatorXY.transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, step);    //using x and z here for visibility

        rotatorXY.transform.RotateAround(refObj.transform.position, Vector3.up, words[1] * speed * Time.deltaTime);
        rotatorXY.transform.RotateAround(refObj.transform.position, Vector3.right, words[2] * speed * Time.deltaTime);
        rotatorXY.transform.rotation = Quaternion.LookRotation(rotatorXY.transform.position - refObj.transform.position);

        float newPos = posQueue[3] / 10;
        Vector3 tPos = new Vector3(0, newPos, 0);
        //lerp from current to tPos
        rotatorXY.transform.position = tPos;

        /* if (words[1] == "right")
         {
             rotatorXY.transform.Rotate(float.Parse(words[2]), 0, 0);
         }
         else if (words[1] == "left")
         {
             rotatorXY.transform.Rotate(float.Parse(words[2]), 0, 0);
         }
         else if (words[1] == "up")
         {
             rotatorXY.transform.Rotate(0,float.Parse(words[2]), 0);
         }
         else if (words[1] == "down")
         {
             rotatorXY.transform.Rotate(0, float.Parse(words[2]), 0);
         }
         else
         {
             return;
         }
        

        if (Input.GetKey("w"))
        {
            Vector3 position = rotatorXY.transform.position;
            position.x++;
            rotatorXY.transform.position = position;
        }
        if (Input.GetKey("s"))
        {
            /*Vector3 position = rotatorXY.transform.position;
            position.x--;
            rotatorXY.transform.position = position;
        }
        

        if (Input.GetMouseButton(0))
        {
            MeshRenderer meshRenderer = rotatorXY.GetComponent<MeshRenderer>();
            Material oldMaterial = meshRenderer.material;
            meshRenderer.material = mat1;
        }
        else
        {
            MeshRenderer meshRenderer = rotatorXY.GetComponent<MeshRenderer>();
            Material oldMaterial = meshRenderer.material;
            meshRenderer.material = mat2;
        }*//*
    }

    // ------------------------------------------------------------------------
    // Returns a new unread message from the serial device. You only need to
    // call this if you don't provide a message listener.
    // ------------------------------------------------------------------------
    public string ReadSerialMessage()
    {
        // Read the next message from the queue
        return (string)serialThread.ReadMessage();
    }

    // ------------------------------------------------------------------------
    // Puts a message in the outgoing queue. The thread object will send the
    // message to the serial device when it considers it's appropriate.
    // ------------------------------------------------------------------------
    public void SendSerialMessage(string message)
    {
        serialThread.SendMessage(message);
    }

    // ------------------------------------------------------------------------
    // Executes a user-defined function before Unity closes the COM port, so
    // the user can send some tear-down message to the hardware reliably.
    // ------------------------------------------------------------------------
    public delegate void TearDownFunction();
    private TearDownFunction userDefinedTearDownFunction;
    public void SetTearDownFunction(TearDownFunction userFunction)
    {
        this.userDefinedTearDownFunction = userFunction;
    }
*/
}
