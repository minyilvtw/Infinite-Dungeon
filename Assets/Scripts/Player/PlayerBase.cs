using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{

    public bool TestMode;

    SaveManager m_SaveManager;
    public SaveManager StatusSave {
        get
        {
            if (m_SaveManager == null)
            {
                m_SaveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
            }

            return m_SaveManager;
        }
    }

    LevelManager m_LevelManager;
    public LevelManager LevelManager
    {
        get
        {
            if (m_LevelManager == null)
            {
                 m_LevelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
            }

            return m_LevelManager;
        }

    }

    PlayerMovement m_Movement;
    public PlayerMovement PlayerMovement
    {
        get
        {
            if (m_Movement == null)
            {
                m_Movement = GetComponent<PlayerMovement>();
            }

            return m_Movement;
        }

    }

    PlayerStatus m_Status;
    public PlayerStatus PlayerStatus
    {
        get
        {
            if (m_Status == null)
            {
                m_Status = GetComponent<PlayerStatus>();
            }

            return m_Status;
        }

    }
    
    PlayerAnimation m_Animation;
    public PlayerAnimation PlayerAnimation {
        get {
            if (m_Animation == null) {
                m_Animation = GetComponent<PlayerAnimation>();
            }

            return m_Animation;
        }
    }

    InventoryController m_Inventory;
    public InventoryController InventoryController
    {
        get
        {
            if (m_Inventory == null)
            {
                m_Inventory = GetComponent<InventoryController>();
            }

            return m_Inventory;
        }
    }

    PlayerSFX m_playerSFX;
    public PlayerSFX playerSFX
    {
        get
        {
            if (m_playerSFX == null)
            {
                m_playerSFX = GetComponent<PlayerSFX>();
            }

            return m_playerSFX;
        }
    }

}
