using UnityEngine;
using UnityEngine.Networking; // For UnityWebRequest
using System.Collections; // For Coroutines
using System; // For Uri

public class LoginManager : MonoBehaviour
{
    private string KAKAO_CLIENT_ID = "KAKAO_CLIENT_ID"; // Kakao Developers에서 발급받은 네이티브 앱 키
    private string KAKAO_REDIRECT_URI = "KAKAO_REDIRECT_URI"; // Kakao Developers에 등록한 Redirect URI
    // REST API 키는 주로 서버 간 통신에 사용되며, 클라이언트에서 직접 사용하는 것은 보안상 권장되지 않습니다.
    // 대신 네이티브 앱 키를 사용하고, 토큰 요청 시 client_secret이 필요하다면 해당 값을 안전하게 관리해야 합니다.
    // Kakao REST API 중 토큰 받기 요청 시 client_secret이 필요한 경우가 있습니다. (https://developers.kakao.com/docs/latest/ko/kakaologin/rest-api#request-token-client-secret)
    // 대부분의 Unity 클라이언트 기반 OAuth 흐름에서는 client_secret을 클라이언트에 직접 포함하지 않는 방식을 사용하거나,
    // 서버를 통해 토큰을 교환하는 것이 더 안전합니다. 아래는 클라이언트에서 직접 처리하는 예시입니다.
    // private string KAKAO_CLIENT_SECRET = "YOUR_KAKAO_CLIENT_SECRET"; // 필요한 경우 사용

    private WebViewObject webViewObject;
    public RectTransform webViewUIRect; // 옵션: WebView 영역을 정의할 UI Panel (RectTransform)을 Inspector에서 할당


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartKakaoLogin();
    }

    // 카카오 로그인 시작 (UI 버튼 등에서 호출)
    public void StartKakaoLogin()
    {
        // Kakao OAuth URL 생성. REDIRECT_URI는 URL 인코딩합니다.
        string authorizationRequestUrl = $"https://kauth.kakao.com/oauth/authorize?client_id={KAKAO_CLIENT_ID}&redirect_uri={Uri.EscapeDataString(KAKAO_REDIRECT_URI)}&response_type=code";
        Debug.Log($"카카오 인가 요청 URL: {authorizationRequestUrl}");


        if (webViewObject == null)
        {
            // WebViewObject용 GameObject 생성
            GameObject webViewGameObject = new GameObject("KakaoWebViewObject");
            webViewObject = webViewGameObject.AddComponent<WebViewObject>();
            
            // WebViewObject 초기화
            // ld (onLoaded): 페이지 로드 완료 시 호출될 콜백 (msg는 로드된 URL)
            // cb (onCalled): JavaScript에서 window.Unity.call(msg) 호출 시 콜백
            // err (onError): 에러 발생 시 콜백
            webViewObject.Init(
                ld: (loadedUrl) => {
                    Debug.Log($"WebView page loaded: {loadedUrl}");
                    // 현재 로드된 URL은 콜백 파라미터인 loadedUrl을 직접 사용합니다.
                    string currentUrl = loadedUrl; 

                    if (!string.IsNullOrEmpty(currentUrl) && currentUrl.StartsWith(KAKAO_REDIRECT_URI))
                    {
                        Debug.Log($"리다이렉트 URL 감지: {currentUrl}");
                        string authCode = GetQueryParam(currentUrl, "code");

                        if (!string.IsNullOrEmpty(authCode))
                        {
                            Debug.Log($"인가 코드 추출 성공: {authCode}");
                            webViewObject.SetVisibility(false); // WebView 숨기기
                            // 필요하다면 Destroy(webViewObject.gameObject); webViewObject = null; 등으로 정리
                            OnAuthorizationCodeReceived(authCode);
                        }
                        else
                        {
                            Debug.LogError("인가 코드를 URL에서 추출하지 못했습니다.");
                            webViewObject.SetVisibility(false); // 에러 발생 시 WebView 숨기기
                            // 사용자에게 에러 메시지 표시 등의 처리
                        }
                    }
                },
                err: (errorMsg) => {
                    Debug.LogError($"WebView error: {errorMsg}");
                    if (webViewObject != null)
                    {
                        webViewObject.SetVisibility(false); // 에러 발생 시 WebView 숨기기
                    }
                    // 사용자에게 에러 메시지 표시 등의 처리
                },
                enableWKWebView: true // iOS에서 WKWebView 사용 권장 (true 또는 false로 설정)
            );
        }

        // WebView의 화면상 여백(margins) 설정
        if (webViewUIRect != null && webViewUIRect.gameObject.activeInHierarchy)
        {
            // 지정된 UI RectTransform 기준으로 WebView 크기 및 위치 설정
            // RectTransform의 코너 좌표를 Screen Point로 변환
            Vector3[] corners = new Vector3[4];
            webViewUIRect.GetWorldCorners(corners); // 월드 좌표 코너

            // Canvas 설정에 따라 Camera를 다르게 전달해야 할 수 있음 (Screen Space - Overlay는 null)
            Camera renderCamera = (webViewUIRect.GetComponentInParent<Canvas>().renderMode == RenderMode.ScreenSpaceOverlay) ? null : webViewUIRect.GetComponentInParent<Canvas>().worldCamera;

            Vector2 bottomLeftScreen = RectTransformUtility.WorldToScreenPoint(renderCamera, corners[0]);
            Vector2 topRightScreen = RectTransformUtility.WorldToScreenPoint(renderCamera, corners[2]);
            
            // WebView 마진: (left, top, right, bottom) - 화면 가장자리로부터의 픽셀 단위 거리
            // Screen Point는 좌하단이 (0,0), WebView 마진의 top은 화면 상단으로부터의 거리임
            int marginLeft = (int)bottomLeftScreen.x;
            int marginTop = (int)(Screen.height - topRightScreen.y);
            int marginRight = (int)(Screen.width - topRightScreen.x);
            int marginBottom = (int)bottomLeftScreen.y;

            Debug.Log($"WebView Margins (UI Rect): L={marginLeft}, T={marginTop}, R={marginRight}, B={marginBottom}");
            webViewObject.SetMargins(marginLeft, marginTop, marginRight, marginBottom);
        }
        else
        {
            // webViewUIRect가 지정되지 않았거나 비활성화 상태이면 전체 화면으로 설정
            Debug.Log("WebView Margins: Full Screen");
            webViewObject.SetMargins(0, 0, 0, 0);
        }
        
        webViewObject.LoadURL(authorizationRequestUrl);
        webViewObject.SetVisibility(true); // WebView 보이기

        // 아래 직접 호출 부분은 WebView 콜백에서 처리하므로 제거합니다.
        // OnAuthorizationCodeReceived("TEST_AUTH_CODE_FROM_WEBVIEW"); 
    }

    // URL에서 특정 쿼리 파라미터 값을 추출하는 헬퍼 함수
    private string GetQueryParam(string url, string paramName)
    {
        try
        {
            Uri uri = new Uri(url);
            string query = uri.Query; // 예: "?code=AUTH_CODE_HERE&state=XYZ"
            if (string.IsNullOrEmpty(query) || !query.StartsWith("?"))
            {
                return null;
            }

            query = query.Substring(1); // '?' 제거
            string[] pairs = query.Split('&');
            foreach (string pair in pairs)
            {
                string[] keyValue = pair.Split('=');
                if (keyValue.Length == 2 && keyValue[0] == paramName)
                {
                    return Uri.UnescapeDataString(keyValue[1]);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"URL 쿼리 파라미터 파싱 오류 ('{paramName}' from '{url}'): {ex.Message}");
        }
        return null;
    }


    // WebView 등으로부터 인가 코드를 전달받아 호출되는 함수
    public void OnAuthorizationCodeReceived(string authCode)
    {
        if (string.IsNullOrEmpty(authCode))
        {
            Debug.LogError("카카오 로그인 실패: 인가 코드가 없습니다.");
            // 로그인 실패 관련 UI 처리 또는 콜백 호출
            return;
        }
        Debug.Log($"카카오 인가 코드 수신: {authCode}");
        StartCoroutine(RequestTokenRoutine(authCode));
    }

    // 2. 액세스 토큰 요청 코루틴
    private IEnumerator RequestTokenRoutine(string authCode)
    {
        WWWForm form = new WWWForm();
        form.AddField("grant_type", "authorization_code");
        form.AddField("client_id", KAKAO_CLIENT_ID);
        form.AddField("redirect_uri", KAKAO_REDIRECT_URI); // 토큰 요청 시 redirect_uri는 인가코드 요청 시와 동일해야 함
        form.AddField("code", authCode);
        // Kakao API 정책에 따라 client_secret이 필요할 수 있습니다.
        // 필요하다면 아래 주석을 해제하고 KAKAO_CLIENT_SECRET 값을 설정하세요.
        // form.AddField("client_secret", KAKAO_CLIENT_SECRET);

        string tokenUrl = "https://kauth.kakao.com/oauth/token";

        using (UnityWebRequest www = UnityWebRequest.Post(tokenUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"카카오 토큰 요청 실패: {www.error}");
                Debug.LogError($"응답 내용: {www.downloadHandler.text}");
                // 로그인 실패 관련 UI 처리 또는 콜백 호출
            }
            else
            {
                Debug.Log($"카카오 토큰 요청 성공.");
                // Debug.Log($"응답 전문: {www.downloadHandler.text}"); // 전체 응답 확인용
                KakaoTokenResponse tokenResponse = JsonUtility.FromJson<KakaoTokenResponse>(www.downloadHandler.text);

                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.access_token))
                {
                    Debug.Log($"액세스 토큰: {tokenResponse.access_token}");
                    // 액세스 토큰을 사용하여 사용자 정보 요청
                    StartCoroutine(RequestUserProfileRoutine(tokenResponse.access_token));
                }
                else
                {
                    Debug.LogError($"카카오 액세스 토큰 파싱 실패. 응답: {www.downloadHandler.text}");
                    // 로그인 실패 관련 UI 처리 또는 콜백 호출
                }
            }
        }
    }

    // 3. 사용자 정보 요청 코루틴
    private IEnumerator RequestUserProfileRoutine(string accessToken)
    {
        string userProfileUrl = "https://kapi.kakao.com/v2/user/me";
        using (UnityWebRequest www = UnityWebRequest.Get(userProfileUrl))
        {
            www.SetRequestHeader("Authorization", $"Bearer {accessToken}");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"카카오 사용자 정보 요청 실패: {www.error}");
                Debug.LogError($"응답 내용: {www.downloadHandler.text}");
                // 사용자 정보 요청 실패 처리
            }
            else
            {
                Debug.Log($"카카오 사용자 정보 요청 성공.");
                // Debug.Log($"응답 전문: {www.downloadHandler.text}"); // 전체 응답 확인용
                KakaoUserProfile userProfile = JsonUtility.FromJson<KakaoUserProfile>(www.downloadHandler.text);

                if (userProfile != null)
                {
                    Debug.Log($"사용자 ID: {userProfile.id}");
                    if (userProfile.kakao_account != null)
                    {
                        Debug.Log($"연결 시각: {userProfile.connected_at}");
                        Debug.Log($"이메일: {userProfile.kakao_account.email}"); // 동의 항목에 따라 null일 수 있음
                        Debug.Log($"닉네임: {userProfile.kakao_account.profile?.nickname}"); // 동의 항목 및 null 체크
                        Debug.Log($"프로필 이미지 URL: {userProfile.kakao_account.profile?.profile_image_url}");
                        // 필요한 사용자 정보를 변수에 저장하거나 다른 로직 처리
                        // 예: GameManager.Instance.OnLoginSuccess(userProfile);
                    }
                    else
                    {
                        Debug.LogWarning("kakao_account 정보가 없습니다.");
                    }
                    // 로그인 성공 처리 (예: 다음 씬으로 이동, UI 업데이트 등)
                }
                else
                {
                    Debug.LogError($"카카오 사용자 정보 파싱 실패. 응답: {www.downloadHandler.text}");
                    // 실패 처리
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

// 카카오 토큰 응답을 위한 데이터 구조
[System.Serializable]
public class KakaoTokenResponse
{
    public string token_type;
    public string access_token;
    public int expires_in;
    public string refresh_token;
    public int refresh_token_expires_in;
    public string scope; // 사용자가 동의한 항목들 (공백으로 구분)
}

// 카카오 사용자 프로필 응답을 위한 데이터 구조
[System.Serializable]
public class KakaoUserProfile
{
    public long id; // 회원번호
    public string connected_at; // 서비스에 연결된 시각 (UTC)
    public KakaoAccount kakao_account; // 카카오계정 정보
    // 'properties' 필드는 카카오 API v1에서 사용되던 것으로, v2에서는 주로 kakao_account 하위 필드로 제공됩니다.
    // 필요시 public KakaoProfileProperties properties; 와 같이 추가할 수 있으나, 최신 API 문서를 확인하세요.
}

[System.Serializable]
public class KakaoAccount
{
    public bool profile_needs_agreement; // 프로필 정보 제공 동의 필요 여부
    public bool profile_nickname_needs_agreement; // 닉네임 제공 동의 필요 여부
    public bool profile_image_needs_agreement; // 프로필 사진 제공 동의 필요 여부
    public KakaoProfile profile; // 프로필 정보

    public bool email_needs_agreement; // 이메일 제공 동의 필요 여부
    public bool is_email_valid; // 이메일 유효 여부
    public bool is_email_verified; // 이메일 인증 여부
    public string email; // 이메일 주소

    // 추가적으로 동의받을 수 있는 항목들 (예: age_range, birthday, gender 등)
    // public string age_range;
    // public bool age_range_needs_agreement;
    // public string birthday;
    // public bool birthday_needs_agreement;
    // public string gender;
    // public bool gender_needs_agreement;
}

[System.Serializable]
public class KakaoProfile
{
    public string nickname; // 닉네임
    public string thumbnail_image_url; // 썸네일 프로필 사진 URL
    public string profile_image_url; // 원본 프로필 사진 URL
    public bool is_default_image; // 현재 프로필 사진이 기본 프로필인지 여부
}
