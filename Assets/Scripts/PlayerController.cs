using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public Rigidbody2D rb;
    public AudioClip jumpSound;
    
    // 4 colors with hex values - 00FF15, E01F1F, 0001FF, FF7B00
    public static Color Green = new Color32(0x00, 0xFF, 0x15, 0xFF);
    public static Color Red = new Color32(0xE0, 0x1F, 0x1F, 0xFF);
    public static Color Blue = new Color32(0x00, 0x01, 0xFF, 0xFF);
    public static Color Orange = new Color32(0xFF, 0x7B, 0x00, 0xFF);

    private void Awake()
    {
        Color[] colors = {Green, Red, Blue, Orange};
        Color randomColor = colors[Random.Range(0, colors.Length)];
        GetComponent<SpriteRenderer>().color = randomColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpForce;
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }
    }
}
