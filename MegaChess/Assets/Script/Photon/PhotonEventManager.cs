using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public enum PunEvent
    {
        NormalMoveEvent = 1,
        KillMoveEvent
    }

    public class MoveEventData
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

    public class NormalMoveEvent : MoveEventData
    {
        public NormalMoveEvent(EventData photonEvent) : base(photonEvent) { }
        public NormalMoveEvent(Vector2 origional, Vector2 destination) : base(origional, destination) { }

        public void Invoke()
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            object[] content = new object[] { origLoc, destLoc };
            PhotonNetwork.RaiseEvent((byte)PunEvent.NormalMoveEvent, content, raiseEventOptions, SendOptions.SendReliable);
        }
    }

    public class KillMoveEvent : MoveEventData
    {
        public KillMoveEvent(EventData photonEvent) : base(photonEvent) { }
        public KillMoveEvent(Vector2 origional, Vector2 destination) : base(origional, destination) { }

        public void Invoke()
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            object[] content = new object[] { origLoc, destLoc };
            PhotonNetwork.RaiseEvent((byte)PunEvent.KillMoveEvent, content, raiseEventOptions, SendOptions.SendReliable);
        }
    }



}