using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;


public interface IGetable
{
    public void SendItem(PlayerControl player, ItemData itemData);
}


public sealed class GettableItem : MonoBehaviour, IGetable
{
    #region 아이템 회전 관련 변수
    [SerializeField]
    private float rotSpeed = 100.0f;

    [SerializeField]
    private GameObject ItemModel;
    #endregion

    [SerializeField]
    [Header("아이템 개별 속성 (Scriptable object)")] private ItemData itemData;

    [SerializeField]
    [Header("아이템이 사라질 때 출력되는 효과")] private ParticleSystem destroyParticle;

    [SerializeField]
    [Header("아이템 근접 시 활성화되는 UI")] private Image ItemInteractImage;

    [SerializeField]
    [Header("자신의 상위에 위치한 아이템 스포너")] private ItemSpawnctrl myParent;

    [SerializeField]
    [Header("포톤뷰")] private PhotonView photonView;

    public void SendItem(PlayerControl player, ItemData itemData)
    {
        //플레이어가 아이템의 정보를 받아올 메서드
        player.GetItem(itemData);

        /*
         * 나 이거 받아왔어
         * 이 아이템 가지고 있어
         * 총 무기 장탄수
         * 
         * 맨손 총 짱돌
         *
         *플레이어가 아이템 받아올 그무언가....
         *에다가 저 정보를 저장해서
         *아이템 정보를 공격 등에 써먹음
         *장착,직접 공격 등
         */

        Debug.Log("플레이어에게 아이템 전달" + itemData.itemName);
    }

    private void Update()
    {
        ItemModel.transform.rotation = Quaternion.Euler(ItemModel.transform.rotation.eulerAngles.x, 
            ItemModel.transform.rotation.eulerAngles.y + (rotSpeed * Time.deltaTime), ItemModel.transform.rotation.eulerAngles.z);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControl player = other.GetComponentInParent<PlayerControl>();

            if (player == null)
            {
                return;
            }

            Image[] PlayerInteractImages = other.GetComponentsInChildren<Image>(true);
            
            foreach(Image image in PlayerInteractImages)
            {
                if(image.gameObject.name == "PressFImage")
                {
                    ItemInteractImage = image;
                    break;
                }
            }

            if(ItemInteractImage != null)
            {
                ItemInteractImage.gameObject.SetActive(true);
            }

            else
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                SendItem(player, itemData);
                Instantiate(destroyParticle, transform.TransformPoint(0, 1.0f, 0), Quaternion.identity);
                ItemInteractImage.gameObject.SetActive(false);
                this.gameObject.SetActive(false);

                photonView.RPC("NoticeIsAllitemOff", RpcTarget.MasterClient);
            }
        }
    }

    [PunRPC]
    private void NoticeIsAllitemOff()
    {
        myParent.IsAllItemOff = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemInteractImage.gameObject.SetActive(false);
        }
    }


}
