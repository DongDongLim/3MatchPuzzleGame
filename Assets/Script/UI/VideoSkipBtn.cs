using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSkipBtn : MonoBehaviour
{

    private VideoPlayer m_Video;

    private void Awake()
    {
        m_Video = GetComponentInChildren<VideoPlayer>();
        m_Video.loopPointReached += DeActive;
    }

    public void DeActive(VideoPlayer vp)
    {
        gameObject.SetActive(false);
    }
}