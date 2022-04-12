using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Networking
{
    public enum PunEvent
    {
        NormalMoveEvent = 1,
        KillMoveEvent,
        ShowChatEvent,
    }

    public abstract class PunEventData
    {
        protected RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All
        };

        protected object[] eventContent;

        protected void Invoke(PunEvent eventCode)
        {
            PhotonNetwork.RaiseEvent((byte)eventCode, eventContent, raiseEventOptions, SendOptions.SendReliable);
        }
    }

    public class MoveEventData : PunEventData
    {
        public Vector2 origLoc;
        public Vector2 destLoc;

        public MoveEventData(EventData photonEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            origLoc = (Vector2)data[0];
            destLoc = (Vector2)data[1];
        }

        public MoveEventData(Vector2 origional, Vector2 destination)
        {
            origLoc = origional;
            destLoc = destination;
        }
    }

    public class NormalMoveEventData : MoveEventData
    {
        public NormalMoveEventData(EventData photonEvent) : base(photonEvent) { }
        public NormalMoveEventData(Vector2 origional, Vector2 destination) : base(origional, destination) { }

        public void Invoke()
        {
            eventContent = new object[] { origLoc, destLoc };
            base.Invoke(PunEvent.NormalMoveEvent);
        }
    }

    public class KillMoveEventData : MoveEventData
    {
        public KillMoveEventData(EventData photonEvent) : base(photonEvent) { }
        public KillMoveEventData(Vector2 origional, Vector2 destination) : base(origional, destination) { }

        public void Invoke()
        {
            eventContent = new object[] { origLoc, destLoc };
            base.Invoke(PunEvent.KillMoveEvent);
        }
    }

    public class ShowChatEventData : PunEventData
    {
        public string chatText;

        public ShowChatEventData(string chatMsg)
        {
            chatText = chatMsg;
        }

        public ShowChatEventData(EventData photonEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            chatText = (string)data[0];
        }

        public void Invoke()
        {
            eventContent = new object[] { chatText };
            base.Invoke(PunEvent.ShowChatEvent);
        }
    }
}