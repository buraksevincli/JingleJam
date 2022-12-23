using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject[] reindeers;
    GameObject frontReindeer;

    Vector3 dir1;
    Vector3 dir2;
    Vector3 dir3;

    public float rotSpeed = 40;
    float angle = 0f;

    public int health = 3;

    private void Start()
    {
        frontReindeer = reindeers[2];
    }

    // Update is called once per frame
    void Update()
    {
        if(health != 0)
        {
            if (health > 0)
            {
                float maxY1 = Mathf.Abs(reindeers[0].transform.position.y - transform.position.y); // �n�ndeki karakterin y ekseni ile kendi y ekseni aras�ndaki fark�n mutlak de�erini buluyor. Bu de�eri karakter �n�ndekinden ne kadar uzaksa o kadar h�zl� hareket etsin diye atad�k.
                dir1 = (new Vector3(transform.position.x, reindeers[0].transform.position.y, 0f) - transform.position).normalized;
                transform.position = transform.position + (dir1 / 75f * maxY1 * Time.deltaTime); // Karakter y ekseninde hareket ediyor. Ee�er �n�ndeki karakterin y ekseni ile e�it ise duruyor.
                transform.position = new Vector3(reindeers[0].transform.position.x - 1f, transform.position.y, 0f); // Karakterin �n�ndeki karakter ile mesafesini koruyor.
            }

            if (health > 1)
            {
                float maxY2 = Mathf.Abs(reindeers[1].transform.position.y - reindeers[0].transform.position.y);
                dir2 = (new Vector3(reindeers[0].transform.position.x, reindeers[1].transform.position.y, 0f) - reindeers[0].transform.position).normalized;
                reindeers[0].transform.position = reindeers[0].transform.position + (dir2 / 75f * maxY2 * Time.deltaTime);
                reindeers[0].transform.position = new Vector3(reindeers[1].transform.position.x - 0.8f, reindeers[0].transform.position.y, 0f);
            }

            if (health > 2)
            {
                float maxY3 = Mathf.Abs(reindeers[2].transform.position.y - reindeers[1].transform.position.y);
                dir3 = (new Vector3(reindeers[1].transform.position.x, reindeers[2].transform.position.y, 0f) - reindeers[1].transform.position).normalized;
                reindeers[1].transform.position = reindeers[1].transform.position + (dir3 / 75f * maxY3 * Time.deltaTime);
                reindeers[1].transform.position = new Vector3(reindeers[2].transform.position.x - 0.8f, reindeers[1].transform.position.y, 0f);
            }


            //frontReindeer.transform.position += frontReindeer.transform.TransformDirection(Vector3.right).normalized / 500f;
            frontReindeer = reindeers[health - 1];

            if (Input.GetKey(KeyCode.UpArrow))
            {
                angle += rotSpeed * Time.deltaTime;
                frontReindeer.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                frontReindeer.transform.position = new Vector3(frontReindeer.transform.position.x, frontReindeer.transform.position.y + 2f * Time.deltaTime, 0f);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                angle -= rotSpeed * Time.deltaTime;
                frontReindeer.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                frontReindeer.transform.position = new Vector3(frontReindeer.transform.position.x, frontReindeer.transform.position.y - 2f * Time.deltaTime, 0f);
            }
            else
            {
                if (angle > 0.1)
                {
                    angle -= rotSpeed * Time.deltaTime;
                    frontReindeer.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                }
                else if (angle < -0.1)
                {
                    angle += rotSpeed * Time.deltaTime;
                    frontReindeer.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                }
                else
                {
                    angle = 0;
                    frontReindeer.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                }

            }

            // Buras� objelerin bir �n�ndeki objeye bakmas�n� sa�l�yo.
            if (health > 0)
            {
                transform.right = reindeers[0].transform.position - transform.position;
            }
            if (health > 1)
            {
                reindeers[0].transform.right = reindeers[1].transform.position - reindeers[0].transform.position;
            }
            if (health > 2)
            {
                reindeers[1].transform.right = reindeers[2].transform.position - reindeers[1].transform.position;
            }

            angle = Mathf.Clamp(angle, -17f, 17f);

            if (health == 3)
            {
                reindeers[2].SetActive(true);
                reindeers[1].SetActive(true);
                reindeers[0].SetActive(true);
            }
            else if (health == 2)
            {
                reindeers[2].SetActive(false);
                reindeers[1].SetActive(true);
                reindeers[0].SetActive(true);
                reindeers[2].transform.position = new Vector3(reindeers[1].transform.position.x + 0.8f, reindeers[1].transform.position.y, 0f);
            }
            else if (health == 1)
            {
                reindeers[2].SetActive(false);
                reindeers[1].SetActive(false);
                reindeers[0].SetActive(true);
                reindeers[2].transform.position = new Vector3(reindeers[1].transform.position.x + 0.8f, reindeers[1].transform.position.y, 0f);
                reindeers[1].transform.position = new Vector3(reindeers[0].transform.position.x + 0.8f, reindeers[0].transform.position.y, 0f);
            }
        }
        else
        {
            reindeers[2].SetActive(false);
            reindeers[1].SetActive(false);
            reindeers[0].SetActive(false);
            reindeers[2].transform.position = new Vector3(reindeers[1].transform.position.x + 0.8f, reindeers[1].transform.position.y, 0f);
            reindeers[1].transform.position = new Vector3(reindeers[0].transform.position.x + 0.8f, reindeers[0].transform.position.y, 0f);
            reindeers[0].transform.position = new Vector3(transform.position.x + 1f, transform.position.y, 0f);
        }


    }

    

}
