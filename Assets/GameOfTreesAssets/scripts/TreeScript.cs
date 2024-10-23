using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public float health = 10;
    public GameObject stumpPrefab;
    public GameObject treePrefab;
    public bool isCut = false;
    public float cutForce;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        transform.localScale = transform.localScale * Random.Range(0.75f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !isCut)
        {
            isCut = true;
            CreateStump();
            createRagdoll();
            GetComponent<TreeFalling>().PlayFallingSound();
            statsmanager component = FindObjectOfType<statsmanager>();
            component.AddTree(gameObject);
            //Destroy(gameObject);
            //GameObject newObject = Instantiate(stumpPrefab, transform.position, Quaternion.identity);
        }
    }

    public void takeDamage()
    {
        health--;
    }
    public void CreateStump()
    {
        GameObject newObject = Instantiate(stumpPrefab, transform.position, transform.rotation);
        newObject.transform.localScale = transform.localScale;
    }

    public void CreateTree()
    {
        Instantiate(treePrefab, transform.position, Quaternion.identity);
    }
    public void createRagdoll()
    {
        if (TryGetComponent(out Rigidbody rb))
        {
            Vector3 force = Random.onUnitSphere;
            force.y = 0;
            force.Normalize();
            rb.constraints = RigidbodyConstraints.None;
            rb.AddRelativeForce(force * cutForce, ForceMode.Impulse);
        }
    }
}
