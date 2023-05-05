using Python.Runtime;

namespace Middleware;

public class UserSettingsGlobal : IUserSettingsGlobal
{
    private readonly dynamic _userSettingsGlobalInstance;

    public UserSettingsGlobal()
    {
        using (Py.GIL())
        {
            dynamic userSettingsGlobalModule = Py.Import("Persistent_Data.User_Settings_Global");
            _userSettingsGlobalInstance = userSettingsGlobalModule.User_Settings_Global();
        }
    }

    public void SetUserSettingsGlobal(string section, string key, string value)
    {
        try
        {
            using (Py.GIL())
            {
                _userSettingsGlobalInstance.set_user_setting(section, key, value);
            }
        }
        catch (PythonException e)
        {
            Console.WriteLine("Python Exception:");
            Console.WriteLine($"Message: {e.Message}");
            Console.WriteLine("Python StackTrace:");

            using (Py.GIL())
            {
                dynamic traceback = Py.Import("traceback");
                Console.WriteLine(traceback.format_exc());
            }
        }
    }

    public string GetUserSettingsGlobal(string key)
    {
        var res = string.Empty;
        try
        {
            using (Py.GIL())
            {
                res = _userSettingsGlobalInstance.get_user_settings(key);
            }
        }
        catch (PythonException e)
        {
            Console.WriteLine("Python Exception:");
            Console.WriteLine($"Message: {e.Message}");
            Console.WriteLine("Python StackTrace:");

            using (Py.GIL())
            {
                dynamic traceback = Py.Import("traceback");
                Console.WriteLine(traceback.format_exc());
            }
        }

        return res;
    }

    #region Setting of the user settings
    /// <summary>
    /// Can be found here: https://platform.openai.com/account/org-settings
    /// </summary>
    /// <param name="organization"></param>
    public void SetOpenAiOrganizationGlobal(string organization = "None")
    {
        SetUserSettingsGlobal("user settings", "OPENAI_ORGANIZATION", organization);
    }

    /// <summary>
    /// Can be found here: https://platform.openai.com/account/api-keys
    /// </summary>
    /// <param name="apiKey"></param>
    public void SetOpenAiApiKeyGlobal(string apiKey = "None")
    {
        SetUserSettingsGlobal("user settings", "OPENAI_API_KEY", apiKey);
    }

    /// <summary>
    /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
    /// Learn more(https://platform.openai.com/docs/guides/safety-best-practices/end-user-ids). 
    /// </summary>
    /// <param name="userName"></param>
    public void SetOpenAiApiUserNameGlobal(string userName = "None")
    {
        SetUserSettingsGlobal("user settings", "USER_NAME", userName);
    }
    #endregion

    #region Setting of the default settings
    /// <summary>
    /// Can be found here: https://platform.openai.com/docs/models/overview
    /// </summary>
    /// <param name="modelId"></param>
    public void SetModelIdGlobal(string modelId = "gpt-3.5-turbo")
    {
        SetUserSettingsGlobal("default settings", "MODEL_ID", modelId);
    }

    /// <summary>
    /// The maximum number of tokens to generate in the chat completion.
    /// The total length of input tokens and generated tokens is limited by the model's context length.
    /// </summary>
    /// <param name="maxResponseTokens"></param>
    public void SetMaxResponseTokensGlobal(int maxResponseTokens = 1000)
    {
        SetUserSettingsGlobal("default settings", "max_response_tokens", maxResponseTokens.ToString());
    }

    /// <summary>
    /// The maximum number of tokens in a conversation.
    /// </summary>
    /// <param name="tokenLimit"></param>
    public void SetTokenLimitGlobal(int tokenLimit = 4096)
    {
        SetUserSettingsGlobal("default settings", "token_limit", tokenLimit.ToString());
    }

    /// <summary>
    /// What sampling temperature to use, between 0 and 2.
    /// Higher values like 0.8 will make the output more random,
    /// while lower values like 0.2 will make it more focused and deterministic.
    /// We generally recommend altering this or top_p but not both.
    /// </summary>
    /// <param name="temperature"></param>
    public void SetTemperatureGlobal(int temperature = 1)
    {
        SetUserSettingsGlobal("default settings", "temperature", temperature.ToString());
    }

    /// <summary>
    /// An alternative to sampling with temperature,
    /// called nucleus sampling,
    /// where the model considers the results of the tokens with top_p probability mass.
    /// So 0.1 means only the tokens comprising the top 10% probability mass are considered.
    /// We generally recommend altering this or temperature but not both.
    /// </summary>
    /// <param name="topP"></param>
    public void SetTopP(int topP = 1)
    {
        SetUserSettingsGlobal("default settings", "top_p", topP.ToString());
    }

    /// <summary>
    /// How many chat completion choices to generate for each input message.
    /// </summary>
    /// <param name="n"></param>
    public void SetNGlobal(int n = 1)
    {
        SetUserSettingsGlobal("default settings", "n", n.ToString());
    }

    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in
    /// the text so far, decreasing the model's likelihood to repeat the same line verbatim.
    /// See more information about frequency and presence penalties(https://platform.openai.com/docs/api-reference/parameter-details)
    /// </summary>
    /// <param name="frequencyPenalty"></param>
    public void SetFrequencyPenalty(int frequencyPenalty = 0)
    {
        SetUserSettingsGlobal("default settings", "frequency_penalty", frequencyPenalty.ToString());
    }

    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the
    /// text so far, increasing the model's likelihood to talk about new topics.
    /// See more information about frequency and presence penalties(https://platform.openai.com/docs/api-reference/parameter-details)
    /// </summary>
    /// <param name="presencePenalty"></param>
    public void SetPresencePenaltyGlobal(int presencePenalty = 0)
    {
        SetUserSettingsGlobal("default settings", "presence_penalty", presencePenalty.ToString());
    }

    /// <summary>
    /// Up to 4 sequences where the API will stop generating further tokens.
    /// </summary>
    /// <param name="stop"></param>
    public void SetStopGlobal(string stop = "None")
    {
        SetUserSettingsGlobal("default settings", "stop", stop);
    }

    /// <summary>
    /// Defaults to None.
    /// Modify the likelihood of specified tokens appearing in the completion.
    /// Accepts a json object that maps tokens(specified by their token ID in the tokenizer) to an associated
    /// bias value from -100 to 100. Mathematically, the bias is added to the logits generated by the model prior
    /// to sampling.The exact effect will vary per model, but values between -1 and 1 should decrease or
    /// increase likelihood of selection: values like -100 or 100 should result in a ban or exclusive selection of the
    /// relevant token.
    /// </summary>
    /// <param name="logitBias"></param>
    public void SetLogitBias(string logitBias = "None")
    {
        SetUserSettingsGlobal("default settings", "logit_bias", logitBias);
    }
    #endregion

    #region Getting user settings
    /// <summary>
    /// Can be found here: https://platform.openai.com/account/org-settings
    /// </summary>
    public string GetOpenAiOrganizationGlobal()
    {
        return GetUserSettingsGlobal("OPENAI_ORGANIZATION");
    }

    /// <summary>
    /// Can be found here: https://platform.openai.com/account/api-keys
    /// </summary>
    public string GetOpenAiApiKeyGlobal()
    {
        return GetUserSettingsGlobal("OPENAI_API_KEY");
    }

    /// <summary>
    /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
    /// Learn more(https://platform.openai.com/docs/guides/safety-best-practices/end-user-ids). 
    /// </summary>
    public string GetOpenAiApiUserNameGlobal()
    {
        return GetUserSettingsGlobal("USER_NAME");
    }
    #endregion

    #region Getting default settings
    /// <summary>
    /// Can be found here: https://platform.openai.com/docs/models/overview
    /// </summary>
    public string GetModelIdGlobal()
    {
        return GetUserSettingsGlobal("MODEL_ID");
    }

    /// <summary>
    /// The maximum number of tokens to generate in the chat completion.
    /// The total length of input tokens and generated tokens is limited by the model's context length.
    /// </summary>
    public string GetMaxResponseTokensGlobal()
    {
        return GetUserSettingsGlobal("max_response_tokens");
    }

    /// <summary>
    /// The maximum number of tokens in a conversation.
    /// </summary>
    public int GetTokenLimitGlobal()
    {
        var tokenLimit = GetUserSettingsGlobal("token_limit");

        return int.Parse(tokenLimit);
    }

    /// <summary>
    /// What sampling temperature to use, between 0 and 2.
    /// Higher values like 0.8 will make the output more random,
    /// while lower values like 0.2 will make it more focused and deterministic.
    /// We generally recommend altering this or top_p but not both.
    /// </summary>
    public int GetTemperatureGlobal()
    {
        return int.Parse(GetUserSettingsGlobal("temperature"));
    }

    /// <summary>
    /// An alternative to sampling with temperature,
    /// called nucleus sampling,
    /// where the model considers the results of the tokens with top_p probability mass.
    /// So 0.1 means only the tokens comprising the top 10% probability mass are considered.
    /// We generally recommend altering this or temperature but not both.
    /// </summary>
    public int GetTopPGlobal()
    {
        return int.Parse(GetUserSettingsGlobal("top_p"));
    }

    /// <summary>
    /// How many chat completion choices to generate for each input message.
    /// </summary>
    public int GetNGlobal()
    {
        return int.Parse(GetUserSettingsGlobal("n"));
    }

    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in
    /// the text so far, decreasing the model's likelihood to repeat the same line verbatim.
    /// See more information about frequency and presence penalties(https://platform.openai.com/docs/api-reference/parameter-details)
    /// </summary>
    public int GetFrequencyPenaltyGlobal()
    {
        return int.Parse(GetUserSettingsGlobal("frequency_penalty"));
    }

    /// <summary>
    /// Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the
    /// text so far, increasing the model's likelihood to talk about new topics.
    /// See more information about frequency and presence penalties(https://platform.openai.com/docs/api-reference/parameter-details)
    /// </summary>
    public int GetPresencePenaltyGlobal()
    {
        return int.Parse(GetUserSettingsGlobal("presence_penalty"));
    }

    /// <summary>
    /// Up to 4 sequences where the API will stop generating further tokens.
    /// </summary>
    public string GetStopGlobal()
    {
        return GetUserSettingsGlobal("stop");
    }

    /// <summary>
    /// Defaults to None.
    /// Modify the likelihood of specified tokens appearing in the completion.
    /// Accepts a json object that maps tokens(specified by their token ID in the tokenizer) to an associated
    /// bias value from -100 to 100. Mathematically, the bias is added to the logits generated by the model prior
    /// to sampling.The exact effect will vary per model, but values between -1 and 1 should decrease or
    /// increase likelihood of selection: values like -100 or 100 should result in a ban or exclusive selection of the
    /// relevant token.
    /// </summary>
    public string GetLogitBiasGlobal()
    {
        return GetUserSettingsGlobal("logit_bias");
    }
    #endregion
}