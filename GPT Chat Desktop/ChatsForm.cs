using FastColoredTextBoxNS;
using Middleware;
using System.Collections;


namespace GPT_Chat_Desktop;

public partial class ChatsForm : Form // TODO: GUI'en streamer ikke texten i chunks, men alt på en gang. Brug async/await for at undgå at GUI'en fryser for hver Api Response.
{
    private readonly Type _chatType;
    private readonly Dictionary<TabPage, IGPTChat> _chatInstances;

    public ChatsForm(Type chatType)
    {
        // TODO: Load chat history.
        InitializeComponent();

        _chatInstances = new Dictionary<TabPage, IGPTChat>();

        if (typeof(IGPTChat).IsAssignableFrom(chatType))
        {
            _chatType = chatType;
        }
        else
        {
            throw new ArgumentException("Invalid type. Must be a subclass of IChatGPT.", nameof(chatType));
        }

        NewTabClicked();
    }

    #region Event handlers

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
    }

    private async void btnSendMessage_Click_Async(object sender, EventArgs e)
    {
        await SendMessageAsync();
    }

    private async void TxtBoxInput_KeyDown_Async(object sender, KeyEventArgs e)
        {
            if (args.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(inputBox.Text))
            {
                AppendTextToFastColoredTextBox(fastColoredTextBox, "Human:\n" + inputBox.Text + "\n\n" + "Ai: ");

            await SendMessageAsync();
            }
        };
        tabPage.Controls.Add(inputBox);

        var sendButton = new Button();
        sendButton.Text = "Send";
        sendButton.Dock = DockStyle.Bottom;
        sendButton.Click += (sender, args) =>
        {
            if (!string.IsNullOrWhiteSpace(inputBox.Text))
            {
        var newTabPage = new TabPage("Chat " + (GetNextTabNumber() + 1));

                inputBox.Text = string.Empty;
            }
        };
        tabPage.Controls.Add(sendButton);
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

    private async Task SendMessageAsync()
        // TODO: highlight.js og alt styling i chat.html virker ikke.
    {
        string message = txtBoxInput.Text;
        txtBoxInput.Clear();
        //string escapedMessage = System.Security.SecurityElement.Escape(message);
        //string script = $"appendMessage('{escapedMessage}', {true.ToString().ToLower()}, {true.ToString().ToLower()});";
        string script = $"appendMessage('{message}', {true.ToString().ToLower()}, {true.ToString().ToLower()});";
        webView2Chat1.CoreWebView2.ExecuteScriptAsync(script);

        bool isFirstChunk = true;
        await foreach (var chatResult in _chatInstance.AddToConversationAsync(message))
            {
            //string escapedReply = System.Security.SecurityElement.Escape(chatResult.ContentChunk.TrimEnd());
            //string replyScript = $"appendMessage('{escapedReply}', {false.ToString().ToLower()}, {isFirstChunk.ToString().ToLower()});";
            string replyScript = $"appendMessage('{chatResult.ContentChunk}', {false.ToString().ToLower()}, {isFirstChunk.ToString().ToLower()});";
            webView2Chat1.CoreWebView2.ExecuteScriptAsync(replyScript);
            isFirstChunk = false;
        }
        }

    #endregion
}