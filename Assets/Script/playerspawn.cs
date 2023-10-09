using UnityEngine;

public class playerspawn : MonoBehaviour
{
  private void Awake()
  {
      GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
  }
}
