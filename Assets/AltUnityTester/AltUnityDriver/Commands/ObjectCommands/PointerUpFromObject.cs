public class PointerUpFromObjectDriver : CommandReturningAltElement
{
    AltUnityObject altUnityObject;

    public PointerUpFromObjectDriver(SocketSettings socketSettings, AltUnityObject altUnityObject) : base(socketSettings)
    {
        this.altUnityObject = altUnityObject;
    }
    public AltUnityObject Execute(){
        string altObject = Newtonsoft.Json.JsonConvert.SerializeObject(altUnityObject);
        Socket.Client.Send( System.Text.Encoding.ASCII.GetBytes(CreateCommand("pointerUpFromObject", altObject )));
        return ReceiveAltUnityObject();
    }
}