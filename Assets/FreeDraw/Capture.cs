using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            CaptureScreen();
        }
    }

    public void CaptureScreen()
    {
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = "capture" + timestamp + ".png";
        
        CaptureScreenForPC(fileName);
    }

    private void CaptureScreenForPC(string fileName)
    {
        ScreenCapture.CaptureScreenshot("C:/Capture/" + fileName);
        print("에러뜨면 경로확인 >>>> " + "C:/Capture/" + fileName);
    }
}
