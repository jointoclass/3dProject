using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class UnityToJS : MonoBehaviour
{
    // 자바스크립트 함수 연결
    [DllImport("__Internal")]
    private static extern void showAlert(string message); //showAlert 애가 함수라서 중괄호가 필요없음, 우리가 직접 윈도우에 추가 해야한다

    void Start()
    {
        // 버튼 찾기 및 클릭 이벤트 등록
        Button button = GameObject.Find("SendMessageButton").GetComponent<Button>();
        button.onClick.AddListener(SendMessageToJS);
    }

    // 버튼 클릭 시 호출되는 메서드
    public void SendMessageToJS()
    {
        showAlert("Hello from Unity Button!");
    }
}
