namespace Cebv.app.data;

public class BroadCast
{
    public static void Message(string message)
    {
        if (OnMessageTransmitted != null)
            OnMessageTransmitted(message);
    }

    public static Action<string> OnMessageTransmitted;
}