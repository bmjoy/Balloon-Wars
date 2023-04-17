using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameMenuBackButton : MonoBehaviour
{
    [SerializeField] Animator m_Animator;
    [SerializeField] SceneNavigator m_SceneNavigator;
    [SerializeField] PhotonRoomsConnector m_PhotonRoomsConnector;
    public enum e_MenuState {MainMenu, CreateRoomMenu, JoinRoomMenu, CreatedRoomDetails, JoinedRoomDetails};
    private Button m_Button;
    Dictionary<e_MenuState, UnityAction> m_MenuStateBackActions;
    void Start()
    {
        m_Button = GetComponent<Button>();
        initMenuStateBackActions();
        ChangeButtonBackAction(e_MenuState.MainMenu);
    }

    private void ChangeButtonBackAction(e_MenuState menuState)
    {
        m_Button.onClick.RemoveAllListeners();
        m_Button.onClick.AddListener(m_MenuStateBackActions[menuState]);
    }

    public void setMenuStateMainMenu()
    {
        ChangeButtonBackAction(e_MenuState.MainMenu);    
    }
    public void setMenuStateCreateRoomMenu()
    {
        ChangeButtonBackAction(e_MenuState.CreateRoomMenu);
    }
    public void setMenuStateJoinRoomMenu()
    {
        ChangeButtonBackAction(e_MenuState.JoinRoomMenu);
    }
    public void setMenuStateCreatedRoomDetails()
    {
        ChangeButtonBackAction(e_MenuState.CreatedRoomDetails);
    }
    public void setMenuStateJoinedRoomDetails()
    {
        ChangeButtonBackAction(e_MenuState.JoinedRoomDetails);
    }

    private void initMenuStateBackActions()
    {
        m_MenuStateBackActions = new Dictionary<e_MenuState, UnityAction>
        {
            { e_MenuState.MainMenu, () => {
                m_PhotonRoomsConnector.LeavePhotonLobby();
                m_SceneNavigator.MoveToMainMenu();}},
            { e_MenuState.CreateRoomMenu, () => {
                m_Animator.SetTrigger("CreateRoomExit");
                setMenuStateMainMenu();
                m_PhotonRoomsConnector.restartLoby();} },
            { e_MenuState.JoinRoomMenu, () => {
                m_Animator.SetTrigger("JoinRoomExit");
                setMenuStateMainMenu();
                m_PhotonRoomsConnector.restartLoby();} },
            { e_MenuState.CreatedRoomDetails, () => {
                m_PhotonRoomsConnector.LeavePhotonRoom();
                setMenuStateCreateRoomMenu();
                m_Animator.SetTrigger("DetailsToCreateRoom");} },
            { e_MenuState.JoinedRoomDetails, () => {
                m_PhotonRoomsConnector.LeavePhotonRoom();
                setMenuStateJoinRoomMenu();
                m_Animator.SetTrigger("DetailsToJoinRoom");} }
        };
    }
}
