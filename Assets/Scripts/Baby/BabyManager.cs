using System.Collections;
using Other;
using Player;
using Toys;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Baby
{
    public class BabyManager : MonoBehaviour
    {
        private float _howHappyIsBaby;
        private int _neededTypeOfToy;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private UIManager uIManager;
        [SerializeField] private float howFastWillBayHappynesGoDown = .1f;
        [SerializeField] private GameObject wrongToyText;
        [SerializeField] private Sprite[] existingToyTypeImages;
        public bool isGettingHappyLess;
        public Image imageForToyTypeInUse;

        private void Start()
        {
            _howHappyIsBaby = 100;
            wrongToyText.SetActive(false);
            _neededTypeOfToy = 0;
            imageForToyTypeInUse.sprite = existingToyTypeImages[_neededTypeOfToy];
        }
        private void Update()
        {
            if (isGettingHappyLess)
            {
                if (_howHappyIsBaby <= 0f)
                {
                    uIManager.Death();
                    isGettingHappyLess = false;
                }
                else if (_howHappyIsBaby > 100f)
                {
                    _howHappyIsBaby = 100f;
                }
                else
                {
                    _howHappyIsBaby -= howFastWillBayHappynesGoDown * Time.deltaTime;
                }
                uIManager.SetHappyBar(_howHappyIsBaby);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Toys"))
            {
                if (collision.gameObject.GetComponent<Toy>().WhatToyTypeIsThis() == _neededTypeOfToy)
                {
                    gameManager.player.GetComponent<CursorController>().DropItem();
                    AddOrTakeBayHappiness(collision.gameObject.GetComponent<Toy>().HowMuchDoesToyGiveHappiness());
                    Destroy(collision.gameObject);
                    ChangeToyTypeWanted();
                }
                else
                {
                    StartCoroutine(Wait());
                }
            }
        }
        public void AddOrTakeBayHappiness(float addorSubtractNum)
        {
            if (isGettingHappyLess)
            {
                _howHappyIsBaby += addorSubtractNum;
            }
        }
        private void ChangeToyTypeWanted()
        {
            var myRandomIndex = Random.Range(0, existingToyTypeImages.Length);
            imageForToyTypeInUse.sprite = existingToyTypeImages[myRandomIndex];
            _neededTypeOfToy = myRandomIndex;
        }
        IEnumerator Wait()
        {
            wrongToyText.SetActive(true);
            yield return new WaitForSeconds(.7f);
            wrongToyText.SetActive(false);
        }
    }
}
