using System;
using Kameffee.Audio;
using UnityEngine;

namespace Unity1week202309.InGame.Config
{
    using ViewModel = ConfigWindowView.ViewModel;

    [Serializable]
    public class ConfigWindowSettings
    {
        [SerializeField]
        private bool _visibleAudioSettings = true;

        [SerializeField]
        private bool _visibleCursorSizeSettings;

        [SerializeField]
        private bool _visibleStartButtonSettings;

        public bool VisibleAudioSettings => _visibleAudioSettings;
        public bool VisibleCursorSizeSettings => _visibleCursorSizeSettings;
        public bool VisibleStartButtonSettings => _visibleStartButtonSettings;
    }

    public class CreateConfigWindowViewModelUseCase
    {
        private readonly CreateCursorSizeSettingsViewModelUseCase _createCursorSizeSettingsViewModelUseCase;
        private readonly CreateAudioSettingViewModelUseCase _createAudioSettingViewModelUseCase;
        private readonly ConfigWindowSettings _configWindowSettings;

        public CreateConfigWindowViewModelUseCase(
            CreateCursorSizeSettingsViewModelUseCase createCursorSizeSettingsViewModelUseCase,
            CreateAudioSettingViewModelUseCase createAudioSettingViewModelUseCase,
            ConfigWindowSettings configWindowSettings)
        {
            _createCursorSizeSettingsViewModelUseCase = createCursorSizeSettingsViewModelUseCase;
            _createAudioSettingViewModelUseCase = createAudioSettingViewModelUseCase;
            _configWindowSettings = configWindowSettings;
        }

        public ViewModel Create()
        {
            return new ViewModel(
                _configWindowSettings.VisibleAudioSettings ? _createAudioSettingViewModelUseCase.Create() : null,
                _configWindowSettings.VisibleCursorSizeSettings
                    ? _createCursorSizeSettingsViewModelUseCase.Create()
                    : null,
                _configWindowSettings.VisibleStartButtonSettings
                    ? new StartButtonSettingsView.ViewModel(false)
                    : null
            );
        }
    }
}