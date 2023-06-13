using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Memory;


namespace PoC.OpenAIEmbeddedSK
{
    /// <summary>
    /// https://zenn.dev/microsoft/articles/semantic-kernel-8
    /// </summary>
    public partial class Form1 : Form
    {
        const string apiKey = "**********************";
        const string endPoint = "https://cs-openai-simplise.openai.azure.com/";
        const string deployName = "deploy-text-embedding-ada-002";
        const string memoryCollectionName = "SKGitHub";

        IKernel kernel = Kernel.Builder
            .WithAzureTextEmbeddingGenerationService(deployName, endPoint, apiKey)
            // ÉÅÉÇÉäè„Ç…ï€ë∂
            .WithMemoryStorage(new VolatileMemoryStore())
            .Build();


        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            //
            var githubFiles = new Dictionary<string, string>()
            {
                ["https://github.com/microsoft/semantic-kernel/blob/main/README.md"]
                    = "README: Installation, getting started, and how to contribute",
                ["https://github.com/microsoft/semantic-kernel/blob/main/samples/notebooks/dotnet/02-running-prompts-from-file.ipynb"]
                    = "Jupyter notebook describing how to pass prompts from a file to a semantic skill or function",
                ["https://github.com/microsoft/semantic-kernel/blob/main/samples/notebooks/dotnet/00-getting-started.ipynb"]
                    = "Jupyter notebook describing how to get started with the Semantic Kernel",
                ["https://github.com/microsoft/semantic-kernel/tree/main/samples/skills/ChatSkill/ChatGPT"]
                    = "Sample demonstrating how to create a chat skill interfacing with ChatGPT",
                ["https://github.com/microsoft/semantic-kernel/blob/main/dotnet/src/SemanticKernel/Memory/VolatileMemoryStore.cs"]
                    = "C# class that defines a volatile embedding store",
                ["https://github.com/microsoft/semantic-kernel/blob/main/samples/dotnet/KernelHttpServer/README.md"]
                    = "README: How to set up a Semantic Kernel Service API using Azure Function Runtime v4",
                ["https://github.com/microsoft/semantic-kernel/blob/main/samples/apps/chat-summary-webapp-react/README.md"]
                    = "README: README associated with a sample chat summary react-based webapp",
            };

            // ========= Store memories =========

            Console.WriteLine("Adding some GitHub file URLs and their descriptions to a volatile Semantic Memory.");
            var i = 0;
            foreach (var entry in githubFiles)
            {
                await kernel.Memory.SaveReferenceAsync(
                    collection: memoryCollectionName,
                    description: entry.Value,
                    text: entry.Value,
                    externalId: entry.Key,
                    externalSourceName: "GitHub"
                );
                listBox1.Items.Add($"  URL {++i} saved");
            }

            listBox1.Items.Add("Files added.");

            /*
            Output:

            Adding some GitHub file URLs and their descriptions to a volatile Semantic Memory.
                URL 1 saved
                URL 2 saved
                URL 3 saved
                URL 4 saved
                URL 5 saved
                URL 6 saved
                URL 7 saved
            Files added.
            */



            //ILogger CreateConsoleLogger()
            //{
            //    return LoggerFactory.Create(builder =>
            //    {
            //        builder.AddFilter(_ => true).AddConsole();
            //    }).CreateLogger<Program>();
            //}
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // ========= Search semantic memory #1 =========

            string ask = "How do I get started?";
            listBox1.Items.Add("===========================\n" +
                                "Query: " + ask + "\n");

            var memories = kernel.Memory.SearchAsync(memoryCollectionName, ask, limit: 5, minRelevanceScore: 0.77);

            var i = 0;
            await foreach (MemoryQueryResult memory in memories)
            {
                listBox1.Items.Add($"Result {++i}:");
                listBox1.Items.Add("  URL:     : " + memory.Metadata.Id);
                listBox1.Items.Add("  Title    : " + memory.Metadata.Description);
                listBox1.Items.Add("  Relevance: " + memory.Relevance);
                listBox1.Items.Add("");
            }

        }
    }
}