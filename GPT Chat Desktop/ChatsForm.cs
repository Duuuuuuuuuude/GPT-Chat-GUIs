﻿using FastColoredTextBoxNS;
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

    private IGPTChat GetNewChatInstance()
    {
        return (IGPTChat)Activator.CreateInstance(_chatType);
    }

    #region Syntax highlighting
    private void AppendTextToFastColoredTextBox(FastColoredTextBox fctb, string newText)
    {
        fctb.BeginUpdate();
        int prevTextLength = fctb.TextLength;
        fctb.AppendText(newText);
        fctb.EndUpdate();

        // Update syntax highlighting for the appended text
        Place startPlace = fctb.PositionToPlace(prevTextLength);
        Place endPlace = fctb.PositionToPlace(fctb.TextLength);
        var appendedRange = new FastColoredTextBoxNS.Range(fctb, startPlace, endPlace);
        fctb.OnTextChanged(appendedRange);
    }

    private void ConfigureSyntaxHighlighting(FastColoredTextBox fctb, string language)
    {
        // Clear existing styles
        fctb.Range.ClearStyle(StyleIndex.All);

        // Set up syntax highlighting for the detected language
        switch (language)
        {
            case "csharp":
                fctb.Language = Language.CSharp;
                break;
            case "HTML":
                fctb.Language = Language.HTML;
                break;
            // Add more cases for other languages
            default:
                fctb.Language = Language.Custom;
                break;
        }
    }

    private string DetectLanguage(string inputText)
    { // TODO: Skal implementeres rigtigt.
        return "custom";
    }
    #endregion

    #region Button event handlers
    private void BtnNewTab_Click(object sender, EventArgs e)
    {
        NewTabClicked();
    }

    private void BtnCloseTab_Click(object sender, EventArgs e)
    {
        CloseSelectedTabClicked();
    }
    #endregion

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

    private void InitializeTabPage(TabPage tabPage)
    {
        FastColoredTextBox fastColoredTextBox = new FastColoredTextBox();
        fastColoredTextBox.Language = Language.Custom;
        fastColoredTextBox.Dock = DockStyle.Fill;
        tabPage.Controls.Add(fastColoredTextBox);

        var inputBox = new TextBox();
        inputBox.Dock = DockStyle.Bottom;
        inputBox.KeyDown += (sender, args) =>
        {
            if (args.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(inputBox.Text))
            {
                AppendTextToFastColoredTextBox(fastColoredTextBox, "Human:\n" + inputBox.Text + "\n\n" + "Ai: ");

                SendPromptClicked(inputBox.Text);
                args.Handled = true;
                args.SuppressKeyPress = true;

                inputBox.Text = string.Empty;
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

    public void SendPromptClicked(string prompt)
        // TODO: highlight.js og alt styling i chat.html virker ikke.
    {
        string message = txtBoxInput.Text;
        txtBoxInput.Clear();
        //string escapedMessage = System.Security.SecurityElement.Escape(message);
        //string script = $"appendMessage('{escapedMessage}', {true.ToString().ToLower()}, {true.ToString().ToLower()});";
        string script = $"appendMessage('{message}', {true.ToString().ToLower()}, {true.ToString().ToLower()});";
        webView2Chat1.CoreWebView2.ExecuteScriptAsync(script);

        // Find the FastColoredTextBox within the selected TabPage
        FastColoredTextBox fastColoredTextBox = null;
        foreach (Control control in selectedTab.Controls)
        {
            if (control is FastColoredTextBox)
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