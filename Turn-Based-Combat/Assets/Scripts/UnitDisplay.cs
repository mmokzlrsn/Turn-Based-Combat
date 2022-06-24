using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UnitDisplay : MonoBehaviour
{
    public Unit unit;

    public TextMeshProUGUI nameText;

    public Image artworkImage;

    public TextMeshProUGUI unitLevelText;

    public Slider hpSlider;

    // Update is called once per frame
    void Start()
    {
        SetUnitDisplayer();
    }

    public void SetUnitDisplayer()
    {
        nameText.text = unit.unitName;

        artworkImage.sprite = unit.artwork;

        unitLevelText.text = unit.unitLevel.ToString();
        hpSlider.maxValue = unit.maxHP;

        hpSlider.value = unit.currentHP;

    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;

    }

}
