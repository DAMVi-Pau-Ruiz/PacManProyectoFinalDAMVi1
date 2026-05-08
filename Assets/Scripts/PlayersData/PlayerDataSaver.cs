using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Diagnostics;

public class PlayerDataSaver : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public string username;
        public int score;
        public int gVolume;
        public int mVolume;
        public int eVolume;
    }

    public void GuardarDatos(string username, int score)
    {
        // 1. Crear JSON
        PlayerData data = new PlayerData();
        data.username = username;
        data.score = score;
        data.gVolume = AudioSettings.instance.getGVolume();
        data.mVolume = AudioSettings.instance.getMVolume();
        data.eVolume = AudioSettings.instance.getEVolume();

        string json = JsonUtility.ToJson(data, true);

        // 2. Guardar JSON en la carpeta persistente
        string jsonPath = Path.Combine(Application.persistentDataPath, "player_data.json");
        File.WriteAllText(jsonPath, json);

        UnityEngine.Debug.Log("JSON guardado en " + jsonPath);

        // 3. Ejecutar el programa Java automáticamente
        EjecutarJava(jsonPath);
    }

    private void EjecutarJava(string jsonPath)
    {
        string javaFolder = @"D:\pacmanJava";
        string javaFile = "readJSONinsertIntoDB";
        string connector = "mysql-connector-j-8.3.0.jar";

        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = "java";
        psi.Arguments = $"-cp \"{javaFolder};{javaFolder}\\{connector}\" {javaFile} \"{jsonPath}\"";
        psi.CreateNoWindow = true;
        psi.UseShellExecute = false;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        Process p = new Process();
        p.StartInfo = psi;

        p.OutputDataReceived += (sender, e) => {
            if (!string.IsNullOrEmpty(e.Data))
                UnityEngine.Debug.Log("JAVA OUT: " + e.Data);
        };

        p.ErrorDataReceived += (sender, e) => {
            if (!string.IsNullOrEmpty(e.Data))
                UnityEngine.Debug.LogError("JAVA ERROR: " + e.Data);
        };

        p.Start();
        p.BeginOutputReadLine();
        p.BeginErrorReadLine();

        UnityEngine.Debug.Log("Ejecutando Java con JSON: " + jsonPath);
    }
}
