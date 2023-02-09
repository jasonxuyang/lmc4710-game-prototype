using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using System;

/*
 * Player Network: Manages Player Data across all clients 
 */
public class PlayerNetwork : NetworkBehaviour
{ 
    [SerializeField]
    private NetworkVariable<PlayerNetworkData> state = new(writePerm: NetworkVariableWritePermission.Owner);

    // Variables for Movement Interpolation
    private Vector2 velocity = Vector2.zero;
    [SerializeField] private float smoothTime = 0.01f;

    // Update
    void Update()
    {
        //// Owner Player Updates (Writing to state)
        //if (IsOwner)
        //{
        //    state.Value = new PlayerNetworkData()
        //    {
        //        Position = transform.position // transform.position // add more using a comma -> ,
        //    };
        //}

        //// Other Player Updates (Reading from state)
        //else
        //{
        //    transform.position = Vector2.SmoothDamp(transform.position, state.Value.Position, ref velocity, smoothTime);
        //}
    }

    // Player Network Data
    struct PlayerNetworkData : INetworkSerializable
    {
        // Attributes
        private float _x, _y;

        internal Vector2 Position
        {
            get => new Vector2(_x, _y);
            set
            {
                _x = value.x;
                _y = value.y;
            }
        }

        // Serializing
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _x);
            serializer.SerializeValue(ref _y);
        }
    }
}
