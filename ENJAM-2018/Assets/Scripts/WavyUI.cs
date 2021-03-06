﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavyUI : MonoBehaviour
{

    public float Frequency = 8.0f;
    public float Magnitude = 1.5f;
    public float Offset;
    private Vector3 m_startPos;

    // Use this for initialization
    void Start()
    {
        m_startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        float y = Mathf.Sin((Offset + Time.time) * Frequency) * Magnitude;
        transform.localPosition = m_startPos + new Vector3(0.0f, y, 0.0f);

    }
}
