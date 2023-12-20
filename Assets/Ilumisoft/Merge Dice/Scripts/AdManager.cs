using GamePush;
using UnityEngine;
public class AdManager : MonoBehaviour
{
    public float timerInterval = 180f; // Интервал времени в секундах (3 минуты)
    private float timer = 0f; // Счетчик времени
    private bool isTimerActive = true; // Флаг для активации таймера
    private bool isAdsShowing = false; // Флаг, чтобы отключить клики, когда реклама показывается
    private bool isWarningActive = false; // Флаг, чтобы отключить предупреждение перед показом рекламы
    private float warningDuration = 3f; // Длительность предупреждения (3 секунды)
    public static AdManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    private void Update()
    {
        // Проверяем, если таймер активен и прошло timerInterval секунд с момента последнего Preloader()
        if (isTimerActive && !isAdsShowing)
        {
            timer += Time.deltaTime;

            if (timer >= timerInterval)
            {
                //Preloader();
                Fullscreen();
                timer = 0f; // Сбросить таймер
                isTimerActive = false; // Выключаем таймер после запуска Preloader

                isAdsShowing = false; // Реклама больше не показывается
                isTimerActive = true; // Включаем таймер снова
                isWarningActive = false; // Сбрасываем флаг предупреждения
            }
            else
            {
                if (timer >= timerInterval - warningDuration && !isWarningActive)
                {
                    isWarningActive = true;
                    //textWarning.SetActive(true);
                }

                // Обновляем текст таймера
                float remainingTime = timerInterval - timer;
            }
        }
    }
    public void Preloader() => GP_Ads.ShowPreloader();
    public void Fullscreen() => GP_Ads.ShowFullscreen();
    /*
        private void Start()
        {
            if (!GP_Device.IsMobile())
            {
                Sticky();
            }
        }
    */
    private void OnEnable()
    {
        GP_Game.OnPause += Pause;
        GP_Game.OnResume += Resume;
        GP_Ads.OnRewardedReward += OnRewarded;
    }

    private void OnDisable()
    {
        GP_Game.OnPause -= Pause;
        GP_Game.OnResume -= Resume;
        GP_Ads.OnRewardedReward -= OnRewarded;
    }

    private void Resume()
    {
        Time.timeScale = 1;
        // Установите состояние AudioListener
        AudioListener.pause = false;
        if (PlayerPrefs.GetInt("SoundOn", 1) == 1)
        {
            // Включаем звук
            AudioListener.volume = 1;
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        AudioListener.volume = 0;
    }

    private void Sticky()
    {
        // Показать стики баннер
        GP_Ads.ShowSticky();

        // Закрыть стики баннер
        GP_Ads.CloseSticky();

        // Обновить стики баннер
        GP_Ads.RefreshSticky();

        // Стики баннеры обновляются автоматически раз в 30 секунд.
    }

    // Метод для показа вознагражденной рекламы
    public void Rewarded() => GP_Ads.ShowRewarded("Money_Bonus");

    // Метод, вызываемый при получении вознаграждения за просмотр рекламы
    private void OnRewarded(string value)
    {
        /*
        if (value == "Money_Bonus")
        { // Получаем экземпляр GameManager
            GameManager gameManager = GameManager.Instance;

            if (gameManager != null)
            {

                int moneyToAdd = 2000; // Например, добавим 500 денег
                gameManager.AddMoney(moneyToAdd); // Вызов метода AddMoney для добавления денег
            }
            else
            {
                Debug.LogError("GameManager не найден.");
            }

        }
        */
    }

    public void MoreGames()
    {
        // By custom TAG
        //GP_GamesCollections.Open({ tag: 'OurGames' });

        // By ID

        //GP_GamesCollections.Open("219");
        GP_GamesCollections.Open("219");
    }

    public void ShareGame()
    {
        GP_Socials.Share();
    }
}
