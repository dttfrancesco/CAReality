using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
	public GameObject[] characterPrefabs;
	public Transform spawnPoint;
	public TMP_Text label;

	void StartGame()
	{
		int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
		GameObject prefab = characterPrefabs[selectedCharacter];
		//GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
		transform.GetComponent<GameManager>().GoRunning();
		label.text = prefab.name;
	}
}
