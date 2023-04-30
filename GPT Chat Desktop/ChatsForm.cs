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

    #region Button functionality
    private void NewTabClicked()
    {
        var newTabPage = new TabPage("Chat " + (GetNextTabNumber() + 1));

        _chatInstances.Add(newTabPage, GetNewChatInstance());

        tabCtrlChats.Controls.Add(newTabPage);

        InitializeTabPage(newTabPage);
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
                AppendTextToFastColoredTextBox(fastColoredTextBox, "Human: " + inputBox.Text + "\n\n" + "Ai: ");
                SendPromptClicked(inputBox.Text);


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
    {
        var selectedTab = tabCtrlChats.SelectedTab;
        var chatInstance = _chatInstances[selectedTab];

        // Find the FastColoredTextBox within the selected TabPage
        FastColoredTextBox fastColoredTextBox = null;
        foreach (Control control in selectedTab.Controls)
        {
            if (control is FastColoredTextBox)
            {
                fastColoredTextBox = control as FastColoredTextBox;
                break;
            }
        }


        ChatResult lastChatResult = null;

        AppendTextToFastColoredTextBox(fastColoredTextBox, "\n");

        foreach (var chatResult in chatInstance.AddToConversation(prompt))
        {
            string newText = chatResult.ContentChunk;

            AppendTextToFastColoredTextBox(fastColoredTextBox, newText);

            string language = DetectLanguage(fastColoredTextBox.Text);

            ConfigureSyntaxHighlighting(fastColoredTextBox, language);

            lastChatResult = chatResult;
        }

        var createdLocalDateTime = lastChatResult.CreatedLocalDateTime.ToString("F");
        var tokenCostLatestMessage = lastChatResult.TokenCostLatestMessage;
        var tokenCostFullConversation = lastChatResult.TokenCostFullConversation;

        AppendTextToFastColoredTextBox(fastColoredTextBox, $"\n\nCreated (Local Time): {createdLocalDateTime}\n" +
                                                           $"Token Cost Latest Message: {tokenCostLatestMessage}\n" +
                                                           $"Token Cost Full Conversation: {tokenCostFullConversation}\n");


    }
    #endregion
}