// Unity 애니메이션 이벤트를 사용하는 방법

// 1. 애니메이션 클립에서 특정 프레임에 이벤트 추가
// 2. 아래처럼 이벤트로 호출될 메서드 작성

using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    [SerializeField]
    GasGame game;

    public void OnAnimationEnd()
    {
        Debug.Log("애니메이션 이벤트로 끝났음을 감지했습니다!");
        // 원하는 로직 실행
        game.GameStart();
    }
}
