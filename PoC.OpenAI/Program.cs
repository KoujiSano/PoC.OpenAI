using Azure;
using Azure.AI.OpenAI;


OpenAIClient client = new OpenAIClient(
    new Uri("https://cs-openai-simplise.openai.azure.com/"),
    new AzureKeyCredential(""));

// If streaming is not selected
//Response<Completions> completionsResponse = await client.GetCompletionsAsync(
//    deploymentOrModelName: "deploy-text-davinci-003",
//    new CompletionsOptions()
//    {
//        Prompts = { "京都テックとは、京都デザイン＆テクノロジー専門学校は、２０２２年４月開校の新しい専門学校です。" +
//        "京都TECHとは、京都デザイン＆テクノロジー専門学校の略称です。\n\n" +
//        "上の文章から、以下の問いに答えてください。\n\n" +
//        "Q: 京都TECHとは？\n" +
//        "A: " },
//        Temperature = (float)0.7,
//        MaxTokens = 256,
//        StopSequences = { "\n" },
//        NucleusSamplingFactor = (float)1,
//        FrequencyPenalty = (float)0,
//        PresencePenalty = (float)0,
//        GenerationSampleCount = 1,
//    });

//Completions completions = completionsResponse.Value;
//foreach (var choice in completions.Choices)
//{
//    Console.WriteLine(choice.Text);
//}





// ### If streaming is not selected

const string question = @"京都TECHとは？";

Response<ChatCompletions> responseWithoutStream = await client.GetChatCompletionsAsync(
    "deploy-gpt-35-turbo",
    new ChatCompletionsOptions()
    {
        Messages =
        {
            new ChatMessage(ChatRole.System, @"You are an AI assistant that helps people find information."),
            new ChatMessage(ChatRole.User, @"京都TECH"),
            new ChatMessage(ChatRole.Assistant, @"京都デザイン＆テクノロジー専門学校の略称です。
「京都テクノロジーセンター」の略称ではない。
「TECH」について説明する必要はない。"),
            new ChatMessage(ChatRole.User, @"京都テック"),
            new ChatMessage(ChatRole.Assistant, @"京都デザイン＆テクノロジー専門学校の略称です。"),
            new ChatMessage(ChatRole.User, @"京都デザイン＆テクノロジー専門学校の住所"),
            new ChatMessage(ChatRole.Assistant, @"〒600-8357 京都府京都市下京区 五条通猪熊西入柿本町 596"),
            new ChatMessage(ChatRole.User, @"京都デザイン＆テクノロジー専門学校のホームページ"),
            new ChatMessage(ChatRole.Assistant, @"https://www.kyoto-tech.ac.jp"),
            new ChatMessage(ChatRole.User, @"京都デザイン＆テクノロジー専門学校の所属"),
            new ChatMessage(ChatRole.Assistant, @"学校法人滋慶コミュニケーションアート"),
            new ChatMessage(ChatRole.User, @"京都デザイン＆テクノロジー専門学校の電話番号"),
            new ChatMessage(ChatRole.Assistant, @"0120-109-525"),
            new ChatMessage(ChatRole.User, @"京都デザイン＆テクノロジー専門学校のメールアドレス"),
            new ChatMessage(ChatRole.Assistant, @"info@kyoto-tech.ac.jp"),
            new ChatMessage(ChatRole.User, @"京都デザイン＆テクノロジー専門学校の学科"),
            new ChatMessage(ChatRole.Assistant, @"4年制のスーパーAI＆テクノロジー科　
3年制　デジタルクリエイター科"),
            new ChatMessage(ChatRole.User, @"京都デザイン＆テクノロジー専門学校の入学資格"),
            new ChatMessage(ChatRole.Assistant, @"下記のうち1項目を満たしている方
・高等学校卒業（卒業見込みを含む）以上の方
・文部科学省高等学校卒業程度認定試験（旧大学入学資格検定）合格者（合格見込みを含む）専修学校高等課程（大学入学資格付与校3年課程）卒業者（卒業見込みを含む）
・高等専門学校の3年以上の修了者（修了見込みを含む）外国において学校教育l2年以上の課程を修了した方
・文部科学大臣が高等学校の課程と同等の課程を有するものとして、認定した在外教育施設の当該課程を修了した方
※留学生用の募集要項をこ希望の方はお申し出ください。
"),
            new ChatMessage(ChatRole.User,question),
            //new ChatMessage(ChatRole.Assistant, @"「京都TECH」という用語には複数の意味がありますが、一般的には「京都デザイン＆テクノロジー専門学校」の略称として使われることが多いです。また、同専門学校のスーパーAI＆テクノロジー科を指して「京都TECH AI」という表現もされることがあります。"),
            //new ChatMessage(ChatRole.User, @"京都デザイン＆テクノロジー専門学校とは？"),
            //new ChatMessage(ChatRole.Assistant, @"京都デザイン＆テクノロジー専門学校は、滋慶コミュニケーションアートが運営する専門学校の一つで、デザインやテクノロジーに特化した教育を行っています。スーパーAI＆テクノロジー科とデジタルクリエイター科の2つの学科があり、ITやAIの最新技術を学びながら、デザインやクリエイティブに関する知識も深めることができます。また、教育現場での実践的な取り組みやインターンシップなど、就職に直結するカリキュラムも充実しています。"),
        },
        Temperature = (float)0.7,
        MaxTokens = 800,
        NucleusSamplingFactor = (float)0.95,
        FrequencyPenalty = 0,
        PresencePenalty = 0,
    });
Console.WriteLine(question);
var chatCompletions = responseWithoutStream.Value;
foreach (var choice in chatCompletions.Choices)
{
    Console.WriteLine(choice.Message.Content);
}


