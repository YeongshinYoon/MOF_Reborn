using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenControl : MonoBehaviour 
{
    private static TitleScreenControl instance;

    public static TitleScreenControl MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TitleScreenControl>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Animator anim2;

    [SerializeField]
    private Image black;

    [SerializeField]
    private Image ingameLoadingImage;

    [SerializeField]
    private Image[] LoadingImage;

    [SerializeField]
    private Image BGImage;

    [SerializeField]
    private Image selectServerAndChannel;

    [SerializeField]
    private Image CharacterSelect;

    [SerializeField]
    private Image CreateCharacter;

    [SerializeField]
    private GameObject channelObjects;

    [SerializeField]
    private Sprite serverSelectedImage;

    [SerializeField]
    private Sprite serverUnselectedImage;

    [SerializeField]
    private Sprite channelSelectedImage;

    [SerializeField]
    private Sprite channelUnselectedImage;

    [SerializeField]
    private Sprite classSelectedImage;

    [SerializeField]
    private Sprite classUnselectedImage;

    [SerializeField]
    private Button[] servers;

    [SerializeField]
    private Button[] channels;

    [SerializeField]
    private Text channelPhrase;

    [SerializeField]
    private GameObject dialogue;

    [SerializeField]
    private Text dialogueText;

    [SerializeField]
    private Image classImage;

    [SerializeField]
    private Text classDescription;

    [SerializeField]
    private Sprite[] classImages;

    private bool IsServerSelected;

    private bool IsChannelSelected;

    private bool IsClassSelected;

    private bool IsCharacterSelected;

    private GameObject selectedServer;

    private GameObject selectedChannel;

    private GameObject selectedClass;

    private GameObject selectedCharacter;

    private int str;
    private int vit;
    private int agi;
    private int intel;
    private int bonuspt;

    [SerializeField]
    private Text strText;

    [SerializeField]
    private Text vitText;

    [SerializeField]
    private Text agiText;

    [SerializeField]
    private Text intelText;

    [SerializeField]
    private Text bonusptText;

    private int currentIndex;

    [SerializeField]
    private InputField charName;

    // Use this for initialization
    void Start () {
        ingameLoadingImage.gameObject.SetActive(false);
        currentIndex = SaveManager.MyInstance.GetCurrentIndex(SaveManager.MyInstance.MySaveSlots);
        CreateCharacter.gameObject.SetActive(false);
        dialogue.SetActive(false);
        BGImage.gameObject.SetActive(false);
        LoadingImage[0].gameObject.SetActive(true);
        LoadingImage[1].gameObject.SetActive(false);
        selectServerAndChannel.gameObject.SetActive(false);
        channelObjects.gameObject.SetActive(false);
        channelPhrase.gameObject.SetActive(false);
        CharacterSelect.gameObject.SetActive(false);
        classImage.gameObject.SetActive(false);
        classDescription.gameObject.SetActive(false);
        StartCoroutine(IntroScreen());
        str = vit = agi = intel = 4;
        bonuspt = 10;
    }

    private void Update()
    {
        ServerButtonStateUpdate();
        ChannelButtonStateUpdate();
        ClassButtonStateUpdate();
        SelectButtonStateUpdate();
        StatInfoUpdate(); //분배능력포인트 업데이트
    }

    private void StatInfoUpdate()
    {
        strText.text = str.ToString();
        vitText.text = vit.ToString();
        agiText.text = agi.ToString();
        intelText.text = intel.ToString();
        bonusptText.text = bonuspt.ToString();
    }

    private void ServerButtonStateUpdate()
    {
        if (!IsServerSelected)
        {
            channelObjects.SetActive(false);
            if (selectedServer != null)
            {
                selectedServer.GetComponent<Image>().sprite = serverUnselectedImage;
                selectedServer = null;
            }
        }
        else if (IsServerSelected)
        {
            channelObjects.SetActive(true);
            selectedServer.GetComponent<Image>().sprite = serverSelectedImage;
        }
    }

    private void ChannelButtonStateUpdate()
    {
        if (!IsChannelSelected)
        {
            if (selectedChannel != null)
            {
                selectedChannel.GetComponent<Image>().sprite = channelUnselectedImage;
                selectedChannel = null;
            }
            channelPhrase.gameObject.SetActive(false);
        }
        else if (IsChannelSelected)
        {
            selectedChannel.GetComponent<Image>().sprite = channelSelectedImage;
            channelPhrase.gameObject.SetActive(true);
        }
    }

    private void ClassButtonStateUpdate()
    {
        if (!IsClassSelected)
        {
            if (selectedClass != null)
            {
                selectedClass.GetComponent<Image>().sprite = classUnselectedImage;
                selectedClass = null;
            }
            classImage.gameObject.SetActive(false);
            classDescription.gameObject.SetActive(false);
        }
        else if (IsClassSelected)
        {
            selectedClass.GetComponent<Image>().sprite = classSelectedImage;
            classImage.gameObject.SetActive(true);
            classDescription.gameObject.SetActive(true);
        }
    }

    private void SelectButtonStateUpdate()
    {
        if (!IsCharacterSelected)
        {
            if (selectedCharacter != null)
            {
                selectedCharacter.GetComponentInParent<SavedGame>().Unselected();
                selectedCharacter.GetComponent<Image>().sprite = classUnselectedImage;
                selectedCharacter = null;
            }
        }
        else if (IsCharacterSelected)
        {
            if (selectedCharacter != null)
            {
                selectedCharacter.GetComponentInParent<SavedGame>().Selected();
                selectedCharacter.GetComponent<Image>().sprite = classSelectedImage;
            }
        }
    }

    public void selectServer(GameObject button)
    {
        SoundManager.MyInstance.onClickLargeButton();

        if (!IsServerSelected)
        {
            IsServerSelected = true;
            selectedServer = button;
        }
        else if (IsServerSelected && selectedServer == button)
        {
            IsServerSelected = false;
            IsChannelSelected = false;
        }
        else if (IsServerSelected && selectedServer != button)
        {
            selectedServer.GetComponent<Image>().sprite = serverUnselectedImage;
            selectedServer = button;
            IsChannelSelected = false;
        }
    }

    public void selectChannel(GameObject button)
    {
        SoundManager.MyInstance.onClickLargeButton();

        if (!IsChannelSelected)
        {
            IsChannelSelected = true;
            selectedChannel = button;
        }
        else if (IsChannelSelected && selectedChannel == button)
        {
            IsChannelSelected = false;
        }
        else if (IsChannelSelected && selectedChannel != button)
        {
            selectedChannel.GetComponent<Image>().sprite = channelUnselectedImage;
            selectedChannel = button;
        }
    }

    public void selectCharacter(GameObject button)
    {
        SoundManager.MyInstance.onClickLargeButton();

        if (!IsCharacterSelected)
        {
            if (SaveManager.MyInstance.checkSaveDataFile(button.gameObject.GetComponentInParent<SavedGame>().MyIndex, SaveManager.MyInstance.MySaveSlots))
            {
                IsCharacterSelected = true;
                selectedCharacter = button;
            }
        }
        else if (IsCharacterSelected && selectedCharacter == button)
        {
            IsCharacterSelected = false;
        }
        else if (IsCharacterSelected && selectedCharacter != button)
        {
            selectedCharacter.GetComponent<Image>().sprite = classUnselectedImage;
            if (SaveManager.MyInstance.checkSaveDataFile(button.gameObject.GetComponentInParent<SavedGame>().MyIndex, SaveManager.MyInstance.MySaveSlots))
            {
                selectedCharacter = button;
            }
            else
            {
                IsCharacterSelected = false;
            }
        }
    }

    private IEnumerator IntroScreen()
    {
        anim.SetBool("Fade", false);
        yield return new WaitForSeconds(3f);
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(2f);
        LoadingImage[0].gameObject.SetActive(false);
        LoadingImage[1].gameObject.SetActive(true);
        anim.SetBool("Fade", false);
        yield return new WaitForSeconds(3f);
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(2f);
        LoadingImage[1].gameObject.SetActive(false);
        BGImage.gameObject.SetActive(true);
        anim.SetBool("Fade", false);
        yield return new WaitForSeconds(3f);
        SoundManager.MyInstance.onOpenWindow();
        selectServerAndChannel.gameObject.SetActive(true);
        black.gameObject.SetActive(false);
    }

    public void ShowDialogue()
    {
        SoundManager.MyInstance.onOpenWindow();
        
        if (IsServerSelected && IsChannelSelected)
        {
            dialogue.SetActive(true);
            dialogueText.text = selectedServer.name + " " + selectedChannel.name + "에 접속합니다.";
        }
    }

    public void CloseDialogue()
    {
        SoundManager.MyInstance.onCloseWindow();
        dialogue.SetActive(false);
    }

    public void CharSelectScreen()
    {
        SoundManager.MyInstance.onClickLargeButton();
        StartCoroutine(CharSelectFading());
    }

    public IEnumerator CharSelectFading()
    {
        black.gameObject.SetActive(true);
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(2f);
        selectServerAndChannel.gameObject.SetActive(false);
        anim.SetBool("Fade", false);
        yield return new WaitForSeconds(3f);
        SoundManager.MyInstance.onOpenWindow();
        if (currentIndex > 0)
            SaveManager.MyInstance.RefreshSaveSlots(SaveManager.MyInstance.MySaveSlots);
        CharacterSelect.gameObject.SetActive(true);
        black.gameObject.SetActive(false);
    }

    public void createCharacter()
    {
        SoundManager.MyInstance.onClickLargeButton();
        if (currentIndex < 3)
            StartCoroutine(CreateCharFading());
    }

    public IEnumerator CreateCharFading()
    {
        black.gameObject.SetActive(true);
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(2f);
        CharacterSelect.gameObject.SetActive(false);
        anim.SetBool("Fade", false);
        yield return new WaitForSeconds(3f);
        SoundManager.MyInstance.onOpenWindow();
        initCharCreate();
        CreateCharacter.gameObject.SetActive(true);
        black.gameObject.SetActive(false);
    }

    public void initCharCreate()
    {
        charName.text = "";
        IsClassSelected = false;
        str = vit = agi = intel = 4;
        bonuspt = 10;
    }

    public void backToCharSelect()
    {
        SoundManager.MyInstance.onClickLargeButton();
        StartCoroutine(BackToCharSelectFading());
    }

    public IEnumerator BackToCharSelectFading()
    {
        black.gameObject.SetActive(true);
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(2f);
        CreateCharacter.gameObject.SetActive(false);
        anim.SetBool("Fade", false);
        yield return new WaitForSeconds(3f);
        CharacterSelect.gameObject.SetActive(true);
        black.gameObject.SetActive(false);
    }

    public void changeClassImage(GameObject button)
    {
        SoundManager.MyInstance.onClickLargeButton();

        if (!IsClassSelected)
        {
            switch (button.name)
            {
                case "파이터":
                    classImage.sprite = classImages[0];
                    break;
                case "아처":
                    classImage.sprite = classImages[1];
                    break;
                case "메이지":
                    classImage.sprite = classImages[2];
                    break;
                case "클레릭":
                    classImage.sprite = classImages[3];
                    break;
            }
            IsClassSelected = true;
            selectedClass = button;
        }
        else if (IsClassSelected && button != selectedClass)
        {
            switch (button.name)
            {
                case "파이터":
                    classImage.sprite = classImages[0];
                    break;
                case "아처":
                    classImage.sprite = classImages[1];
                    break;
                case "메이지":
                    classImage.sprite = classImages[2];
                    break;
                case "클레릭":
                    classImage.sprite = classImages[3];
                    break;
            }
            selectedClass.GetComponent<Image>().sprite = classUnselectedImage;
            selectedClass = button;
        }
        else if (IsClassSelected && button == selectedClass)
        {
            IsClassSelected = false;
        }
    }

    public void StatUp(GameObject button)
    {
        SoundManager.MyInstance.onClickSmallButton();

        if (bonuspt > 0)
        {
            bonuspt--;

            switch (button.name)
            {
                case "공격":
                    str++;
                    break;
                case "체력":
                    vit++;
                    break;
                case "민첩":
                    agi++;
                    break;
                case "지력":
                    intel++;
                    break;
            }
        }
    }

    public void StatDown(GameObject button)
    {
        SoundManager.MyInstance.onClickSmallButton();

        if (bonuspt < 10)
        {
            bonuspt++;

            switch (button.name)
            {
                case "공격":
                    str--;
                    break;
                case "체력":
                    vit--;
                    break;
                case "민첩":
                    agi--;
                    break;
                case "지력":
                    intel--;
                    break;
            }
        }
    }

    public void AddCharacter()
    {
        SoundManager.MyInstance.onClickLargeButton();
        Player.MyInstance.CreateNewCharacter(charName.text, selectedClass.name, 1, str, vit, agi, intel);
        SaveManager.MyInstance.Save(SaveManager.MyInstance.MySaveSlots[currentIndex]);
        currentIndex++;
        StartCoroutine(BackToCharSelectFading());

        foreach (SavedGame savedGame in SaveManager.MyInstance.MySaveSlots)
        {
            SaveManager.MyInstance.ShowSavedFiles(savedGame);
        }
    }

    public void StartGame()
    {
        SoundManager.MyInstance.onWarp();
        StartCoroutine(IngameFading());
    }

    public IEnumerator IngameFading()
    {
        black.gameObject.SetActive(true);
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(5f);
        IsCharacterSelected = false;
        ingameLoadingImage.gameObject.SetActive(true);
        black.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        anim2.SetBool("Fade", true);
        BGImage.gameObject.SetActive(false);
        CharacterSelect.gameObject.SetActive(false);
        preload.MyInstance.ONOFF(true);
        preload.MyInstance.MyCamera.SetActive(true);
        SaveManager.MyInstance.prepareSettings();
        SaveManager.MyInstance.LoadScene();
        yield return new WaitForSeconds(1f);
        ingameLoadingImage.gameObject.SetActive(false);
        anim2.SetBool("Fade", false);
        SoundManager.MyInstance.playBGM();
    }

    public IEnumerator LoadingScene(string levelToLoad, string exitPoint)
    {
        black.gameObject.SetActive(true);
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(5f);
        //SoundManager.MyInstance.ingameBGMStop();
        preload.MyInstance.ONOFF(false);
        ingameLoadingImage.gameObject.SetActive(true);
        black.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        anim2.SetBool("Fade", true);
        SceneManager.LoadScene(levelToLoad);
        Player.MyInstance.startPoint = exitPoint;
        StartCoroutine(AfterLoadingScene());
    }
    public IEnumerator AfterLoadingScene()
    {
        preload.MyInstance.ONOFF(true);
        yield return new WaitForSeconds(1f);
        ingameLoadingImage.gameObject.SetActive(false);
        anim2.SetBool("Fade", false);
        SoundManager.MyInstance.playBGM();
        Player.MyInstance.canMove = true;
    }
}
