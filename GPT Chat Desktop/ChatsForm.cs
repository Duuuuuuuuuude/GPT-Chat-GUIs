using Middleware;
using System.Collections;
using System.Diagnostics;

namespace GPT_Chat_Desktop;

public partial class
    ChatsForm : Form
{
    private readonly IGPTChat _chatInstance;
    private IUserSettingsGlobal _userSettingsGlobalInstance;
    private bool _isReplying = false;

    public ChatsForm(IGPTChat chat, IUserSettingsGlobal userSettingsGlobal)
    {
        // TODO: Load chat history.
        InitializeComponent();

        _chatInstance = chat;
        _userSettingsGlobalInstance = userSettingsGlobal;

        NewTabClicked();
    }

    #region Event handlers
    #region Button event handlers
    private void BtnNewTab_Click(object sender, EventArgs e)
    {
        NewTabClicked();
    }

    private void BtnCloseTab_Click(object sender, EventArgs e)
    {
        CloseSelectedTabClicked();
    }

    private async void ChatsForm_Load(object sender, EventArgs e)
    {
        await webView2Chat1.EnsureCoreWebView2Async(null);

        // Load the chat.html file into the WebView2 control
        webView2Chat1.CoreWebView2.Navigate(new Uri(Path.GetFullPath("chat.html")).ToString());

        // Opens the WebView2 Developer Tools window.
#if DEBUG
        webView2Chat1.CoreWebView2.OpenDevToolsWindow();
#endif
        InitializeTextBoxes();
    }

    private void WebView2Chat1_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private async void btnSendMessage_Click_Async(object sender, EventArgs e)
    {
        await SendAndReceiveMessageAsync();
    }

    private async void TxtBoxInput_KeyDown_Async(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            // Prevent the Enter key from creating a new line in the TextBox
            e.SuppressKeyPress = true;
            if (!_isReplying)
            {
                await SendAndReceiveMessageAsync();
            }
        }
    }
    #endregion

    #region TextBox event handlers
    private void txtOpenAiOrganization_TextChanged(object sender, EventArgs e)
    {
        TxtOpenAiOrganizationChanged();
    }

    private void txtOpenAiKey_TextChanged(object sender, EventArgs e)
    {
        TxtOpenAiKeyChanged();
    }

    private void txtUsername_TextChanged(object sender, EventArgs e)
    {
        TxtUsernameChanged();
    }

    private void txtModelId_TextChanged(object sender, EventArgs e)
    {
        TxtModelIdChanged();
    }

    private void txtMaxResponseTokens_TextChanged(object sender, EventArgs e)
    {
        TxtMaxResponseTokensChanged();
    }

    private void txtTokenLimit_TextChanged(object sender, EventArgs e)
    {
        TxtTokenLimitChanged();
    }

    private void txtTemperature_TextChanged(object sender, EventArgs e)
    {
        TxtTemperatureChanged();
    }

    private void txtTopP_TextChanged(object sender, EventArgs e)
    {
        TxtTopPChanged();
    }

    private void txtN_TextChanged(object sender, EventArgs e)
    {
        TxtNChanged();
    }

    private void txtFrequencyPenalty_TextChanged(object sender, EventArgs e)
    {
        TxtFrequencyPenaltyChanged();
    }

    private void txtPresencePenalty_TextChanged(object sender, EventArgs e)
    {
        TxtPresencePenaltyChanged();
    }

    private void txtStop_TextChanged(object sender, EventArgs e)
    {
        TxtStopChanged();
    }

    private void txtLogitBias_TextChanged(object sender, EventArgs e)
    {
        TxtLogitBiasChanged();
    }
    #endregion

    #region Other event handlers

    #endregion

    #region Event handlers for checkboxes
    private void chkShowOrganization_CheckedChanged(object sender, EventArgs e)
    {
        txtOpenAiOrganization.UseSystemPasswordChar = !chkShowOrganization.Checked;
    }

    private void chkShowKey_CheckedChanged(object sender, EventArgs e)
    {
        txtOpenAiKey.UseSystemPasswordChar = !chkShowKey.Checked;
    }
    #endregion
    #endregion

    #region Button functionality
    private void NewTabClicked()
    {
        var newTabPage = new TabPage("Chat " + (GetNextTabNumber() + 1));

        //_chatInstances.Add(newTabPage, GetNewChatInstance()); // TODO: Det ser ikke ud til at være muligt at oprette flere Chats på denne måde. Det giver problemer på linje 30 i 'PythonEnvironmentSetup'. Overvejer at lave om på Chat.py, og gøre det muligt at have flere conversations der.
    }

    /// <summary>
    /// Finds the name of the chat with the highest number in the name
    /// </summary>
    /// <returns></returns>
    private int GetNextTabNumber()
    {
        int highestNumber = 0;
        IEnumerator tabs = tabCtrlChats.Controls.GetEnumerator();

        while (tabs.MoveNext())
        {
            if (tabs.Current is TabPage tab)
            {
                if (tab.Text.StartsWith("Chat"))
                {
                    string[] split = tab.Text.Split(' ');

                    if (int.TryParse(split[^1], out var number))
                    {
                        if (number > highestNumber)
                        {
                            highestNumber = number;
                        }
                    }
                    else
                    {
                        highestNumber = 0;
                    }
                }
            }
        }

        if (highestNumber == 0)
        {
            highestNumber = tabCtrlChats.TabCount;
        }
        return highestNumber;
    }

    private void CloseSelectedTabClicked()
    {
        // TODO: Save chat history.
        tabCtrlChats.Controls.Remove(tabCtrlChats.SelectedTab);
    }

    private async Task SendAndReceiveMessageAsync()
    // TODO: highlight.js og alt styling i chat.html virker ikke.
    {
        SetReplyingStatus(true);

        string message = txtBoxInput.Text;
        txtBoxInput.Clear();

        string messageBase64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(message)); // So it doesn't remove the new line characters.
        string script = $"appendMessageChunkToChat('{messageBase64}', true, true);";
        await webView2Chat1.CoreWebView2.ExecuteScriptAsync(script).ConfigureAwait(true);

        string metadataRequestScript = $"addTimestampToLatestMessage('{DateTimeOffset.Now}')";
        await webView2Chat1.CoreWebView2.ExecuteScriptAsync(metadataRequestScript).ConfigureAwait(true);

        string fullConversation = string.Empty;
        string fullEscapedConversation = string.Empty;

        ChatResult? lastChatResult = null;
        bool isFirstChunk = true;
        await foreach (var chatResult in _chatInstance.AddToConversationAsync(message))
        {
            string contentChunkBase64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(chatResult.ContentChunk)); // So it doesn't remove the new line characters.
            fullConversation += chatResult.ContentChunk;
            fullEscapedConversation += contentChunkBase64;


            string replyScript = $"appendMessageChunkToChat('{contentChunkBase64}', false, {isFirstChunk.ToString().ToLower()})";
            isFirstChunk = false;
            await webView2Chat1.CoreWebView2.ExecuteScriptAsync(replyScript).ConfigureAwait(true);

            lastChatResult = chatResult;

            SetFinishReason(chatResult.FinishReason);
        }

        Debug.WriteLine(fullConversation);
        Debug.WriteLine(fullEscapedConversation);

        // TODO: Nogen gange kaldes addMetaDataTolatestMessage to gange pr. besked.
        string metadataReplyScript = $"addMetaDataTolatestMessage('{lastChatResult.TokenCostLatestMessage}', '{lastChatResult.CreatedLocalDateTime}')";
        await webView2Chat1.CoreWebView2.ExecuteScriptAsync(metadataReplyScript).ConfigureAwait(true);


        SetReplyingStatus(false);

        SetTokenCostFullConversation(lastChatResult.TokenCostFullConversation.ToString());
    }
    #endregion

    #region Textbox changed
    private void TxtOpenAiOrganizationChanged()
    {
        _userSettingsGlobalInstance.SetOpenAiOrganizationGlobal(txtOpenAiOrganization.Text);
    }

    private void TxtOpenAiKeyChanged()
    {
        _userSettingsGlobalInstance.SetOpenAiApiKeyGlobal(txtOpenAiKey.Text);
    }

    private void TxtUsernameChanged()
    {
        _userSettingsGlobalInstance.SetOpenAiApiUserNameGlobal(txtUsername.Text);
    }

    private void TxtModelIdChanged()
    {
        _userSettingsGlobalInstance.SetModelIdGlobal(txtModelId.Text);
    }

    private void TxtMaxResponseTokensChanged()
    {
        _userSettingsGlobalInstance.SetMaxResponseTokensGlobal(int.Parse(txtMaxResponseTokens.Text)); // TODO: Brug regex til at slette forkerte inputs.
    }

    private void TxtTokenLimitChanged()
    {
        _userSettingsGlobalInstance.SetTokenLimitGlobal(int.Parse(txtTokenLimit.Text)); // TODO: Brug regex til at slette forkerte inputs.
    }

    private void TxtTemperatureChanged()
    {
        _userSettingsGlobalInstance.SetTemperatureGlobal(int.Parse(txtTemperature.Text)); // TODO: Brug regex til at slette forkerte inputs.
    }

    private void TxtTopPChanged()
    {
        _userSettingsGlobalInstance.SetTopP(int.Parse(txtTopP.Text)); // TODO: Brug regex til at slette forkerte inputs.
    }
    private void TxtNChanged()
    {
        _userSettingsGlobalInstance.SetNGlobal(int.Parse(txtN.Text)); // TODO: Brug regex til at slette forkerte inputs.
    }

    private void TxtFrequencyPenaltyChanged()
    {
        _userSettingsGlobalInstance.SetFrequencyPenalty(int.Parse(txtFrequencyPenalty.Text)); // TODO: Brug regex til at slette forkerte inputs.
    }

    private void TxtPresencePenaltyChanged()
    {
        _userSettingsGlobalInstance.SetPresencePenaltyGlobal(int.Parse(txtPresencePenalty.Text)); // TODO: Brug regex til at slette forkerte inputs.
    }

    private void TxtStopChanged()
    {
        _userSettingsGlobalInstance.SetStopGlobal(txtStop.Text);
    }
    private void TxtLogitBiasChanged()
    {
        _userSettingsGlobalInstance.SetLogitBias(txtLogitBias.Text);
    }
    #endregion

    private void SetReplyingStatus(bool replying)
    {
        _isReplying = replying;
        btnSendMessage.Enabled = !replying;
    }

    private void InitializeTextBoxes()
    {
        txtOpenAiOrganization.Text = _userSettingsGlobalInstance.GetOpenAiOrganizationGlobal();
        txtOpenAiKey.Text = _userSettingsGlobalInstance.GetOpenAiApiKeyGlobal();
        txtUsername.Text = _userSettingsGlobalInstance.GetOpenAiApiUserNameGlobal();
        txtModelId.Text = _userSettingsGlobalInstance.GetModelIdGlobal();
        txtMaxResponseTokens.Text = _userSettingsGlobalInstance.GetMaxResponseTokensGlobal();
        txtTokenLimit.Text = _userSettingsGlobalInstance.GetTokenLimitGlobal().ToString();
        txtTemperature.Text = _userSettingsGlobalInstance.GetTemperatureGlobal().ToString();
        txtTopP.Text = _userSettingsGlobalInstance.GetTopPGlobal().ToString();
        txtN.Text = _userSettingsGlobalInstance.GetNGlobal().ToString();
        txtFrequencyPenalty.Text = _userSettingsGlobalInstance.GetFrequencyPenaltyGlobal().ToString();
        txtPresencePenalty.Text = _userSettingsGlobalInstance.GetPresencePenaltyGlobal().ToString();
        txtStop.Text = _userSettingsGlobalInstance.GetStopGlobal();
        txtLogitBias.Text = _userSettingsGlobalInstance.GetLogitBiasGlobal();
    }

    private void SetFinishReason(string finishReason)
    {
        lblFinishReason.Text = "&Finish Reason: " + finishReason;
    }

    private void SetTokenCostFullConversation(string tokenCostFullConversation)
    {
        lblTokenCostFullConversation.Text = "&Token Cost Full Conversation: " + tokenCostFullConversation;
    }
}