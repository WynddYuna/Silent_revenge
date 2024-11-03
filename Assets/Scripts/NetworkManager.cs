using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private string baseUrl = "http://192.168.0.141/"; // Replace with your server URL

    public void RegisterUser (string username, string password)
    {
        StartCoroutine(Register(username, password));
    }

    private IEnumerator Register(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Registration successful: " + www.downloadHandler.text);
                // Handle success (e.g., notify user)
            }
        }
    }

    public void LoginUser (string username, string password)
    {
        StartCoroutine(Login(username, password));
    }

    private IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Login successful: " + www.downloadHandler.text);
                // Handle success (e.g., notify user)
            }
        }
    }

    public void DeleteUser (string username, string password)
    {
        StartCoroutine(Delete(username, password));
    }

    private IEnumerator Delete(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "delete.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Account deletion successful: " + www.downloadHandler.text);
                // Handle success (e.g., notify user)
            }
        }
    }
}