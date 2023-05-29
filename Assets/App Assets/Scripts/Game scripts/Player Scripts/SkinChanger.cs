using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using System.Linq;

public class SkinChanger : MonoBehaviour
{
    private List<SpriteResolver> m_Resolvers;

    private void Awake()
    {
        m_Resolvers = GetComponentsInChildren<SpriteResolver>().ToList();    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
