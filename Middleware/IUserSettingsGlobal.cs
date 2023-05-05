namespace Middleware

{
    public interface IUserSettingsGlobal
    {
        void SetUserSettingsGlobal(string section, string key, string value);
        string GetUserSettingsGlobal(string key);

        void SetOpenAiOrganizationGlobal(string organization = "None");

        void SetOpenAiApiKeyGlobal(string apiKey = "None");

        void SetOpenAiApiUserNameGlobal(string userName = "None");

        void SetModelIdGlobal(string modelId = "gpt-3.5-turbo");

        void SetMaxResponseTokensGlobal(int maxResponseTokens = 1000);

        void SetTokenLimitGlobal(int tokenLimit = 4096);

        void SetTemperatureGlobal(int temperature = 1);

        void SetTopP(int topP = 1);

        void SetNGlobal(int n = 1);

        void SetFrequencyPenalty(int frequencyPenalty = 0);

        void SetPresencePenaltyGlobal(int presencePenalty = 0);

        void SetStopGlobal(string stop = "None");

        void SetLogitBias(string logitBias = "None");

        string GetOpenAiOrganizationGlobal();

        string GetOpenAiApiKeyGlobal();

        string GetOpenAiApiUserNameGlobal();

        string GetModelIdGlobal();

        string GetMaxResponseTokensGlobal();

        int GetTokenLimitGlobal();

        int GetTemperatureGlobal();

        int GetTopPGlobal();

        int GetNGlobal();

        int GetFrequencyPenaltyGlobal();

        int GetPresencePenaltyGlobal();

        string GetStopGlobal();

        string GetLogitBiasGlobal();
    }
}