using UnityEngine;
using System.Collections;
using System.IO;
using SWNAdmin.Utility;
using System.Xml.Serialization;

public class XmlDeserialize : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        XmlSerializer SerializerObj = new XmlSerializer(typeof(StarSystems));
        // Create a new file stream for reading the XML file
        FileStream ReadFileStream = new FileStream(@"C:\Test\test.xml", FileMode.Open, FileAccess.Read, FileShare.Read);

        // Load the object saved above by using the Deserialize function
        StarSystems LoadedObj = (StarSystems)SerializerObj.Deserialize(ReadFileStream);

        // Cleanup
        ReadFileStream.Close();
        foreach (Stars item in LoadedObj.Stars)
        {
            Debug.Log(item.StarOrder); 
        }
        
    }
	
}
