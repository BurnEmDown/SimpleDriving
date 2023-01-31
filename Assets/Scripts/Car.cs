using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedGainPerSecond = 0.5f;
    [SerializeField] private float turnSpeed = 200f;

    private int steerValue;
    
    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        
        transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);

        speed += speedGainPerSecond * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Steer(int value)
    {
        steerValue = value;
    }
}
