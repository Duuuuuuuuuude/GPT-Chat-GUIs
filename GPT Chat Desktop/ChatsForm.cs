using Microsoft.Web.WebView2.WinForms;
using Middleware;
using Middleware.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Diagnostics;

namespace GPT_Chat_Desktop;

public partial class ChatsForm : Form
{
    private readonly IUserSettingsGlobal _userSettingsGlobalInstance;

    /// <summary>
    /// Used to prevent a new message being send if another one is already being send at the moment.
    /// </summary>
    private readonly Dictionary<TabPage, IGptChat> _chatInstancesAndTabs = new();
    private const string HighlightjsLanguagesSelectedJson = @"Assets\highlights-languages-settings\highlightjs-languages-selected.json";

    /// <summary>
    /// type parameters in the class would have caused problems with the WinForm designer.
    /// </summary>
    private readonly Type _chatType;

    private ChatsForm(IUserSettingsGlobal userSettingsGlobal, Type tChat)
    {
        if (typeof(IGptChat).IsAssignableFrom(tChat))
        {
            _chatType = tChat;
        }
        else
        {
            throw new ArgumentException($"Invalid type. Chat type must be a subclass of IChatGPT, but was of type: {tChat}.", nameof(tChat));
        }

        _userSettingsGlobalInstance = userSettingsGlobal;

        // TODO: Load chat history.
        InitializeComponent();

        //NewTabClicked();

        //InitializeTextBoxes();

        AddLanguageCheckboxesToMenuStrip();
    }

    /// <summary> // TODO: Fjern metode.
    /// There is no public constructor, use this instead.
    /// This method implements the 'asynchronous factory method' pattern.
    /// </summary>
    /// <param name="userSettingsGlobal"></param>
    /// <param name="tChat">Type of the chat class. Must be a subclass of IGptChat.</param>
    /// <returns></returns>
    public static async Task<ChatsForm> ChatsFormAsync(IUserSettingsGlobal userSettingsGlobal, Type tChat) // TODO: Flyt til constructor.
    {
        var form = new ChatsForm(userSettingsGlobal, tChat);
        await form.NewTabClickedAsync();
        form.InitializeTextBoxes();
        return form;
    }


    private async Task NewTabClickedAsync()
    {
        IGptChat newChat = GetNewChatInstance();  // Has to be outside the this.shown += block. TODO: Async problems.

        TabPage newTabPage = InitializeNewTab();
        _chatInstancesAndTabs.Add(newTabPage, newChat);

        WebView2 currentWebView2 = GetWebView2FromCurrentTabPage(newTabPage);


        var tcs = new TaskCompletionSource<bool>();

        currentWebView2.HandleCreated += async (sender, args) =>
        {
            currentWebView2.Invoke((MethodInvoker)(async () =>
            {
                await currentWebView2.EnsureCoreWebView2Async(tcs.SetResult);
            }));
        };

        await tcs.Task;

        // Load the chat.html file into the WebView2 control

        currentWebView2.CoreWebView2.Navigate(new Uri(Path.GetFullPath("chat.html")).ToString());

#if DEBUG
        // Open the WebView2 Developer Tools window.
        GetWebView2FromCurrentTabPage(newTabPage).CoreWebView2.OpenDevToolsWindow();
#endif





        //        //// To avoid this problem: "System.InvalidOperationException
        //        ////                         HResult = 0x80131509
        //        ////                         Message = Invoke or BeginInvoke cannot be called on a control until the window handle has been created.
        //        ////                         Source = System.Windows.Forms"
        //        //this.Shown += ((sender, args) =>
        //        //{
        //        WebView2 currentWebView2 = GetWebView2FromCurrentTabPage(newTabPage);
        //        //Invoke(new Action(async () => { 
        //        await currentWebView2.EnsureCoreWebView2Async(null); // })); // TODO: Skulle den ikke være async.
        //                                                             //await currentWebView2.EnsureCoreWebView2Async(null);
        //                                                             // Load the chat.html file into the WebView2 control

        //        currentWebView2.CoreWebView2.Navigate(new Uri(Path.GetFullPath("chat.html")).ToString());

        //#if DEBUG
        //        // Open the WebView2 Developer Tools window.
        //        GetWebView2FromCurrentTabPage(newTabPage).CoreWebView2.OpenDevToolsWindow();
        //#endif
        //        //});
    }






    #region Event handlers

    #region Button event handlers

    private void BtnNewTab_Click(object sender, EventArgs e)  // TODO: async Task kan ikke bruges til event handlers, men async void kan give problemer. Find løsning.
    {
        NewTabClickedAsync();
    }

    private void BtnCloseTab_Click(object sender, EventArgs e)
    {
        CloseSelectedTabClicked();
    }

    private async void NewBtnSendMessage_ClickAsync(object sender, EventArgs e) // TODO: async Task kan ikke bruges til event handlers, men async void kan give problemer. Find løsning.
    {
        tabCtrlChats.Enabled = false; // Prevents someone from changing the tab while getting the current tab controls, which caused the wrong controls to be used. Should only happen for a few milliseconds and therefore not be noticable by the user.
        TabPage selectedTabPage = GetCurrentTabPage();
        SetReplyingStatus(true, selectedTabPage);

        WebView2 currentWebView2 = GetWebView2FromCurrentTabPage(selectedTabPage);
        //await currentWebView2.EnsureCoreWebView2Async(null);

        if (!_chatInstancesAndTabs[selectedTabPage].IsReplying)
        {
            await SendAndReceiveMessageAsync(GetTxtBoxInputFromCurrentTabPage(selectedTabPage), currentWebView2,
                selectedTabPage);
        }

        tabCtrlChats.Enabled = true;
        SetReplyingStatus(false, selectedTabPage);
    }

    private async void NewTxtBoxInputKey_DownAsync(object sender, KeyEventArgs e) // TODO: async Task kan ikke bruges til event handlers, men async void kan give problemer. Find løsning.
    {
        tabCtrlChats.Enabled = false; // Prevents someone from changing the tab while getting the current tab controls, which caused the wrong controls to be used. Should only happen for a few milliseconds and therefore not be noticable by the user.
        TabPage selectedTabPage = GetCurrentTabPage();
        SetReplyingStatus(true, selectedTabPage);

        WebView2 currentWebView2 = GetWebView2FromCurrentTabPage(selectedTabPage);
        TextBox currentTxtBox = GetTxtBoxInputFromCurrentTabPage(selectedTabPage);
        //await currentWebView2.EnsureCoreWebView2Async(null);

        tabCtrlChats.Enabled = true;
        SetReplyingStatus(false, selectedTabPage);

        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true; // Prevents the Enter key from creating a new line in the TextBox

            if (!_chatInstancesAndTabs[selectedTabPage].IsReplying)
            {
                await SendAndReceiveMessageAsync(currentTxtBox, currentWebView2, selectedTabPage);
            }
        }
    }

    #endregion

    #region TextBox event handlers

    private void TxtBoxOpenAiOrganizationTextChanged(object sender, EventArgs e)
    {
        TxtOpenAiOrganizationChanged();
    }

    private void TxtBoxOpenAiKeyTextChanged(object sender, EventArgs e)
    {
        TxtOpenAiKeyChanged();
    }

    private void TxtBoxUsernameTextChanged(object sender, EventArgs e)
    {
        TxtUsernameChanged();
    }

    private void TxtBoxModelIdTextChanged(object sender, EventArgs e)
    {
        TxtModelIdChanged();
    }

    private void TxtBoxMaxResponseTokensTextChanged(object sender, EventArgs e)
    {
        TxtMaxResponseTokensChanged();
    }

    private void TxtBoxTokenLimitTextChanged(object sender, EventArgs e)
    {
        TxtTokenLimitChanged();
    }

    private void TxtBoxTemperatureTextChanged(object sender, EventArgs e)
    {
        TxtTemperatureChanged();
    }

    private void TxtBoxTopPTextChanged(object sender, EventArgs e)
    {
        TxtTopPChanged();
    }

    private void TxtBoxNTextChanged(object sender, EventArgs e)
    {
        TxtNChanged();
    }

    private void TxtBoxFrequencyPenaltyTextChanged(object sender, EventArgs e)
    {
        TxtFrequencyPenaltyChanged();
    }

    private void TxtBoxPresencePenaltyTextChanged(object sender, EventArgs e)
    {
        TxtPresencePenaltyChanged();
    }

    private void TxtBoxStopTextChanged(object sender, EventArgs e)
    {
        TxtStopChanged();
    }

    private void TxtBoxLogitBiasTextChanged(object sender, EventArgs e)
    {
        TxtLogitBiasChanged();
    }

    private void txtBoxFirstSystemMessage_TextChanged(object sender, EventArgs e)
    {
        TxtFirstSystemMessageChanged();
    }

    #endregion

    #region Other event handlers

    #endregion

    #region Event handlers for checkboxes

    private void chkShowOrganization_CheckedChanged(object sender, EventArgs e)
    {
        txtBoxOpenAiOrganization.UseSystemPasswordChar = !chkShowOrganization.Checked;
    }

    private void chkShowKey_CheckedChanged(object sender, EventArgs e)
    {
        txtBoxOpenAiKey.UseSystemPasswordChar = !chkShowKey.Checked;
    }

    private void menuItem_CheckedChanged(object sender, EventArgs e)
    {
        CheckedChanged(sender);
    }
    #endregion

    #endregion

    #region Button functionality
    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentInputTxtBox"></param>
    /// <param name="currentWebView2"></param>
    /// <param name="selectedTabPage"></param>
    /// <param name="latestInputMessage">Only used in the catch block in case of retries, since inputTxtBox is cleared everytime this method is called.</param>
    /// <returns></returns>
    private async Task SendAndReceiveMessageAsync(TextBox currentInputTxtBox, WebView2 currentWebView2, TabPage selectedTabPage, string? latestInputMessage = null)
    {
        latestInputMessage = latestInputMessage ?? currentInputTxtBox.Text; // Saved outside of the try scope in case of recursive retries in the catch block.

        try
        {
            SetReplyingStatus(true, selectedTabPage);

            currentInputTxtBox.Clear();

            await SetProgrammingLanguagesToHighlightAsync(currentWebView2);

            await ExecuteJavascriptAppendMessageChunkToChatAsync(latestInputMessage, true, true, currentWebView2);

            await ExecuteJavascriptAddTimestampToLatestMessageAsync(currentWebView2);

            ChatResult? lastChatResult = null;
            bool isFirstChunk = true;

            IGptChat currentChatInstance;
            if (!_chatInstancesAndTabs.TryGetValue(selectedTabPage, out currentChatInstance)) // In case someone deleted the entry in another thread.
            {
                return;
            }

            await foreach (var chatResult in currentChatInstance.SendMessageAsync(latestInputMessage))
            {
                await ExecuteJavascriptAppendMessageChunkToChatAsync(chatResult.ContentChunk, false, isFirstChunk,
                    currentWebView2);

                isFirstChunk = false;
                lastChatResult = chatResult;

                SetFinishReason(chatResult.FinishReason);
            }

            await ExecuteJavascriptAddTimeAndTokenCostToLatestMessageAsync(lastChatResult.TokenCostLatestMessage,
                lastChatResult.CreatedLocalDateTime, currentWebView2);

            SetReplyingStatus(false, selectedTabPage);
            SetTokenCostFullConversation(lastChatResult.TokenCostFullConversation.ToString());
        }
        catch (InvalidOperationException)
        {
            // TODO: Informere brugeren.
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception in SendMessageAsync:\nMessage: {ex.Message}\nStackTrace: {ex.StackTrace}\nInnerException: {ex.InnerException}");

            DialogResult dialogResult = MessageBox.Show($"There was a problem sending latest input message or recieving reply.\n\n{ex.Message}", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

            if (dialogResult == DialogResult.Retry)
            {
                await SendAndReceiveMessageAsync(currentInputTxtBox, currentWebView2, selectedTabPage, latestInputMessage);
            }
        }
    }

















    private void CloseSelectedTabClicked()
    {
        tabCtrlChats.Enabled = false; // Prevents someone from changing the tab while getting the current tab controls, which caused the wrong controls to be used. Should only happen for a few milliseconds and therefore not be noticable by the user.

        TabPage selectedTabPage = GetCurrentTabPage();
        // TODO: Save chat history.
        TabPage selectedTab = GetCurrentTabPage();
        tabCtrlChats.Controls.Remove(selectedTab);
        _chatInstancesAndTabs.Remove(selectedTab);

        tabCtrlChats.Enabled = true;
    }
    #endregion

    #region Checkbox functionality
    private static void CheckedChanged(object sender)
    {
        bool isChecked = ((ToolStripMenuItem)sender).Checked;

        // Read the JSON file
        string jsonString = File.ReadAllText(HighlightjsLanguagesSelectedJson);

        // Parse the JSON string into a JObject
        JObject jsonObject = JObject.Parse(jsonString);

        // Set the boolean value
        string language = ((ToolStripMenuItem)sender).Text;

        jsonObject[language] = isChecked;

        // Save the updated JSON to the file
        File.WriteAllText(HighlightjsLanguagesSelectedJson, jsonObject.ToString());
    }
    #endregion

    #region Textbox changed

    private void TxtOpenAiOrganizationChanged()
    {
        string setOrganization = txtBoxOpenAiKey.Text;
        List<string> nullStrings = new() { "none", "null", "" };
        bool isNull = nullStrings.Contains(setOrganization.ToLower());
        string organizationText = isNull ? "Not set" : setOrganization; // If the organization is not set, then set it to a default string instead of "None" or whatever it is set to in environment variables.
        chkShowOrganization.Checked = isNull && !chkShowOrganization.Checked; // No reason to hide the organization if it is not set.

        _userSettingsGlobalInstance.SetOpenAiOrganizationGlobal(organizationText);
    }

    private void TxtOpenAiKeyChanged()
    {
        string setApiKey = txtBoxOpenAiKey.Text;
        List<string> nullStrings = new() { "none", "null", "" };
        bool isNull = nullStrings.Contains(setApiKey.ToLower());
        string apiKeyText = isNull ? "Not set" : setApiKey; // If the api key is not set, then set it to a default string instead of "None" or whatever it is set to in environment variables.
        chkShowOrganization.Checked = !isNull && chkShowOrganization.Checked; // No reason to hide the appi keu if it is not set.

        _userSettingsGlobalInstance.SetOpenAiApiKeyGlobal(apiKeyText);
    }

    private void TxtUsernameChanged()
    {
        _userSettingsGlobalInstance.SetOpenAiApiUserNameGlobal(txtBoxUsername.Text);
    }

    private void TxtModelIdChanged()
    {
        _userSettingsGlobalInstance.SetModelIdGlobal(txtBoxModelId.Text);
    }

    private void TxtMaxResponseTokensChanged()
    {
        _userSettingsGlobalInstance.SetMaxResponseTokensGlobal(
            int.Parse(txtBoxMaxResponseTokens.Text)); // TODO: Brug regex til at slette forkerte inputs.
    }

    private void TxtTokenLimitChanged()
    {
        _userSettingsGlobalInstance.SetTokenLimitGlobal(
            int.Parse(txtBoxTokenLimit.Text)); // TODO: Brug regex til at slette forkerte inputs.
    }

    private void TxtTemperatureChanged()
    {
        _userSettingsGlobalInstance.SetTemperatureGlobal(
            int.Parse(txtBoxTemperature.Text)); // TODO: Brug regex til at slette forkerte inputs.
    }

    private void TxtTopPChanged()
    {
        _userSettingsGlobalInstance.SetTopP(int.Parse(txtBoxTopP.Text)); // TODO: Brug regex til at slette forkerte inputs.
    }

    private void TxtNChanged()
    {
        _userSettingsGlobalInstance.SetNGlobal(int.Parse(txtBoxN.Text)); // TODO: Brug regex til at slette forkerte inputs.
    }

    private void TxtFrequencyPenaltyChanged()
    {
        _userSettingsGlobalInstance.SetFrequencyPenalty(int.Parse(txtBoxFrequencyPenalty
            .Text)); // TODO: Brug regex til at slette forkerte inputs.
    }

    private void TxtPresencePenaltyChanged()
    {
        _userSettingsGlobalInstance.SetPresencePenaltyGlobal(
            int.Parse(txtBoxPresencePenalty.Text)); // TODO: Brug regex til at slette forkerte inputs.
    }

    private void TxtStopChanged()
    {
        _userSettingsGlobalInstance.SetStopGlobal(txtBoxStop.Text);
    }

    private void TxtLogitBiasChanged()
    {
        _userSettingsGlobalInstance.SetLogitBias(txtBoxLogitBias.Text);
    }

    private void TxtFirstSystemMessageChanged()
    {
        _userSettingsGlobalInstance.SetFirstSystemMessage(txtBoxFirstSystemMessage.Text);
    }

    #endregion

    #region MenuStrip
    private void AddLanguageCheckboxesToMenuStrip()
    {
        Dictionary<string, bool>? allLanguagesDict = GetAllLanguagesDict();

        ToolStripMenuItem languagesMenuItem = new ToolStripMenuItem("Languages");

        menuStrip1.Items.Add(languagesMenuItem);

        foreach (var language in allLanguagesDict)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem
            {
                Text = language.Key,
                CheckOnClick = true,
                Checked = language.Value
            };
            menuItem.CheckedChanged += menuItem_CheckedChanged;

            //menuItems.Add(menuItem);
            languagesMenuItem.DropDownItems.Add(menuItem);
        }
    }

    private static Dictionary<string, bool>? GetAllLanguagesDict()
    {
        JsonSerializer serializer = new JsonSerializer();
        Dictionary<string, bool> allLanguagesDict = new();

        using (StreamReader file =
               File.OpenText(HighlightjsLanguagesSelectedJson))
        {
            allLanguagesDict = (Dictionary<string, bool>)serializer.Deserialize(file, typeof(Dictionary<string, bool>));
        }

        return allLanguagesDict;
    }
    #endregion

    #region Execution of javascript code
#pragma warning disable AsyncFixer01
    private async Task ExecuteJavascriptAppendMessageChunkToChatAsync(string message, bool isSender, bool isFirstChunk, WebView2 webView2Instance)
    {
        //string messageEscaped = HttpUtility.HtmlEncode(message);

        message = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(message)); // So it doesn't remove the new line characters and cause problems.

        string script = $"appendMessageChunkToChat('{message}', {isSender.ToString().ToLower()}, {isFirstChunk.ToString().ToLower()})";
        // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
        await webView2Instance.CoreWebView2.ExecuteScriptAsync(script);
    }
#pragma warning restore AsyncFixer01

#pragma warning disable AsyncFixer01
    private async Task ExecuteJavascriptAddTimestampToLatestMessageAsync(WebView2 webView2Instance)
    {
        string timestampRequestScript = $"addTimestampToLatestMessage('{DateTimeOffset.Now.LocalDateTime}')"; // TODO: Virker pludselig ikke fordi dato formattet er forkert.
        await webView2Instance.CoreWebView2.ExecuteScriptAsync(timestampRequestScript);
    }
#pragma warning restore AsyncFixer01

#pragma warning disable AsyncFixer01
    private async Task ExecuteJavascriptAddTimeAndTokenCostToLatestMessageAsync(int? tokenCostLatestMessage, DateTimeOffset? createdLocalDateTime, WebView2 webView2Instance)
    {
        // TODO: Nogen gange kaldes addMetaDataTolatestMessage to gange pr. besked. Eller det gjorde den, måske ikke længere.
        string metadataReplyScript = $"addTimeAndTokenCostTolatestMessage('{tokenCostLatestMessage}', '{createdLocalDateTime}')"; // TODO: Virker pludselig ikke fordi dato formattet er forkert.
        // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
        await webView2Instance.CoreWebView2.ExecuteScriptAsync(metadataReplyScript);
    }
#pragma warning restore AsyncFixer01

    private Task SetProgrammingLanguagesToHighlightAsync(WebView2 webView2Instance)
    {
        JsonSerializer serializer = new JsonSerializer();
        Dictionary<string, bool> allLanguagesDict = new();

        using (StreamReader file = File.OpenText(@"Assets\highlights-languages-settings\highlightjs-languages-selected.json"))
        {
            allLanguagesDict = (Dictionary<string, bool>)serializer.Deserialize(file, typeof(Dictionary<string, bool>));
        }

        List<string> languagesToHighlight = allLanguagesDict
            .Where(kv => kv.Value)
            .Select(kv => kv.Key)
            .ToList();

        string languagesToHighlightJsonArray = JsonConvert.SerializeObject(languagesToHighlight);

        Debug.WriteLine("languagesToHighlightJsonArray: " + languagesToHighlightJsonArray);

        return webView2Instance.CoreWebView2.ExecuteScriptAsync($"setProgrammingLanguagesToHighlight('{languagesToHighlightJsonArray}')");
    }
    #endregion

    #region Other
    private void SetReplyingStatus(bool replying, TabPage selectedTabPage)
    {
        GetBtnSendMessageFromCurrentTabpage(selectedTabPage).Enabled = !replying;
    }

    private void InitializeTextBoxes()
    {
        txtBoxOpenAiOrganization.Text = _userSettingsGlobalInstance.GetOpenAiOrganizationGlobal();
        txtBoxOpenAiKey.Text = _userSettingsGlobalInstance.GetOpenAiApiKeyGlobal();
        txtBoxUsername.Text = _userSettingsGlobalInstance.GetOpenAiApiUserNameGlobal();
        txtBoxModelId.Text = _userSettingsGlobalInstance.GetModelIdGlobal();
        txtBoxMaxResponseTokens.Text = _userSettingsGlobalInstance.GetMaxResponseTokensGlobal();
        txtBoxTokenLimit.Text = _userSettingsGlobalInstance.GetTokenLimitGlobal().ToString();
        txtBoxTemperature.Text = _userSettingsGlobalInstance.GetTemperatureGlobal().ToString();
        txtBoxTopP.Text = _userSettingsGlobalInstance.GetTopPGlobal().ToString();
        txtBoxN.Text = _userSettingsGlobalInstance.GetNGlobal().ToString();
        txtBoxFrequencyPenalty.Text = _userSettingsGlobalInstance.GetFrequencyPenaltyGlobal().ToString();
        txtBoxPresencePenalty.Text = _userSettingsGlobalInstance.GetPresencePenaltyGlobal().ToString();
        txtBoxStop.Text = _userSettingsGlobalInstance.GetStopGlobal();
        txtBoxLogitBias.Text = _userSettingsGlobalInstance.GetLogitBiasGlobal();
        txtBoxFirstSystemMessage.Text = _userSettingsGlobalInstance.GetFirstSystemMessage();
    }

    private void SetFinishReason(string finishReason)
    {
        lblFinishReason.Text = "&Finish Reason: " + finishReason;
    }

    private void SetTokenCostFullConversation(string tokenCostFullConversation)
    {
        lblTokenCostFullConversation.Text = "&Token Cost Full Conversation: " + tokenCostFullConversation;
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

        return highestNumber + 1;
    }

    private async void JavaMethod_ClickAsync(object sender, EventArgs e) // TODO: Slet
    {
        tabCtrlChats.Enabled = false; // Prevents someone from changing the tab while getting the current tab controls, which caused the wrong controls to be used. Should only happen for a few milliseconds and therefore not be noticable by the user.
        TabPage selectedTabPage = GetCurrentTabPage();
        SetReplyingStatus(true, selectedTabPage);

        GetTxtBoxInputFromCurrentTabPage(selectedTabPage).Text = "Show me a Java method";
        WebView2 currentWebView2 = GetWebView2FromCurrentTabPage(selectedTabPage);
        TextBox currentTxtBoxInput = GetTxtBoxInputFromCurrentTabPage(selectedTabPage);

        tabCtrlChats.Enabled = true;
        SetReplyingStatus(false, selectedTabPage);

        await SendAndReceiveMessageAsync(currentTxtBoxInput, currentWebView2, selectedTabPage);
    }

    private async void btnTable_Click(object sender, EventArgs e) // TODO: Slet
    {
        tabCtrlChats.Enabled = false; // Prevents someone from changing the tab while getting the current tab controls, which caused the wrong controls to be used. Should only happen for a few milliseconds and therefore not be noticable by the user.
        TabPage selectedTabPage = GetCurrentTabPage();
        SetReplyingStatus(true, selectedTabPage);

        GetTxtBoxInputFromCurrentTabPage(selectedTabPage).Text = "Show me a made up data table of spam prices";
        WebView2 currentWebView2 = GetWebView2FromCurrentTabPage(selectedTabPage);
        TextBox currentTxtBoxInput = GetTxtBoxInputFromCurrentTabPage(selectedTabPage);

        tabCtrlChats.Enabled = true;
        SetReplyingStatus(false, selectedTabPage);

        await SendAndReceiveMessageAsync(currentTxtBoxInput, currentWebView2, selectedTabPage);
    }

    private TabPage InitializeNewTab()
    {
        int nextTabNumber = GetNextTabNumber();


        TabPage newTabPageChat = new TabPage();
        SplitContainer newSplitContainerChat = new SplitContainer();
        WebView2 newWebView2Chat = new WebView2();
        TextBox newTxtBoxInput = new TextBox();
        Button newBtnSendMessage = new Button();


        ((System.ComponentModel.ISupportInitialize)newSplitContainerChat).BeginInit();
        ((System.ComponentModel.ISupportInitialize)newWebView2Chat).BeginInit();

        SuspendLayout();
        newTabPageChat.SuspendLayout();
        newSplitContainerChat.SuspendLayout();
        newSplitContainerChat.Panel1.SuspendLayout();
        newSplitContainerChat.Panel2.SuspendLayout();


        tabCtrlChats.Controls.Add(newTabPageChat);

        // 
        // newTabPageChat
        // 
        newTabPageChat.Controls.Add(newSplitContainerChat);
        newTabPageChat.BackColor = Color.FromArgb(34, 34, 34);
        newTabPageChat.Controls.Add(newSplitContainerChat);
        newTabPageChat.Location = new Point(4, 29);
        newTabPageChat.Name = "tabPageChat" + nextTabNumber;
        newTabPageChat.Padding = new Padding(3);
        newTabPageChat.Size = new Size(911, 859);
        newTabPageChat.TabIndex = 0;
        newTabPageChat.Text = "Chat " + nextTabNumber;

        // 
        // newSplitContainerChat
        // 
        newSplitContainerChat.Dock = DockStyle.Fill;
        newSplitContainerChat.Location = new Point(3, 3);
        newSplitContainerChat.Name = "splitContainerChat";
        newSplitContainerChat.Orientation = Orientation.Horizontal;
        newSplitContainerChat.Size = new Size(905, 853);
        newSplitContainerChat.SplitterDistance = 796;
        newSplitContainerChat.TabIndex = 0;

        // 
        // splitContainerChat1.Panel1
        // 
        newSplitContainerChat.Panel1.Controls.Add(newWebView2Chat);

        // 
        // newWebView2Chat
        // 
        newWebView2Chat.AllowExternalDrop = true;
        newWebView2Chat.CreationProperties = null;
        newWebView2Chat.DefaultBackgroundColor = Color.White;
        newWebView2Chat.Dock = DockStyle.Fill;
        newWebView2Chat.Location = new Point(0, 0);
        newWebView2Chat.Name = "webView2Chat";
        newWebView2Chat.Size = new Size(905, 796);
        newWebView2Chat.TabIndex = 0;
        newWebView2Chat.ZoomFactor = 1D;

        // 
        // splitContainerChat1.PanelTwo
        // 
        newSplitContainerChat.Panel2.Controls.Add(newTxtBoxInput);
        newSplitContainerChat.Panel2.Controls.Add(newBtnSendMessage);

        // 
        // newTxtBoxInput
        // 
        newTxtBoxInput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        newTxtBoxInput.Location = new Point(3, 5);
        newTxtBoxInput.MaxLength = 0;
        newTxtBoxInput.Multiline = true;
        newTxtBoxInput.Name = "txtBoxInput";
        newTxtBoxInput.Size = new Size(796, 45);
        newTxtBoxInput.TabIndex = 1;
        newTxtBoxInput.KeyDown += NewTxtBoxInputKey_DownAsync;

        // 
        // newBtnSendMessage
        // 
        newBtnSendMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        newBtnSendMessage.Location = new Point(805, 4);
        newBtnSendMessage.Name = "btnSendMessage";
        newBtnSendMessage.Size = new Size(94, 45);
        newBtnSendMessage.TabIndex = 0;
        newBtnSendMessage.Text = "&Send";
        newBtnSendMessage.UseVisualStyleBackColor = true;
        newBtnSendMessage.Click += NewBtnSendMessage_ClickAsync;


        newTabPageChat.ResumeLayout(false);

        ((System.ComponentModel.ISupportInitialize)newSplitContainerChat).EndInit();

        newSplitContainerChat.ResumeLayout(false);
        newSplitContainerChat.Panel1.ResumeLayout(false);
        newSplitContainerChat.Panel2.ResumeLayout(false);
        newSplitContainerChat.Panel2.PerformLayout();
        ResumeLayout(false);
        PerformLayout();

        ((System.ComponentModel.ISupportInitialize)newWebView2Chat).EndInit();

        tabCtrlChats.SelectedTab = newTabPageChat;
        return newTabPageChat;
    }

    private IGptChat GetNewChatInstance()
    {
        return (IGptChat)Activator.CreateInstance(_chatType);
    }

    #region Get controls under currently selected tab.
    private WebView2 GetWebView2FromCurrentTabPage(TabPage currentTabPage)
    {
        return GetSplitContainerFromCurrentTabPage(currentTabPage).Panel1.Controls.Find("webView2Chat", false)[0] as WebView2;
    }

    private Button GetBtnSendMessageFromCurrentTabpage(TabPage currentTabPage)
    {
        return GetSplitContainerFromCurrentTabPage(currentTabPage).Panel2.Controls.Find("btnSendMessage", false)[0] as Button;
    }

    private TextBox GetTxtBoxInputFromCurrentTabPage(TabPage currentTabPage)
    {
        return GetSplitContainerFromCurrentTabPage(currentTabPage).Panel2.Controls.Find("txtBoxInput", false)[0] as TextBox;
    }

    private SplitContainer GetSplitContainerFromCurrentTabPage(TabPage currentTabPage)
    {
        return currentTabPage.Controls.Find("splitContainerChat", false)[0] as SplitContainer;
    }

    private TabPage GetCurrentTabPage()
    {
        return tabCtrlChats.SelectedTab;
    }
    #endregion

    #endregion
}