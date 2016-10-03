using UnityEngine;
using System.Collections;
using System;
using System.Text;

public class SphinxTest : MonoBehaviour {
	[SerializeField]
	GameObject cube;
	[SerializeField]
	GameObject sphere;
	[SerializeField]
	GameObject quad;
	[SerializeField]
	GameObject capsule;

	private GameObject currentGameObject;

	// Use this for initialization
	void Start () {
		UnitySphinx.Init ();
		UnitySphinx.Run ();
	}

	void Update()
	{
		string rawCommandString = UnitySphinx.DequeueString ();

		if (UnitySphinx.GetSearchModel() == "kws")
		{
			Debug.Log("Listening for Keyword");

			if (rawCommandString.Length > 0) {
				Debug.Log ("Command: " + rawCommandString);
				UnitySphinx.SetSearchModel (UnitySphinx.SearchModel.jsgf);
			}
		}
		else if (UnitySphinx.GetSearchModel() == "jsgf")
		{
			Debug.Log ("Listening for order");

			if (rawCommandString.Length > 0) 
			{
				char[] delimChars = { ' ' };
				string[] cmd = rawCommandString.Split (delimChars);

				Debug.Log (cmd);

				GameObject gameObject = FindObject (cmd [1]);

				if (currentGameObject != null) {
					Destroy (currentGameObject);
				}

				currentGameObject = (GameObject) Instantiate (gameObject, new Vector3(0, 1, 0), Quaternion.identity);

				UnitySphinx.SetSearchModel (UnitySphinx.SearchModel.kws);
			}
		}
	}

	GameObject FindObject(string objectName)
	{
		Debug.Log ("Looking for: " + objectName);

		GameObject gameObject = cube;

		switch (objectName) {
		case "sphere":
			return sphere;
		case "capsule":
			return capsule;
		case "quad":
			return quad;
		default:
			return cube;
		}
	}
}