using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public float speed = 4f; // 플레이어 속도
    public float jumpForce = 5f; // 점프 힘
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트 참조
    private Animator animator; // Animator 컴포넌트 참조
    private bool isGrounded; // 캐릭터가 땅에 닿아있는지 여부를 나타내는 변수

    // groundCheck에 사용할 Transform
    public Transform groundCheck; // Inspector에서 할당할 땅 감지 위치
    public float checkRadius; // 땅 감지 반경
    public string groundTag = "Ground"; // 땅으로 간주할 레이어

  void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 방향키 입력 받기
        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // 캐릭터 방향 설정
        if(move > 0){
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if(move < 0){
            transform.rotation = Quaternion.Euler(0,180,0);
        }

        // 이동 애니메이션 설정
        if(rb.velocity.x != 0){
            animator.SetBool("isWalk", true);
        }
        else{
            animator.SetBool("isWalk", false) ;
        }

    
        // 점프 처리
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }

        // 하강 상태 애니메이션 처리
        if (rb.velocity.y < 0 && isGrounded == false)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }
        
    }
}