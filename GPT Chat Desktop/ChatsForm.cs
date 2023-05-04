using Middleware;
using System.Collections;

namespace GPT_Chat_Desktop;

public partial class
    ChatsForm : Form
{
    private readonly IGPTChat _chatInstance;

    public ChatsForm()
    {
        // TODO: Load chat history.
        InitializeComponent();

        _chatInstance = new Chat();

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
        if (e.KeyCode == Keys.Enter)
        {
            // Prevent the Enter key from creating a new line in the TextBox
            e.SuppressKeyPress = true;

            await SendMessageAsync();
        }
    }
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