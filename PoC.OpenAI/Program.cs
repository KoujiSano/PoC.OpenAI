
using OpenAI_API;

Uri endpoint = new Uri("https://api.openai.com/v1/embeddings");
const string key = "";

OpenAIAPI api = new OpenAIAPI(key);

await api.Embeddings.CreateEmbeddingAsync("京都テックとは、京都デザイン＆テクノロジー専門学校は、２０２２年４月開校の新しい専門学校です。京都TECHとは、京都デザイン＆テクノロジー専門学校の略称です。");


var chat = api.Chat.CreateConversation();

// now let's ask it a question'
chat.AppendUserInput("京都テックとは？");
// and get the response
string response = await chat.GetResponseFromChatbotAsync();
Console.WriteLine(response); // "Yes"

// and continue the conversation by asking another
chat.AppendUserInput("京都TECHとは？");
// and get another response
response = await chat.GetResponseFromChatbotAsync();
Console.WriteLine(response); // "No"


