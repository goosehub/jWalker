﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCar : MonoBehaviour
{
    public float frequencyMin;
    public float frequencyMax;
    public float speed;
    public Vector3 startingPosition;
    public Quaternion startingRotation;

    // Use this for initialization
    void Start ()
    {
        startingPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        startingRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);

        StartCoroutine(startSpawn());
    }

    // Update is called once per frame
    IEnumerator startSpawn()
    {
        while (true)
        {
            float respawnDelay = Random.Range(frequencyMin, frequencyMax);
            yield return new WaitForSeconds(respawnDelay);
            Spawn();
        }
    }

    void Spawn()
    {
        Rigidbody currentCarInstance = GameObject.Find(randomCar()).GetComponent<Rigidbody>();
        Rigidbody newCarInstance = Instantiate(currentCarInstance, startingPosition, startingRotation);
        StartCoroutine(driveCar(newCarInstance));
    }

    // Update is called once per frame
    IEnumerator driveCar(Rigidbody newCarInstance)
    {
        while (true)
        {
            float driveFrequency = 0.1f;
            yield return new WaitForSeconds(driveFrequency);
            newCarInstance.velocity = transform.forward * (speed);
            cleanupCar(newCarInstance);
        }
    }

    void cleanupCar(Rigidbody newCarInstance)
    {
        if (
            newCarInstance.position.z > 1200
            || newCarInstance.position.z < -1200
            || newCarInstance.position.x > 1200
            || newCarInstance.position.x < -1200
            )
        {
            if (newCarInstance.name.Contains("Clone"))
            {
                Destroy(newCarInstance.gameObject);
            }
        }
    }

    string randomCar()
    {
        List<string> carList = new List<string>();
        carList.Add("cop");
        carList.Add("cityBus");
        carList.Add("redTruck");
        carList.Add("yellowTruck");
        carList.Add("garbageTruck");
        carList.Add("sportsCar");
        carList.Add("schoolBus");
        carList.Add("taxi");
        carList.Add("ambulance");
        carList.Add("iceCreamTruck");
        carList.Add("fireTruck");
        System.Random rnd = new System.Random();
        string randomCar = carList[rnd.Next(carList.Count)];
        return randomCar;
    }
}