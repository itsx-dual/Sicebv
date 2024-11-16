namespace Cebv.app.data;

public class BroadCast
{
    public static void Message(string message)
    {
        OnMessageTransmitted(message);
    }

    public static Action<string> OnMessageTransmitted = delegate { };
}