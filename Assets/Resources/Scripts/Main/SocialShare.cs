using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VoxelBusters;
using VoxelBusters.NativePlugins;

public class SocialShare : MonoBehaviour {
    private bool isProcessingShare = false;
    private bool isFocusShare = false;
    public GameMain main;
    public void StorePage()
    {
        Application.OpenURL("market://details?id=ru.Maths.UniqumGames");
    }
    public void OpenURL(string st)
    {
        Application.OpenURL(st);
    }
    public void Share()
    {
        if (!isProcessingShare)
        {
           
            StartCoroutine(ShareScreenshot());
        }
    }
    IEnumerator ShareScreenshot()
    {

        isProcessingShare = true;

        
        
        yield return new WaitForEndOfFrame();
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
        ShareSheet share = new ShareSheet();


        share.Text = "Скачай новое приложение Математика для детей и взрослых";
        share.URL = "https://play.google.com/store/apps/details?id=ru.Maths.UniqumGames&hl=en_US";
       share.AttachImage(texture);

        NPBinding.Sharing.ShowView(share, FinishSharing);

       // yield return new WaitUntil(() => isFocusShare);
        

    }

    private void FinishSharing(eShareResult _result)
    {
        isProcessingShare = false;
        Debug.Log(_result);
    }
}
