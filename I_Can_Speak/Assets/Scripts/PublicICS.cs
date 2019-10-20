using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

namespace kr.ac.se.pqs.ICS {
    public class PublicICS : MonoBehaviourPunCallbacks {
        #region Private Serializable Fields
        /// <summary>
        /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        /// </summary>
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        [Tooltip("The Ui Panel to let the user enter name, connect and play")]
        [SerializeField]
        private GameObject controlPanel;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;

        [SerializeField]
        private GameObject initPanel;
        [SerializeField]
        private GameObject channelPanel;
        [SerializeField]
        private GameObject settingPanel;
        [SerializeField]
        private GameObject selectionPanel;

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

        private void Start() {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }
        #endregion

        #region MonoBegaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster() {
            //PhotonNetwork.JoinRandomRoom();
            progressLabel.SetActive(false);
            initPanel.SetActive(false);
            channelPanel.SetActive(true);
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        }

        public override void OnJoinRandomFailed(short returnCode, string message) {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions{ MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom() {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
            SceneManager.LoadScene("ConferenceRoom_001");
        }

        public override void OnDisconnected(DisconnectCause cause) {
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
            initPanel.SetActive(true);
            channelPanel.SetActive(false);
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        #endregion

        #region Public Methods
        public void Connect() {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            if (PhotonNetwork.IsConnected) {
                progressLabel.SetActive(false);
                initPanel.SetActive(false);
                channelPanel.SetActive(true);
                //PhotonNetwork.JoinRandomRoom();
            } else {
                PhotonNetwork.GameVersion = appVersion;
                PhotonNetwork.ConnectUsingSettings();   // Photon Cloud에 연결되는 시작 지점
            }
        }

        public void RoomConnect() {
            initPanel.SetActive(false);
            channelPanel.SetActive(true);
            PhotonNetwork.JoinRandomRoom();
        }
        #endregion
    }
}

