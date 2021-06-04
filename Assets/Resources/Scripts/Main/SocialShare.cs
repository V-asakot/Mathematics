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

        /*  main.ads.HideBanner(true);
          yield return new WaitForSecondsRealtime(0.5f);
          yield return new WaitForEndOfFrame();
          string path = Application.persistentDataPath + "/ScreenShot";
          if (!Directory.Exists(path))
          {
              Directory.CreateDirectory(path);

          }
          ScreenCapture.CaptureScreenshot("ScreenShot/screenshot.png", 2);
          string destination = Path.Combine(path, "screenshot.png");
          yield return new WaitForSecondsRealtime(0.3f);

          if (!Application.isEditor)
          { 

              AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
              AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
              intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
              AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
              AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
              intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
              intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Поделитесь игрой с друзьями! ");
              intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
              AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
              AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
              AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Поделись с друзьями");
              currentActivity.Call("startActivity", chooser);

              yield return new WaitForSecondsRealtime(1);
          }
          main.ads.HideBanner(false);
          isProcessingShare = false;
         */
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
