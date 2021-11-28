using System.Collections.Generic;
using UnityEngine;
using BayatGames.Serialization.Formatters.Json;
using System;

[CreateAssetMenu(fileName = "LocalizationService", menuName = "Services/LocalizationService")]
public class LocalizationService : Service
{
    [SerializeField] private SystemLanguage _currentLanguage;

    private Dictionary<SystemLanguage, string> _localizationFiles = new Dictionary<SystemLanguage, string>
    {
        {SystemLanguage.English, "en" },
        {SystemLanguage.Spanish, "es" }
    };

    Dictionary<string, string> _dictionary = new Dictionary<string, string>();

    SystemLanguage _defaultLanguage = SystemLanguage.Spanish;

    public override void AddToContainer(IServiceContainer container)
    {
        var localizationService = Instantiate(this) as LocalizationService;
        container.Register<LocalizationService>(localizationService);
        container.Register<Service>(localizationService);
        container.Register<IDisposable>(localizationService);
    }

    public override void Initialize()
    {
        if (!_localizationFiles.ContainsKey(_currentLanguage))
        {
            _currentLanguage = _defaultLanguage;
        }

        TextAsset languageLoaded = Resources.Load<TextAsset>(GetLanguageFileName(_currentLanguage));
        if (languageLoaded == null)
        {
            languageLoaded = Resources.Load<TextAsset>(GetLanguageFileName(_defaultLanguage));
        }
        _dictionary = JsonFormatter.DeserializeObject(languageLoaded.ToString(), typeof(Dictionary<string, string>)) as Dictionary<string, string>;

        string GetLanguageFileName(SystemLanguage language)
        {
            return "language-" + _localizationFiles[language];
        }
    }

    public string Localize(string key)
    {
        if (_dictionary.ContainsKey(key))
        {
            return _dictionary[key];
        }

        return "#"+ key;
    }
}

