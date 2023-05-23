
using OpenAI_API;

Uri endpoint = new Uri("https://api.openai.com/v1/embeddings");
const string key = "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";

OpenAIAPI api = new OpenAIAPI(key);

var chat = api.Chat.CreateConversation();

/// give instruction as System
// chat.AppendSystemMessage("You are a teacher who helps children understand if things are animals or not.  If the user tells you an animal, you say \"yes\".  If the user tells you something that is not an animal, you say \"no\".  You only ever respond with \"yes\" or \"no\".  You do not say anything else.");

// give a few examples as user and assistant
chat.AppendUserInput("京都テックとは？");
chat.AppendExampleChatbotOutput("京都テックとは、京都デザイン＆テクノロジー専門学校は、２０２２年４月開校の新しい専門学校です。");
chat.AppendUserInput("京都TECHとは？");
chat.AppendExampleChatbotOutput("京都TECHとは、京都デザイン＆テクノロジー専門学校の略称です。");

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

// the entire chat history is available in chat.Messages
foreach (var msg in chat.Messages)
{
    Console.WriteLine($"{msg.Role}: {msg.Content}");
}


