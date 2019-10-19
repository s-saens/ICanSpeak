using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace kr.ac.se.pqs.ICS {
    public class PublicICS : MonoBehaviourPunCallbacks {
        #region Private Serializable Fields

        /// <summary>
        /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        /// </summary>
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        #endregion

        #region Private Fields

        string appVersion = "1";

        #endregion

        #region MonoBehaviour CallBacks

        private void Awake() {
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        // Start is called before the first frame update
        void Start() {
            Connect();
        }

        #endregion

        #region MonoBegaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster() {
            PhotonNetwork.JoinRandomRoom();
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        }

        public override void OnJoinRandomFailed(short returnCode, string message) {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions{ MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom() {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        }

        public override void OnDisconnected(DisconnectCause cause) {
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }

        #endregion

        #region Public Methods
        public void Connect() {
            if(PhotonNetwork.IsConnected) {
                PhotonNetwork.JoinRandomRoom();
                Debug.Log("asd");
            } else {
                PhotonNetwork.GameVersion = appVersion;
                PhotonNetwork.ConnectUsingSettings();   // Photon Cloud에 연결되는 시작 지점
            }
        }
        #endregion
    }
}

